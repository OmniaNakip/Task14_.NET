using Microsoft.AspNetCore.Mvc;
using Movie_Management_System.Services.Interfaces;

namespace Movie_Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public HomeController(
            IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard =
                await _dashboardService.GetDashboardAsync();

            return View(dashboard);
        }
    }
}