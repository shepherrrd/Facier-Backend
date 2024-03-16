using AttendanceCapture.Models;

namespace AttendanceCapture.Services.Interfaces;

public interface IAttendanceService
{
    BaseResponse VerifyImageMatch(string image1, string image2);
}
