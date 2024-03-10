﻿using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Lecturer;

public class ViewAttendanceByIDRequest : IRequest<BaseResponse<Attendance>>
{
    public Guid SessionKey { get; set; }
    public long AttendanceID { get; set; }  
}

public class ViewAttendanceByIDRequestHandler : IRequestHandler<ViewAttendanceByIDRequest, BaseResponse<Attendance>>
{
    private readonly UniversityContext _context;

    public ViewAttendanceByIDRequestHandler(UniversityContext context)
    {
        _context = context;
    }

    public async Task<BaseResponse<Attendance>> Handle(ViewAttendanceByIDRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
            if (session is null || DateTimeOffset.UtcNow >= session.ExpiresAt)
            {
                return new BaseResponse<Attendance>(false, "The Session Tied to this operation does not exist");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == Infrastructure.UserType.Lecturer, cancellationToken);
            if (user is null)
                return new BaseResponse<Attendance>(false, "The User tied to this session was not found");

            var attendance = await _context.Attendances.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.AttendanceID && x.LecturerID ==user.Id, cancellationToken);
            if (attendance is null)
                return new BaseResponse<Attendance>(false, "Attendance Not Found");
            return new BaseResponse<Attendance>(true, "Attendance Fetched", attendance);

        }
        catch (Exception)
        {

            throw;
        }
    }
}
