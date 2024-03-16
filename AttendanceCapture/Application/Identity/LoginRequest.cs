using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AttendanceCapture.Application.Identity;

public class LoginRequest : IRequest<BaseResponse<LoginResponse>>
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


public class LoginRequestHandler : IRequestHandler<LoginRequest, BaseResponse<LoginResponse>>
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

    
    public async Task<BaseResponse<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken);
            if (user is null)
                return new BaseResponse<LoginResponse>(false, "Invalid Username or passowrd");
            var signing = await _signinManager.PasswordSignInAsync(user, request.Password, true, true);
            if(!signing.Succeeded)
                return new BaseResponse<LoginResponse>(false, "Invalid Username or passowrd");
            var userr = new UserResponse
             {
                 FirstName = user.FirstName,
                 LastName = user.LastName,
                 Email = user.Email!,
                 UserName = user.UserName!
             };
            var session = new DBSessions
            {
                SessionKey = Guid.NewGuid(),
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                UserID = user.Id,
                UserType = user.UserType,
                ExpiresAt = DateTimeOffset.UtcNow.AddHours(3),

            };
            var lgrp = new LoginResponse
            {
                Session = session,
                User = userr
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

            return new BaseResponse<LoginResponse>(true, "Signing Successfull", lgrp);

        }
        catch (Exception)
        {
            return new BaseResponse<LoginResponse>(false, "An Error Occured WHile trying to Sign You In");
        }
    }
}
