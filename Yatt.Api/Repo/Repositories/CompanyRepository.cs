using Microsoft.EntityFrameworkCore;
using Yatt.Api.Data;
using Yatt.Api.Repo.Interfaces;
using Yatt.Api.Repo.Repositories.Extensions;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;
using Yatt.Models.RequestFeatures;

namespace Yatt.Api.Repo.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        public AppDbContext _context { get; }

        public CompanyRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<CompanyDto>> Create(CompanyDto dto)
        {
            var company = await _context.Companies.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == dto.Id);

            if (company == null)
                return new ResponseDto<CompanyDto> { Status = ResponseStatus.NotFound };
            if ((!company.User!.EmailConfirmed && !company.User!.PhoneConfirmed) || (!company.User!.EmailConfirmed && !company.User!.PhoneConfirmed))
                return new ResponseDto<CompanyDto> { Status = ResponseStatus.Unautorize, Message = "You need to confirm email or phone number" };

            var current = DateTime.UtcNow;
            var companyDetail = new CompanyDetail
            {
                CompanyId = dto.Id,
                DomainId= dto.DomainId,
                CompanyName = dto.CompanyName,
                CompanyPhone = dto.CompanyPhone,
                ContactName = dto.ContactName,
                ContactPhone = dto.ContactPhone,
                CountryId = dto.CountryId,
                Address = dto.Address,
                CreatedDate = current,
                ModifyDate = current, 
                WebUrl=dto.WebUrl,

            };

            _context.CompanyDetails.Add(companyDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<CompanyDto> { Model = companyDetail, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<CompanyDto> { Model = companyDetail, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<CompanyDto>> Delete(string id)
        {
            var user = await _context.Users.Include(a => a.Company).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return new ResponseDto<CompanyDto> { Status = ResponseStatus.NotFound };

            user.DeletedDate = DateTime.UtcNow;
            Company? candidate = new Company();
            if (user.Candidate != null)
            {
                candidate = user.Company;
                _context.Companies.Remove(candidate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return new ResponseDto<CompanyDto> { Model = candidate, Status = ResponseStatus.Error, Message = ex.Message };
                }
            }

            return new ResponseDto<CompanyDto> { Model = candidate, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<CompanyDto>> GetById(string id)
        {
            var user = await _context.CompanyDetails
               .Include(a => a.Country)
               .Include(a => a.Domain)
               .Include(a => a.Company)
               .ThenInclude(a=>a.User)
               .FirstOrDefaultAsync(x => x.CompanyId == id);
            if(user==null)
                return new ResponseDto<CompanyDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<CompanyDto> {Model=user, Status = ResponseStatus.Success };
        }

        public async Task<PagedList<CompanyDto>> GetPagedList(PageParameter pageParameter)
        {
            var users = await _context.CompanyDetails
                .Include(a => a.Country)
                .Include(a => a.Domain)
                .Include(a => a.Company)
                    .ThenInclude(c=>c.User)
                .Search(pageParameter.SearchTerm!)
                .Sort(pageParameter.OrderBy!)
                .Select(a => (CompanyDto)a).ToListAsync();
            return PagedList<CompanyDto>
                .ToPagedList(users, pageParameter.PageNumber, pageParameter.PageSize);
        }

        public async Task<ResponseDto<CompanyDto>> Update(CompanyDto dto)
        {
            var comp = await _context.CompanyDetails.FirstOrDefaultAsync(a => a.CompanyId == dto.Id);
            if (comp == null)
                return new ResponseDto<CompanyDto> { Status = ResponseStatus.NotFound };
           
            var current = DateTime.UtcNow;

            comp.CountryId = dto.CountryId;
            comp.CompanyName = dto.CompanyName;
            comp.CompanyPhone = dto.CompanyPhone;
            comp.ContactPhone = dto.ContactPhone;
            comp.Address = dto.Address;
            comp.WebUrl = dto.WebUrl;
            comp.ModifyDate = current;
            _context.CompanyDetails.Update(comp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<CompanyDto> { Model = comp, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<CompanyDto> { Model = comp, Status = ResponseStatus.Success };
        }
    }
}
