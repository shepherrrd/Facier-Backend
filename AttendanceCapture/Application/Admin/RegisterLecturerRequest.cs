
using AttendanceCapture.Infrastructure;
using AttendanceCapture.Models;
using AttendanceCapture.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AttendanceCapture.Application.Admin;

public class RegisterLecturerRequest : IRequest<BaseResponse>
{
    public Guid SessionKey { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; } 

}

public class RegisterLecturerRequestValidator : AbstractValidator<RegisterLecturerRequest>
{
    public RegisterLecturerRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.FirstName).NotNull().NotEmpty();
        RuleFor(x => x.LastName).NotNull().NotEmpty();
        RuleFor(x => x.UserName).NotNull().NotEmpty();
        RuleFor(x =>x.Password).NotNull().NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!#%*?&])[A-Za-z\d@$!#%*?&]{8,}$")
            .WithMessage("Password must contain 1 digit , 1 uppercase and 1 Ascii character").MinimumLength(8); 

       
    }
}


public class RegisterLecturerRequestHandler : IRequestHandler<RegisterLecturerRequest, BaseResponse>
{
    private readonly UniversityContext _context;
    private UserManager<User> _userManager;

    public RegisterLecturerRequestHandler(UniversityContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<BaseResponse> Handle(RegisterLecturerRequest request, CancellationToken cancellationToken)
    {

        try
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            {
                try
                {
                    var session = await _context.UserSessions.FirstOrDefaultAsync(x => x.SessionKey == request.SessionKey, cancellationToken);
                    if (session is null || DateTimeOffset.UtcNow >= session.ExpiresAt || session.UserType != UserType.Admin)
                    {
                        return new BaseResponse(false, "The Session Tied to this operation does not exist");
                    }
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == session.UserID && x.UserType == UserType.Admin, cancellationToken);
                    if (user is null)
                        return new BaseResponse(false, "The User tied to this session was not found");

                    var existing = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName!.ToLower() == request.UserName!.ToLower());
                    if (existing is not null)
                        return new BaseResponse(false, "A User with that username already exists");

                    var newlecturer = new User
                    {
                        Email = request.Email,
                        FirstName = request.FirstName!,
                        LastName = request.LastName!,
                        UserName = request.UserName!.ToLower(),
                        TimeCreated = DateTimeOffset.UtcNow,
                        TimeUpdated = DateTimeOffset.UtcNow,
                        AccountStatus = AccountStatusEnum.Active,
                        EmailConfirmed = true,
                        UserType = UserType.Lecturer,
                    };

                    await _context.AddAsync(newlecturer, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                    var addpassword = await _userManager.AddPasswordAsync(newlecturer, request.Password!);
                    if (!addpassword.Succeeded)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return new BaseResponse(false, "An Error Occured While Trying to Complete your Registration");
                    }
                    var lecturer = new LecturTutor
                    {
                        Name = request.FirstName + " " + request.LastName,
                        TimeCreated = DateTimeOffset.UtcNow,
                        TimeUpdated = DateTimeOffset.UtcNow,
                        UserId = newlecturer.Id,

                    };
                    var log = new LogActivity
                    {
                        Description = "Created new Lecturer",
                        TimeCreated = DateTimeOffset.UtcNow,
                        TimeUpdated = DateTimeOffset.UtcNow,
                        Type = LogActivityType.CreateNewUser
                    };
                    await _context.LogActivities.AddAsync(log);
                    await _context.AddAsync(lecturer, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
                    return new BaseResponse(true, "Lecturer Added Successfully");

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return new BaseResponse(false, "An Error Occured while trying to add a lecturer");
                }
            }
        }
        catch (Exception)
        {
            return new BaseResponse(false, "DB Error");
        }

    }
}
