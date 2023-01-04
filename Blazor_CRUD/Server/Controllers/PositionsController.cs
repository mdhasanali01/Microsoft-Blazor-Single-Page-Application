using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blazor_CRUD.Shared.Models;
using Blazor_CRUD.Shared.DTO;

namespace Blazor_CRUD.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public PositionsController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
          if (_context.Positions == null)
          {
              return NotFound();
          }
            return await _context.Positions.ToListAsync();
        }
        [HttpGet("DTO")]
        public async Task<ActionResult<IEnumerable<PositionViewDTO>>> GetOrderDTOs()
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            return await _context.Positions
                
                .Include(o => o.EmployeePositions).ThenInclude(oi => oi.Employee)
                .Select(o =>
                    new PositionViewDTO
                    {
                        PositionId = o.PositionId,
                        PositionName= o.PositionName                                                                  

                    })
                .ToListAsync();
        }
        // GET: api/Positions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPosition(int id)
        {
          if (_context.Positions == null)
          {
              return NotFound();
          }
            var position = await _context.Positions.FindAsync(id);

            if (position == null)
            {
                return NotFound();
            }

            return position;
        }
        [HttpGet("DTO/{id}")]
        public async Task<ActionResult<PositionViewDTO>> GetOrderViewDTO(int id)
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            var position = await _context.Positions
                .Include(o => o.EmployeePositions).ThenInclude(oi => oi.Employee).FirstOrDefaultAsync(o => o.PositionId == id);

            if (position == null)
            {
                return NotFound();
            }

            return new PositionViewDTO
            {
                PositionId = position.PositionId,
                PositionName = position.PositionName

            };
        }
        [HttpGet("Items/{id}")]
        public async Task<ActionResult<IEnumerable<PositionItemViewDTO>>> GetOrderItems(int id)
        {
            if (_context.EmployeePositions == null)
            {
                return NotFound();
            }
            var employeepositions = await _context.EmployeePositions.Include(x => x.Employee).Where(oi => oi.PositionId == id).ToListAsync();

            if (employeepositions == null)
            {
                return NotFound();
            }

            return employeepositions.Select(oi => new PositionItemViewDTO { PositionId = oi.PositionId, EmployeeName = oi.Employee.EmployeeName, Address = oi.Employee.Address,Salary=oi.Employee.Salary,IsContinue=oi.Employee.IsContinue }).ToList();
        }
        [HttpGet("OI/{id}")]
        public async Task<ActionResult<IEnumerable<EmployeePosition>>> GetAgentTourPackageOf(int id)
        {
            if (_context.EmployeePositions == null)
            {
                return NotFound();
            }
            var employeePositions = await _context.EmployeePositions.Where(oi => oi.PositionId == id).ToListAsync();

            if (employeePositions == null)
            {
                return NotFound();
            }

            return employeePositions;
        }
        // PUT: api/Positions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosition(int id, Position position)
        {
            if (id != position.PositionId)
            {
                return BadRequest();
            }

            _context.Entry(position).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionExists(id))
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
        [HttpPut("DTO/{id}")]
        public async Task<IActionResult> PutOrderWithOrderItem(int id, PositionEditDTO position)
        {
            if (id != position.PositionId)
            {
                return BadRequest();
            }
            var existing = await _context.Positions.Include(x => x.EmployeePositions).FirstAsync(o => o.PositionId == id);
            _context.EmployeePositions.RemoveRange(existing.EmployeePositions);
            existing.PositionId = position.PositionId;
            existing.PositionName = position.PositionName;
            foreach (var item in position.PositionItemDTOs)
            {
                _context.EmployeePositions.Add(new EmployeePosition { PositionId = position.PositionId, EmployeeId = item.EmployeeId });
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.InnerException?.Message);

            }

            return NoContent();
        }
        // POST: api/Positions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Position>> PostPosition(Position position)
        {
          if (_context.Positions == null)
          {
              return Problem("Entity set 'EmployeeDbContext.Positions'  is null.");
          }
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosition", new { id = position.PositionId }, position);
        }
        [HttpPost("DTO")]
        public async Task<ActionResult<Position>> PostOrderDTO(PositionDTO dto)
        {
            if (_context.Positions == null)
            {
                return Problem("Entity set 'ProductDbContext.Orders'  is null.");
            }
            var position = new Position { PositionName=dto.PositionName };
            foreach (var oi in dto.PositionItemDTOs)
            {
                position.EmployeePositions.Add(new EmployeePosition { EmployeeId = oi.EmployeeId });
            }
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();

            return position;
        }
        // DELETE: api/Positions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            if (_context.Positions == null)
            {
                return NotFound();
            }
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionExists(int id)
        {
            return (_context.Positions?.Any(e => e.PositionId == id)).GetValueOrDefault();
        }
    }
}
