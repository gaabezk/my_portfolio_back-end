using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Application.Services;

namespace Infra.Services;

public class S3Service(IAmazonS3 s3Client) : IS3Service
{
    public async Task<string> UploadFileAsync(string bucketName, string fileName, Stream fileStream, CancellationToken cancellationToken)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = fileName,
            InputStream = fileStream
        };

        var response = await s3Client.PutObjectAsync(putRequest, cancellationToken);
        return response.HttpStatusCode == HttpStatusCode.OK ? "Success" : "Error";
    }
}
