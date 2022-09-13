using Microsoft.EntityFrameworkCore;
using Yatt.Repo.Data;
using Yatt.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Repo.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        public AppDbContext _context { get; }

        public SubscriptionRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<SubscriptionDto>> Create(SubscriptionDto dto)
        {
            var membership=await _context.Memberships.FirstOrDefaultAsync(a=>a.Id==dto.MembershipId);
            if (membership == null)
                return new ResponseDto<SubscriptionDto>
                {
                    Status = ResponseStatus.NotFound,
                    Message = "Membership is not found"
                };

            if (membership.Amount == 0) dto.Status = ClientStatus.Approved;
            var current = DateTime.UtcNow;
            var subscription = new Subscription
            {
                Id = Guid.NewGuid().ToString(),
                MembershipId = dto.MembershipId,
                CompanyId = dto.CompanyId,
                ServicePeriodInMonth = dto.ServicePeriodInMonth,
                NoOfJobPost = dto.NoOfJobPost,
                NoOfCandidateInterview = dto.NoOfCandidateInterview,
                Amount = dto.Amount,
                CreatedDate = current,
                ModifyDate = current,
                Status=dto.Status
            };

            _context.Subscriptions.Add(subscription);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<SubscriptionDto> { Model = subscription, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<SubscriptionDto> { Model = subscription, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<SubscriptionDto>> Delete(string id)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);

            if (subscription == null)
                return new ResponseDto<SubscriptionDto> { Status = ResponseStatus.NotFound };

            _context.Subscriptions.Remove(subscription);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<SubscriptionDto> { Model = subscription, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<SubscriptionDto> { Model = subscription, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<SubscriptionDto>> GetDtoById(string id)
        {
            var subscription = await _context.Subscriptions
                .Include(a => a.Membership)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (subscription == null)
                return new ResponseDto<SubscriptionDto> { Status=ResponseStatus.NotFound,Message=$"The item with id : {id} could not found"};
         
            return new ResponseDto<SubscriptionDto> {Model=subscription, Status=ResponseStatus.Success};
        }

        public async Task<ResponseDto<List<SubscriptionDto>>> GetListByCompanyId(string companyId)
        {
            var subscriptions = await _context.Subscriptions.Where(a=>a.CompanyId==companyId).ToListAsync();

            return new ResponseDto<List<SubscriptionDto>> { Model = subscriptions.Select(a => (SubscriptionDto)a).ToList(), Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<SubscriptionDto>>> GetListByMembershipId(string membershipId)
        {
            var subscriptions = await _context.Subscriptions
                .Include(a=>a.Membership)
                .Where(a => a.MembershipId == membershipId).ToListAsync();

            return new ResponseDto<List<SubscriptionDto>> { Model = subscriptions.Select(a => (SubscriptionDto)a).ToList(), Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<SubscriptionDto>> Update(SubscriptionDto dto)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (subscription == null)
                return new ResponseDto<SubscriptionDto> { Status = ResponseStatus.NotFound };

            subscription.MembershipId = dto.MembershipId;
            subscription.NoOfJobPost = dto.NoOfJobPost;
            subscription.NoOfCandidateInterview = dto.NoOfCandidateInterview;
            subscription.ServicePeriodInMonth = dto.ServicePeriodInMonth;
            subscription.Amount = dto.Amount;
            subscription.ModifyDate = DateTime.UtcNow;

            // IF COMPANY UPGRADE MEMBERSHIP = MAKE STATUS TO PENDING FOR ADMIN TO APPROVED
            subscription.Status = ClientStatus.Pending;
            _context.Subscriptions.Update(subscription);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<SubscriptionDto> { Model = subscription, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<SubscriptionDto> { Model = subscription, Status = ResponseStatus.Success };
        }
    }
}
