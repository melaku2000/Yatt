using Yatt.Models.Dtos;

namespace Yatt.Web.Services
{
    public interface IFileService
    {
        Task<bool> UploadProfileImage(FileData content);
        Task<FileData> GetProfileImage(string id);
    }
}
