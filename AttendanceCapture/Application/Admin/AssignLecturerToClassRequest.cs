using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Admin;

public class AssignLecturerToClassRequest : IRequest<BaseResponse>
{
    public Guid SessionKey { get; set; }

    public string LecturerUserName { get; set; } = default!;

    public long ClassID { get; set; }
}

public class AssignLecturerToClassRequestHandler : IRequestHandler<AssignLecturerToClassRequest, BaseResponse>
{
    private readonly UniversityContext _context;

    public AssignLecturerToClassRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse> Handle(AssignLecturerToClassRequest request, CancellationToken cancellationToken)
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

            var lecturer = await _context.Users.FirstOrDefaultAsync(x => x.UserName!.ToLower() == request.LecturerUserName.ToLower(), cancellationToken);
            if (lecturer is null)
                return new BaseResponse(false, "This Lecturer DOes Not exist");
            var classe = await _context.Classes.FirstOrDefaultAsync(x => x.Id == request.ClassID, cancellationToken);
            if (classe is null)
                return new BaseResponse(false, "This Class DOes Not exist");
            classe.LecturerID = user.Id;
            classe.TimeUpdated = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse(true, "CLass Assigned Successfully");
        }
        catch (Exception)
        {

            throw;
        }
    }
}
