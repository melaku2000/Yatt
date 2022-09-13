using Yatt.Models.Dtos;
using Yatt.Models.Entities;

namespace Yatt.Repo.Interfaces
{
    public interface IPaymentRepository
    {
        Task<ResponseDto<PaymentDto>> GetDtoById(string id);
        Task<ResponseDto<List<PaymentDto>>> GetListByAdminId(string adminId);
        Task<ResponseDto<List<PaymentDto>>> GetListBySubscriptionId(string serviceId); 
        Task<ResponseDto<PaymentDto>> Create(PaymentDto dto);
        Task<ResponseDto<PaymentDto>> Update(PaymentDto dto);
        Task<ResponseDto<PaymentDto>> Delete(string id);

    }
}
