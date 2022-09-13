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
    public class JobDutyRepository : IJobDutyRepository
    {
        public AppDbContext _context { get; }

        public JobDutyRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<JobDutyDto>> Create(JobDutyDto dto)
        {
            var current = DateTime.UtcNow;
            var job = new JobDuty
            {
                Id = Guid.NewGuid().ToString(),
                JobId=dto.JobId,    
                Duty = dto.Duty, 
                CreatedDate = current,
                ModifyDate = current,
            };

            _context.JobDuties.Add(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobDutyDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobDutyDto> { Model = job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobDutyDto>> Delete(string id)
        {
            var job = await _context.JobDuties.FirstOrDefaultAsync(x => x.Id == id);

            if (job == null)
                return new ResponseDto<JobDutyDto> { Status = ResponseStatus.NotFound };
            _context.JobDuties.Remove(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobDutyDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }

            return new ResponseDto<JobDutyDto> { Model = job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobDutyDto>> GetById(string id)
        {
            var job = await _context.JobDuties
               .FirstOrDefaultAsync(x => x.Id == id);
            if(job==null)
                return new ResponseDto<JobDutyDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<JobDutyDto> {Model=job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<JobDutyDto>>> GetListByJobId(string jobId)
        {
            var educations = await _context.JobDuties
                .Where(a=>a.JobId==jobId)
                .Select(a => (JobDutyDto)a).ToListAsync();
            return new ResponseDto<List<JobDutyDto>> { Model = educations, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobDutyDto>> Update(JobDutyDto dto)
        {
            var job = await _context.JobDuties.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (job == null)
                return new ResponseDto<JobDutyDto> { Status = ResponseStatus.NotFound };
           
            var current = DateTime.UtcNow;

            job.Duty = dto.Duty;
            job.ModifyDate = current;
            _context.JobDuties.Update(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobDutyDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobDutyDto> { Model = job, Status = ResponseStatus.Success };
        }
    }
}
