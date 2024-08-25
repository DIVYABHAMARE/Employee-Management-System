using Microsoft.AspNetCore.Mvc;

namespace EmployeeAndDepartmentManagementSystem.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
