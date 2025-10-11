using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Interfaces.Form;

namespace HRM_API.Application.Services
{
    public class FormService(IFormRepository repository)
    {
        private readonly IFormRepository _repository = repository;

        public async Task<GetUserModulesResponseDto?> GetUserModulesAsync(string userId)
        {
            int userIdSP = int.TryParse(userId, out int createdBy) ? createdBy : 0;
            var form = await _repository.GetUserModulesAsync(userIdSP);
            GetUserModulesResponseDto response = new() { Response = form ?? [] };

            return (response);
        }
    }
}
