using Microsoft.AspNetCore.Mvc;

namespace SignalR_TableDependency.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
