namespace api.Models
{
    public class signUpModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? passwordRepeat { get; set; }  
        public string? phone { get; set; }  
        public string? image {get; set;}
    }
}