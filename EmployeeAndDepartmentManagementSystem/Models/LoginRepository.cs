using Microsoft.Data.SqlClient;

namespace EmployeeAndDepartmentManagementSystem.Models
{
    public class LoginRepository
    {
        private readonly string _connectionString;

        public LoginRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool ValidateUser(string username, string password)
        {
            bool isValid=false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(1) FROM LoginPassword WHERE Username = @Username AND [Password] = @Password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count == 1)
                {
                    isValid = true;
                }
            }

            return isValid;
        }
    }
}

     