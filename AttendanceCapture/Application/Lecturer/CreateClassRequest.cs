using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Lecturer;

public class CreateClassRequest : IRequest<BaseResponse>
{
    public Guid SessionKey { get; set; }

    public string Name { get; set; } = default!;

    public string ClassNumber { get; set; } = default(string)!;
}


public class CreateClassRequestValidator : AbstractValidator<CreateClassRequest>
{
    public CreateClassRequestValidator()
    {
          RuleFor(x => x.Name).NotNull().NotEmpty();
          RuleFor(x => x.SessionKey).NotNull().NotEmpty();
          RuleFor(x => x.ClassNumber).NotNull().NotEmpty();
    }
}


public class CreateClassRequestHandler : IRequestHandler<CreateClassRequest, BaseResponse>
{
    private readonly UniversityContext _context;

    public CreateClassRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse> Handle(CreateClassRequest request, CancellationToken cancellationToken)
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

            var classe = new Class
            {
                Name = request.Name,
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                StudentIds = ";",
                ClassNumber = request.ClassNumber,
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
