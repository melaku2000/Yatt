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
    public class JobQualificationRepository : IJobQualificationRepository
    {
        public AppDbContext _context { get; }

        public JobQualificationRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<JobQualificationDto>> Create(JobQualificationDto dto)
        {
            var current = DateTime.UtcNow;
            var job = new JobQualification
            {
                Id = Guid.NewGuid().ToString(),
                JobId = dto.JobId,
                Qualification = dto.Qualification,
                CreatedDate = current, 
                ModifyDate = current, 
            };

            _context.JobQualifications.Add(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobQualificationDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobQualificationDto> { Model = job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobQualificationDto>> Delete(string id)
        {
            var job = await _context.JobQualifications.FirstOrDefaultAsync(x => x.Id == id);

            if (job == null)
                return new ResponseDto<JobQualificationDto> { Status = ResponseStatus.NotFound };
            _context.JobQualifications.Remove(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobQualificationDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }

            return new ResponseDto<JobQualificationDto> { Model = job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobQualificationDto>> GetById(string id)
        {
            var job = await _context.JobQualifications
               .FirstOrDefaultAsync(x => x.Id == id);
            if(job==null)
                return new ResponseDto<JobQualificationDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<JobQualificationDto> {Model=job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<JobQualificationDto>>> GetListByJobId(string jobId)
        {
            var educations = await _context.JobQualifications
                .Where(a=>a.JobId==jobId)
                .Select(a => (JobQualificationDto)a).ToListAsync();
            return new ResponseDto<List<JobQualificationDto>> { Model = educations, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobQualificationDto>> Update(JobQualificationDto dto)
        {
            var job = await _context.JobQualifications.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (job == null)
                return new ResponseDto<JobQualificationDto> { Status = ResponseStatus.NotFound };
           
            var current = DateTime.UtcNow;

            job.Qualification = dto.Qualification;
            job.ModifyDate = current;
            _context.JobQualifications.Update(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobQualificationDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobQualificationDto> { Model = job, Status = ResponseStatus.Success };
        }
    }
}
