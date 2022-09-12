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
    public class ExperianceRepository : IExperianceRepository
    {
        public AppDbContext _context { get; }

        public ExperianceRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<ExperianceDto>> Create(ExperianceDto dto)
        {
            var current = DateTime.UtcNow;
            var edu = new Experiance
            {
                Id=Guid.NewGuid().ToString(),
                CompanyName = dto.CompanyName,
                CompanyPhone = dto.CompanyPhone,
                CandidateId = dto.CandidateId,
                DomainId=dto.DomainId,
                Level = dto.Level,
                Occupation = dto.Occupation,
                HiringDate = dto.HiringDate,
                LastDate = dto.LastDate, 
                Address = dto.Address,
                CreatedDate = current,
                ModifyDate = current,
            };

            _context.Experiances.Add(edu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<ExperianceDto> { Model = edu, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<ExperianceDto> { Model = edu, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<ExperianceDto>> Delete(string id)
        {
            var edu = await _context.Experiances.FirstOrDefaultAsync(x => x.Id == id);

            if (edu == null)
                return new ResponseDto<ExperianceDto> { Status = ResponseStatus.NotFound };
            _context.Experiances.Remove(edu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<ExperianceDto> { Model = edu, Status = ResponseStatus.Error, Message = ex.Message };
            }

            return new ResponseDto<ExperianceDto> { Model = edu, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<ExperianceDto>> GetById(string id)
        {
            var edu = await _context.Experiances
               .FirstOrDefaultAsync(x => x.Id == id);
            if(edu==null)
                return new ResponseDto<ExperianceDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<ExperianceDto> {Model=edu, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<ExperianceDto>>> GetListByCandidateId(string candidateId)
        {
            var educations = await _context.Experiances
                .Where(a=>a.CandidateId==candidateId)
                .Select(a => (ExperianceDto)a).ToListAsync();
            return new ResponseDto<List<ExperianceDto>> { Model = educations, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<ExperianceDto>> Update(ExperianceDto dto)
        {
            var edu = await _context.Experiances.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (edu == null)
                return new ResponseDto<ExperianceDto> { Status = ResponseStatus.NotFound };
           
            var current = DateTime.UtcNow;

            edu.CompanyName = dto.CompanyName;
            edu.CompanyPhone = dto.CompanyPhone;
            edu.Occupation = dto.Occupation;
            edu.Level = dto.Level;
            edu.DomainId = dto.DomainId;
            edu.Address = dto.Address;
            edu.HiringDate = dto.HiringDate;
            edu.LastDate = dto.LastDate;
            edu.ModifyDate = current;
            _context.Experiances.Update(edu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<ExperianceDto> { Model = edu, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<ExperianceDto> { Model = edu, Status = ResponseStatus.Success };
        }
    }
}
