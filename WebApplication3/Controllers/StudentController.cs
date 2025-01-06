using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Model.Entities;
namespace WebApplication3.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
	private readonly StudentDBContext _context;

	public StudentsController(StudentDBContext context)
	{
		_context = context;
	}

	// POST: api/Students
	[HttpPost]
	public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
	{
		// Validate if student is not null
		if (student == null)
		{
			return BadRequest("Student data is missing.");
		}

		// Create a new student entity
		//var newStudent = new Student()
		//{
		//	Name = student.Name,
		//	Phone = student.Phone,
		//	Gender = student.Gender,
		//	Address = student.Address,
		//	Guardians = student.Guardians,
		//};



		if (student.Address != null && student.Guardians.Count > 0)
		{
			foreach (var address in student.Address)
			{
				//book.AuthorId = author.AuthorId; // Set AuthorId for each book
				address.StudentId = student.ID;	
				address.State
			}

			foreach (var guardian in student.Guardians)
			{
				guardian.StudentId = student.ID;
			}
		}


		// Use LINQ to create Address and Guardian entities and associate them with the new student
		//newStudent.Address = student.Address
		//	.Select(a => new Address
		//	{
		//		City = a.City,
		//		State = a.State,
		//		PostalCode = a.PostalCode,
		//		// Set StudentId directly in the mapping using LINQ (we'll update this later after student is saved)
		//		StudentId = newStudent.ID
		//	}).ToList();

		//newStudent.Guardians = student.Guardians
		//	.Select(g => new Guardian
		//	{
		//		Name = g.Name,
		//		Phone = g.Phone,
		//		Email = g.Email,
		//		// Set StudentId directly in the mapping using LINQ
		//		StudentId = newStudent.ID
		//	}).ToList();

		// Add the student to the DbContext
		await _context.Student.AddAsync(student);
		await _context.SaveChangesAsync(); // Save to generate ID for the new student

		// Update StudentId for Address and Guardian after saving the student
		//newStudent.Address.ForEach(a => a.StudentId = newStudent.ID);
		//newStudent.Guardians.ForEach(g => g.StudentId = newStudent.ID);

		// Save again to update the foreign keys for Address and Guardians
		//await _context.SaveChangesAsync();

		// Return the created student along with related entities
		//return CreatedAtAction(nameof(GetStudent), new { id = newStudent.ID }, newStudent);
		return Ok("posted data successfully");
	}




	// GET: api/Students/5
	[HttpGet("{id}")]
	public async Task<ActionResult<Student>> GetStudent(int id)
	{
		var student = await _context.Student
			.Include(s => s.Address)
			.Include(s => s.Guardians)
			.FirstOrDefaultAsync(s => s.ID == id);

		if (student == null)
		{
			return NotFound();
		}

		return student;
	}
}


