using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.LoginDtos;
using RealEstate_Dapper_Api.Tools;
using RealEstate_Dapper_UI.Models.DapperContext;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Context _context;

        public LoginController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(CreateLoginDto loginDto)
        {
            string query = "Select * from AppUser where Username=@username and Password=@password";
            var parameters = new DynamicParameters();
            parameters.Add("@password",loginDto.Password);
            parameters.Add("@username",loginDto.Username);

            using (var connection=_context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<CreateLoginDto>(query, parameters);
                if (values != null)
                {
                    int UserId = values.UserID;
                    GetCheckAppUserViewModel model = new GetCheckAppUserViewModel();
                    model.Username = values.Username;
                    model.Id = UserId;
                    var token = JwtTokenGenerator.GenerateToken(model);
                    return Ok(token);
                }else
                {
                    return Ok("Başarısız");
                }
            }
        }
    }
}
