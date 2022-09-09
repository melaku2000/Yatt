using Microsoft.EntityFrameworkCore;
using Yatt.Api.Data;
using Yatt.Api.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Api.Repo.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        public AppDbContext _context { get; }

        public CountryRepository(AppDbContext repository)
        {
            _context = repository;
        }

        public async Task<Country> GetDtoById(int id)
        {
            var edu = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (edu != null)
                return edu;
            return null;
        }

        public async Task<ResponseDto<Country>> Create(Country dto)
        {
            var current = DateTime.UtcNow;

            var country = new Country
            {
                DialCode = dto.DialCode,
                Name = dto.Name,
            };

            _context.Countries.Add(country);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<Country> { Model = country, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<Country> { Model = country, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<Country>> Update(int id, Country dto)
        {
            if (id != dto.Id)
                return new ResponseDto<Country> { Status = ResponseStatus.Error, Message = "The Id and model id is not match." };

            var country = await _context.Countries.FirstOrDefaultAsync(a => a.Id == id);
            if (country == null)
                return new ResponseDto<Country> { Status = ResponseStatus.NotFound };
            var current = DateTime.UtcNow;
            country.DialCode = dto.DialCode;
            country.Name = dto.Name;

            _context.Countries.Update(country);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<Country> { Model = country, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<Country> { Model = country, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<Country>> Delete(int id)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);

            if (country == null)
                return new ResponseDto<Country> { Status = ResponseStatus.NotFound };

            if (country != null)
                _context.Countries.Remove(country);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<Country> { Model = country, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<Country> { Model = country, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<Country>>> GetList()
        {
            var countries = await _context.Countries.OrderBy(a=>a.Name).ToListAsync();
            if (countries == null)
                return new ResponseDto<List<Country>> { Status = ResponseStatus.NotFound, Message = "The Id and model id is not match." };

            return new ResponseDto<List<Country>> { Model = countries.Select(a => (Country)a).ToList(), Status = ResponseStatus.Success };
        }
    }
}
