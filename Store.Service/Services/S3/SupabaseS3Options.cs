namespace Store.Service.Services.S3;

public class SupabaseS3Options
{
    public string AccessKey { get; set; } = "";
    public string SecretKey { get; set; } = "";
    public string Endpoint { get; set; } = "";
    public string Region { get; set; } = "";
}