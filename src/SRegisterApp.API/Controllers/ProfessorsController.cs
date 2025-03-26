using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SRegisterApp.Persistence;
using SRegisterApp.Domain.entities;
using SRegisterApp.API.Dtos;


namespace SRegisterApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorsController : ControllerBase
    {
        private readonly SRegisterAppContext _context;

        public ProfessorsController(SRegisterAppContext context)
        {
            _context = context;
        }

        // GET: api/Professors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessorsDtos>>> GetProfessors()
        {
            var professors = await _context.Professors
                                          .Select(p => new ProfessorsDtos
                                          {
                                              ID = p.ID,
                                              Nombre = p.Nombre,
                                              Apellido = p.Apellido,
                                              Email = p.Email,
                                              Telefono = p.Telefono
                                          })
                                          .ToListAsync();

            return Ok(professors);
        }

        // GET: api/Professors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfessorsDtos>> GetProfessor(int id)
        {
            var professor = await _context.Professors
                                          .Where(p => p.ID == id)
                                          .Select(p => new ProfessorsDtos
                                          {
                                              ID = p.ID,
                                              Nombre = p.Nombre,
                                              Apellido = p.Apellido,
                                              Email = p.Email,
                                              Telefono = p.Telefono
                                          })
                                          .FirstOrDefaultAsync();

            if (professor == null)
            {
                return NotFound();
            }

            return Ok(professor);
        }

        // PUT: api/Professors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessor(int id, ProfessorsDtos professorDto)
        {
            if (id != professorDto.ID)
            {
                return BadRequest();
            }

            var professor = await _context.Professors.FindAsync(id);

            if (professor == null)
            {
                return NotFound();
            }

            professor.Nombre = professorDto.Nombre;
            professor.Apellido = professorDto.Apellido;
            professor.Email = professorDto.Email;
            professor.Telefono = professorDto.Telefono;

            _context.Entry(professor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessorsExists(id))
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

        // POST: api/Professors
        [HttpPost]
        public async Task<ActionResult<ProfessorsDtos>> PostProfessor(ProfessorsDtos professorDto)
        {
            var professor = new Professors
            {
                Nombre = professorDto.Nombre,
                Apellido = professorDto.Apellido,
                Email = professorDto.Email,
                Telefono = professorDto.Telefono
            };

            _context.Professors.Add(professor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfessor", new { id = professor.ID }, professorDto);
        }

        // DELETE: api/Professors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessor(int id)
        {
            var professor = await _context.Professors.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }

            _context.Professors.Remove(professor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfessorsExists(int id)
        {
            return _context.Professors.Any(e => e.ID == id);
        }
    }
}
