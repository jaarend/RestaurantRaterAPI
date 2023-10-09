using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterAPI.Data;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterApi.Data;

namespace RestaurantRaterAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RatingController : ControllerBase
{
    private readonly ILogger<RatingController> _logger;
    private readonly RestaurantDbContext _context;
    public RatingController(ILogger<RatingController> logger, RestaurantDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> RateRestaurant([FromBody] Rating model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        _context.Ratings.Add(new Rating() {
            Score = model.Score,
            RestaurantId = model.RestaurantId,
        });
        
        await _context.SaveChangesAsync();
        
        return Ok();
    }
}