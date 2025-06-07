using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using squares_api_adform.Data;
using squares_api_adform.Services;

namespace squares_api_adform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SquaresController(ApplicationDbContext context, SquareService squareService) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly SquareService _squareService = squareService;

        [HttpGet]
        public async Task<IActionResult> GetSquares()
        {
            var points = await _context.Points.ToListAsync();
            var squares = _squareService.FindSquares(points);

            // Return a JSON response
            return Ok(new { squares.Count, Squares = squares });
        }
    }
}
