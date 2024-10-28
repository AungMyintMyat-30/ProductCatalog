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

        public async Task<List<ViProduct>> GetAllProduct()
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryAsync<ViProduct>("SELECT * FROM VI_Product WHERE DeletedDate IS NULL ORDER BY CreatedDate DESC");
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<bool> AddProduct(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                string sql = @"INSERT INTO Product (ProductId, SubId, BrandId, Code, ProductName, Price, Description, ImgUrl, CreatedDate, CreatedUser)
                       VALUES (@ProductId, @SubId, @BrandId, @Code, @ProductName, @Price, @Description, @ImgUrl, @CreatedDate, @CreatedUser)";

                int rowsAffected = await connection.ExecuteAsync(sql, product);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<ViProduct> GetProductById(string id)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryFirstOrDefaultAsync<ViProduct>("SELECT * FROM VI_Product WHERE ProductId = @Id", new { Id = id });
                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                string sql = @"UPDATE Product SET SubId=@SubId, BrandId=@BrandId, Code=@Code, ProductName=@ProductName, Price=@Price,
                               Description=@Description, ImgUrl=@ImgUrl, UpdatedDate=@UpdatedDate, UpdatedUser=@UpdatedUser WHERE ProductId=@ProductId";

                int rowsAffected = await connection.ExecuteAsync(sql, product);

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<int> DeleteProduct(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.ExecuteAsync("UPDATE Product SET DeletedDate=@DeletedDate,DeletedUser=@DeletedUser WHERE ProductId=@ProductId", product);
                return result!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }
    }
}
