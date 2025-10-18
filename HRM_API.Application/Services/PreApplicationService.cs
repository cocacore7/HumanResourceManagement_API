using HRM_API.Application.Helpers;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.File;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Dtos.PreApplication;
using HRM_API.Core.Enum.PreApplication;
using HRM_API.Core.Interfaces.Catalog;
using HRM_API.Core.Interfaces.File;
using HRM_API.Core.Interfaces.PreApplication;

namespace HRM_API.Application.Services
{
    public class PreApplicationService(IPreApplicationRepository repository, IFileRepository fileRepository, ICatalogRepository catalogRepository, EnumHelper enumHelper, FileHelper fileHelper)
    {
        private readonly IPreApplicationRepository _repository = repository;
        private readonly IFileRepository _fileRepository = fileRepository;
        private readonly ICatalogRepository _catalogRepository = catalogRepository;
        private readonly EnumHelper _enumHelper = enumHelper;
        private readonly FileHelper _fileHelper = fileHelper;

        public async Task<GetPreApplicationsResponseDto?> GetPreApplicationsAsync(string estado, string id)
        {
            var form = await _repository.GetPreApplicationsAsync(estado, id);
            GetPreApplicationsResponseDto response = new() { Response = form ?? [] };

            return (response);
        }

        public async Task<SetPreApplicationsReponseDto?> SetPreApplicationsAsync(GeneralFormRequestDto request, LoginDBResponseDto user)
        {
            SetPreApplicationsDBRequestDto newApplication = new();

            foreach (var item in request.Answers ?? Enumerable.Empty<GeneralFormRequestAnswerDto>())
            {
                switch (item.Code)
                {
                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.FullName):
                        newApplication.FullName = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.DPI):
                        newApplication.DPI = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Age):
                        newApplication.Age = int.TryParse(item.ValueText, out int createdByfile) ? createdByfile : 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Gender):
                        newApplication.Gender = item.OptionValue == "F" ? "Masculino" : "Femenino";
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Phone):
                        newApplication.Phone = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Email):
                        newApplication.Email = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Town):
                        var towns = await _catalogRepository.GetTownCatalogAsync();
                        newApplication.TownId = towns.FirstOrDefault(p => p.Value.ToLower().Contains(item.OptionValue?.ToLower() ?? "", StringComparison.OrdinalIgnoreCase)) ?.Id ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Address):
                        newApplication.Address = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.EducationLevel):
                        newApplication.EducationLevel = item.OptionValue ?? "";
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Vacancy):
                        var jobs = await _catalogRepository.GetJobCatalogAsync();
                        newApplication.VacancyId = jobs.FirstOrDefault(p => p.Value.ToLower().Contains(item.OptionValue?.ToLower() ?? "", StringComparison.OrdinalIgnoreCase))?.Id ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.Experience):
                        newApplication.Experience = item.OptionValue == "no";
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.HowHeard):
                        newApplication.HowHeard = item.OptionValue ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.File):
                        var filepath = _fileHelper.SaveFile(item, request?.Origin ?? new GeneralFormRequestOriginDto());

                        SetFileDBRequestDto newfile = new()
                        {
                            FileName = item.FileName ?? string.Empty,
                            ContentType = item.ContentType ?? string.Empty,
                            FilePath = filepath ?? string.Empty,
                            SizeBytes = item.SizeBytes ?? 0,
                            UploadedBy = int.TryParse(user.IdUser, out int createdByfileCV) ? createdByfileCV : 0
                        };
                        var responsedb = await _fileRepository.SetFileAsync(newfile);

                        newApplication.CVFileId = responsedb ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetPreApplicationCodeEnum.AcceptedTerms):
                        newApplication.AcceptedTerms = item.ValueBool;
                        break;

                    default:
                        break;
                }
            }

            bool form = false;
            if (newApplication.TownId != 0 && newApplication.VacancyId != 0)
            {
                newApplication.CreatedBy = int.TryParse(user.IdUser, out int createdByfileCV) ? createdByfileCV : 0;
                form = (bool)await _repository.SetPreApplicationsAsync(newApplication);
            }
            SetPreApplicationsReponseDto response = new() { Response = form ? "Pre Aplicacion Registrada Exitosamente" : "Error Al Registrar Pre Aplicacion" };

            return (response);
        }
    }
}

