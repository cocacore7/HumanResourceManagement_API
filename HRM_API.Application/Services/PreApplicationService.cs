using HRM_API.Application.Helpers;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.File;
using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Dtos.PreApplication;
using HRM_API.Core.Enum.PreApplication;
using HRM_API.Core.Interfaces.Catalog;
using HRM_API.Core.Interfaces.File;
using HRM_API.Core.Interfaces.PreApplication;

namespace HRM_API.Application.Services
{
    public class PreApplicationService(IPreApplicationRepository repository, IFileRepository fileRepository, ICatalogRepository catalogRepository, FileHelper fileHelper)
    {
        private readonly IPreApplicationRepository _repository = repository;
        private readonly IFileRepository _fileRepository = fileRepository;
        private readonly ICatalogRepository _catalogRepository = catalogRepository;
        private readonly FileHelper _fileHelper = fileHelper;

        public async Task<GetPreApplicationsResponseDto?> GetPreApplicationsAsync(string estado, string id)
        {
            var form = await _repository.GetPreApplicationsAsync(estado, id);
            GetPreApplicationsResponseDto response = new() { Response = form ?? [] };

            return (response);
        }

        public async Task<GetPreApplicationsResponseDto?> GetPreApplicationsByUserAsync(string estado, string id, LoginDBResponseDto user)
        {
            var form = await _repository.GetPreApplicationsByUserAsync(estado, id, int.TryParse(user.IdUser, out int createdBy) ? createdBy : 0);
            GetPreApplicationsResponseDto response = new() { Response = form ?? [] };

            return (response);
        }

        public async Task<GetFormAnswersResponseDto?> GetPreApplicationFilesAsync(int? id)
        {
            var forms = await _repository.GetPreApplicationFormsFilesAsync(id);
            var formpre = await _repository.GetPreApplicationPreApplicationFileAsync(id);
            bool containCV = forms?.Any(f => f.Code == "ADJUNTA_CV") == true;
            var allForms = !containCV ?  forms?.Concat(formpre ?? []).ToList() : forms;

            GetFormAnswersDBResponseDto responsedb = new()
            {
                Answers = allForms ?? []
            };

            GetFormAnswersResponseDto response = new()
            {
                Response = responsedb
            };

            return (response);
        }

        public async Task<GetCountPreApplicationByStateResponseDto?> GetCountPreApplicationByStateAsync(List<string> states)
        {
            var response = new GetCountPreApplicationByStateResponseDto();
            foreach (var state in states) 
            { 
                var responsedb = await _repository.GetCountPreApplicationByStateAsync(state);
                if (responsedb == null)
                {
                    response.Response.Add(new() { State = state, Count = 0});
                }
                else
                {
                    response.Response.Add(responsedb);
                }
            }

            return (response);
        }

        public async Task<GetCountPreApplicationByStateResponseDto?> GetCountPreApplicationByProccessAsync()
        {
            var response = new GetCountPreApplicationByStateResponseDto();

            var responsedb1 = await _repository.GetHiredCountPreApplicationAsync();
            if (responsedb1 == null) { response.Response.Add(new() { State = "contratado", Count = 0 }); } else { response.Response.Add(responsedb1); }
                var responsedb2 = await _repository.GetFailCountPreApplicationAsync();
            if (responsedb2 == null) { response.Response.Add(new() { State = "failedStatus", Count = 0 }); } else { response.Response.Add(responsedb2); }
            var responsedb3 = await _repository.GetProcessCountPreApplicationAsync();
            if (responsedb3 == null) { response.Response.Add(new() { State = "prcessStatus", Count = 0 }); } else { response.Response.Add(responsedb3); }

            return (response);
        }

        public async Task<bool?> PreAppValidatePublicAsync(long? dpi, int? id)
        {
            var responsedb = await _repository.PreAppValidatePublicAsync(dpi, id);

            return responsedb;
        }

