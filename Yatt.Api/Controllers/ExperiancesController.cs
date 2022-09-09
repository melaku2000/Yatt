using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Yatt.Api.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.RequestFeatures;

namespace Yatt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiancesController : ControllerBase
    {
        public IExperianceRepository _repository { get; }

        public ExperiancesController(IExperianceRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _repository.GetById(id));
        }
        
        [HttpGet("list/{candidateId}")]
        public async Task<IActionResult> GetCandidateList(string candidateId)
        {
            return Ok(await _repository.GetListByCandidateId(candidateId));
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExperianceDto userModel)
        {
            return Ok(await _repository.Create(userModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ExperianceDto userModel)
        {
            if (id != userModel.Id)
                return Conflict("The id's do not match");

            return Ok(await _repository.Update(userModel));
        }
      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}
