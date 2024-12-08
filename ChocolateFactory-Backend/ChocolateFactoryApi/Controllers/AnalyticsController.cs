using ChocolateFactoryApi.DTO.request;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateFactoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsRepository _analyticsRepository;

        public AnalyticsController(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getAnalytics()
        {
            return Ok(await _analyticsRepository.GetAllAnalyticsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> createAnalytics(AnalyticsDto analyticsDto)
        {
            Analytics analytics = new Analytics()
            {
                Type = analyticsDto.Type,
                GeneratedDate = DateTime.Now,
                Data = analyticsDto.Data,
                CreatedBy = analyticsDto.CreatedBy,
            };

            await _analyticsRepository.createAnalystics(analytics);
            return StatusCode(StatusCodes.Status201Created, "Analytics is created");
        }
    }
}
