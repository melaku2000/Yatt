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
            var current = DateTime.UtcNow;
            var subscription = new Subscription
            {
                Id = Guid.NewGuid().ToString(),
                MembershipId = dto.MembershipId,
                CompanyId = dto.CompanyId,
                CreatedDate = current,
                ModifyDate = current
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

            if (subscription != null)
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
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
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
            var subscriptions = await _context.Subscriptions.Where(a => a.MembershipId == membershipId).ToListAsync();

            return new ResponseDto<List<SubscriptionDto>> { Model = subscriptions.Select(a => (SubscriptionDto)a).ToList(), Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<SubscriptionDto>> Update(SubscriptionDto dto)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (subscription == null)
                return new ResponseDto<SubscriptionDto> { Status = ResponseStatus.NotFound };

            subscription.MembershipId = dto.MembershipId;
            subscription.ModifyDate = DateTime.UtcNow;

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
