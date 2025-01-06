using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Model.Entities;

namespace WebApplication3.Controllers
{
	[Route("api/[Controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly StudentDBContext dbContext;

		public StudentController(StudentDBContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		[Route("api")]
		public IActionResult GetAllStudents()
		{
			var allStudents = dbContext.Students.Include("Address").ToList();

			return Ok(allStudents);
		}


		[HttpGet]
		[Route("{id:int}")]
		public IActionResult GetStudentsById(int id)
		{
			var student = dbContext.Students.Include("Address").FirstOrDefault(a => a.ID == id);
			if (student is null)
			{
				return NotFound();
			}
			else
			{
				return Ok(student);
			}
		}


		//[HttpPost]

		//public IActionResult AddStudents(Student addstudent)
		//{
		//	var studentEntity = new Student()
		//	{
		//		Name = addstudent.Name,
		//		Phone = addstudent.Phone,
		//		Gender = addstudent.Gender,
		//	};
		//	dbContext.Students.Add(studentEntity);
		//	dbContext.SaveChanges();

		//	return Ok(studentEntity);
		//}


		[HttpPost]
		[Route("AddStudentsWithAddress")]
		public IActionResult AddAddress(Student student)
		{
			var studentEntity = new Student()
			{
				Name = student.Name,
				Phone = student.Phone,
				Gender = student.Gender,
				//child class ko call kre rhe hai
				Address = new Address()
				{
					City = student.Address.City,
					PostalCode = student.Address.PostalCode,
					State = student.Address.State,
				}
			};
			dbContext.Students.Add(student);
			dbContext.SaveChanges();

			return Ok(student);
		}

		[HttpDelete]
		[Route("{id:int}")]
		public IActionResult DeleteStudent(int id) 
		{
			var Student = dbContext.Students.Include("Address").FirstOrDefault(b=>b.ID ==id);
			if (Student is null)
			{
				return NotFound();
			}
			dbContext.Students.Remove(Student);
			dbContext.SaveChanges();
			return Ok(Student);

		}

		[HttpPut]
		public IActionResult UpdateStudent(Student updatedStudent)
		{
			var existingStudent = dbContext.Students.Include(s => s.Address).FirstOrDefault(s => s.ID == updatedStudent.ID);

			if (existingStudent == null)
				return NotFound();

			// Update Student details
			existingStudent.Name = updatedStudent.Name;
			existingStudent.Phone = updatedStudent.Phone;
			existingStudent.Gender = updatedStudent.Gender;

			// Update or add Address
			if (updatedStudent.Address != null)
			{
				if (existingStudent.Address == null)
					existingStudent.Address = new Address();

				existingStudent.Address.City = updatedStudent.Address.City;
				existingStudent.Address.PostalCode = updatedStudent.Address.PostalCode;
				existingStudent.Address.State = updatedStudent.Address.State;
			}

			dbContext.SaveChanges();
			return Ok(existingStudent);
		}

	}
}
