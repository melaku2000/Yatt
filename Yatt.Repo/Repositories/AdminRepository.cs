using Microsoft.EntityFrameworkCore;
using Yatt.Repo.Data;
using Yatt.Repo.Interfaces;
using Yatt.Repo.Repositories.Extensions;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;
using Yatt.Models.RequestFeatures;

namespace Yatt.Repo.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        public AppDbContext _context { get; }

        public AdminRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<AdminDto>> Create(AdminDto dto)
        {
            var user = await _context.Users.Include(a => a.Admin).FirstOrDefaultAsync(a => a.Id == dto.Id);

            if (user == null)
                return new ResponseDto<AdminDto> { Status = ResponseStatus.NotFound };
          
            if(user.Admin!=null)
                return new ResponseDto<AdminDto> { Status = ResponseStatus.Error, Message = "User already register in admin table" };

            var current = DateTime.UtcNow;
            var admin = new Admin
            {
                RegisterarId = dto.RegisterarId,FirstName=dto.FirstName,FatherName=dto.FatherName, CountryId=dto.CountryId, MobilePhone=dto.MobilePhone, 
                CreatedDate = current,
                ModifyDate = current, Status=dto.Status
            };

            _context.Admins.Add(admin);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<AdminDto> { Model = admin, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<AdminDto> { Model = admin, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<AdminDto>> Delete(string id)
        {
            var user = await _context.Users.Include(a => a.Admin).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return new ResponseDto<AdminDto> { Status = ResponseStatus.NotFound };

            user.DeletedDate = DateTime.UtcNow;
            Admin? admin = new Admin();
            if (user.Admin != null)
            {
                admin = user.Admin;
                _context.Admins.Remove(admin);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return new ResponseDto<AdminDto> { Model = admin, Status = ResponseStatus.Error, Message = ex.Message };
                }
            }

            return new ResponseDto<AdminDto> { Model = admin, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<AdminDto>> GetById(string id)
        {
            var user = await _context.Admins
               .Include(a => a.User)
               .ThenInclude(u=>u!.Role)
               .FirstOrDefaultAsync(x => x.Id == id);
            if(user==null)
                return new ResponseDto<AdminDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<AdminDto> {Model=user, Status = ResponseStatus.Success };
        }

        public async Task<PagedList<AdminDto>> GetPagedList(PageParameter pageParameter)
        {
            var users = await _context.Admins
                .Include(a => a.User)
                .ThenInclude(u => u!.Role)
                .Search(pageParameter.SearchTerm!)
                .Sort(pageParameter.OrderBy!)
                .Select(a => (AdminDto)a).ToListAsync();
            return PagedList<AdminDto>
                .ToPagedList(users, pageParameter.PageNumber, pageParameter.PageSize);
        }

        public async Task<ResponseDto<AdminDto>> Update(AdminDto dto)
        {
            var user = await _context.Admins.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (user == null)
                return new ResponseDto<AdminDto> { Status = ResponseStatus.NotFound };
           
            var current = DateTime.UtcNow;

            user.CountryId = dto.CountryId;
            user.FirstName = dto.FirstName;
            user.FatherName = dto.FatherName;
            user.MobilePhone = dto.MobilePhone;
            user.ModifyDate = current;
            _context.Admins.Update(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<AdminDto> { Model = user, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<AdminDto> { Model = user, Status = ResponseStatus.Success };
        }
    }
}
