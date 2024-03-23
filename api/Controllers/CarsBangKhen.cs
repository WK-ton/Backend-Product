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
    public class CarsBangKhen : ControllerBase
    {
        private readonly IBangkhenRepository _ICarsBangkhenRepository;

        public CarsBangKhen(IBangkhenRepository IBangkhenRepository)
        {
            _ICarsBangkhenRepository = IBangkhenRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateData(Cars data)
        {
            var res = await _ICarsBangkhenRepository.createData(data);
            return (res.success) ? Ok(res) : BadRequest(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(Cars data)
        {
            var res = await _ICarsBangkhenRepository.updateData(data);
            return (res.success) ? Ok(res) : BadRequest(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteData(Cars data)
        {
            var res = await _ICarsBangkhenRepository.deleteData(data);
            return (res.success) ? Ok(res) : BadRequest(res);
        }
    }
}