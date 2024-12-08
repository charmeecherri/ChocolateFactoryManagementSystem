using ChocolateFactoryApi.DTO.request;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateFactoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualityController : ControllerBase
    {
        private readonly IQualityRepository _qualityRepository;
        private readonly IProductionScheduleRepository _productSchedulingRepository;

        public QualityController(IQualityRepository qualityRepository,IProductionScheduleRepository productionScheduleRepository) {
            _qualityRepository = qualityRepository;
            _productSchedulingRepository = productionScheduleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getQualityI()
        {
            return Ok(await _qualityRepository.GetQualitiesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> createQuality(QualityRequestDto qualityRequestDto)
        {
            using (var transaction = await _qualityRepository.getAppDbContext().Database.BeginTransactionAsync())
            {
                try
                {
                    Quality quality = new Quality()
                    {
                        BatchId = qualityRequestDto.BatchId,
                        Inspectorid = qualityRequestDto.InspectorId,
                        InspectionDate = qualityRequestDto.InspectionDate,
                        TestResults = qualityRequestDto.TestResults,
                    };

                    if (qualityRequestDto.TestResults == "passed")
                        quality.status = "approved";
                    else
                        quality.status = "rejected";
                    ProductionSchedule productionSchedule = await _productSchedulingRepository.getProductScheduleByIdAsync(qualityRequestDto.BatchId);

                    if (productionSchedule.Status != "completed")
                        return BadRequest("Product cannot be moved to Quality Control until production is completed.");

                    await _qualityRepository.createQualityAsync(quality);
                    await transaction.CommitAsync();
                    return StatusCode(StatusCodes.Status201Created, "Quality checking is created");
                }
                catch(Exception e)
                {
                    await transaction.RollbackAsync();
                    if (e.InnerException != null)
                        return BadRequest(e.InnerException.Message);
                    else
                        return BadRequest(e.Message);
                }
            }

        }

        [HttpPut]
        public async Task<IActionResult> updateQualityStatus(int id, string testResult)
        {
            Quality quality = await _qualityRepository.getQualityByIdAsync(id);
            if (quality == null)
                return BadRequest("Cannot find the quality with the id specified");

            quality.TestResults = testResult;
            if (testResult == "passed")
                quality.status = "approved";
            else
                quality.status = "rejected";
            await _qualityRepository.updateQualityAsync(quality);
            return Ok("Updated the quality status");
        }

        [HttpDelete]
        public async Task<IActionResult> deleteQuality(int id)
        {
            Quality quality = await _qualityRepository.getQualityByIdAsync(id);
            await _qualityRepository.deleteQualityAsync(quality);
            return Ok("deleted successfully");

        }
    }
}
