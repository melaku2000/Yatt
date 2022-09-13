using Yatt.Models.Dtos;
using Yatt.Models.Entities;

namespace Yatt.Repo.Interfaces
{
    public interface IVacancyRepository
    {
        Task<ResponseDto<VacancyDto>> GetDtoById(string id);
        Task<ResponseDto<List<VacancyDto>>> GetListBySubscriptionId(string subscriptionId);
        Task<ResponseDto<List<VacancyDto>>> GetListByCompanyId(string companyId);
        Task<ResponseDto<VacancyDto>> Create(VacancyDto dto);
        Task<ResponseDto<VacancyDto>> Update(VacancyDto dto);
        Task<ResponseDto<VacancyDto>> Delete(string id);

    }
}
