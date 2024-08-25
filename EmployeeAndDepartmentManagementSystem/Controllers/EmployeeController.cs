using Microsoft.AspNetCore.Mvc;
using EmployeeAndDepartmentManagementSystem.Models;
using System.Threading.Tasks;

namespace EmployeeAndDepartmentManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepo;
        private readonly DepartmentRepository _departmentRepo;

        public EmployeeController(EmployeeRepository employeeRepository, DepartmentRepository departmentRepo)
        {
            _employeeRepo = employeeRepository;
            _departmentRepo = departmentRepo;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepo.GetAllEmployeesAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _employeeRepo.AddEmployeeAsync(employee);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepo.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee, string action)
        {
            if (action == "Save")
            {
                await _employeeRepo.UpdateEmployeeAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            else if (action == "Delete")
            {
                await _employeeRepo.DeleteEmployeeAsync(employee.Id);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepo.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeRepo.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeRepo.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
    }
}
