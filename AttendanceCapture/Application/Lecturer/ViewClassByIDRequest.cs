using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Lecturer;

public class ViewClassByIDRequest : IRequest<BaseResponse<Class>>
{
    public Guid SessionKey { get; set; }
    public long CLassID { get; set; }
}

public class ViewClassByIDRequestHandler : IRequestHandler<ViewClassByIDRequest, BaseResponse<Class>>
{
    private readonly UniversityContext _context;

    public ViewClassByIDRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<Class>> Handle(ViewClassByIDRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
            if (session is null || DateTimeOffset.UtcNow >= session.ExpiresAt)
            {
                return new BaseResponse<Class>(false, "The Session Tied to this operation does not exist");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == Infrastructure.UserType.Lecturer, cancellationToken);
            if (user is null)
                return new BaseResponse<Class>(false, "The User tied to this session was not found");
            var classe = await _context.Classes.SingleOrDefaultAsync(x => x.Id == request.CLassID && x.LecturerID == user.Id, cancellationToken);
            if (classe is null)
                return new BaseResponse<Class>(false, "Class not found");
            return new BaseResponse<Class>(true, "CLass Fetched", classe);

        }
        catch (Exception)
        {

            throw;
        }
    }
}
