using Microsoft.EntityFrameworkCore;
using Yatt.Repo.Data;
using Yatt.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Repo.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        public AppDbContext _context { get; }

        public MembershipRepository(AppDbContext repository)
        {
            _context = repository;
        }

        public async Task<MembershipDto> GetDtoById(string id)
        {
            var membership = await _context.Memberships.FirstOrDefaultAsync(x => x.Id == id);
            if (membership != null)
                return membership;
            return null;
        }

        public async Task<ResponseDto<MembershipDto>> Create(MembershipDto dto)
        {
            var current = DateTime.UtcNow;
            var membership = new Membership
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Amount = dto.Amount,
                NoOfCandidateInterview = dto.NoOfCandidateInterview,
                NoOfJobPost = dto.NoOfJobPost,
                ServicePeriodInMonth = dto.ServicePeriodInMonth,
                CreatedDate = current,
                ModifyDate = current
            };

            _context.Memberships.Add(membership);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<MembershipDto> { Model = membership, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<MembershipDto> { Model = membership, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<MembershipDto>> Update(MembershipDto dto)
        {
            var membership = await _context.Memberships.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (membership == null)
                return new ResponseDto<MembershipDto> { Status = ResponseStatus.NotFound };
            
            membership.Name = dto.Name;
            membership.Amount = dto.Amount;
            membership.NoOfJobPost = dto.NoOfJobPost;
            membership.NoOfCandidateInterview = dto.NoOfCandidateInterview;
            membership.ServicePeriodInMonth = dto.ServicePeriodInMonth;
            membership.ModifyDate = DateTime.UtcNow;

            _context.Memberships.Update(membership);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<MembershipDto> { Model = membership, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<MembershipDto> { Model = membership, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<MembershipDto>> Delete(string id)
        {
            var membership = await _context.Memberships.FirstOrDefaultAsync(x => x.Id == id);

            if (membership == null)
                return new ResponseDto<MembershipDto> { Status = ResponseStatus.NotFound };

            if (membership != null)
                _context.Memberships.Remove(membership);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<MembershipDto> { Model = membership, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<MembershipDto> { Model = membership, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<MembershipDto>>> GetList()
        {
            var memberships = await _context.Memberships.OrderBy(a=>a.Name).ToListAsync();
            if (memberships == null)
                return new ResponseDto<List<MembershipDto>> { Status = ResponseStatus.NotFound, Message = "The Id and model id is not match." };

            return new ResponseDto<List<MembershipDto>> { Model = memberships.Select(a => (MembershipDto)a).ToList(), Status = ResponseStatus.Success };
        }
    }
}
