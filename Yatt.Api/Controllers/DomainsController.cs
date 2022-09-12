using Microsoft.AspNetCore.Mvc;
using Yatt.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        public IDomainRepository _repository { get; }

        public DomainsController(IDomainRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _repository.GetDtoById(id));
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _repository.GetList());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DomainDto model)
        {
            try
            {
                return Ok(await _repository.Create(model));
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] DomainDto model)
        {
            if (id != model.Id)
                return Ok(new ResponseDto<Country> { Message = "The id's do not match", Status = ResponseStatus.Error });

            return Ok(await _repository.Update(model));

        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _repository.Delete(id));

        }
    }
}
