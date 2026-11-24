namespace Instructor_Information_System.Models;

public class DashboardViewModel
{
    public string DisplayName { get; set; } = string.Empty;
    public int TotalInstructors { get; set; }
    public string LatestInstructorName { get; set; } = "None";
}
