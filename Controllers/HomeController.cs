using Microsoft.AspNetCore.Mvc;
namespace flowers.web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View("~/Views/Home/Landing.cshtml");
    }
}
