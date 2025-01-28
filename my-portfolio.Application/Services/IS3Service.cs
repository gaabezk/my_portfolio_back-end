namespace Application.Services;

public interface IS3Service
{
    public Task<string> UploadFileAsync(string bucketName, string fileName, Stream fileStream, CancellationToken cancellationToken);
}