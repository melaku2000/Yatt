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
    public class JobEducationRepository : IJobEducationRepository
    {
        public AppDbContext _context { get; }

        public JobEducationRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<JobEducationDto>> Create(JobEducationDto dto)
        {
            var current = DateTime.UtcNow;
            var job = new JobEducation
            {
                Id = Guid.NewGuid().ToString(),
                JobId = dto.JobId,
                Level = dto.Level,
                FieldOfStudy = dto.FieldOfStudy,
                YearsOfExperiance = dto.YearsOfExperiance,
                CreatedDate = current,
                ModifyDate = current, 
            };

            _context.JobEducations.Add(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobEducationDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobEducationDto> { Model = job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobEducationDto>> Delete(string id)
        {
            var job = await _context.JobEducations.FirstOrDefaultAsync(x => x.Id == id);

            if (job == null)
                return new ResponseDto<JobEducationDto> { Status = ResponseStatus.NotFound };
            _context.JobEducations.Remove(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobEducationDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }

            return new ResponseDto<JobEducationDto> { Model = job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobEducationDto>> GetById(string id)
        {
            var job = await _context.JobEducations
               .FirstOrDefaultAsync(x => x.Id == id);
            if(job==null)
                return new ResponseDto<JobEducationDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<JobEducationDto> {Model=job, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<JobEducationDto>>> GetListByJobId(string jobId)
        {
            var educations = await _context.JobEducations
                .Where(a=>a.JobId==jobId)
                .Select(a => (JobEducationDto)a).ToListAsync();
            return new ResponseDto<List<JobEducationDto>> { Model = educations, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobEducationDto>> Update(JobEducationDto dto)
        {
            var job = await _context.JobEducations.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (job == null)
                return new ResponseDto<JobEducationDto> { Status = ResponseStatus.NotFound };
           
            var current = DateTime.UtcNow;

            job.Level = dto.Level;
            job.FieldOfStudy = dto.FieldOfStudy;
            job.YearsOfExperiance = dto.YearsOfExperiance;
            job.ModifyDate = current;
            _context.JobEducations.Update(job);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobEducationDto> { Model = job, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobEducationDto> { Model = job, Status = ResponseStatus.Success };
        }
    }
}
