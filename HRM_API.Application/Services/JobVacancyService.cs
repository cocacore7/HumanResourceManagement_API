using HRM_API.Application.Helpers;
using HRM_API.Core.Dtos.Authorization;
using HRM_API.Core.Dtos.File;
using HRM_API.Core.Dtos.General;
using HRM_API.Core.Dtos.JobVacancy;
using HRM_API.Core.Enum.JobVacancy;
using HRM_API.Core.Interfaces.File;
using HRM_API.Core.Interfaces.JobVacancy;

namespace HRM_API.Application.Services
{
    public class JobVacancyService(IJobVacancyRepository repository, IFileRepository fileRepository, EnumHelper enumHelper, FileHelper fileHelper)
    {
        private readonly IJobVacancyRepository _repository = repository;
        private readonly IFileRepository _fileRepository = fileRepository;
        private readonly EnumHelper _enumHelper = enumHelper;
        private readonly FileHelper _fileHelper = fileHelper;

        public async Task<GetJobVacanciesResponseDto?> GetJobVacanciesAsync(string estado, string id)
        {
            var vacancy = await _repository.GetJobVacanciesAsync(estado, id);
            GetJobVacanciesResponseDto response = new() { Response = vacancy ?? [] };

            return (response);
        }

        public async Task<bool?> SetJobVacancyAsync(GeneralFormRequestDto request, LoginDBResponseDto user)
        {
            SetJobVacancyDBRequestDto newvacant = new();

            foreach (var item in request.Answers ?? Enumerable.Empty<GeneralFormRequestAnswerDto>())
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
                        newvacant.Objective = item.ValueText ?? string.Empty;
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
                        newvacant.AvailablePositions = item.ValueNumber ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.RequisitionFileId):
                        var filepath = _fileHelper.SaveFile(item, request?.Origin ?? new GeneralFormRequestOriginDto());

                        SetFileDBRequestDto newfile = new()
                        {
                            FileName = item.FileName ?? string.Empty,
                            ContentType = item.ContentType ?? string.Empty,
                            FilePath = filepath ?? string.Empty,
                            SizeBytes = item.SizeBytes ?? 0,
                            UploadedBy = int.TryParse(user.IdUser, out int createdByfile) ? createdByfile : 0
                        };
                        var responsedb = await _fileRepository.SetFileAsync(newfile);

                        newvacant.RequisitionFileId = responsedb ?? 0;
                        break;

                    default:
                        break;
                }
            }

            newvacant.Status = request?.Origin.State ?? string.Empty;
            newvacant.CreatedBy = int.TryParse(user.IdUser, out int createdBy) ? createdBy : 0;

            var newvacancy = (bool)await _repository.SetJobVacancyAsync(newvacant);
            return newvacancy;
        }

        public async Task<bool?> UpdateJobVacancyAsync(GeneralFormRequestDto request, LoginDBResponseDto user)
        {
            UpdateJobVacancyDBRequestDto vacant = new();

            foreach (var item in request.Answers ?? Enumerable.Empty<GeneralFormRequestAnswerDto>())
            {
                switch (item.Code)
                {
                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.JobPositionName):
                        vacant.JobPositionName = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.RequesterName):
                        vacant.RequesterName = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.RequesterPosition):
                        vacant.RequesterPosition = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.AreaText):
                        vacant.AreaText = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.RegionText):
                        vacant.RegionText = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.HubText):
                        vacant.HubText = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.Objective):
                        vacant.Objective = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.Comment):
                        vacant.Comment = item.ValueText ?? string.Empty;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.VacancyTypeId):
                        vacant.VacancyTypeId = item.OptionId ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.ReasonId):
                        vacant.ReasonId = item.OptionId ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.Salary):
                        vacant.Salary = item.ValueNumber ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.TotalPositions):
                        vacant.TotalPositions = item.ValueNumber ?? 0;
                        vacant.AvailablePositions = item.ValueNumber ?? 0;
                        break;

                    case var code when code == _enumHelper.GetEnumDescription(SetJobVacancyCodeEnum.RequisitionFileId):
                        var filepath = _fileHelper.SaveFile(item, request?.Origin ?? new GeneralFormRequestOriginDto());

                        UpdateFileDBRequestDto newfile = new()
                        {
                            IdFile = item.IdFile ?? 0,
                            FileName = item.FileName ?? string.Empty,
                            ContentType = item.ContentType ?? string.Empty,
                            FilePath = filepath ?? string.Empty,
                            SizeBytes = item.SizeBytes ?? 0,
                            UploadedBy = int.TryParse(user.IdUser, out int createdByfile) ? createdByfile : 0
                        };
                        var responsedb = await _fileRepository.UpdateFileAsync(newfile);
                        break;

                    default:
                        break;
                }
            }

            vacant.Status = request?.Origin.State ?? string.Empty;
            vacant.IdVacancy = request?.Origin.RegisterId;

            var newvacancy = (bool)await _repository.UpdateJobVacancyAsync(vacant);
            return newvacancy;
        }
    }
}

