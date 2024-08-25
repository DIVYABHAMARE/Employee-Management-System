using EmployeeAndDepartmentManagementSystem.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeAndDepartmentManagementSystem.Models
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var employees = new List<Employee>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Employees", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new Employee
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                JobTitle = reader.GetString(2),
                                WorkPhone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                CellPhone = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Manager = reader.GetString(5),
                                PrimaryDepartmentId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            Employee employee = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Employees WHERE Id = @EmployeeId", connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            employee = new Employee
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                JobTitle = reader.GetString(2),
                                WorkPhone = reader.GetString(3),
                                CellPhone = reader.GetString(4),
                                Manager = reader.GetString(5),
                                PrimaryDepartmentId = reader.IsDBNull(6) ? (int?)null : reader.GetInt32(6)
                            };
                        }
                    }
                }
            }
            return employee;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(
                    "INSERT INTO Employees (Name, JobTitle, WorkPhone, CellPhone, Manager, PrimaryDepartmentId) " +
                    "VALUES (@Name, @JobTitle, @WorkPhone, @CellPhone, @Manager, @PrimaryDepartmentId)", connection))
                {
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@JobTitle", employee.JobTitle);
                    command.Parameters.AddWithValue("@WorkPhone", employee.WorkPhone);
                    command.Parameters.AddWithValue("@CellPhone", employee.CellPhone);
                    command.Parameters.AddWithValue("@Manager", employee.Manager);
                    command.Parameters.AddWithValue("@PrimaryDepartmentId", employee.PrimaryDepartmentId.HasValue ? (object)employee.PrimaryDepartmentId.Value : DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(
                    "UPDATE Employees SET Name = @Name, JobTitle = @JobTitle, WorkPhone = @WorkPhone, CellPhone = @CellPhone, Manager = @Manager, PrimaryDepartmentId = @PrimaryDepartmentId " +
                    "WHERE Id = @EmployeeId", connection))
                {
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@JobTitle", employee.JobTitle);
                    command.Parameters.AddWithValue("@WorkPhone", employee.WorkPhone);
                    command.Parameters.AddWithValue("@CellPhone", employee.CellPhone);
                    command.Parameters.AddWithValue("@Manager", employee.Manager);
                    command.Parameters.AddWithValue("@PrimaryDepartmentId", employee.PrimaryDepartmentId.HasValue ? (object)employee.PrimaryDepartmentId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@EmployeeId", employee.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task DeleteEmployeeAsync(int employeeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("DELETE FROM Employees WHERE Id = @EmployeeId", connection))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}



