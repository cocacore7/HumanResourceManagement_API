using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Interfaces.Form;
using HRM_API.Application.Helpers;

namespace HRM_API.Application.Services
{
    public class FormService
    {
        private readonly IFormRepository _repository;
        private readonly ConversionHelper _conversionHelper;

        public FormService(IFormRepository repository, ConversionHelper conversionHelper)
        {
            _repository = repository;
            _conversionHelper = conversionHelper;
        }

        public async Task<GetUserModulesResponseDto?> GetUserModulesAsync(string userId)
        {
            int userIdSP = _conversionHelper.ToInt(userId);
            var form = await _repository.GetUserModulesAsync(userIdSP);
            GetUserModulesResponseDto response = new GetUserModulesResponseDto { Response = form ?? new List<ModuleDto>() };

            return (response);
        }
    }
}
