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
    [Route("api/Nodes")]
    public class NodesController : Controller
    {
        private readonly SituatorContext _context;

        public NodesController(SituatorContext context)
        {
            _context = context;
        }

        // GET: api/Nodes
        [HttpGet]
        public IEnumerable<Node> GetNodes()
        {
            return _context.Nodes.Include(n => n.Scores).Include(n => n.Parents).Include(n => n.Children);
        }

        // GET: api/Nodes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var node = await _context.Nodes.Include(n => n.Scores).Include(n => n.Parents).Include(n => n.Children).SingleOrDefaultAsync(m => m.Id == id);

            if (node == null)
            {
                return NotFound();
            }

            return Ok(node);
        }

        // PUT: api/Nodes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNode([FromRoute] int id, [FromBody] Node node)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != node.Id)
            {
                return BadRequest();
            }

            _context.Entry(node).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NodeExists(id))
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

        // POST: api/Nodes
        [HttpPost]
        public async Task<IActionResult> PostNode([FromBody] Node node)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newNode = new Node
            {
                IsLeaf = node.IsLeaf,
                IsRoot = node.IsRoot,
                PositionX = node.PositionX,
                PositionY = node.PositionY,
                CourseId = 1
            };

            _context.Nodes.Add(newNode);
            await _context.SaveChangesAsync();

            //return _context.Nodes.Where(i => i.Id == node.Id)

            return CreatedAtAction("GetNode", new { id = newNode.Id }, node);
        }

        // DELETE: api/Nodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNode([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var node = await _context.Nodes.SingleOrDefaultAsync(m => m.Id == id);
            if (node == null)
            {
                return NotFound();
            }

            _context.Nodes.Remove(node);
            await _context.SaveChangesAsync();

            return Ok(node);
        }

        private bool NodeExists(int id)
        {
            return _context.Nodes.Any(e => e.Id == id);
        }
    }
}