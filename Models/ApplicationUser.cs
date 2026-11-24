using Microsoft.AspNetCore.Identity;

namespace Instructor_Information_System.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
