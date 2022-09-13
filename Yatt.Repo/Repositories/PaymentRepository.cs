using Microsoft.EntityFrameworkCore;
using Yatt.Repo.Data;
using Yatt.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Repo.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public AppDbContext _context { get; }

        public PaymentRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<PaymentDto>> Create(PaymentDto dto)
        {
            var subscription=await _context.Subscriptions.FirstOrDefaultAsync(a=>a.Id==dto.SubscriptionId);
            if (subscription == null)
                return new ResponseDto<PaymentDto>
                {
                    Status = ResponseStatus.NotFound,
                    Message = "subscription is not found"
                };

            if (subscription.Amount == 0) dto.Status = ClientStatus.Approved;
            var current = DateTime.UtcNow;
            var payment = new Payment
            {
                Id = Guid.NewGuid().ToString(),
                SubscriptionId = dto.SubscriptionId,
                AdminId= dto.AdminId,
                CreatedDate = current,
                ModifyDate = current,
            };

            _context.Subscriptions.Add(subscription);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<PaymentDto> { Model = payment, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<PaymentDto> { Model = payment, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<PaymentDto>> Delete(string id)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(x => x.Id == id);

            if (payment == null)
                return new ResponseDto<PaymentDto> { Status = ResponseStatus.NotFound };

            _context.Payments.Remove(payment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<PaymentDto> { Model = payment, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<PaymentDto> { Model = payment, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<PaymentDto>> GetDtoById(string id)
        {
            var subscription = await _context.Payments
                .Include(a => a.Subscription)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (subscription == null)
                return new ResponseDto<PaymentDto> { Status=ResponseStatus.NotFound,Message=$"The item with id : {id} could not found"};
         
            return new ResponseDto<PaymentDto> {Model=subscription, Status=ResponseStatus.Success};
        }

        public async Task<ResponseDto<List<PaymentDto>>> GetListByAdminId(string adminId)
        {
            var payments = await _context.Payments
                .Include(a=>a.Subscription).Where(a=>a.AdminId==adminId).ToListAsync();

            return new ResponseDto<List<PaymentDto>> { Model = payments.Select(a => (PaymentDto)a).ToList(), Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<PaymentDto>>> GetListBySubscriptionId(string subscriptionId)
        {
            var subscriptions = await _context.Payments
                .Include(a=>a.Subscription)
                .Where(a => a.SubscriptionId == subscriptionId).ToListAsync();

            return new ResponseDto<List<PaymentDto>> { Model = subscriptions.Select(a => (PaymentDto)a).ToList(), Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<PaymentDto>> Update(PaymentDto dto)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (payment == null)
                return new ResponseDto<PaymentDto> { Status = ResponseStatus.NotFound };

            payment.AdminId = dto.AdminId;
            payment.ModifyDate = DateTime.UtcNow;

            // IF COMPANY UPGRADE MEMBERSHIP = MAKE STATUS TO PENDING FOR ADMIN TO APPROVED
            _context.Payments.Update(payment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<PaymentDto> { Model = payment, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<PaymentDto> { Model = payment, Status = ResponseStatus.Success };
        }
    }
}
