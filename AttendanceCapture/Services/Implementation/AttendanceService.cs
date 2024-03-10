using AttendanceCapture.Models;
using AttendanceCapture.Services.Interfaces;
using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace AttendanceCapture.Services.Implementation;

public class AttendanceService : IAttendanceService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AttendanceService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public BaseResponse VerifyImageMatch(Mat image1, Mat image2)
    {
        ORB orb = new ORB();
        BFMatcher matcher = new BFMatcher(DistanceType.Hamming);

        VectorOfKeyPoint keypoints1 = new VectorOfKeyPoint();
        VectorOfKeyPoint keypoints2 = new VectorOfKeyPoint();
        UMat descriptors1 = new UMat();
        UMat descriptors2 = new UMat();
        orb.DetectAndCompute(image1, null, keypoints1, descriptors1, false);
        orb.DetectAndCompute(image2, null, keypoints2, descriptors2, false);
        VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch();
        matcher.KnnMatch(descriptors1, descriptors2, matches, 2);

        double uniqueThreshold = 65; 
        List<MDMatch[]> goodMatches = new List<MDMatch[]>();
        for (int i = 0; i < matches.Size; i++)
        {
            MDMatch[] matchPair = matches[i].ToArray();
            if (matchPair.Length == 2 && matchPair[0].Distance < 0.75 * matchPair[1].Distance)
            {
                goodMatches.Add(matchPair);
            }
        }
        // Define a threshold for the number of good matches
        bool areImagesSimilar = goodMatches.Count > uniqueThreshold;


        return new BaseResponse(areImagesSimilar, "Faces Detected");

    }
}
