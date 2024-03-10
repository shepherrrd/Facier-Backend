using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Lecturer;

public class ViewAttendanceByDateRequest : IRequest<BaseResponse<IEnumerable<Attendance>>>
{
    public Guid SessionKey { get; set; } = default!;
    public DateTime StartDate {  get; set; }
    public long CourseID { get; set; }
}

public class ViewAttendanceByDateValidator : AbstractValidator<ViewAttendanceByDateRequest>
{
    public ViewAttendanceByDateValidator()
    {
        RuleFor(x => x.SessionKey).NotNull().NotEmpty();
        RuleFor(x => x.StartDate).NotNull().NotEmpty();

    }
}

public class ViewAttendanceByDateRequestHandler : IRequestHandler<ViewAttendanceByDateRequest, BaseResponse<IEnumerable<Attendance>>>
{
    private readonly UniversityContext _context;

    public ViewAttendanceByDateRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<IEnumerable<Attendance>>> Handle(ViewAttendanceByDateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
            if (session is null || DateTimeOffset.UtcNow >= session.ExpiresAt)
            {
                return new BaseResponse<IEnumerable<Attendance>>(false, "The Session Tied to this operation does not exist");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == Infrastructure.UserType.Lecturer, cancellationToken);
            if (user is null)
                return new BaseResponse<IEnumerable<Attendance>>(false, "The User tied to this session was not found");
            
            DateTimeOffset startOfToday = request.StartDate.Date; 
            DateTimeOffset endOfToday = startOfToday.AddDays(1).AddTicks(-1);
            var attendaces = await _context.Attendances.AsNoTracking().Where(x => x.TimeCreated >=startOfToday && x.TimeCreated <= endOfToday
            && x.Course == request.CourseID
            && x.LecturerID == user.Id
            ).ToListAsync(cancellationToken);
            return new BaseResponse<IEnumerable<Attendance>>(true,"Attendances Fetched",attendaces);
        }
        catch (Exception)
        {

            throw;
        }
    }
}




