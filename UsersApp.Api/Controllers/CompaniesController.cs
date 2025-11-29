using Microsoft.AspNetCore.Mvc;
using UsersApp.Application.DTOs.Companies;
using UsersApp.Application.UseCases.ICompanies;
using UsersApp.Domain.Common.Model;
using UsersApp.Domain.Common.Validation;

namespace UsersApp.API.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICreateCompany _createCompany;
        private readonly IGetAllCompanies _getAllCompanies;
        private readonly IGetCompanyID _getCompanyID;
        private readonly IUpdateCompany _updateCompany;
        private readonly IDeleteCompany _deleteCompany;

        public CompaniesController(
            ICreateCompany createCompany,
            IGetAllCompanies getAllCompanies,
            IGetCompanyID getCompanyID,
            IUpdateCompany updateCompany,
            IDeleteCompany deleteCompany)
        {
            _createCompany = createCompany;
            _getAllCompanies = getAllCompanies;
            _getCompanyID = getCompanyID;
            _updateCompany = updateCompany;
            _deleteCompany = deleteCompany;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<CompanyResponse>>>> GetAll([FromQuery] string username, [FromQuery] string password)
        {
            var result = await _getAllCompanies.ExecuteAsync(username, password);
            if (result.ValidationResult.HasErrors)
                return BadRequest(result.ValidationResult.Items);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<CompanyResponse>>> GetById(int id, [FromQuery] string username, [FromQuery] string password)
        {
            var result = await _getCompanyID.ExecuteAsync(id, username, password);
            if (result.ValidationResult.HasErrors)
                return BadRequest(result.ValidationResult.Items);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Result<CompanyResponse>>> Create([FromBody] CreateCompanyRequest request)
        {
            var result = await _createCompany.ExecuteAsync(request);
            if (result.ValidationResult.HasErrors)
                return BadRequest(result.ValidationResult.Items);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Result<CompanyResponse>>> Update(int id, [FromBody] UpdateCompanyRequest request)
        {
            var result = await _updateCompany.ExecuteAsync(request, id);
            if (result.ValidationResult.HasErrors)
                return BadRequest(result.ValidationResult.Items);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, [FromQuery] string username, [FromQuery] string password)
        {
            var result = await _deleteCompany.ExecuteAsync(id, username, password);
            if (result.ValidationResult.HasErrors)
                return BadRequest(result.ValidationResult.Items);
            return NoContent();
        }
    }
}

