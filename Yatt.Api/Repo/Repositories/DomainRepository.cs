using Microsoft.EntityFrameworkCore;
using Yatt.Api.Data;
using Yatt.Api.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Api.Repo.Repositories
{
    public class DomainRepository : IDomainRepository
    {
        public AppDbContext _context { get; }

        public DomainRepository(AppDbContext repository)
        {
            _context = repository;
        }

        public async Task<DomainDto> GetDtoById(int id)
        {
            var edu = await _context.Domains.FirstOrDefaultAsync(x => x.Id == id);
            if (edu != null)
                return edu;
            return null;
        }

        public async Task<ResponseDto<DomainDto>> Create(DomainDto dto)
        {
            var current = DateTime.UtcNow;
            var domain = new Domain
            {
                Name = dto.Name,
                DeletedDate = current,
                ModifyDate = current
            };

            _context.Domains.Add(domain);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<DomainDto> { Model = domain, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<DomainDto> { Model = domain, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<DomainDto>> Update(int id, DomainDto dto)
        {
            if (id != dto.Id)
                return new ResponseDto<DomainDto> { Status = ResponseStatus.Error, Message = "The Id and model id is not match." };

            var domain = await _context.Domains.FirstOrDefaultAsync(a => a.Id == id);
            if (domain == null)
                return new ResponseDto<DomainDto> { Status = ResponseStatus.NotFound };
            
            domain.Name = dto.Name;
            domain.ModifyDate = DateTime.UtcNow;

            _context.Domains.Update(domain);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<DomainDto> { Model = domain, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<DomainDto> { Model = domain, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<DomainDto>> Delete(int id)
        {
            var domain = await _context.Domains.FirstOrDefaultAsync(x => x.Id == id);

            if (domain == null)
                return new ResponseDto<DomainDto> { Status = ResponseStatus.NotFound };

            if (domain != null)
                _context.Domains.Remove(domain);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<DomainDto> { Model = domain, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<DomainDto> { Model = domain, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<DomainDto>>> GetList()
        {
            var domains = await _context.Domains.OrderBy(a=>a.Name).ToListAsync();
            if (domains == null)
                return new ResponseDto<List<DomainDto>> { Status = ResponseStatus.NotFound, Message = "The Id and model id is not match." };

            return new ResponseDto<List<DomainDto>> { Model = domains.Select(a => (DomainDto)a).ToList(), Status = ResponseStatus.Success };
        }
    }
}
