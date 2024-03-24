using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CarsMorchit : ControllerBase
    {
        private readonly IMorchitRepository _IMorchitRepository;


        public CarsMorchit(IMorchitRepository IMorchitRepository)
        {
            _IMorchitRepository = IMorchitRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateData(Cars data)
        {
            var res = await _IMorchitRepository.createData(data);
            return (res.success) ? Ok(res) : BadRequest(res);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateData(Cars data)
        {
            var res = await _IMorchitRepository.updateData(data);
            return (res.success) ? Ok(res) : BadRequest(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(Cars data, string? action = "Morchit")
        {
            var res = await _IMorchitRepository.deleteData(data, action);
            return (res.success) ? Ok(res) : BadRequest(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetMainData(string? action = "Morchit")
        {
            var res = await _IMorchitRepository.getMainCars(action);
            return (res.success) ? Ok(res) : BadRequest(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetMainDataByID(int? id, string? action = "Morchit")
        {
            var res = await _IMorchitRepository.getMainByID(id, action);
            return (res.success) ? Ok(res) : BadRequest(res);
        }
    }
}