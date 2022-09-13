using Microsoft.EntityFrameworkCore;
using Yatt.Repo.Data;
using Yatt.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Repo.Repositories
{
    public class VacancyRepository : IVacancyRepository
    {
        public AppDbContext _context { get; }

        public VacancyRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<VacancyDto>> Create(VacancyDto dto)
        {
            var subscribe=await _context.Subscriptions.FirstOrDefaultAsync(a=>a.Id==dto.SubscrioptionId);
            if (subscribe == null)
                return new ResponseDto<VacancyDto>
                {
                    Status = ResponseStatus.NotFound,
                    Message = "Membership is not found"
                };

            if (subscribe.Status != ClientStatus.Approved)
                return new ResponseDto<VacancyDto>
                {
                    Status = ResponseStatus.Unautorize,
                    Message = "Yor subscription is not allowed this operation"
                };
            var current = DateTime.UtcNow;
            var vacancy = new Vacancy
            {
                Id = Guid.NewGuid().ToString(),
                SubscrioptionId = dto.SubscrioptionId,
                ApplyUrl = dto.ApplyUrl,
                DeadLineDate = current,
                Description = dto.Description,
                CreatedDate = current,
                ModifyDate = current,
                Status = dto.Status
            };

            _context.Vacancies.Add(vacancy);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<VacancyDto> { Model = vacancy, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<VacancyDto> { Model = vacancy, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<VacancyDto>> Delete(string id)
        {
            var vacancy = await _context.Vacancies.FirstOrDefaultAsync(x => x.Id == id);

            if (vacancy == null)
                return new ResponseDto<VacancyDto> { Status = ResponseStatus.NotFound };

            _context.Vacancies.Remove(vacancy);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<VacancyDto> { Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<VacancyDto> { Model = vacancy, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<VacancyDto>> GetDtoById(string id)
        {
            var subscription = await _context.Vacancies
                .Include(a => a.Subscription)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (subscription == null)
                return new ResponseDto<VacancyDto> { Status=ResponseStatus.NotFound,Message=$"The item with id : {id} could not found"};
         
            return new ResponseDto<VacancyDto> {Model=subscription, Status=ResponseStatus.Success};
        }

        public async Task<ResponseDto<List<VacancyDto>>> GetListByCompanyId(string companyId)
        {
            var vacancies = await _context.Vacancies
                .Include(a=>a.Subscription)
                .Where(a=>a.Subscription!.CompanyId==companyId)
                .ToListAsync();

            return new ResponseDto<List<VacancyDto>> { Model = vacancies.Select(a => (VacancyDto)a).ToList(), Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<VacancyDto>>> GetListBySubscriptionId(string subscriptionId)
        {
            var subscriptions = await _context.Vacancies
                .Include(a=>a.Subscription)
                .Where(a => a.SubscrioptionId == subscriptionId).ToListAsync();

            return new ResponseDto<List<VacancyDto>> { Model = subscriptions.Select(a => (VacancyDto)a).ToList(), Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<VacancyDto>> Update(VacancyDto dto)
        {
            var vacancy = await _context.Vacancies.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (vacancy == null)
                return new ResponseDto<VacancyDto> { Status = ResponseStatus.NotFound };

            vacancy.SubscrioptionId = dto.SubscrioptionId;
            vacancy.Description = dto.Description;
            vacancy.ApplyUrl = dto.ApplyUrl;
            vacancy.DeadLineDate = dto.DeadLineDate;
            vacancy.Status = dto.Status;
            vacancy.ModifyDate = DateTime.UtcNow;

            // IF COMPANY UPGRADE MEMBERSHIP = MAKE STATUS TO PENDING FOR ADMIN TO APPROVED
            _context.Vacancies.Update(vacancy);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<VacancyDto> { Model = vacancy, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<VacancyDto> { Model = vacancy, Status = ResponseStatus.Success };
        }
    }
}
