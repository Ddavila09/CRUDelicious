using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext db;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        List<Dish> allDishes = db.Dishes.ToList();
        return View("Index", allDishes);
    }

    // -------------------------------------------------------
    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        return View("New");
    }

    // -------------------------------------------------------
    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (!ModelState.IsValid)
        {
            return View("New");
        }
        db.Dishes.Add(newDish);
        db.SaveChanges();

        return RedirectToAction("Index");
    }
    // -------------------------------------------------------
    [HttpGet("dishes/{id}")]
    public IActionResult ViewDish(int id)
    {
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == id);

        if (dish == null)
        {
            return RedirectToAction("Index");
        }
        return View("Details", dish);
    }
    // -------------------------------------------------------
    [HttpPost("dishes/{dishId}/delete")]
    public IActionResult DeleteDish(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if (dish != null)
        {
            db.Dishes.Remove(dish);
            db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    // -------------------------------------------------------
        [HttpGet("dishes/{dishId}/edit")]
    public IActionResult Edit (int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if (dish == null)
        {
            return RedirectToAction("Index");
        }
        return View("Edit", dish);
    }
// -------------------------------------------------------
    [HttpPost("dishes/{dishId}/edit")]
    public IActionResult UpdateDish(int dishId, Dish updatedDish)
    {
        if (!ModelState.IsValid)
        {
            // Post? originalPost = db.Posts.FirstOrDefault(post => post.PostId == postId);
            // return View("Edit", originalPost);
            // we can replace the previous two lines with the following one. 
            // this runs the code within the Edit function, without creating a new req/res cycle
            // for this to work, the View() function in the Edit method !!!!!!CANNOT!!!!! default the .cshtml file
            return Edit(dishId);
        }

        Dish? dbDish = db.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
        if (dbDish == null)
        {
            return RedirectToAction("Index");
        }

        dbDish.UserName = updatedDish.UserName;
        dbDish.DishName = updatedDish.DishName;
        dbDish.Calories = updatedDish.Calories;
        dbDish.Tastiness = updatedDish.Tastiness;
        dbDish.Description = updatedDish.Description;
        dbDish.ImgUrl = updatedDish.ImgUrl;
        dbDish.UpdatedAt = DateTime.Now;

        db.SaveChanges();

        return RedirectToAction("ViewDish", new { id = dbDish.DishId});
    }
    //-------------------------------------------------------
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
