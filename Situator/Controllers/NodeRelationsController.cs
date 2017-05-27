using Microsoft.AspNetCore.Http;
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
    [Route("api/NodeRelations")]
    public class NodeRelationsController : Controller
    {
        private readonly SituatorContext _context;

        public NodeRelationsController(SituatorContext context)
        {
            _context = context;
        }

        // GET: api/NodeRelations
        [HttpGet]
        public IEnumerable<NodeRelation> GetNodeRelation()
        {
            return _context.NodeRelations;
        }

        // GET: api/NodeRelations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNodeRelation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nodeRelation = await _context.NodeRelations.SingleOrDefaultAsync(m => m.ParentId == id);

            if (nodeRelation == null)
            {
                return NotFound();
            }

            return Ok(nodeRelation);
        }

        // PUT: api/NodeRelations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNodeRelation([FromRoute] int id, [FromBody] NodeRelation nodeRelation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nodeRelation.ParentId)
            {
                return BadRequest();
            }

            _context.Entry(nodeRelation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NodeRelationExists(id))
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

        // POST: api/NodeRelations
        [HttpPost]
        public async Task<IActionResult> PostNodeRelation([FromBody] NodeRelation nodeRelation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.NodeRelations.Add(nodeRelation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NodeRelationExists(nodeRelation.ParentId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNodeRelation", new { id = nodeRelation.ParentId }, nodeRelation);
        }

        // DELETE: api/NodeRelations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNodeRelation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nodeRelation = await _context.NodeRelations.SingleOrDefaultAsync(m => m.ParentId == id);
            if (nodeRelation == null)
            {
                return NotFound();
            }

            _context.NodeRelations.Remove(nodeRelation);
            await _context.SaveChangesAsync();

            return Ok(nodeRelation);
        }

        private bool NodeRelationExists(int id)
        {
            return _context.NodeRelations.Any(e => e.ParentId == id);
        }
    }
}