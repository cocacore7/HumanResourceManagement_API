using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Interfaces.Form;

namespace HRM_API.Application.Services
{
    public class FormService
    {
        private readonly IFormRepository _repository;

        public FormService(IFormRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetUserModulesResponseDto?> GetUserModulesAsync(string userId)
        {
            var form = await _repository.GetUserModulesAsync(userId);

            //form trae info del sp
            //armar dto de response - GetUserModulesResponseDto 
            GetUserModulesResponseDto response = new GetUserModulesResponseDto { };
            return (response);
        }
    }
}
