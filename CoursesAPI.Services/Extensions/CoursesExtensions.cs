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
		public static Course GetCourseByID(this IRepository<Course> repo, int id)
		{
			var course = repo.All().SingleOrDefault(c => c.ID == id);
			if (course == null)
			{
				throw new ArgumentException("There is no course registered with this ID");
			}
			return course;
		}
	}
}
