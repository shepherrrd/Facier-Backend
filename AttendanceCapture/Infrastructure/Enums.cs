using System.ComponentModel;

namespace AttendanceCapture.Infrastructure;

public class Enums
{
}

public enum UserType
{
    [Description("Admin")]
    Admin = 1,

    [Description("Lecturer")]
    Lecturer

}
public enum AccountStatusEnum
{
    [Description("Active")]
    Active = 1,
    [Description("Suspended")]
    Suspended,
    [Description("InActive")]
    InActive
}

public enum LogActivityType
{
    [Description("Create New User")]
    CreateNewUser,
    [Description("Create New Student")]
    CreateStudent,
    [Description("Create New AttendanceRecord")]
    CaptureAttendance,
    [Description("Signin ")]
    Login,
    [Description("Create New Class ")]
    Createclass
}