using Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands;

public class UploadFileToS3Command : IRequest<Unit>
{
    public IFormFile File { get; set; }
    public string BucketName { get; set; }
}

public class UploadFileToS3CommandCommandHandler(IS3Service s3Service) : IRequestHandler<UploadFileToS3Command, Unit>
{
    public async Task<Unit> Handle(UploadFileToS3Command request, CancellationToken cancellationToken)
    {
        return await s3Service.UploadFileAsync(request.BucketName, request.File.FileName, request.File.OpenReadStream(), cancellationToken);
    }
}
