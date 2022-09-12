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
    public class PaymentsController : ControllerBase
    {
        public IPaymentRepository _repository { get; }

        public PaymentsController(IPaymentRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _repository.GetDtoById(id));
        }
        
        [HttpGet("listByAdminId/{id}")]
        public async Task<IActionResult> ListByAdminId(string id)
        {
            return Ok(await _repository.GetListByAdminId(id));
        }
        [HttpGet("listBySubscriptionId/{id}")]
        public async Task<IActionResult> GetListBySubscriptionId(string id)
        {
            return Ok(await _repository.GetListBySubscriptionId(id));
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentDto model)
        {
            return Ok(await _repository.Create(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] PaymentDto model)
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
