using AttendanceCapture.Models;
using Emgu.CV;

namespace AttendanceCapture.Services.Interfaces;

public interface IAttendanceService
{
    BaseResponse VerifyImageMatch(Mat image1, Mat image2);
}
