using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Lecturer;

public class CreateCourseRequest : IRequest<BaseResponse>
{
    public Guid SessionKey { get; set; }

    public string Title { get; set; } = default!;
    public string Level { get; set; } = default!;
    public string CourseCode { get; set; } = default!;
    public long DepartmentID { get; set; }
    public long ClassID { get; set; }
    public long LecturerID { get; set; }
}


public class CreateClassRequestValidator : AbstractValidator<CreateCourseRequest>
{
    public CreateClassRequestValidator()
    {
          RuleFor(x => x.ClassID).NotNull().NotEmpty();
          RuleFor(x => x.SessionKey).NotNull().NotEmpty();
          RuleFor(x => x.Title).NotNull().NotEmpty();
          RuleFor(x => x.Level).NotNull().NotEmpty();
          RuleFor(x => x.LecturerID).NotNull().NotEmpty();
          RuleFor(x => x.DepartmentID).NotNull().NotEmpty();
    }
}


public class CreateClassRequestHandler : IRequestHandler<CreateCourseRequest, BaseResponse>
{
    private readonly UniversityContext _context;

    public CreateClassRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse> Handle(CreateCourseRequest request, CancellationToken cancellationToken)
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
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Id == request.DepartmentID,cancellationToken);
            if (department is null)
                return new BaseResponse(false, " Department with thar id does not exist");
            var classe = new Course
            {
                ClassId = request.ClassID,
                Title = request.Title,                
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                CourseCode = request.CourseCode,
                LecturerID = user.Id,
                Level = request.Level,
                DepartmentID = request.DepartmentID
            };
            var log = new LogActivity
            {
                Description = "Created new CLass",
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                Type = Infrastructure.LogActivityType.Createclass
            };
            await _context.LogActivities.AddAsync(log);
            await _context.AddAsync(classe, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse(true, "Class Added Successfully");
        }
        catch (Exception)
        {

            throw;
        }
    }
}
