using ChocolateFactoryApi.DTO.request;
using ChocolateFactoryApi.Models;
using ChocolateFactoryApi.repositories.interfaces;
using ChocolateFactoryApi.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateFactoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly CommonService _commonService;

        public WarehouseController(IWarehouseRepository warehouseRepository,CommonService commonService) { 
            _warehouseRepository = warehouseRepository;
            _commonService = commonService;

        }

        [HttpGet]
        public async Task<IActionResult> getWarehouses()
        {
            return Ok(await _warehouseRepository.getAllWarehouses());
        }

        [HttpPost]
        public async Task<IActionResult> createWarehouse(WarehouseDto warehouseDto)
        {
            Warehouse warehouse = new Warehouse()
            {
                Location = warehouseDto.Location,
                Capacity = warehouseDto.Capacity,
                ManagerId = warehouseDto.ManagerId,
            };
            warehouse.CurrentStockCapacity = await _commonService.getMaterialStockQuantity() + " kilograms";
            await _warehouseRepository.createWarehouseAsync(warehouse);
            return StatusCode(StatusCodes.Status201Created,"Created the warehouse");
        } 

        [HttpPut]
        public async Task<IActionResult> updateWarehouse(int id,WarehouseDto warehouseDto)
        {
            Warehouse warehouse = await _warehouseRepository.getWarehouseByIdAsync(id);
                warehouse.Location = warehouseDto.Location;
                warehouse.Capacity = warehouseDto.Capacity;
                warehouse.ManagerId = warehouseDto.ManagerId;
            warehouse.CurrentStockCapacity = _commonService.getMaterialStockQuantity() + " kilograms";
            await _warehouseRepository.updateWarehouseAsync(warehouse);
            return Ok("warehouse updated successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> deleteWarehouse(int id)
        {
            Warehouse warehouse = await _warehouseRepository.getWarehouseByIdAsync(id);
            await _warehouseRepository.deleteWarehouseAsync(warehouse);
            return Ok("warehouse is deleted");
        }
    }
}
