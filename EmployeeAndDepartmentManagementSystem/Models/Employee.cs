namespace EmployeeAndDepartmentManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }

        public string Manager { get; set; }
        public int? PrimaryDepartmentId { get; set; }  // Changed to nullable int

        public Department PrimaryDepartment { get; set; }
        public ICollection<Department> SecondaryDepartments { get; set; }
    }
}
