using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Admin;

public class VIewLogsRequest : IRequest<BaseResponse<IEnumerable<LogActivity>>>
{
    public Guid SessionKey { get; set; }

}


public class VIewLogsRequestHandler : IRequestHandler<VIewLogsRequest, BaseResponse<IEnumerable<LogActivity>>>
{
    private readonly UniversityContext _context;

    public VIewLogsRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<IEnumerable<LogActivity>>> Handle(VIewLogsRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
            if (session is null || DateTimeOffset.UtcNow >= session.ExpiresAt)
            {
                return new BaseResponse<IEnumerable<LogActivity>>(false, "The Session Tied to this operation does not exist");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == Infrastructure.UserType.Admin, cancellationToken);
            if (user is null)
                return new BaseResponse<IEnumerable<LogActivity>>(false, "The User tied to this session was not found");

            var logs = await _context.LogActivities.AsNoTracking().ToListAsync(cancellationToken);
            return new BaseResponse<IEnumerable<LogActivity>>(true, "Logs Fetched SuccessFully", logs);

        }
        catch (Exception)
        {

            throw;
        }
    }
}
