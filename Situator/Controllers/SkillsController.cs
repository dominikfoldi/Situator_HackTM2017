using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Situator.Model;
using Situator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Situator.Controllers
{
    [Produces("application/json")]
    [Route("api/Skills")]
    public class SkillsController : Controller
    {
        private readonly SituatorContext _context;

        public SkillsController(SituatorContext context)
        {
            _context = context;
        }

        // GET: api/Skills
        [HttpGet]
        public IEnumerable<Skill> GetSkills()
        {
            return _context.Skills;
        }

        // GET: api/Skills/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skill = await _context.Skills.SingleOrDefaultAsync(m => m.Id == id);

            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        // PUT: api/Skills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill([FromRoute] int id, [FromBody] Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skill.Id)
            {
                return BadRequest();
            }

            _context.Entry(skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
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

        // POST: api/Skills
        [HttpPost]
        public async Task<IActionResult> PostSkill([FromBody] Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSkill", new { id = skill.Id }, skill);
        }

        // DELETE: api/Skills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var skill = await _context.Skills.SingleOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return Ok(skill);
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.Id == id);
        }
    }
}