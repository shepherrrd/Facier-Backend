using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using AttendanceCapture.Services.Interfaces;
using Emgu.CV;
using Emgu.CV.CvEnum;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;

namespace AttendanceCapture.Application.Lecturer;

public class CaptureAttendanceRequest : IRequest<BaseResponse>
{
    public Guid SessionKey { get; set; } = default!;
    public IFormFile AttendancePicture { get; set; } = default!;

    public string MatricNo { get; set; } = default!;

    public string Remarks { get; set; } = default!;

    public long CourseID { get; set; } = default!;
}


public class CaptureAttendanceRequestValidator : AbstractValidator<CaptureAttendanceRequest>
{
    public CaptureAttendanceRequestValidator()
    {
         RuleFor(x => x.AttendancePicture).NotNull().NotEmpty();
         RuleFor(x => x.SessionKey).NotNull().NotEmpty();
         RuleFor(x => x.MatricNo).NotNull().NotEmpty();
    }
}

public class CaptureAttendanceRequestHandler : IRequestHandler<CaptureAttendanceRequest, BaseResponse>
{
    private readonly UniversityContext _context;
    private readonly ICloudinaryService _cloudinary;
    private readonly IAttendanceService _attendance;


    public CaptureAttendanceRequestHandler(ICloudinaryService cloudinary, UniversityContext context, IAttendanceService attendance)
    {
        _cloudinary = cloudinary;
        _context = context;
        _attendance = attendance;
    }

    public async Task<BaseResponse> Handle(CaptureAttendanceRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
            if (session is null || DateTimeOffset.UtcNow >= session.ExpiresAt)
            {
                return new BaseResponse(false, "The Session Tied to this operation does not exist");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == Infrastructure.UserType.Lecturer, cancellationToken);
            if (user is null)
                return new BaseResponse(false, "The User tied to this session was not found");
            var student = await _context.Students.FirstOrDefaultAsync(x => x.MatricNumber == request.MatricNo, cancellationToken);
            if (student is null)
                return new BaseResponse(false, "No student is tied to this matric number");
            var course = await _context.Courses.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == request.CourseID, cancellationToken);
            if (course is null)
                return new BaseResponse(false, "Course not found");
            var base64String = await _cloudinary.DownloadImageAsync(student.Photo!);
            byte[] imageBytes = Convert.FromBase64String(base64String.Message!);
            string tempFilePath = Path.GetTempFileName(); // Create a temporary file
            File.WriteAllBytes(tempFilePath, imageBytes);
            Mat studentimage = CvInvoke.Imread(tempFilePath, ImreadModes.Color);
           
            string tempFilePath2 = Path.GetTempFileName(); // Create a temporary file

            FileStream stream = new FileStream(tempFilePath2, FileMode.Create);
            await request.AttendancePicture.CopyToAsync(stream);
            stream.Position = 0;
            stream.Dispose();

            Mat attendanceimage = CvInvoke.Imread(tempFilePath2, ImreadModes.Color);

            var att = _attendance.VerifyImageMatch(studentimage, attendanceimage);
            if (!att.Status) return new BaseResponse(false, "Faces DO Not Match");

            var attendance = new Attendance
            {
                Remarks = request.Remarks,
                Student = student.Id,
                Course = request.CourseID,
                LecturerID = user.Id,
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
            };
            await _context.AddAsync(attendance,cancellationToken);
            student.Attendances += $"{attendance.Id};";
            student.TimeUpdated = DateTimeOffset.UtcNow;
            var log = new LogActivity
            {
                Description = $"Student wth matic number : {student.MatricNumber} Captured Attendance",
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                Type = Infrastructure.LogActivityType.CaptureAttendance
            };
            await _context.LogActivities.AddAsync(log);
            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse(true, "Attendace Captured Successfully");
        }
        catch (Exception)
        {

            throw;
        }
    }
}
