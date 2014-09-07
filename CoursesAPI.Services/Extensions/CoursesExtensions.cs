using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Models.Entities;

namespace CoursesAPI.Services.Extensions
{
	public static class CoursesExtensions
	{
		/// <summary>
		/// Check if id is a valid courseID
		/// </summary>
		/// <param name="repo">repo for all courses</param>
		/// <param name="id">id for course</param>
		/// <returns>object of the course if he exists but null if it does not exist</returns>
		public static Course GetCourseByID(this IRepository<Course> repo, int id)
		{
			var course = repo.All().SingleOrDefault(c => c.ID == id);
			if (course == null)
			{
				throw new ArgumentException("There is no course registered with this ID");
			}
			return course;
		}

		public static CourseStudent GetStudentInCourse(this IRepository<CourseStudent> repo, int id, string ssn)
		{
			var courseStudent = repo.All().SingleOrDefault(cs => cs.SSN == ssn
				&& cs.CourseInstanceID == id);

			return courseStudent;
		}
	}
}
