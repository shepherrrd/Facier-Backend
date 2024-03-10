using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Identity;

public class LoginRequest : IRequest<BaseResponse<DBSessions>>
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();

    }
}


public class LoginRequestHandler : IRequestHandler<LoginRequest, BaseResponse<DBSessions>>
{
    private readonly UniversityContext _context;
    private SignInManager<User> _signinManager;
    public LoginRequestHandler(UniversityContext context,
        SignInManager<User> signInManager
        )
    {
        _context = context;
        _signinManager = signInManager;
    }

    
    public async Task<BaseResponse<DBSessions>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
            if (user is null)
                return new BaseResponse<DBSessions>(false, "Invalid Username or passowrd");
            var signing = await _signinManager.PasswordSignInAsync(user, request.Password, true, true);
            if(!signing.Succeeded)
                return new BaseResponse<DBSessions>(false, "Invalid Username or passowrd");
            var session = new DBSessions
            {
                SessionKey = Guid.NewGuid(),
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                UserID = user.Id,
                UserType = user.UserType,
                ExpiresAt = DateTimeOffset.UtcNow.AddHours(3),

            };
            var log = new LogActivity
            {
                Description = $"{user.UserName} Logged In",
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                Type = Infrastructure.LogActivityType.Login
            };
            await _context.LogActivities.AddAsync(log);
            await _context.AddAsync(session,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<DBSessions>(true, "Signing Successfull", session);

        }
        catch (Exception)
        {
            return new BaseResponse<DBSessions>(false, "An Error Occured WHile trying to Sign You In");
        }
    }
}
