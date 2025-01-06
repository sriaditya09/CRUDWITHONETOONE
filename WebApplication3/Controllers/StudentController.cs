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
    public IActionResult PostStudent([FromBody] Student student)
    {
        if (student == null)
        {
            return BadRequest("Student object is null");
        }

        var newStudent = new Student
        {
            Name = student.Name,
            Phone = student.Phone,
            Gender = student.Gender,
            Addresses = student.Addresses.Select(a => new Address
            {
                City = a.City,
                State = a.State,
                PostalCode = a.PostalCode,
                StudentId = a.StudentId
            }).ToList(),
            Guardians = student.Guardians.Select(g => new Guardian
            {
                Name = g.Name,
                Relation = g.Relation,
                StudentId = g.StudentId
            }).ToList()
        };

        _context.Students.Add(newStudent);
        _context.SaveChanges();

        return Ok(new {message = "Student Added Sucessfully"});
    }




    // GET: api/Students/5
    [HttpGet("{id}")]
	public async Task<ActionResult<Student>> GetStudent(int id)
	{
		var student = await _context.Students
			.Include(s => s.Addresses)
			.Include(s => s.Guardians)
			.FirstOrDefaultAsync(s => s.ID == id);

		if (student == null)
		{
			return NotFound();
		}

		return student;
	}
}


