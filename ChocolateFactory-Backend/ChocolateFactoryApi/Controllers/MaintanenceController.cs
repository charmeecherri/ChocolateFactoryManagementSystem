using ChocolateFactoryApi.DTO.request;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateFactoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintanenceController : ControllerBase
    {
        private readonly IMaintanenceRepostiory _maintanenceRepository;

        public MaintanenceController(IMaintanenceRepostiory maintanenceRepostiory)
        {
            _maintanenceRepository = maintanenceRepostiory;
        }

        [HttpGet]
        public async Task<IActionResult> getMaintanences()
        {
            return Ok(await _maintanenceRepository.getMaintanences());
        }

        [HttpPost]
        public async Task<IActionResult> createMaintanence(MaintanenceDto maintanenceDto)
        {
            if (maintanenceDto.MaintanenceDate <= DateTime.Now.ToUniversalTime() || maintanenceDto.NextSchedulingDate <= DateTime.Now.ToUniversalTime())
            {
                return BadRequest("date cannot be in the past");
            }
            if (maintanenceDto.NextSchedulingDate <= maintanenceDto.MaintanenceDate)
            {
                return BadRequest("Next scheduling Date cannot be greater than Maintanence date");
            }
            Maintanence maintanence = new Maintanence()
            {
                EquipmentId = maintanenceDto.EquipmentId,
                MaintanenceDate = maintanenceDto.MaintanenceDate,
                NextSchedulingDate = maintanenceDto.NextSchedulingDate,
                Technician = maintanenceDto.Technician,
                Details = maintanenceDto.Details,

            };

            await _maintanenceRepository.createMaintanence(maintanence);
            return StatusCode(StatusCodes.Status201Created, "maintanence is created");
        }
    }
}
