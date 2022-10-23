using Yatt.Models.Dtos;

namespace Yatt.Web.Services
{
    public interface IFileService
    {
        Task<bool> UploadProfileImage(string url,FileData content);
        Task<FileData> GetProfileImage(string url);
    }
}
