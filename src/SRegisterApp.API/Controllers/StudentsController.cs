using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRegisterApp.Persistence;
using SRegisterApp.Domain.entities;
using SRegisterApp.API.Dtos;

namespace SRegisterApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SRegisterAppContext _context;

        public StudentsController(SRegisterAppContext context)
        {
            _context = context;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentsDtos>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            // Convertimos la entidad en DTO antes de devolverla
            var studentDto = new StudentsDtos
            {
                Id = student.Id,
                Nombre = student.Nombre,
                Correo = student.Correo,
                Matricula = student.Matricula
            };

            return Ok(studentDto);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentsDtos studentDto)
        {
            if (id != studentDto.Id)
            {
                return BadRequest();
            }

            // Convertimos DTO en entidad antes de actualizar
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            student.Nombre = studentDto.Nombre;
            student.Correo = studentDto.Correo;
            student.Matricula = studentDto.Matricula;

            _context.Students.Update(student);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Students>> PostStudent(StudentsDtos studentDto)
        {
            // Convertimos DTO en entidad
            var student = new Students
            {
                Nombre = studentDto.Nombre,
                Correo = studentDto.Correo,
                Matricula = studentDto.Matricula
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, studentDto);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentsExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
