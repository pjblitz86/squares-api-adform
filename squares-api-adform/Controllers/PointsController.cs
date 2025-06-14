﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using squares_api_adform.Data;
using squares_api_adform.Models;

namespace squares_api_adform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PointsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/points
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Point>>> GetPoints()
        {
            return await _context.Points.ToListAsync();
        }

        // POST: api/points
        [HttpPost]
        public async Task<ActionResult<Point>> AddPoint(Point point)
        {
            _context.Points.Add(point);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPoints), new { id = point.Id }, point);
            }
            catch (DbUpdateException ex)
            {
                if (IsUniqueConstraintViolation(ex))
                {
                    return Conflict("A point with the same X and Y coordinates already exists.");
                }

                // Re-throw if not a duplicate error
                throw;
            }
        }

        // DELETE: api/points?x=1&y=2
        [HttpDelete]
        public async Task<IActionResult> DeletePoint([FromQuery] int x, [FromQuery] int y)
        {
            var point = await _context.Points.FirstOrDefaultAsync(p => p.X == x && p.Y == y);
            if (point == null)
            {
                return NotFound();
            }

            _context.Points.Remove(point);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/points/import
        [HttpPost("import")]
        public async Task<IActionResult> ImportPoints([FromBody] List<Point> newPoints)
        {
            var existingPoints = await _context.Points
                .Select(p => new { p.X, p.Y })
                .ToListAsync();

            var toInsert = newPoints
                .Where(p => !existingPoints.Any(ep => ep.X == p.X && ep.Y == p.Y))
                .ToList();

            if (toInsert.Count > 0)
            {
                _context.Points.AddRange(toInsert);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    if (IsUniqueConstraintViolation(ex))
                    {
                        return Conflict("One or more points already exist in the database.");
                    }

                    throw;
                }
            }

            return Ok(new { Inserted = toInsert.Count });
        }

        // Helper methods
        private bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            return ex.InnerException?.Message.Contains("duplicate key") == true;
        }
    }
}
