using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.TestimonialRepositories;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialRepository _repository;

        public TestimonialsController(ITestimonialRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> TestimonialList()
        {
            var values = await _repository.GetAllTestimonialAsync();
            return Ok(values);
        }
    }
}
