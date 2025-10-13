using HRM_API.Core.Dtos.Form;
using HRM_API.Core.Interfaces.Form;

namespace HRM_API.Application.Services
{
    public class FormService(IFormRepository repository)
    {
        private readonly IFormRepository _repository = repository;

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
    }
}
