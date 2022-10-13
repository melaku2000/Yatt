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
    public class JobRepository : IJobRepository
    {
        public AppDbContext _context { get; }

        public JobRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<JobDto>> Create(JobDto dto)
        {
            var current = DateTime.UtcNow;
            var job = new Job
            {
                Id = Guid.NewGuid().ToString(),
                SubscrioptionId = dto.SubscrioptionId,
                JobType = dto.JobType,
                Title = dto.Title,
                Description = dto.Description,
                DeadLineDate = dto.DeadLineDate,
                Level = dto.Level,
                Status = RowStatus.Pending,
                ApplayLocation = dto.Location,
                ApplayUrl = dto.ApplayUrl,
                Salary = dto.Salary,
                CreatedDate = current,
                ModifyDate = current,
            };

            _context.Jobs.Add(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobDto> { Model = job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobDto>> Delete(string id)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == id);

            if (job == null)
                return new ResponseDto<JobDto> { Status = ResponseStatus.NotFound };
            _context.Jobs.Remove(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }

            return new ResponseDto<JobDto> { Model = job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobDto>> GetById(string id)
        {
            var job = await _context.Jobs
               .FirstOrDefaultAsync(x => x.Id == id);
            if(job==null)
                return new ResponseDto<JobDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<JobDto> {Model=job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<JobDto>>> GetListByCompanyId(string companyId)
        {
            var educations = await _context.Jobs
                .Include(a=>a.Subscription)
                .Where(a=>a.Subscription!.CompanyId==companyId)
                .Select(a => (JobDto)a).ToListAsync();
            return new ResponseDto<List<JobDto>> { Model = educations, Status = ResponseStatus.Success };
        }
        public async Task<PagedList<JobDto>> GetPagedList(PageParameter pageParameter)
        {
            var jobs = await _context.Jobs
                .Include(a=>a.Educations)
                .Search(pageParameter.SearchTerm!)
                .Sort(pageParameter.OrderBy!)
                .Select(a => (JobDto)a).ToListAsync();
            return PagedList<JobDto>
                .ToPagedList(jobs, pageParameter.PageNumber, pageParameter.PageSize);
        }
        public async Task<ResponseDto<JobDto>> Update(JobDto dto)
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (job == null)
                return new ResponseDto<JobDto> { Status = ResponseStatus.NotFound };
           
            var current = DateTime.UtcNow;

            job.SubscrioptionId = dto.SubscrioptionId;
            job.Title = dto.Title;
            job.Description = dto.Description;
            job.DeadLineDate = dto.DeadLineDate;
            job.JobType = dto.JobType;
            job.Level = dto.Level;
            job.ApplayUrl = dto.ApplayUrl;
            job.ApplayLocation = dto.Location;
            job.Salary = dto.Salary;
            job.ModifyDate = current;
            _context.Jobs.Update(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobDto> { Model = job, Status = ResponseStatus.Success };
        }
    }
}
