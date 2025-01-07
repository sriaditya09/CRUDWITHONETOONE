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
            Addresses = student?.Addresses?.Select(a => new Address
            {
                City = a.City,
                State = a.State,
                PostalCode = a.PostalCode,
                StudentId = a.StudentId
            }).ToList(),
            Guardians = student?.Guardians?.Select(g => new Guardian
            {
                Name = g.Name,
                Relation = g.Relation,
                StudentId = g.StudentId
            }).ToList()
        };

        _context.Students.Add(newStudent);
        _context.SaveChanges();

        //return Ok(new {message = "Student Added Sucessfully"});
        return Ok(newStudent);
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

    [HttpGet]
	public async Task<ActionResult<IEnumerable<StudentDBContext>>> GetAllStudentDetails()
	{
		var AllStudentDetails = await _context.Students
			.Include(p => p.Addresses)
            .Include(g=>g.Guardians)
			.ToListAsync();

		return Ok(AllStudentDetails);
	}


	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateStudent(int id, Student updatedStudent)
	{
		// Fetch the existing student with related data
		var existingStudent = await _context.Students
			.Include(s => s.Addresses)
			.Include(s => s.Guardians)
			.FirstOrDefaultAsync(s => s.ID == id);

		if (existingStudent == null)
		{
			return NotFound();
		}

		// Update student properties
		existingStudent.Name = updatedStudent.Name;
		existingStudent.Phone = updatedStudent.Phone;
		existingStudent.Gender = updatedStudent.Gender;

		// Update addresses
		foreach (var updatedAddress in updatedStudent.Addresses)
		{
			var existingAddress = existingStudent.Addresses?.FirstOrDefault(a => a.ID == updatedAddress.ID);

			if (existingAddress != null)
			{
				// Update existing address
				existingAddress.City = updatedAddress.City;
				existingAddress.State = updatedAddress.State;
				existingAddress.PostalCode = updatedAddress.PostalCode;
			}
			else
			{
				// Add new address
				existingStudent.Addresses?.Add(new Address
				{
					City = updatedAddress.City,
					State = updatedAddress.State,
					PostalCode = updatedAddress.PostalCode,
					StudentId = existingStudent.ID
				});
			}
		}
		// Save changes to the database
		await _context.SaveChangesAsync();

		return Ok("Here No Data Available");
	}


		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteStudent(int id)
		{
			// Fetch the student with related data
			var student = await _context.Students
				.Include(s => s.Addresses)
				.Include(s => s.Guardians)
				.FirstOrDefaultAsync(s => s.ID == id);

			if (student == null)
			{
				return NotFound(); // Return 404 if the student does not exist
			}

			// Remove the student (related addresses and guardians will be deleted due to cascade delete)
			_context.Students.Remove(student);

			// Save changes to the database
			await _context.SaveChangesAsync();

			return NoContent(); // Return 204 No Content
		}

}


