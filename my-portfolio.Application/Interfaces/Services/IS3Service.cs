using MediatR;

namespace Application.Interfaces.Services;

public interface IS3Service
{
    public Task<Unit> UploadFileAsync(string bucketName, string fileName, Stream fileStream, CancellationToken cancellationToken);
}