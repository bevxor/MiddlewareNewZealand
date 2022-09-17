using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiddlewareNewZealand.Api.Models.ViewModels;
using MiddlewareNewZealand.Api.Services.Interfaces;
using System.Threading.Tasks;

namespace MiddlewareNewZealand.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Company), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCompanyId([FromRoute]int id)
        {
            var company = await _companyService.GetByCompanyId(id);

            if (company == null)
                return NotFound();

            return Ok(company);
        }
    }
}
