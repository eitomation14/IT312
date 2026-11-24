using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instructor_Information_System.Models
{
    public class Instructor
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Instructor ID is required.")]
        [RegularExpression(@"^\d{2}-\d{4}$", ErrorMessage = "Instructor ID must use the format 00-0000.")]
        public string InstructorID { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be 2 to 50 characters long.")]
        [RegularExpression(@"^[A-Za-z][A-Za-z\s.'-]*$", ErrorMessage = "Name must contain letters only.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [StringLength(254)]
        [Column("Username")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Majorship is required.")]
        [StringLength(100)]
        public string Majorship { get; set; } = string.Empty;

        [Required(ErrorMessage = "Designation is required.")]
        [StringLength(100)]
        public string Designation { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact number is required.")]
        [StringLength(20)]
        [RegularExpression(@"^[0-9+()\s-]{7,20}$", ErrorMessage = "Enter a valid contact number.")]
        public string ContactNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Bachelor degree is required.")]
        [StringLength(150)]
        public string BachelorDegree { get; set; } = string.Empty;

        // Compatibility with the original database schema. This is not used for authentication.
        [Column("Password")]
        public string LegacyPasswordHash { get; set; } = string.Empty;

    }

}


