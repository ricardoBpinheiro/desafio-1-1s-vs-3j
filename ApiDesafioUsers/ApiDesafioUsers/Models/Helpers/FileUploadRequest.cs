using Microsoft.AspNetCore.Mvc;

namespace ApiDesafioUsers.Models.Helpers;

public class FileUploadRequest
{
    [FromForm]
    public IFormFile? File { get; set; } = null;
}
