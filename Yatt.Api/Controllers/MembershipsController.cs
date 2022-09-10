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
    public class MembershipsController : ControllerBase
    {
        public IMembershipRepository _repository { get; }

        public MembershipsController(IMembershipRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _repository.GetDtoById(id));
        }
        
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _repository.GetList());
        }
       
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MembershipDto userModel)
        {
            return Ok(await _repository.Create(userModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] MembershipDto userModel)
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
