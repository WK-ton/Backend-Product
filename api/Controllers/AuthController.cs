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
        private readonly IOtpRepository _IOtpRepository;
        public AuthController(IAuthRepository IAuthRepository, IOtpRepository IOtpRepository)
        {
            _IAuthRepository = IAuthRepository;
            _IOtpRepository = IOtpRepository;
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

        [HttpPost]
        public async Task<IActionResult> loginPhone (LoginPhone data)
        {
            var result = await _IAuthRepository.loginPhone(data);
            return (result.success) ? Ok(result) : BadRequest(result);
        }
        [HttpPut]
        public async Task<IActionResult> updateProfile(Register data)
        {
            var result = await _IAuthRepository.updateProfile(data);
            return (result.success) ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> sendOTP (SendOTP data, string? action)
        {
            var result = await _IOtpRepository.sendOTP(data, action);
            return (result.success) ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> activeOTP (ActiveOTP data)
        {
            var result = await _IOtpRepository.activeOTP(data);
            return (result.success) ? Ok(result) : BadRequest(result);
        }
    }
}
