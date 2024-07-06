using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.ServiceRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceRepoitory _serviceRepoitory;

        public ServicesController(IServiceRepoitory serviceRepoitory)
        {
            _serviceRepoitory = serviceRepoitory;
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceList()
        {
            var values = await _serviceRepoitory.GetAllServiceAsync();
            return Ok(values);
        }
    }
}
