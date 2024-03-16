using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Lecturer;

public class ViewAllCoursesRequest : IRequest<BaseResponse<IEnumerable<Course>>>
{
    public Guid SessionKey { get; set; }

}

public class ViewAllCLassesRequestHandler : IRequestHandler<ViewAllCoursesRequest, BaseResponse<IEnumerable<Course>>>
{
    private readonly UniversityContext _context;

    public ViewAllCLassesRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<IEnumerable<Course>>> Handle(ViewAllCoursesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
            if (session is null || DateTimeOffset.UtcNow >= session.ExpiresAt)
            {
                return new BaseResponse<IEnumerable<Course>>(false, "The Session Tied to this operation does not exist");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == Infrastructure.UserType.Lecturer, cancellationToken);
            if (user is null)
                return new BaseResponse<IEnumerable<Course>>(false, "The User tied to this session was not found");
            var classes = await _context.Courses.AsNoTracking().Where(x => x.LecturerID == user.Id).ToListAsync(cancellationToken);
            return new BaseResponse<IEnumerable<Course>>(true, "Classes Fetched", classes);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
