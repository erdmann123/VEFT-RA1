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


		[HttpGet]
		[Route("{id:int}/students")]
		public List<StudentDTO> GetStudentsInCourse(int id)
		{
			return _service.GetStudentsInCourse(id);
		}


		[HttpPost]
		[Route("{id:int}/students")]
		public StudentDTO AddStudentToCourse(int id, AddStudentViewModel model)
		{
			return _service.AddStudentToCourse(id, model);
		}
	}
}