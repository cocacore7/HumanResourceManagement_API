using Azure;
using HRM_API.Application.Helpers;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.File;
using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Enum.Form;
using HRM_API.Core.Interfaces.File;
using HRM_API.Core.Interfaces.Form;
using HRM_API.Core.Interfaces.PreApplication;

namespace HRM_API.Application.Services
{
    public class FormService(IFormRepository repository, IPreApplicationRepository preApplicationRepository, IFileRepository fileRepository, EnumHelper enumHelper, FileHelper fileHelper)
    {
        private readonly IFormRepository _repository = repository;
        private readonly IPreApplicationRepository _preApplicationRepository = preApplicationRepository;
        private readonly IFileRepository _fileRepository = fileRepository;
        private readonly EnumHelper _enumHelper = enumHelper;
        private readonly FileHelper _fileHelper = fileHelper;

        public async Task<GetFormAnswersResponseDto?> GetFormAnswersAsync(int PreApplicationId, int FormId)
        {
            //Traer Form
            var form = await _repository.GetFormAsync(FormId);
            //Traer cabecera response
            var header = await _repository.GetFormHeaderAsync(PreApplicationId, FormId);
            //Traer Answers Response
            var answers = await _repository.GetFormAnswersAsync(header?.IdResponse);

            //Asignar resultados a respuesta

            GetFormAnswersDBResponseDto result = new()
            { 
                Form = form ?? new(),
                Header = header ?? new(),
                Answers = answers ?? []
            };
            GetFormAnswersResponseDto response = new() { Response = result };

            return (response);
        }

