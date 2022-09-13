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
    public class JobDescriptionRepository : IJobDescriptionRepository
    {
        public AppDbContext _context { get; }

        public JobDescriptionRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<JobDescrioptionDto>> Create(JobDescrioptionDto dto)
        {
            var current = DateTime.UtcNow;
            var job = new JobDescription
            {
                Id = Guid.NewGuid().ToString(),
                JobId=dto.JobId,
                Descripttion = dto.Descripttion,
                CreatedDate = current,
                ModifyDate = current,
            };

            _context.JobDescriptions.Add(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobDescrioptionDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobDescrioptionDto> { Model = job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobDescrioptionDto>> Delete(string id)
        {
            var job = await _context.JobDescriptions.FirstOrDefaultAsync(x => x.Id == id);

            if (job == null)
                return new ResponseDto<JobDescrioptionDto> { Status = ResponseStatus.NotFound };
            _context.JobDescriptions.Remove(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobDescrioptionDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }

            return new ResponseDto<JobDescrioptionDto> { Model = job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobDescrioptionDto>> GetById(string id)
        {
            var job = await _context.JobDescriptions
               .FirstOrDefaultAsync(x => x.Id == id);
            if(job==null)
                return new ResponseDto<JobDescrioptionDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<JobDescrioptionDto> {Model=job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<JobDescrioptionDto>>> GetListByJobId(string jobId)
        {
            var educations = await _context.JobDescriptions
                .Where(a=>a.JobId==jobId)
                .Select(a => (JobDescrioptionDto)a).ToListAsync();
            return new ResponseDto<List<JobDescrioptionDto>> { Model = educations, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobDescrioptionDto>> Update(JobDescrioptionDto dto)
        {
            var job = await _context.JobDescriptions.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (job == null)
                return new ResponseDto<JobDescrioptionDto> { Status = ResponseStatus.NotFound };
           
            var current = DateTime.UtcNow;

            job.Descripttion = dto.Descripttion;
            job.ModifyDate = current;
            _context.JobDescriptions.Update(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobDescrioptionDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobDescrioptionDto> { Model = job, Status = ResponseStatus.Success };
        }
    }
}
