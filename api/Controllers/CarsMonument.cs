using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CarsMonument : ControllerBase
    {
        private readonly IMonumentRepository _IMonumentRepository;

        public CarsMonument(IMonumentRepository IMonumentRepository)
        {
            _IMonumentRepository = IMonumentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateData (Cars data)
        {
            var res = await _IMonumentRepository.createData(data);
            return (res.success) ? Ok(res) : BadRequest(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateData (Cars data)
        {
            var res = await _IMonumentRepository.updateData(data);
            return (res.success) ? Ok(res) : BadRequest(res);
        }

        
    }
}