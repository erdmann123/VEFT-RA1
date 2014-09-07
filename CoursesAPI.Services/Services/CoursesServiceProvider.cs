using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoursesAPI.Models;
using CoursesAPI.Services.Models.Entities;
using CoursesAPI.Services.DataAccess;
using CoursesAPI.Services.Extensions;

namespace CoursesAPI.Services.Services
{
	public class CoursesServiceProvider
	{
		private readonly IUnitOfWork          _uow;
		private readonly IRepository<Student> _students;
		private readonly IRepository<Course> _courses;
		private readonly IRepository<CourseStudent> _courseStudents;
		
		public CoursesServiceProvider(IUnitOfWork uow)
		{
			_uow      = uow;
			_courses  = _uow.GetRepository<Course>();
			_students = _uow.GetRepository<Student>();
			_courseStudents = _uow.GetRepository<CourseStudent>();
			
		}

		public List<StudentDTO> GetStudentsInCourse(int id)
		{
			// Business rule 0: operation on courses must use valid courses IDs
			var course = _courses.GetCourseByID(id);

			var result = (from s in _students.All()
						  join cs in _courseStudents.All() on s.SSN equals cs.SSN
						  where cs.CourseInstanceID == course.ID
						  && cs.Status == 1
						  select new StudentDTO 
						  {
							  ID   = s.ID,
 							  SSN  = s.SSN,
							  Name = s.Name
						  }
			).ToList();

			return result;
		}

		public StudentDTO AddStudentToCourse(int id, AddStudentViewModel model)
		{

			// Business rule 0: operation on courses must use valid courses IDs
			var course = _courses.GetCourseByID(id);
	
			// Business rule 1: only registered students can be registered to the course:
			var student = _students.All().SingleOrDefault(s => s.SSN == model.SSN);
			if (student == null)
			{
				throw new ArgumentException("No student with this SSN exists");
			}

			// Business rule 2: student can not be registered more than once to the same 
			// course (but they can be re-registered)
			var courseStudent = _courseStudents.All().SingleOrDefault( cs => cs.SSN == model.SSN 
				&& cs.CourseInstanceID == id);
			if (courseStudent != null)
			{
				if (courseStudent.Status == 1)
				{
					throw new ArgumentException("Student is already registered to this course");
				}
				courseStudent.Status = 1;
				_uow.Save();
			}
			else
			{
				var cs = new CourseStudent
				{
					SSN = student.SSN,
					CourseInstanceID = course.ID,
					Status = 1
				};
				_courseStudents.Add(cs);
				_uow.Save();
			}
			return new StudentDTO
			{
				ID = student.ID,
				SSN = student.SSN,
				Name = student.Name
			};

		}
	}
}
