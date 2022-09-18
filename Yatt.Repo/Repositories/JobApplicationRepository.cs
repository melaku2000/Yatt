using Microsoft.EntityFrameworkCore;
using Yatt.Repo.Data;
using Yatt.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Repo.Repositories
{
    public class JobApplicationRepository : IJobApplicationRepository
    {
        public AppDbContext _context { get; }

        public JobApplicationRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<JobApplicationDto>> Create(JobApplicationDto dto)
        {
            var job=await _context.Jobs.FirstOrDefaultAsync(a=>a.Id==dto.JobId);
            if (job == null)
                return new ResponseDto<JobApplicationDto>
                {
                    Status = ResponseStatus.NotFound,
                    Message = "Membership is not found"
                };

            var current = DateTime.UtcNow;
            var application = new JobApplication
            {
                Id = Guid.NewGuid().ToString(),
                JobId = dto.JobId,
                CandidateId = dto.CandidateId, 
                CreatedDate = current,
                ModifyDate = current,
                Status=dto.Status
            };

            _context.JobApplications.Add(application);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobApplicationDto> { Model = application, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobApplicationDto> { Model = application, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobApplicationDto>> Delete(string id)
        {
            var application = await _context.JobApplications.FirstOrDefaultAsync(x => x.Id == id);

            if (application == null)
                return new ResponseDto<JobApplicationDto> { Status = ResponseStatus.NotFound };

            _context.JobApplications.Remove(application);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobApplicationDto> { Model = application, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobApplicationDto> { Model = application, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobApplicationDto>> GetDtoById(string id)
        {
            var application = await _context.JobApplications
                .FirstOrDefaultAsync(x => x.Id == id);
            if (application == null)
                return new ResponseDto<JobApplicationDto> { Status=ResponseStatus.NotFound,Message=$"The item with id : {id} could not found"};
         
            return new ResponseDto<JobApplicationDto> {Model=application, Status=ResponseStatus.Success};
        }

        public async Task<ResponseDto<List<JobApplicationDto>>> GetListByJobId(string jobId)
        {
            var applications = await _context.JobApplications.Where(a=>a.JobId==jobId).ToListAsync();

            return new ResponseDto<List<JobApplicationDto>> { Model = applications.Select(a => (JobApplicationDto)a).ToList(), Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<JobApplicationDto>>> GetListByCandidateId(string candidateId)
        {
            var applications = await _context.JobApplications
                .Where(a => a.CandidateId == candidateId).ToListAsync();

            return new ResponseDto<List<JobApplicationDto>> { Model = applications.Select(a => (JobApplicationDto)a).ToList(), Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<JobApplicationDto>> Update(JobApplicationDto dto)
        {
            var application = await _context.JobApplications.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (application == null)
                return new ResponseDto<JobApplicationDto> { Status = ResponseStatus.NotFound };

            application.Status = dto.Status;
            application.ModifyDate = DateTime.UtcNow;

            _context.JobApplications.Update(application);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<JobApplicationDto> { Model = application, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<JobApplicationDto> { Model = application, Status = ResponseStatus.Success };
        }
    }
}
