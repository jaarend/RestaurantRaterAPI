using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestaurantRaterAPI.Data;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterApi.Data;

namespace RestaurantRaterAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly RestaurantDbContext _context;
    public RestaurantController(RestaurantDbContext context)
    {
        _context = context;
    }
    //Async POST Endpoint
    [HttpPost]
    public async Task<IActionResult> PostRestaurant([FromBody] Restaurant request)
    {
        if (ModelState.IsValid)
        {
            _context.Restaurants.Add(request);
            await _context.SaveChangesAsync();
            return Ok();
        }

        return BadRequest(ModelState);
    }


    //Async GET Endpoint
    public async Task<IActionResult> GetRestaurants()
    {

        var restaurants = await _context.Restaurants.Include(r => r.Ratings).ToListAsync();
        List<RestaurantListItem> restaurantList = restaurants.Select(r => new RestaurantListItem() {
            Id = r.Id,
            Name = r.Name,
            Location = r.Location,
            AverageRating = r.AverageRating,
        }).ToList();

        return Ok(restaurantList);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRestaurantById(int id)
    {
        Restaurant? restaurant = await _context.Restaurants.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.Id == id);
        if (restaurant is null)
        {
            return NotFound();
        }

        return Ok(restaurant);
    }

    //PUT Endpoint (Update)
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> PutRestaurant([FromForm] Restaurant request)
    {
        var oldRestaurant = await _context.Restaurants.FindAsync(request.Id);
        if(oldRestaurant == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        Restaurant? restaurant = await _context.Restaurants.FindAsync(request.Id);

        if (restaurant is null)
        {
            return NotFound();
        }

        restaurant.Name = request.Name;
        restaurant.Location = request.Location;

        _context.Restaurants.Update(restaurant);

        await _context.SaveChangesAsync();
        return Ok();
    }

    //DELETE endpoint
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);
        if (restaurant is null)
        {
            return NotFound();
        }

        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
        return Ok();
    }
}