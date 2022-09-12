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
    public class SubscriptionsController : ControllerBase
    {
        public ISubscriptionRepository _repository { get; }

        public SubscriptionsController(ISubscriptionRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _repository.GetDtoById(id));
        }
        
        [HttpGet("listByMemberId/{id}")]
        public async Task<IActionResult> GetListByMemberId(string id)
        {
            return Ok(await _repository.GetListByMembershipId(id));
        }
        [HttpGet("listByCompanyId/{id}")]
        public async Task<IActionResult> GetListByCompanyId(string id)
        {
            return Ok(await _repository.GetListByCompanyId(id));
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubscriptionDto userModel)
        {
            return Ok(await _repository.Create(userModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SubscriptionDto userModel)
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
