using EmployeeAndDepartmentManagementSystem.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeAndDepartmentManagementSystem.Models
{
    public class DepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            var departments = new List<Department>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Departments", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            departments.Add(new Department
                            {
                                DepartmentId = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            return departments;
        }
    }
}
