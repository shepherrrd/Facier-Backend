using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Lecturer;

public class AssignStudentToClassRequest : IRequest<BaseResponse>
{
    public Guid SessionKey { get; set; }
    public string MatricNo { get; set; } = default!;
    public long ClassID { get; set; }
}

public class AssignStudentToClassRequestHandler : IRequestHandler<AssignStudentToClassRequest, BaseResponse>
{
    private readonly UniversityContext _context;

    public AssignStudentToClassRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse> Handle(AssignStudentToClassRequest request, CancellationToken cancellationToken)
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
            var student = await _context.Students.FirstOrDefaultAsync(x => x.MatricNumber == request.MatricNo, cancellationToken);
            if (student is null)
                return new BaseResponse(false, "No student is tied to this matric number");
            var classe = await _context.Classes.FirstOrDefaultAsync(x => x.Id == request.ClassID, cancellationToken);
            if (classe is null)
                return new BaseResponse(false, "class not found");
            classe.StudentIds += $"{student.Id};";
            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse(true, "Student Assigned to class");
        }
        catch (Exception)
        {

            throw;
        }
    }
}
