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
        private readonly ICarsBangkhenRepository _ICarsBangkhenRepository;

        public CarsBangKhen(ICarsBangkhenRepository ICarsBangkhenRepository)
        {
            _ICarsBangkhenRepository = ICarsBangkhenRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateData(BangKhen data)
        {
            var res = await _ICarsBangkhenRepository.createData(data);
            return (res.success) ? Ok(res) : BadRequest(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData(BangKhen data)
        {
            var res = await _ICarsBangkhenRepository.updateData(data);
            return (res.success) ? Ok(res) : BadRequest(res);
        }
    }
}