        public async Task<SetPreApplicationsReponseDto?> SetPreApplicationsAsync(GeneralFormRequestDto request, LoginDBResponseDto user)
        {
            SetPreApplicationsDBRequestDto newApplication = new();

            foreach (var item in request.Answers ?? Enumerable.Empty<GeneralFormRequestAnswerDto>())
            {
                switch (item.Code)
                {
                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.FullName):
                        newApplication.FullName = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.DPI):
                        newApplication.DPI = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Age):
                        newApplication.Age = int.TryParse((item.ValueNumber).ToString(), out int createdByfile) ? createdByfile : 0;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Gender):
                        newApplication.Gender = item.OptionValue == "Femenino" ? "Femenino" : "Masculino";
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Phone):
                        newApplication.Phone = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Email):
                        newApplication.Email = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Town):
                        var towns = await _catalogRepository.GetTownCatalogAsync();
                        newApplication.TownId = towns.FirstOrDefault(p => p.Value.ToLower().Contains(item.OptionValue?.ToLower() ?? "", StringComparison.OrdinalIgnoreCase)) ?.optionId ?? 0;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Address):
                        newApplication.Address = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.EducationLevel):
                        newApplication.EducationLevel = item.OptionValue ?? "";
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Vacancy):
                        var jobs = await _catalogRepository.GetJobCatalogAsync();
                        newApplication.VacancyId = jobs.FirstOrDefault(p => p.Value.ToLower().Contains(item.OptionValue?.ToLower() ?? "", StringComparison.OrdinalIgnoreCase))?.optionId ?? 0;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Experience):
                        newApplication.Experience = item.OptionValue == "no";
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.HowHeard):
                        newApplication.HowHeard = item.OptionValue ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.File):
                        SetFileDBRequestDto newfile = new()
                        {
                            FileName = item.FileName ?? string.Empty,
                            ContentType = item.ContentType ?? string.Empty,
                            FilePath = "" ?? string.Empty,
                            SizeBytes = item.SizeBytes ?? 0,
                            UploadedBy = int.TryParse(user.IdUser, out int createdByfileCV) ? createdByfileCV : 0
                        };
                        var responsedb = (int)await _fileRepository.SetFileAsync(newfile);
                        newApplication.CVFileId = responsedb;
                        item.IdFile = responsedb;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.AcceptedTerms):
                        newApplication.AcceptedTerms = item.ValueBool ?? null;
                        break;

                    default:
                        break;
                }
            }

            int form = 0;
            if (newApplication.TownId != 0 && newApplication.VacancyId != 0)
            {
                newApplication.CreatedBy = int.TryParse(user.IdUser, out int createdByfileCV) ? createdByfileCV : 0;
                form = (int)await _repository.SetPreApplicationsAsync(newApplication);
                request.Origin.RegisterId = form;
            }
            else { return new() { Response = "Error Al Registrar Pre Aplicacion" }; }

            foreach (var item in request.Answers ?? Enumerable.Empty<GeneralFormRequestAnswerDto>())
            {
                switch (item.Code)
                {
                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.File):
                        var filepath = _fileHelper.SaveFile(item, request?.Origin ?? new GeneralFormRequestOriginDto());
                        UpdateFileDBRequestDto updatefile = new()
                        {
                            IdFile = item.IdFile,
                            FileName = item.FileName ?? string.Empty,
                            ContentType = item.ContentType ?? string.Empty,
                            FilePath = filepath ?? string.Empty,
                            SizeBytes = item.SizeBytes ?? 0
                        };
                        var responsedbupdate = (bool)await _fileRepository.UpdateFileAsync(updatefile);
                        break;

                    default:
                        break;
                }
            }

            SetPreApplicationsReponseDto response = new() { Response = form > 0 ? "Pre Aplicacion Registrada Exitosamente" : "Error Al Registrar Pre Aplicacion" };

            return (response);
        }

        public async Task<bool?> UpdatePreApplicationsAsync(GeneralFormRequestDto request, LoginDBResponseDto user)
        {
            UpdatePreApplicationsDBRequestDto newApplication = new();

            foreach (var item in request.Answers ?? Enumerable.Empty<GeneralFormRequestAnswerDto>())
            {
                switch (item.Code)
                {
                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.FullName):
                        newApplication.FullName = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.DPI):
                        newApplication.DPI = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Age):
                        newApplication.Age = int.TryParse(item.ValueText, out int createdByfile) ? createdByfile : 0;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Gender):
                        newApplication.Gender = item.OptionValue == "Femenino" ? "Femenino" : "Masculino";
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Phone):
                        newApplication.Phone = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Email):
                        newApplication.Email = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Town):
                        var towns = await _catalogRepository.GetTownCatalogAsync();
                        newApplication.TownId = towns.FirstOrDefault(p => p.Value.ToLower().Contains(item.OptionValue?.ToLower() ?? "", StringComparison.OrdinalIgnoreCase))?.optionId ?? 0;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Address):
                        newApplication.Address = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.EducationLevel):
                        newApplication.EducationLevel = item.OptionValue ?? "";
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Vacancy):
                        var jobs = await _catalogRepository.GetJobCatalogAsync();
                        newApplication.VacancyId = jobs.FirstOrDefault(p => p.Value.ToLower().Contains(item.OptionValue?.ToLower() ?? "", StringComparison.OrdinalIgnoreCase))?.optionId ?? 0;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Experience):
                        newApplication.Experience = item.OptionValue == "no";
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.HowHeard):
                        newApplication.HowHeard = item.OptionValue ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.File):
                        if (!string.IsNullOrEmpty(item.Base64))
                        {
                            var file = await _repository.GetPreApplicationFileIdAsync(request?.Origin.RegisterId ?? new());
                            item.IdFile= file;
                            var filepath = _fileHelper.SaveFile(item, request?.Origin ?? new GeneralFormRequestOriginDto());

                            UpdateFileDBRequestDto newfile = new()
                            {
                                IdFile = item.IdFile,
                                FileName = item.FileName ?? string.Empty,
                                ContentType = item.ContentType ?? string.Empty,
                                FilePath = filepath ?? string.Empty,
                                SizeBytes = item.SizeBytes ?? 0
                            };
                            var responsedb = await _fileRepository.UpdateFileAsync(newfile);
                        } 
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.AcceptedTerms):
                        newApplication.AcceptedTerms = item.ValueBool;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.AssignHub):
                        newApplication.AssignHub = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.ReferredBy):
                        newApplication.RefferedBy = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == EnumHelper.GetEnumDescription(SetPreApplicationCodeEnum.IsReferred):
                        newApplication.IsReferred = item.ValueBool ?? null;
                        break;

                    default:
                        break;
                }
            }

            if (request != null)
            {
                newApplication.Status = request.Origin.State;
                newApplication.IdPreApplication = request.Origin.RegisterId;
            }
            bool form = false;
            if (newApplication.TownId != 0 && newApplication.VacancyId != 0)
            {
                form = (bool)await _repository.UpdatePreApplicationsAsync(newApplication);
            }

            return (form);
        }

        public async Task<bool?> AssignToAsync(int? PreapplicationId, LoginDBResponseDto user, bool IsAssign)
        {
            bool form;
            if (IsAssign)
            {
                form = (bool)await _repository.AssignToAsync(PreapplicationId, int.TryParse(user.IdUser, out int createdBy) ? createdBy : 0);
            }
            else
            {
                form = (bool)await _repository.AssignToAsync(PreapplicationId, null);
            }

            return (form);
        }

    }
}

