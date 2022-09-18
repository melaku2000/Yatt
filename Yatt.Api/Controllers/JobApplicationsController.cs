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
    public class JobApplicationsController : ControllerBase
    {
        public IJobApplicationRepository _repository { get; }

        public JobApplicationsController(IJobApplicationRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _repository.GetDtoById(id));
        }
        
        [HttpGet("listByCandidateId/{id}")]
        public async Task<IActionResult> GetListByCandidateId(string id)
        {
            return Ok(await _repository.GetListByCandidateId(id));
        }
        [HttpGet("listByJobId/{id}")]
        public async Task<IActionResult> GetListByJobId(string id)
        {
            return Ok(await _repository.GetListByJobId(id));
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JobApplicationDto model)
        {
            return Ok(await _repository.Create(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] JobApplicationDto model)
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
