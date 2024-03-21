namespace api.Models
{
    public class SignUp
    {
        public int? id { get; set; } = null;
        public string? name { get; set; } = null;
        public string? email { get; set; } = null;
        public string? password { get; set; } = null;
        public string? passwordRepeat { get; set; } = null;
        public string? phone { get; set; } = null;
        public string? image {get; set;} = null;
    }

    public class SaveOTP
    {
        public int? id {get; set;} = null; 
        public string? phone {get; set;} = null;
        public string? otp {get; set;} = null;
    }
}