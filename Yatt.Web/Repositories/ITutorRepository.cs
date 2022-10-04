using Yatt.Models.Dtos;
using Yatt.Models.RequestFeatures;
using Yatt.Web.Features;

namespace Yatt.Web.Repositories
{
    public interface ITutorRepository<T> where T : class
    {
        Task<List<T>> GetLists(string url);
        Task<T> GetById(string url,string id);
        Task<PagingResponse<T>> GetPagedList(string url,PageParameter pageParameters);
        Task<ResponseDto<T>> Create(string url,T item);
        Task<ResponseDto<T>> Update(string url, T item);
        Task<ResponseDto<T>> Delete(string url, string id);
    }
}
