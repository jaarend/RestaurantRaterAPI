using Microsoft.AspNetCore.Mvc;
using RestaurantRaterAPI.Data;

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
}