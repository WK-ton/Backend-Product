using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using api.Repository.Interface;
using api.Dto;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _IAuthRepository;
        public AuthController(IAuthRepository IAuthRepository)
        {
            _IAuthRepository = IAuthRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var result = await _IAuthRepository.AddProduct(product);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _IAuthRepository.GetAllProducts();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> signUp(Register data)
        {
            var result = await _IAuthRepository.signUp(data);
            return Ok(result);
        }
    }
}
