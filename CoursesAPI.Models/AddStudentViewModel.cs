using System.ComponentModel.DataAnnotations;

namespace CoursesAPI.Models
{
	public class AddStudentViewModel
	{
		/// <summary>
		/// This class represents the data which are needed as input when
		/// a student is added to a course.
		/// </summary>
		[Required]
		public string SSN { get; set; }
	}
}
