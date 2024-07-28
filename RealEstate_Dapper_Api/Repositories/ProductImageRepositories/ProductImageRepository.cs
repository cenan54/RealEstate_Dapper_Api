using Dapper;
using RealEstate_Dapper_Api.Dtos.ProductImageDtos;
using RealEstate_Dapper_UI.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ProductImageRepositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly Context _context;

        public ProductImageRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<GetProductImageByProductIdDto>> GetProductImageByProductId(int id)
        {
            string query = "select * from ProductImage where ProductID=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductImageByProductIdDto>(query, parameters);
                return values.ToList();
            }
        }
    }
}
