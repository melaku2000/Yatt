using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yatt.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.RequestFeatures;

namespace Yatt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobQualificationsController : ControllerBase
    {
        public IJobQualificationRepository _repository { get; }

        public JobQualificationsController(IJobQualificationRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _repository.GetById(id));
        }
        
        [HttpGet("listByJobId/{jobId}")]
        public async Task<IActionResult> ListByJobId(string jobId)
        {
            return Ok(await _repository.GetListByJobId(jobId));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JobQualificationDto model)
        {
            return Ok(await _repository.Create(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] JobQualificationDto model)
        {
            if (id != model.Id)
                return Conflict("The id's do not match");

            return Ok(await _repository.Update(model));
        }
      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}
