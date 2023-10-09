using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantRaterApi.Data;

public class RestaurantListItem
{
    public int Id{get; set;}
    public string Name { get; set; }
    public string Location { get; set; }
    public double AverageRating { get; set; }
}