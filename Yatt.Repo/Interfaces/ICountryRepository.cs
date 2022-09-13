using Yatt.Models.Dtos;
using Yatt.Models.Entities;

namespace Yatt.Repo.Interfaces
{
    public interface ICountryRepository
    {
        Task<Country> GetDtoById(int id);
        Task<ResponseDto<List<Country>>> GetList();
        Task<ResponseDto<Country>> Create(Country dto);
        Task<ResponseDto<Country>> Update(int id, Country dto);
        Task<ResponseDto<Country>> Delete(int id);

    }
}
