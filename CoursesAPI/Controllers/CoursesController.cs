using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CoursesAPI.Models;
using CoursesAPI.Services.Services;
using CoursesAPI.Services.DataAccess;

namespace CoursesAPI.Controllers
{
	[RoutePrefix("api/v1/courses")]
	public class CoursesController : ApiController
	{
		private readonly  CoursesServiceProvider _service;

		/// <summary>
		/// Constructor
		/// </summary>
		public CoursesController()
		{
			_service = new CoursesServiceProvider(new UnitOfWork<AppDataContext>());
		}

		/// <summary>
		/// Gets all students that are registered to a course
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("{id:int}/students")]
		public List<StudentDTO> GetStudentsInCourse(int id)
		{
			return _service.GetStudentsInCourse(id);
		}

		/// <summary>
		/// Adds a student to a course if he is not already registered
		/// </summary>
		/// <param name="id">id of course</param>
		/// <param name="model">object holds info about student like SSN</param>
		/// <returns>Returns 200 ok - name, SSN and ID of the student</returns>
		[HttpPost]
		[Route("{id:int}/students")]
		public StudentDTO AddStudentToCourse(int id, AddStudentViewModel model)
		{
			return _service.AddStudentToCourse(id, model);
		}

		
		[HttpDelete]
		[Route("{id:int}/students/{ssn}")]
		public void RemoveStudentFromCourse(int id, string ssn )
		{
			_service.RemoveStudentFromCourse(id, ssn);
		}
	}
}