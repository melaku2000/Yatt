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
    public class VacancyiesController : ControllerBase
    {
        public IVacancyRepository _repository { get; }

        public VacancyiesController(IVacancyRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _repository.GetDtoById(id));
        }
        
        [HttpGet("listBySubscription/{subscriptionId}")]
        public async Task<IActionResult> ListBySubscriptionId(string subscriptionId)
        {
            return Ok(await _repository.GetListBySubscriptionId(subscriptionId));
        }
        
        [HttpGet("listByCompany/{companyId}")]
        public async Task<IActionResult> ListByCompanyId(string companyId)
        {
            return Ok(await _repository.GetListByCompanyId(companyId));
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VacancyDto userModel)
        {
            return Ok(await _repository.Create(userModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] VacancyDto userModel)
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
