namespace AttendanceCapture.Models;

using AttendanceCapture.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public DateTimeOffset TimeCreated { get; set; }
    public DateTimeOffset TimeUpdated { get; set; }
}

public class ApplicationRole : IdentityRole<long>
{
    public string? Description { get; set; }
    public DateTimeOffset TimeCreated { get; set; }
    public DateTimeOffset TimeUpdated { get; set; }
}

public class LecturTutor : BaseEntity
{
    public string? Name { get; set; } // Add other lecturer properties as required

    // Explicit foreign key property
    public int DepartmentId { get; set; } = default!;
    public string? Courses { get; set; }

    public long UserId { get; set; }
}

public class Faculty : BaseEntity
{
    public string? Name { get; set; } // Add other faculty properties as required

    // Use a string for the collection navigation property I

    // Navigation properties
    public string? Departments { get; set; }
}

public class Department : BaseEntity
{
    public string? Name { get; set; } // Add other department properties as required

    // Explicit foreign key property
    public int? FacultyId { get; set; }

    // Navigation properties
    public long? FacultyID { get; set; }
    public string? Lecturers { get; set; }
}

public class LogActivity : BaseEntity
{
    public LogActivityType Type { get; set; }
    public string? Description { get; set; }
}
public class Class : BaseEntity
{
    public string? Name { get; set; } // Add other class properties as required

    // Use a string for the collection navigation property ID
    public string? StudentIds { get; set; }

    public string? ClassNumber { get; set; }
    public long LecturerID { get; set; }

}

public class Student : BaseEntity
{
    public string? Name { get; set; } // Add other student properties as required

    // Explicit foreign key property
    public int? ClassId { get; set; }
    public string MatricNumber { get; set; } = string.Empty;
    public string? Attendances { get; set; }

    public string? Photo {  get; set; }
}

public class Course : BaseEntity
{
    public string? Name { get; set; } 
    public long? LectureriD { get; set; }
    public long? DepartmentID { get; set; }
}

public class Attendance : BaseEntity
{
    public string? Remarks { get; set; } // Add other attendance properties as required

    // Explicit foreign key properties

    // Navigation properties
    public long? Student { get; set; }
    public long?  Course { get; set; }
    public long LecturerID { get; set; }

}

public class DBSessions : BaseEntity
{
    public long UserID { get; set; }
    public UserType UserType { get; set; }

    public Guid SessionKey { get; set; }

    public DateTimeOffset ExpiresAt { get; set; }

}
public class User : IdentityUser<long> { 

    public UserType UserType {  get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public AccountStatusEnum AccountStatus { get; set; }
    public DateTimeOffset TimeCreated { get; set; }
    public DateTimeOffset TimeUpdated { get; set;}
}

