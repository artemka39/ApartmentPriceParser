using ApartmentPriceParser.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentPriceParser.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly ILogger<ApartmentController> _logger;
        private readonly ApartmentService apartmentService;

        public ApartmentController(ILogger<ApartmentController> logger, ApartmentService apartmentService)
        {
            _logger = logger;
            this.apartmentService = apartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetApartmentDefinitions()
        {
            var result = await apartmentService.GetApartmentDefinitions();
            return new JsonResult(result);
        }
    }
}