        public async Task<SetFormAnswersReponseDto?> SetFormAnswersAsync(GeneralFormRequestDto request, LoginDBResponseDto user)
        {
            //Validar que existe el formulario
            var form = await _repository.GetFormAsync(request.Form.FormId);
            if (form == null) { return  new() { Response = ["No existe el form solicitado"] }; }
            //Validar que existe la preapplication
            var preApplication = await _preApplicationRepository.GetPreApplicationsAsync(string.Empty, request.Origin.RegisterId.ToString());
            if (preApplication == null) { return new() { Response = ["No existe la Pre Aplicación solicitada"] }; }
            //Validar Registro PreApplicationFormResponse
            var preApplicationFormIdValid = await _repository.GetPreApplicationFormResponseAsync(form.IdForm, preApplication.FirstOrDefault()?.Id);
            if (preApplicationFormIdValid != null) { return new() { Response = ["Ya existe una respuesta asociada a la pre solicitud y formulario solicitados"] }; }

            //Crear Registro PreApplicationFormResponse
            SetPreApplicationFormResponseDBRequestDto requestDB = new()
            {
                PreApplicationId = preApplication.FirstOrDefault()?.Id,
                FormId = form.IdForm,
                CreatedBy = int.TryParse(user.IdUser, out int createdBy) ? createdBy : 0 ,
                UpdatedBy = createdBy
            };
            var responseId = await _repository.SetPreApplicationFormResponseAsync(requestDB);

            //registrar cada pregunta por su tipo
            List<string> responseList = [];
            foreach (var item in request.Answers ?? []) 
            {
                var question = await _repository.GetValidQuestionAsync(form.IdForm, item.Code);
                if (question?.Type == item.Type)
                {
                    switch (item.Type)
                    {
                        case var type when type == _enumHelper.GetEnumDescription(SetFormAnswersTypeFileEnum.Text) || type == _enumHelper.GetEnumDescription(SetFormAnswersTypeFileEnum.Textarea):
                            var textAnswer = new SetTextAnswerDBResponseDto()
                            {
                                ResponseId = responseId,
                                QuestionId = question.QuestionId,
                                AnswerType = question.Type,
                                ValueText = item.ValueText ?? string.Empty
                            };
                            var textQuestionResponse = (bool)await _repository.SetTextAnswerAsync(textAnswer);
                            if (textQuestionResponse) { responseList.Add("Respuesta registrada con exito, codigo: " + item.Code); }
                            else { responseList.Add("Error al intentar guardar respuesta con codigo: " + item.Code); }
                            break;

                        case var type when type == _enumHelper.GetEnumDescription(SetFormAnswersTypeFileEnum.Number) || type == _enumHelper.GetEnumDescription(SetFormAnswersTypeFileEnum.Currency):
                            var numberAnswer = new SetNumberAnswerDBResponseDto()
                            {
                                ResponseId = responseId,
                                QuestionId = question.QuestionId,
                                AnswerType = question.Type,
                                ValueNumber = item.ValueNumber
                            };
                            var numberQuestionResponse = (bool)await _repository.SetNumberAnswerAsync(numberAnswer);
                            if (numberQuestionResponse) { responseList.Add("Respuesta registrada con exito, codigo: " + item.Code); }
                            else { responseList.Add("Error al intentar guardar respuesta con codigo: " + item.Code); }
                            break;

                        case var type when type == _enumHelper.GetEnumDescription(SetFormAnswersTypeFileEnum.Boolean):
                            var boolAnswer = new SetBoolAnswerDBResponseDto()
                            {
                                ResponseId = responseId,
                                QuestionId = question.QuestionId,
                                AnswerType = question.Type,
                                ValueBool = item.ValueBool
                            };
                            var boolQuestionResponse = (bool)await _repository.SetBoolAnswerAsync(boolAnswer);
                            if (boolQuestionResponse) { responseList.Add("Respuesta registrada con exito, codigo: " + item.Code); }
                            else { responseList.Add("Error al intentar guardar respuesta con codigo: " + item.Code); }
                            break;

                        case var type when type == _enumHelper.GetEnumDescription(SetFormAnswersTypeFileEnum.File):
                            //Guardar imagen y traer idFile
                            var filepath = _fileHelper.SaveFile(item, request?.Origin ?? new GeneralFormRequestOriginDto());
                            SetFileDBRequestDto newfile = new()
                            {
                                FileName = item.FileName ?? string.Empty,
                                ContentType = item.ContentType ?? string.Empty,
                                FilePath = filepath ?? string.Empty,
                                SizeBytes = item.SizeBytes ?? 0,
                                UploadedBy = int.TryParse(user.IdUser, out int createdByfile) ? createdByfile : 0
                            };
                            var responseFileId = await _fileRepository.SetFileAsync(newfile);

                            var fileAnswer = new SetFileAnswerDBResponseDto()
                            {
                                ResponseId = responseId,
                                QuestionId = question.QuestionId,
                                AnswerType = question.Type,
                                FileId = responseFileId
                            };
                            var fileQuestionResponse = (bool)await _repository.SetFileAnswerAsync(fileAnswer);
                            if (fileQuestionResponse) { responseList.Add("Respuesta registrada con exito, codigo: " + item.Code); }
                            else { responseList.Add("Error al intentar guardar respuesta con codigo: " + item.Code); }
                            break;

                        case var type when type == _enumHelper.GetEnumDescription(SetFormAnswersTypeFileEnum.Date):
                            var dateAnswer = new SetDateAnswerDBResponseDto()
                            {
                                ResponseId = responseId,
                                QuestionId = question.QuestionId,
                                AnswerType = question.Type
                            };
                            var dateQuestionResponse = (bool)await _repository.SetDateAnswerAsync(dateAnswer);
                            if (dateQuestionResponse) { responseList.Add("Respuesta registrada con exito, codigo: " + item.Code); }
                            else { responseList.Add("Error al intentar guardar respuesta con codigo: " + item.Code); }
                            break;

                        case var type when type == _enumHelper.GetEnumDescription(SetFormAnswersTypeFileEnum.Enum) || type == _enumHelper.GetEnumDescription(SetFormAnswersTypeFileEnum.Multienum):
                            //Obtener questionOptionId
                            var questionOptionId = (int)await _repository.GetQuestionOptionAsync(question.QuestionId, item.OptionId);
                            if (questionOptionId <= 0) { responseList.Add("Respuesta registrada con exito, codigo: " + item.Code); break; }

                            //Guardar Answer
                            var enumAnswer = new SetEnumAnswerDBResponseDto()
                            {
                                ResponseId = responseId,
                                QuestionId = question.QuestionId,
                                AnswerType = question.Type,
                                CatalogId = item.OptionId
                            };
                            var answerId = (int)await _repository.SetEnumAnswerAsync(enumAnswer);

                            //Guardar AnswerOption
                            var answerOptionId = (bool)await _repository.SetEnumAnswerOptionAsync(answerId, questionOptionId);
                            if (answerOptionId) { responseList.Add("Respuesta registrada con exito, codigo: " + item.Code); }
                            else { responseList.Add("Error al intentar guardar respuesta con codigo: " + item.Code); }
                            break;

                        case var type when type == _enumHelper.GetEnumDescription(SetFormAnswersTypeFileEnum.Void):
                            responseList.Add("Error al intentar guardar respuesta con codigo: " + item.Code);
                            break;

                        default:
                            responseList.Add("Error al intentar guardar respuesta con codigo: " + item.Code);
                            break;
                    }
                }
            }
            responseList.Add(responseId > 0 ? "Pre Aplicacion Registrada Exitosamente" : "Error Al Registrar Pre Aplicacion");
            SetFormAnswersReponseDto response = new() { Response = responseList };
            return (response);
        }
    }
}
