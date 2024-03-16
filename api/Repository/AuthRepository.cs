using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using api.Models;
using Microsoft.EntityFrameworkCore;
using api.Dto;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Transactions;
using Dapper;
using System.Linq;
using BCrypt.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using AxonsMoveTMSService.Utils;


namespace api.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;

        // private readonly IConfiguration _configuration;

        public AuthRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            // _configuration = configuration;
        }

        public async Task<Result> saveData(Register data, string action)
        {
            try
            {
                string? strError = await ValidateData(data, action);
                if (strError != null)
                {
                    return new Result
                    {
                        success = false,
                        errorMessage = strError
                    };
                }
                using (TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Authentication S = new();
                    S.id = data.id!;
                    S.name = data.name;

                    if (action == "CREATE")
                    {
                        S.email = data.email;
                        S.password = BCrypt.Net.BCrypt.HashPassword(data.password, BCrypt.Net.BCrypt.GenerateSalt(10));
                        S.passwordRepeat = BCrypt.Net.BCrypt.HashPassword(data.passwordRepeat, BCrypt.Net.BCrypt.GenerateSalt(10));
                        S.phone = data.phone;
                        S.image = data.image;
                    }

                    if (action == "CREATE")
                    {
                        await _appDbContext.signUp.AddAsync(S);
                    }
                    else if (action == "UPDATE")
                    {
                        var idRecord = await _appDbContext.signUp.FirstOrDefaultAsync(x => x.id == S.id);
                        if (idRecord != null)
                        {
                            idRecord.name = S.name;

                            _appDbContext.signUp.Update(idRecord);
                        }
                        else
                        {
                            return new Result
                            {
                                success = false,
                                errorMessage = "ไม่พบข้อมูล"
                            };
                        }

                    }

                    await _appDbContext.SaveChangesAsync();
                    scope.Complete();

                    if (action == "CREATE")
                    {

                        return new Result
                        {
                            success = true,
                            result = "สมัครสมาชิกสำเร็จ"
                        };
                    }
                    else
                    {
                        return new Result
                        {
                            success = true,
                            result = "อัพเดทข้อมูลสำเร็จ"
                        };
                    }


                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error on signUp : " + ex.Message);
            }
        }

        public async Task<string?> ValidateData(Register data, string? action)
        {
            RegularExpression chk = new();

            var phoneRegex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");


            if (String.IsNullOrEmpty(data.name)) return "กรุณาใส่ชื่อให้ถูกต้อง";
            if (chk.IsMaximumLengh(data.name, 45)) return "ชื่อต้องไม่เกิน 45 ตัวอักษร";

            if (action == "CREATE")
            {
                if (String.IsNullOrEmpty(data.email) || !emailRegex.IsMatch(data.email)) return "กรุณาใส่อีเมลให้ถูกต้อง";
                if (String.IsNullOrEmpty(data.password) || string.IsNullOrEmpty(data.passwordRepeat)) return "กรุณาใส่รหัสผ่านให้ถูกต้อง";
                if (data.password.Length < 6) return "กรุณากรอกรหัสผ่านอย่างน้อย 6 ตัวอักษร";
                if (data.passwordRepeat.Length < 6) return "กรุณากรอกรหัสผ่านอย่างน้อย 6 ตัวอักษร";
                if (data.password != data.passwordRepeat) return "รหัสผ่านของคุณไม่ตรงกัน";
                if (String.IsNullOrEmpty(data.phone) || !phoneRegex.IsMatch(data.phone)) return "หมายเลขโทรศัพท์ของคุณไม่ถูกต้อง";
                if (data.phone.Length != 10) return "กรุณากรอกเบอร์โทรศัพท์ให้ครบ 10 ตำแหน่ง";

                var email = await CheckerData(data.email, null, null);
                if (email == true) return "อีเมลใช้งานแล้ว";

                var phone = await CheckerData(null, data.phone, null);
                if (phone == true) return "เบอร์โทรศัพท์ใช้งานแล้ว";
            }
            // else if (action == "UPDATE")
            // {
            //         var nameExists = await CheckerData(null, null, data.name);
            //         if (nameExists == true) return "ชื่อใช้งานแล้ว";       
            // }

            return null;
        }

        public async Task<bool> CheckerData(string email, string phone, string name)
        {
            var res = await _appDbContext.signUp.FirstOrDefaultAsync(u => u.email == email || u.phone == phone || u.name == name);
            return res != null;
        }

        public async Task<Result> login(Login data)
        {
            try
            {
                var result = await _appDbContext.signUp.FirstOrDefaultAsync(u => u.email == data.email);
                if (result == null) return new Result
                {
                    success = false,
                    errorMessage = "อีเมลไม่ถูกต้อง"
                };
                if (!BCrypt.Net.BCrypt.Verify(data.password, result.password)) return new Result
                {
                    success = false,
                    errorMessage = "รหัสผ่านไม่ถูกต้อง"
                };

                string token = CreateToken(result);

                return new Result
                {
                    success = true,
                    result = new { token, username = result.name }
                };

            }
            catch (Exception ex)
            {
                throw new Exception("Error on login : " + ex.Message);
            }
        }

        private string CreateToken(Authentication user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.name)
            };
            var key = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            var cred = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public Task<Result> updateProfile(Register data)
        {
            return saveData(data, "UPDATE");
        }

        public Task<Result> signUp(Register data)
        {
            return saveData(data, "CREATE");
        }
    }
}