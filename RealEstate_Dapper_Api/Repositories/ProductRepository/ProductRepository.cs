using Dapper;
using RealEstate_Dapper_Api.Dtos.CategoryDtos;
using RealEstate_Dapper_Api.Dtos.ProductDetailDtos;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_UI.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {

        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "Select * from Product";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync()
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay from Product Inner Join Category on Product.ProductCategory = Category.CategoryID";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync()
        {
            string query = "select top(5) ProductID, Title, Price, City, District, ProductCategory, CategoryName, AdvertisementDate from Product Inner Join Category on Product.ProductCategory=Category.CategoryID where Type like lower('%kiralık%') order by ProductID desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast5ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertsListByEmployeeByTrueAsync(int id)
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay from Product Inner Join Category on Product.ProductCategory = Category.CategoryID where EmployeeID=@employeeId and ProductStatus=1";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query,parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertsListByEmployeeByFalseAsync(int id)
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay from Product Inner Join Category on Product.ProductCategory = Category.CategoryID where EmployeeID=@employeeId and ProductStatus=0";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            string query = "Update Product Set DealOfTheDay=0 where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("productID", id);

            using(var connection = _context.CreateConnection())
            {
                await connection.QueryAsync(query, parameters);
            }
        }

        public async Task ProductDealOfTheDayStatusChangeToTrue(int id)
        {
            string query = "Update Product Set DealOfTheDay=1 where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("productID", id);

            using (var connection = _context.CreateConnection())
            {
                await connection.QueryAsync(query, parameters);
            }
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            string query = "insert into Product (Title, Price, City, District, CoverImage, Address, Description, Type, DealOfTheDay, AdvertisementDate, ProductStatus, ProductCategory, EmployeeID) values (@Title, @Price, @City, @District, @CoverImage, @Address, @Description, @Type, @DealOfTheDay, @AdvertisementDate, @ProductStatus, @ProductCategory, @EmployeeID)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", createProductDto.Title);
            parameters.Add("@Price", createProductDto.Price);
            parameters.Add("@City", createProductDto.City);
            parameters.Add("@District", createProductDto.District);
            parameters.Add("@CoverImage", createProductDto.CoverImage);
            parameters.Add("@Address", createProductDto.Address);
            parameters.Add("@Description", createProductDto.Description);
            parameters.Add("@DealOfTheDay", createProductDto.DealOfTheDay);
            parameters.Add("@AdvertisementDate", createProductDto.AdvertisementDate);
            parameters.Add("@ProductStatus", createProductDto.ProductStatus);
            parameters.Add("@ProductCategory", createProductDto.ProductCategory);
            parameters.Add("@EmployeeID", createProductDto.EmployeeID);
            parameters.Add("@Type", createProductDto.Type);
           

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<GetProductByProductIdDto> GetProductByProductId(int id)
        {
            string query = "Select ProductID, Title, Price, City, District, CategoryName, CoverImage, Type, Address, DealOfTheDay from Product Inner Join Category on Product.ProductCategory = Category.CategoryID where ProductID=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductByProductIdDto>(query,parameters);
                return values.FirstOrDefault();
            }
        }

        public async Task<GetProductDetailByIdDto> GetProductDetailByProductId(int id)
        {
            string query = "Select * from ProductDetail where ProductID=@productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductDetailByIdDto>(query, parameters);
                return values.FirstOrDefault();
            }
        }
    }
}
