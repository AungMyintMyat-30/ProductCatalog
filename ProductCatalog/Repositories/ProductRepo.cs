using Dapper;
using Microsoft.Data.SqlClient;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Repositories
{
    public class ProductRepo
    {
        private readonly string? _connectionString;
        private readonly ILogger<ProductRepo> _logger;

        public ProductRepo(IConfiguration configuration, ILogger<ProductRepo> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public async Task<List<ViProduct>> GetAllProducts()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<ViProduct>("SELECT * FROM VI_Product WHERE DeletedDate IS NULL ORDER BY CreatedDate DESC");
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(GetAllProducts));
                throw;
            }
        }

        public async Task<bool> AddProduct(Product product)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                string sql = @"
                INSERT INTO Product (ProductId, SubId, BrandId, Code, ProductName, 
                Price, Description, ImgUrl, CreatedDate, CreatedUser)
                VALUES (@ProductId, @SubId, @BrandId, @Code, @ProductName, 
                @Price, @Description, @ImgUrl, @CreatedDate, @CreatedUser)";

                int rowsAffected = await connection.ExecuteAsync(sql, product);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(AddProduct));
                throw;
            }
        }

        public async Task<ViProduct?> GetProductById(string id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryFirstOrDefaultAsync<ViProduct>("SELECT * FROM VI_Product WHERE ProductId = @Id", new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(GetProductById));
                throw;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                string sql = @"
                UPDATE Product SET SubId=@SubId, BrandId=@BrandId, Code=@Code, ProductName=@ProductName, Price=@Price,
                Description=@Description, ImgUrl=@ImgUrl, UpdatedDate=@UpdatedDate, UpdatedUser=@UpdatedUser 
                WHERE ProductId=@ProductId";

                int rowsAffected = await connection.ExecuteAsync(sql, product);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(UpdateProduct));
                throw;
            }
        }

        public async Task<int> DeleteProduct(Product product)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.ExecuteAsync(@"
                UPDATE Product SET DeletedDate=@DeletedDate,DeletedUser=@DeletedUser 
                WHERE ProductId=@ProductId", product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(DeleteProduct));
                throw;
            }
        }
    }
}
