using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TennisBookings.Areas.Admin;

[Area("Admin")]
[Route("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
