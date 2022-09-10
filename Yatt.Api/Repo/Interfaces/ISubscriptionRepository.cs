using Yatt.Models.Dtos;
using Yatt.Models.Entities;

namespace Yatt.Api.Repo.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<ResponseDto<SubscriptionDto>> GetDtoById(string id);
        Task<ResponseDto<List<SubscriptionDto>>> GetListByMembershipId(string membershipId);
        Task<ResponseDto<List<SubscriptionDto>>> GetListByCompanyId(string companyId);
        Task<ResponseDto<SubscriptionDto>> Create(SubscriptionDto dto);
        Task<ResponseDto<SubscriptionDto>> Update(SubscriptionDto dto);
        Task<ResponseDto<SubscriptionDto>> Delete(string id);

    }
}
