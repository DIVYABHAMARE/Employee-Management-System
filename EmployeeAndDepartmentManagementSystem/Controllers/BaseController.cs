using Microsoft.AspNetCore.Mvc;

namespace EmployeeAndDepartmentManagementSystem.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            RedirectToAction("Login", "Login");
        }
    }
}
