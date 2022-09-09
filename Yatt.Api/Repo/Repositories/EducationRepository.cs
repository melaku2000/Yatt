using Microsoft.EntityFrameworkCore;
using Yatt.Api.Data;
using Yatt.Api.Repo.Interfaces;
using Yatt.Api.Repo.Repositories.Extensions;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;
using Yatt.Models.RequestFeatures;

namespace Yatt.Api.Repo.Repositories
{
    public class EducationRepository : IEducationRepository
    {
        public AppDbContext _context { get; }

        public EducationRepository(AppDbContext repository)
        {
            _context = repository;
        }
        public async Task<ResponseDto<EducationDto>> Create(EducationDto dto)
        {
            var current = DateTime.UtcNow;
            var edu = new Education
            {
                AcademyName = dto.AcademyName,
                AcademyPhone = dto.AcademyPhone,
                CandidateId = dto.CandidateId,
                Level = dto.Level,
                FieldOfStudy = dto.FieldOfStudy,
                Grade = dto.Grade,
                ComplitionDate = dto.ComplitionDate,
                Address = dto.Address,
                CreatedDate = current,
                ModifyDate = current,
            };

            _context.Educations.Add(edu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<EducationDto> { Model = edu, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<EducationDto> { Model = edu, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<EducationDto>> Delete(string id)
        {
            var edu = await _context.Educations.FirstOrDefaultAsync(x => x.Id == id);

            if (edu == null)
                return new ResponseDto<EducationDto> { Status = ResponseStatus.NotFound };
            _context.Educations.Remove(edu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<EducationDto> { Model = edu, Status = ResponseStatus.Error, Message = ex.Message };
            }

            return new ResponseDto<EducationDto> { Model = edu, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<EducationDto>> GetById(string id)
        {
            var edu = await _context.Educations
               .FirstOrDefaultAsync(x => x.Id == id);
            if(edu==null)
                return new ResponseDto<EducationDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<EducationDto> {Model=edu, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<List<EducationDto>>> GetListByCandidateId(string candidateId)
        {
            var educations = await _context.Educations
                .Where(a=>a.CandidateId==candidateId)
                .Select(a => (EducationDto)a).ToListAsync();
            return new ResponseDto<List<EducationDto>> { Model = educations, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<EducationDto>> Update(EducationDto dto)
        {
            var edu = await _context.Educations.FirstOrDefaultAsync(a => a.Id == dto.Id);
            if (edu == null)
                return new ResponseDto<EducationDto> { Status = ResponseStatus.NotFound };
           
            var current = DateTime.UtcNow;

            edu.AcademyName = dto.AcademyName;
            edu.AcademyPhone = dto.AcademyPhone;
            edu.FieldOfStudy = dto.FieldOfStudy;
            edu.Level = dto.Level;
            edu.Grade = dto.Grade;
            edu.Address = dto.Address;
            edu.ComplitionDate = dto.CreatedDate;
            edu.ModifyDate = current;
            _context.Educations.Update(edu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<EducationDto> { Model = edu, Status = ResponseStatus.Error, Message = ex.Message };
            }
            return new ResponseDto<EducationDto> { Model = edu, Status = ResponseStatus.Success };
        }
    }
}
