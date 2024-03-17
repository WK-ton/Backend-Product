using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto
{
    public class Register
    {
        public int? id {get; set;} = null;
        public string? name {get; set;} = null;
        public string? email {get; set;} = null;
        public string? password {get; set;} = null;
        public string? passwordRepeat {get; set;} = null;
        // public string? phone {get; set;} = null;
        public string? image {get; set;} = "image_1694531065362.jpg";

    }

    public class Login
    {
        public string? email {get; set;} = null;
        public string? password {get; set;} = null;

    }

    public class authPhone
    {
        public int? id {get; set;} = null;
        public string? phone {get; set;} = null;
    }

    public class OTP
    {
        public int? id {get; set;} = null;
        public string? phone {get; set;} = null;
        public string? otp {get; set;} = null;
    }
}