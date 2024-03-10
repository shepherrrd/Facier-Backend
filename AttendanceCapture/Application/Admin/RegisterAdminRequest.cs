using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Admin;

public class RegisterAdminRequest : IRequest<BaseResponse>
{
    public Guid SessionKey { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string UserName {  get; set; } = string.Empty;
}

public class RegisterAdminRequestHandler : IRequestHandler<RegisterAdminRequest, BaseResponse>
{
    private readonly UniversityContext _context;
    UserManager<User> _userManager;
    public RegisterAdminRequestHandler(UniversityContext context,
        UserManager<User> userManager
        )
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<BaseResponse> Handle(RegisterAdminRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
            if(session is null || DateTimeOffset.UtcNow >= session.ExpiresAt )
            {
                return new BaseResponse(false, "The Session Tied to this operation does not exist");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == Infrastructure.UserType.Admin, cancellationToken);
            if (user is null)
                return new BaseResponse(false, "The User tied to this session was not found");

            var newadmin = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                TimeCreated = DateTimeOffset.Now,
                TimeUpdated = DateTimeOffset.Now,
            };
            var log = new LogActivity
            {
                Description = "Created new Admin",
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                Type = Infrastructure.LogActivityType.CreateNewUser
            };
            await _context.LogActivities.AddAsync(log);
            await _context.Users.AddAsync(newadmin);
            await _context.SaveChangesAsync(cancellationToken);
           var addpassword = await _userManager.AddPasswordAsync(user, request.Password);
            if (!addpassword.Succeeded)
                return new BaseResponse(false, "An Error Occured While Trying to Complete your Registration");
            return new BaseResponse(true, "Admin Added Successfully");
        }
        catch (Exception)
        {
            return new BaseResponse(false, "An Error Occured while trying to Add Admin");
        }
    }
}
