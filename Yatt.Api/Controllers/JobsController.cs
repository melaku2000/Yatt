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
    public class JobsController : ControllerBase
    {
        public IJobRepository _repository { get; }

        public JobsController(IJobRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _repository.GetById(id));
        }
        
        [HttpGet("listByVacancy/{vacancyId}")]
        public async Task<IActionResult> GetListByVacancyId(string vacancyId)
        {
            return Ok(await _repository.GetListByVacancyId(vacancyId));
        }
        [HttpGet("pagedList")]
        public async Task<IActionResult> GetPagedList([FromQuery] PageParameter pageParameter)
        {
            var users = await _repository.GetPagedList(pageParameter);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(users.MetaData));
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JobDto model)
        {
            return Ok(await _repository.Create(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] JobDto model)
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
