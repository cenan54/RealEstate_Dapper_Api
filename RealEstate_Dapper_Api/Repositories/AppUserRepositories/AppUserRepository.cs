using Dapper;
using RealEstate_Dapper_Api.Dtos.AppUserDtos;
using RealEstate_Dapper_UI.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.AppUserRepositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly Context _context;

        public AppUserRepository(Context context)
        {
            _context = context;
        }
        public async Task<GetAppUserByProductId> GetAppUserByProductId(int id)
        {
            string query = "select * from AppUser where UserID=@userId";
            var parameters = new DynamicParameters();
            parameters.Add("@userId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetAppUserByProductId>(query, parameters);
                return values;
            }
        }
    }
}
