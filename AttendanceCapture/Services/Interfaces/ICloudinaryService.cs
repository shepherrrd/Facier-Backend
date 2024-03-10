using AttendanceCapture.Models;

namespace AttendanceCapture.Services.Interfaces;

public interface ICloudinaryService
{
    Task<BaseResponse> UploadImageAsync(MemoryStream stream, string fileName);
    Task<BaseResponse> DownloadImageAsync(string imageUrl);
}
