using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesAPI.Models
{
	/// <summary>
	/// A DTO class for the student entity class
	/// </summary>
	public class StudentDTO
	{
		/// <summary>
		/// unique number that the Database generates
		/// </summary>
		public int ID { get; set; }
		
		/// <summary>
		/// SSN number of the Student
		/// </summary>
		public string SSN { get; set; }
		
		/// <summary>
		/// Full name of the student
		/// </summary>
		public string Name { get; set; }
	}
}
