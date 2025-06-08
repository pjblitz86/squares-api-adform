using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using squares_api_adform.Data;
using squares_api_adform.Services;
using System.Diagnostics;

namespace squares_api_adform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SquaresController(ApplicationDbContext context, SquareService squareService, ILogger<SquaresController> logger) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        private readonly SquareService _squareService = squareService;
        private readonly ILogger<SquaresController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetSquares()
        {
            var stopwatch = Stopwatch.StartNew();
            var points = await _context.Points.ToListAsync();
            var squares = _squareService.FindSquares(points);

            // manual SLI logging
            stopwatch.Stop();
            _logger.LogInformation("GET /api/squares processed in {ElapsedMilliseconds} ms, squares found: {Count}",
                stopwatch.ElapsedMilliseconds, squares.Count);

            // Return a JSON response
            return Ok(new { squares.Count, Squares = squares });
        }
    }
}
