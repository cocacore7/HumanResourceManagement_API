using HRM_API.Core.Dtos.Form;

namespace HRM_API.Core.Dtos.File
{
    public class GetFileBase64ResponseDto
    {
        public GetFileBase64DBResponseDto Response { get; set; } = new GetFileBase64DBResponseDto();
    }
}
