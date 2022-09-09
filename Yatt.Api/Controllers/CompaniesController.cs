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
    public class CompaniesController : ControllerBase
    {
        public ICompanyRepository _repository { get; }

        public CompaniesController(ICompanyRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _repository.GetById(id));
        }
        
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _repository.GetList());

        }
        [HttpGet("pagedList")]
        public async Task<IActionResult> GetPagedList([FromQuery] PageParameter pageParameter)
        {
            var users = await _repository.GetPagedList(pageParameter);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(users.MetaData));
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyDto model)
        {
            try
            {
                var user = await _repository.Create(model);
                if (user != null)
                    return Ok(user);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
            return BadRequest();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CompanyDto model)
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
