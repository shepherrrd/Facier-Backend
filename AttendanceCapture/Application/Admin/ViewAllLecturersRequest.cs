namespace AttendanceCapture.Application.Admin;

using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using global::AttendanceCapture.Models;
using global::AttendanceCapture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;


public class ViewAllLecturersRequest : IRequest<BaseResponse<IEnumerable<LecturTutor>>>
{
    public Guid SessionKey { get; set; }
}


public class ViewAllLecturersRequestHandler : IRequestHandler<ViewAllLecturersRequest, BaseResponse<IEnumerable<LecturTutor>>>
{
    private readonly UniversityContext _context;

    public ViewAllLecturersRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<IEnumerable<LecturTutor>>> Handle(ViewAllLecturersRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
            if (session is null || DateTimeOffset.UtcNow >= session.ExpiresAt)
            {
                return new BaseResponse<IEnumerable<LecturTutor>>(false, "The Session Tied to this operation does not exist");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == Infrastructure.UserType.Admin, cancellationToken);
            if (user is null)
                return new BaseResponse<IEnumerable<LecturTutor>>(false, "The User tied to this session was not found");
            var classes = await _context.Lecturers.AsNoTracking().ToListAsync(cancellationToken);
            return new BaseResponse<IEnumerable<LecturTutor>>(true, "Classes Fetched", classes);
        }
        catch (Exception)
        {

            throw;
        }
    }
}

