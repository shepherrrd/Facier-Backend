namespace AttendanceCapture.Application.Admin;

using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using global::AttendanceCapture.Models;
using global::AttendanceCapture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class ViewAllStudentsRequest : IRequest<BaseResponse<IEnumerable<Student>>>
{
    public Guid SessionKey { get; set; }
}


public class ViewAllStudentsRequestHandler : IRequestHandler<ViewAllStudentsRequest, BaseResponse<IEnumerable<Student>>>
{
    private readonly UniversityContext _context;

    public ViewAllStudentsRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<IEnumerable<Student>>> Handle(ViewAllStudentsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
            if (session is null || DateTimeOffset.UtcNow >= session.ExpiresAt)
            {
                return new BaseResponse<IEnumerable<Student>>(false, "The Session Tied to this operation does not exist");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == Infrastructure.UserType.Admin, cancellationToken);
            if (user is null)
                return new BaseResponse<IEnumerable<Student>>(false, "The User tied to this session was not found");
            var classes = await _context.Students.AsNoTracking().ToListAsync(cancellationToken);
            return new BaseResponse<IEnumerable<Student>>(true, "Classes Fetched", classes);
        }
        catch (Exception)
        {

            throw;
        }
    }
}

