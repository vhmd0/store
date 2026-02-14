using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;

namespace Store.Service.Services.S3;

public class StorageService : IStorageService
{
    private readonly SupabaseS3Options _options;
    private readonly IAmazonS3 _s3Client;

    public StorageService(IOptions<SupabaseS3Options> options)
    {
        _options = options.Value;

        var s3Config = new AmazonS3Config
        {
            ServiceURL = _options.Endpoint,
            ForcePathStyle = true,
            RegionEndpoint = RegionEndpoint.GetBySystemName(_options.Region)
        };

        _s3Client = new AmazonS3Client(_options.AccessKey, _options.SecretKey, s3Config);
    }

    public async Task UploadFileAsync(string bucket, string key, Stream fileStream)
    {
        var request = new PutObjectRequest
        {
            BucketName = bucket,
            Key = key,
            InputStream = fileStream
        };
        await _s3Client.PutObjectAsync(request);
    }

    public async Task<Stream> DownloadFileAsync(string bucket, string key)
    {
        var response = await _s3Client.GetObjectAsync(bucket, key);
        return response.ResponseStream;
    }

    public async Task DeleteFileAsync(string bucket, string key)
    {
        await _s3Client.DeleteObjectAsync(bucket, key);
    }

    public async Task<bool> FileExistsAsync(string bucket, string key)
    {
        try
        {
            var response = await _s3Client.GetObjectMetadataAsync(bucket, key);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return false;
        }
    }


    public async Task<string> UploadProductImageAsync(int productId, Stream imageStream, string fileName)
    {
        var key = $"products/{productId}/{fileName}";
        await UploadFileAsync("ecommerce-bucket", key, imageStream);
        return $"{_options.Endpoint}/ecommerce-bucket/{key}";
    }

    public async Task<string> UploadCategoryBannerAsync(int categoryId, Stream imageStream, string fileName)
    {
        var key = $"categories/{categoryId}/{fileName}";
        await UploadFileAsync(_options.BucketName, key, imageStream);
        return $"{_options.Endpoint}/{_options.BucketName}/{key}";
    }

    public async Task<string> UploadUserProfileImageAsync(int userId, Stream imageStream, string fileName)
    {
        var key = $"users/{userId}/{fileName}";
        await UploadFileAsync(_options.BucketName, key, imageStream);
        return $"{_options.Endpoint}/{_options.BucketName}/{key}";
    }

    public Task<string> GetProductImageUrlAsync(int productId, string fileName)
    {
        return Task.FromResult($"{_options.Endpoint}/{_options.BucketName}/products/{productId}/{fileName}");
    }

    public Task<string> GetCategoryBannerUrlAsync(int categoryId, string fileName)
    {
        return Task.FromResult($"{_options.Endpoint}/{_options.BucketName}/categories/{categoryId}/{fileName}");
    }

    public Task<string> GetUserProfileImageUrlAsync(int userId, string fileName)
    {
        return Task.FromResult($"{_options.Endpoint}/ecommerce-bucket/users/{userId}/{fileName}");
    }
}