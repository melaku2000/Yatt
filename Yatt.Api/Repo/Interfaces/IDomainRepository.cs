using Yatt.Models.Dtos;
using Yatt.Models.Entities;

namespace Yatt.Api.Repo.Interfaces
{
    public interface IDomainRepository
    {
        Task<DomainDto> GetDtoById(int id);
        Task<ResponseDto<List<DomainDto>>> GetList();
        Task<ResponseDto<DomainDto>> Create(DomainDto dto);
        Task<ResponseDto<DomainDto>> Update(DomainDto dto);
        Task<ResponseDto<DomainDto>> Delete(int id);

    }
}
