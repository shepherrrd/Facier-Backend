using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using AttendanceCapture.Services.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Admin;

public class RegisterStudentRequest : IRequest<BaseResponse>
{
    public Guid SessionKey { get; set; }
    public string Name { get; set; } 
    public int? ClassId { get; set; }
    public string MatricNumber { get; set; } = string.Empty;

    public IFormFile Passport { get; set; } = default!;
}

public class RegisterStudentRequestValidator : AbstractValidator<RegisterStudentRequest>
{
    public RegisterStudentRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.MatricNumber).NotNull().NotEmpty();
        RuleFor(x => x.Passport).NotNull().NotEmpty();
        RuleFor(x => x.Passport).Must(BeValidFileType).WithMessage(" Passport Files must be either JPEG or pdf.");
    }

    private bool BeValidFileType(IFormFile file)
    {
        if (file == null) return true; // Assuming files are optional. If mandatory, return false.

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(fileExtension);
    }
}

public class RegisterStudentRequestHandler : IRequestHandler<RegisterStudentRequest, BaseResponse>
{
    private readonly UniversityContext _context;
    private readonly ICloudinaryService _cloudinary;

    public RegisterStudentRequestHandler(UniversityContext context,
        ICloudinaryService cloudinary)
    {
        _context = context;
        _cloudinary = cloudinary;
    }

    public async Task<BaseResponse> Handle(RegisterStudentRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
            if (session is null || DateTimeOffset.UtcNow >= session.ExpiresAt)
            {
                return new BaseResponse(false, "The Session Tied to this operation does not exist");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == Infrastructure.UserType.Admin, cancellationToken);
            if (user is null)
                return new BaseResponse(false, "The User tied to this session was not found");

            var classe = await _context.Classes.FirstOrDefaultAsync(x => x.Id == request.ClassId,cancellationToken);
            if (classe is null)
                return new BaseResponse(false, "This Class DOes Not exist");

            var student = new Student
            {
                Name = request.Name,
                TimeCreated = DateTimeOffset.UtcNow,
                ClassId = request.ClassId,
                MatricNumber = request.MatricNumber,
                TimeUpdated = DateTimeOffset.UtcNow

            };
            var stream = new MemoryStream();
            using (var pic = request.Passport!.OpenReadStream())
            {
                pic?.CopyToAsync(stream);
            }
            stream.Position = 0;
            var cloud = await _cloudinary.UploadImageAsync(stream,$"{request.MatricNumber}");
            if (!cloud.Status)
                return new BaseResponse(false, "An Error occured while uploading the photo");
            student.Photo = cloud.Message;
            var log = new LogActivity
            {
                Description = "Created new Student",
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                Type = Infrastructure.LogActivityType.CreateStudent
            };
            await _context.LogActivities.AddAsync(log);
            await _context.AddAsync(student, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse(true, "Student Created");
        }
        catch (Exception)
        {
            return new BaseResponse(false, " An Error Occured While Trying To Register Student");
        }
    }
}
