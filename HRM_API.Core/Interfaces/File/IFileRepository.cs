using HRM_API.Core.Dtos.File;

namespace HRM_API.Core.Interfaces.File
{
    public interface IFileRepository
    {
        Task<GetFileBase64DBResponseDto?> GetFileBase64Async(string folderName, int id, string code);
        Task<int?> SetFileAsync(SetFileDBRequestDto File);
    }
}