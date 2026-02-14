namespace Store.Service.Services.S3;

public interface IStorageService
{
    Task UploadFileAsync(string bucket, string key, Stream fileStream);
    Task<Stream> DownloadFileAsync(string bucket, string key);
    Task DeleteFileAsync(string bucket, string key);
    Task<bool> FileExistsAsync(string bucket, string key);
    Task<string> UploadProductImageAsync(int productId, Stream imageStream, string fileName);
    Task<string> UploadCategoryBannerAsync(int categoryId, Stream imageStream, string fileName);
    Task<string> UploadUserProfileImageAsync(int userId, Stream imageStream, string fileName);
    Task<string> GetProductImageUrlAsync(int productId, string fileName);
    Task<string> GetCategoryBannerUrlAsync(int categoryId, string fileName);
    Task<string> GetUserProfileImageUrlAsync(int userId, string fileName);
}