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
        public async Task<IActionResult> singUpPhone (SendOTP data)
        {
            var result = await _IAuthRepository.singUpPhone(data);
            return (result.success) ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> activeOTP (ActiveOTP data)
        {
            var result = await _IAuthRepository.activeOTP(data);
            return (result.success) ? Ok(result) : BadRequest(result);
        }
    }
}
