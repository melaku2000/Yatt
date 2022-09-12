using Microsoft.EntityFrameworkCore;
using Yatt.Repo.Data;
using Yatt.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Repo.Repositories
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
                CreatedDate = current,
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

        public async Task<ResponseDto<DomainDto>> Update(DomainDto dto)
        {
            var domain = await _context.Domains.FirstOrDefaultAsync(a => a.Id == dto.Id);
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
           
            return new ResponseDto<List<DomainDto>> { Model = domains.Select(a => (DomainDto)a).ToList(), Status = ResponseStatus.Success };
        }
    }
}
