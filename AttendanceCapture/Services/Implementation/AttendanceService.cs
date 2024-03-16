using AttendanceCapture.Models;
using AttendanceCapture.Services.Interfaces;
using OpenCvSharp;

namespace AttendanceCapture.Services.Implementation;

public class AttendanceService : IAttendanceService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AttendanceService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public BaseResponse VerifyImageMatch(string imagePath1, string imagePath2)
    {
        var areImagesSimilar = false;

        using (var src1 = new Mat(imagePath1, ImreadModes.Grayscale))
        using (var src2 = new Mat(imagePath2, ImreadModes.Grayscale))
        {
            var orb = ORB.Create();
            var matcher = new BFMatcher(NormTypes.Hamming, true);

            // Detect and compute keypoints and descriptors
            KeyPoint[] keypoints1, keypoints2;
            Mat descriptors1 = new Mat();
            Mat descriptors2 = new Mat();
            orb.DetectAndCompute(src1, null, out keypoints1, descriptors1);
            orb.DetectAndCompute(src2, null, out keypoints2, descriptors2);

            // Match descriptors
            var matches = matcher.Match(descriptors1, descriptors2);

            // Filter matches by distance
            var goodMatches = matches.Where(m => m.Distance < 30).ToArray(); // Threshold, adjust as needed
            areImagesSimilar = goodMatches.Length > 10; // Threshold, adjust as needed
        }

        return new BaseResponse(areImagesSimilar, "Faces Detected");
    }
}
