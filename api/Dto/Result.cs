namespace api.Dto
{
    public class Result
    {
            public bool success { get; set; }
            public dynamic? result { get; set; } = null;
            public string? errorMessage { get; set; } = null;
    }
}