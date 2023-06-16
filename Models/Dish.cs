#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models;

public class Dish
{
    [Key] //Primare key
    public int DishId { get; set; }


    [Required]
    [MinLength(2, ErrorMessage = "must be at least 2 characters")]
    [MaxLength(30, ErrorMessage = "can't be longer than characters.")]
    public string UserName { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "must be at least 2 characters")]
    public string DishName { get; set; }

    [Required]
    [Range(0,1000)]
    public int Calories { get; set; }

    [Required]
    [Range(0,10)]
    public int Tastiness { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "must be at least 2 characters")]
    [MaxLength(500, ErrorMessage = "Max is 500 characters")]
    public string Description { get; set; }

    [Display(Name = "Image URL")]
    public string? ImgUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}
