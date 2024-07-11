using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.EmployeeDtos;
using RealEstate_Dapper_UI.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.EmployeeRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context _context;

        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        public async void CreateEmployee(CreateEmployeeDto employeeDto)
        {
            string query = "insert into Employee (Name, Title, Mail, PhoneNumber, ImageUrl, Status) values (@name,@title,@mail,@phoneNumber,@imageUrl,@status)";
            var parameters = new DynamicParameters();
            parameters.Add("@name", employeeDto.Name);
            parameters.Add("@title", employeeDto.Title);
            parameters.Add("@mail", employeeDto.Mail);
            parameters.Add("@phoneNumber", employeeDto.PhoneNumber);
            parameters.Add("@imageUrl", employeeDto.ImageUrl);
            parameters.Add("@status", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteEmployee(int id)
        {
            string query = "Delete from Employee Where EmployeeID=@employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultEmployeeDto>> GetAllEmployeeAsync()
        {
            string query = "Select * from Employee";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdEmployeeDto> GetEmployee(int id)
        {
            string query = "select * from Employee where EmployeeID=@employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("employeeId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIdEmployeeDto>(query, parameters);
                return values;
            }
        }

        public async void UpdateEmployee(UpdateEmployeeDto updateEmployee)
        {
            string query = "Update Employee Set Name=@name, Title=@title, Mail=@mail, PhoneNumber=@phoneNumber, ImageUrl=@imageUrl, Status=@status where EmployeeID=@employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("@name", updateEmployee.Name);
            parameters.Add("@title", updateEmployee.Title);
            parameters.Add("@mail", updateEmployee.Mail);
            parameters.Add("@phoneNumber", updateEmployee.PhoneNumber);
            parameters.Add("@imageUrl", updateEmployee.ImageUrl);
            parameters.Add("@status", updateEmployee.Status);
            parameters.Add("@employeeId", updateEmployee.EmployeeID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
