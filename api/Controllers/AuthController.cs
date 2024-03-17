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

        // [HttpGet]
        // public async Task<IActionResult> GetAllProducts()
        // {
        //     var result = await _IAuthRepository.GetAllProducts();
        //     return Ok(result);
        // }
        [HttpPost]
        public async Task<IActionResult> signUp(Register data)
        {
            var result = await _IAuthRepository.signUp(data);
            return (result.success) ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> login(Login data)
        {
            var result = await _IAuthRepository.login(data);
            return (result.success) ? Ok(result) : BadRequest(result);
        }
        [HttpPut]
        public async Task<IActionResult> updateProfile(Register data)
        {
            var result = await _IAuthRepository.updateProfile(data);
            return (result.success) ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> sendOTP (authPhone data)
        {
            var result = await _IAuthRepository.sendOTP(data);
            return (result.success) ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> activeOTP (otpPhone data)
        {
            var result = await _IAuthRepository.activeOTP(data);
            return (result.success) ? Ok(result) : BadRequest(result);
        }
    }
}
