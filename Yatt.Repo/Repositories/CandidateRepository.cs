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
    public class CandidateRepository : ICandidateRepository
    {
        public AppDbContext _context { get; }

        public CandidateRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<CandidateDto>> Create(CandidateDto dto)
        {
            var user = await _context.Users.Include(a => a.Candidate).FirstOrDefaultAsync(a => a.Id == dto.Id);

            if (user == null)
                return new ResponseDto<CandidateDto> { Status = ResponseStatus.NotFound };
            if ((!user.EmailConfirmed && !user.PhoneConfirmed) || (!user.EmailConfirmed && !user.PhoneConfirmed))
                return new ResponseDto<CandidateDto> { Status = ResponseStatus.Unautorize, Message = "You need to confirm email or phone number" };

            var current = DateTime.UtcNow;
            var candidate = new Candidate
            {
                Id = dto.Id,FirstName=dto.FirstName,FatherName=dto.FatherName, CountryId=dto.CountryId, MobilePhone=dto.MobilePhone, 
                Gender = dto.Gender,
                DoBirth = dto.DoBirth,
                Address = dto.Address,
                CreatedDate = current,
                ModifyDate = current,
                ShowDoBirth= dto.ShowDoBirth,
                ShowPhone= dto.ShowPhone, 
            };

            _context.Candidates.Add(candidate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<CandidateDto> { Model = candidate, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<CandidateDto> { Model = candidate, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<CandidateDto>> Delete(string id)
        {
            var user = await _context.Users.Include(a => a.Candidate).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return new ResponseDto<CandidateDto> { Status = ResponseStatus.NotFound };

            user.DeletedDate = DateTime.UtcNow;
            Candidate? candidate = new Candidate();
            if (user.Candidate != null)
            {
                candidate = user.Candidate;
                _context.Candidates.Remove(candidate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return new ResponseDto<CandidateDto> { Model = candidate, Status = ResponseStatus.Error, Message = ex.Message };
                }
            }

            return new ResponseDto<CandidateDto> { Model = candidate, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<CandidateDto>> GetById(string id)
        {
            var user = await _context.Candidates
               .Include(a => a.User)
               .ThenInclude(u=>u!.Role)
               .FirstOrDefaultAsync(x => x.Id == id);
            if(user==null)
                return new ResponseDto<CandidateDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<CandidateDto> {Model=user, Status = ResponseStatus.Success };
        }

        public async Task<PagedList<CandidateDto>> GetPagedList(PageParameter pageParameter)
        {
            var users = await _context.Candidates
                .Include(a => a.User)
                .ThenInclude(u => u!.Role)
                .Search(pageParameter.SearchTerm!)
                .Sort(pageParameter.OrderBy!)
                .Select(a => (CandidateDto)a).ToListAsync();
            return PagedList<CandidateDto>
                .ToPagedList(users, pageParameter.PageNumber, pageParameter.PageSize);
        }

        public async Task<ResponseDto<CandidateDto>> Update(CandidateDto dto)
        {
            var user = await _context.Candidates.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (user == null)
                return new ResponseDto<CandidateDto> { Status = ResponseStatus.NotFound };
           
            var current = DateTime.UtcNow;

            user.CountryId = dto.CountryId;
            user.FirstName = dto.FirstName;
            user.FatherName = dto.FatherName;
            user.MobilePhone = dto.MobilePhone;
            user.Gender = dto.Gender;
            user.DoBirth = dto.DoBirth;
            user.Address = dto.Address;
            user.ShowDoBirth = dto.ShowDoBirth;
            user.ShowPhone = dto.ShowPhone;
            user.ModifyDate = current;
            _context.Candidates.Update(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<CandidateDto> { Model = user, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<CandidateDto> { Model = user, Status = ResponseStatus.Success };
        }
    }
}
