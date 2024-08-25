using EmployeeAndDepartmentManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace EmployeeAndDepartmentManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository _loginRepository;
        public LoginController(LoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
            
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();  
        }
        [HttpPost]
        public IActionResult Login(LoginPassword login)
        {
            if (ModelState.IsValid)
            {
                bool isValid = _loginRepository.ValidateUser(login.Username, login.Password);

                if (isValid)
                {
                    HttpContext.Session.SetString("User", login.Username);
                    TempData["Message"] = "Login successful!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "Invalid username or password.";
                }
            }

            return View(login);
        }

    }

}

