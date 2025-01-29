using Amazon.S3;
using Amazon.S3.Model;
using Application.Interfaces.Services;
using MediatR;

namespace Infra.Services;

public class S3Service(IAmazonS3 s3Client) : IS3Service
{
    public async Task<Unit> UploadFileAsync(string bucketName, string fileName, Stream fileStream,
        CancellationToken cancellationToken)
    {
        try
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = fileName,
                InputStream = fileStream
            };

            await s3Client.PutObjectAsync(putRequest, cancellationToken);
            return Unit.Task.Result;

        }
        catch (AmazonS3Exception s3Ex)
        {
            throw new AmazonS3Exception($"AWS S3 error: {s3Ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"General error: {ex.Message}");
        }
    }
}
