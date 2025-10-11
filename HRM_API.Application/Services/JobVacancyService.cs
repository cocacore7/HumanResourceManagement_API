using Azure.Core;
using HRM_API.Application.Helpers;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.File;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Dtos.JobVacancy;
using HRM_API.Core.Enum.File;
using HRM_API.Core.Enum.JobVacancy;
using HRM_API.Core.Interfaces.File;
using HRM_API.Core.Interfaces.JobVacancy;

namespace HRM_API.Application.Services
{
    public class JobVacancyService
    {
        private readonly IJobVacancyRepository _repository;
        private readonly IFileRepository _fileRepository;
        private readonly EnumHelper _enumHelper;
        private readonly FileHelper _fileHelper;

        public JobVacancyService(IJobVacancyRepository repository, IFileRepository fileRepository, EnumHelper enumHelper, FileHelper fileHelper)
        {
            _repository = repository;
            _fileRepository = fileRepository;
            _enumHelper = enumHelper;
            _fileHelper = fileHelper;
        }

        public async Task<GetJobVacanciesResponseDto?> GetJobVacanciesAsync(string estado, string id)
        {
            var vacancy = await _repository.GetJobVacanciesAsync(estado, id);
            GetJobVacanciesResponseDto response = new GetJobVacanciesResponseDto { Response = vacancy ?? new List<GetJobVacanciesDBRequestDto>() };

            return (response);
        }

        public async Task<bool?> SetJobVacancyAsync(GeneralFormRequestDto payload, LoginDBResponseDto user)
        {
            var newvacant = new SetJobVacancyDBRequestDto();

            foreach (var item in payload.Answers ?? Enumerable.Empty<GeneralFormRequestAnswerDto>())
            {
                switch (item.Code)
                {
                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.JobPositionName):
                        newvacant.JobPositionName = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.RequesterName):
                        newvacant.RequesterName = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.RequesterPosition):
                        newvacant.RequesterPosition = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.AreaText):
                        newvacant.AreaText = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.RegionText):
                        newvacant.RegionText = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.HubText):
                        newvacant.HubText = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.Objective):
                        newvacant.Objective = item.OptionValue ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.Comment):
                        newvacant.Comment = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.VacancyTypeId):
                        newvacant.VacancyTypeId = item.OptionId ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.ReasonId):
                        newvacant.ReasonId = item.OptionId ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.Salary):
                        newvacant.Salary = item.ValueNumber ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.TotalPositions):
                        newvacant.TotalPositions = item.ValueNumber ?? 0;
                        newvacant.AvailablePosition = item.ValueNumber ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.RequisitionFileId):
                        var filepath = _fileHelper.SaveFile(item.Base64 ?? string.Empty, payload?.Origin?.description ?? string.Empty,
                            payload?.Origin?.registerId ?? 0, item.Code, item.FileName ?? string.Empty);

                        SetFileDBRequestDto newfile = new SetFileDBRequestDto();
                        newfile.fileName = item.FileName ?? string.Empty;
                        newfile.contentType = item.ContentType ?? string.Empty;
                        newfile.filePath = filepath ?? string.Empty;
                        newfile.sizeBytes = item.SizeBytes ?? 0;
                        newfile.uploadedBy = int.TryParse(user.IdUser, out int createdByfile) ? createdByfile : 0;
                        var responsedb = await _fileRepository.SetFileAsync(newfile);

                        newvacant.RequisitionFileId = responsedb ?? 0;
                        break;

                    default:
                        break;
                }
            }

            newvacant.Status = "nuevaVacante";
            newvacant.CreatedBy = int.TryParse(user.IdUser, out int createdBy) ? createdBy : 0;

            var newvacancy = (bool)await _repository.SetJobVacancyAsync(newvacant);
            return newvacancy;
        }
    }
}

