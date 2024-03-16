using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Admin;

public class ViewAllClassesAdminRequest : IRequest<BaseResponse<IEnumerable<Class>>>
{
    public Guid SessionKey { get; set; }
}


public class ViewAllClassesAdminRequestHandler : IRequestHandler<ViewAllClassesAdminRequest, BaseResponse<IEnumerable<Class>>>
{
    private readonly UniversityContext _context;

    public ViewAllClassesAdminRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<IEnumerable<Class>>> Handle(ViewAllClassesAdminRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
            if (session is null || DateTimeOffset.UtcNow >= session.ExpiresAt)
            {
                return new BaseResponse<IEnumerable<Class>>(false, "The Session Tied to this operation does not exist");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == Infrastructure.UserType.Admin, cancellationToken);
            if (user is null)
                return new BaseResponse<IEnumerable<Class>>(false, "The User tied to this session was not found");
            var classes = await _context.Classes.AsNoTracking().ToListAsync(cancellationToken);
            return new BaseResponse<IEnumerable<Class>>(true, "Classes Fetched", classes);
        }
        catch (Exception)
        {

            throw;
        }
    }
}

