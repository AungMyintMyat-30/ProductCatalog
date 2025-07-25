using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Repositories
{
    public class BrandRepo
    {
        private readonly string? _connectionString;
        private readonly ILogger<BrandRepo> _logger;

        private const string SelectAllQuery = "SELECT * FROM Brand WHERE DeletedDate IS NULL";
        private const string SelectByIdQuery = "SELECT * FROM Brand WHERE BrandId = @Id";
        private const string InsertQuery = @"
            INSERT INTO Brand (BrandName, CreatedDate, CreatedUser) 
            VALUES (@BrandName, @CreatedDate, @CreatedUser); 
            SELECT CAST(SCOPE_IDENTITY() as int)";
        private const string UpdateQuery = @"
            UPDATE Brand 
            SET BrandName = @BrandName, UpdatedDate = @UpdatedDate, UpdatedUser = @UpdatedUser 
            WHERE BrandId = @BrandId";
        private const string DeleteQuery = @"
            UPDATE Brand 
            SET DeletedDate = @DeletedDate, DeletedUser = @DeletedUser 
            WHERE BrandId = @BrandId";
        private const string CheckProductByBrandIdQuery = "SELECT * FROM Product WHERE BrandId = @Id";

        public BrandRepo(IConfiguration configuration,
                         ILogger<BrandRepo> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public async Task<List<Brand>> GetAllBrand()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<Brand>(SelectAllQuery);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(GetAllBrand));
                throw;
            }
        }

        public async Task<int> AddBrand(Brand brand)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.ExecuteScalarAsync<int>(InsertQuery, brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(AddBrand));
                throw;
            }
        }

        public async Task<Brand?> GetBrandById(string id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryFirstOrDefaultAsync<Brand>(SelectByIdQuery, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(GetBrandById));
                throw;
            }
        }

        public async Task<int> UpdateBrand(Brand brand)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.ExecuteAsync(UpdateQuery, brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(UpdateBrand));
                throw;
            }
        }

        public async Task<Product?> CheckBrandById(string id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryFirstOrDefaultAsync<Product>(CheckProductByBrandIdQuery, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(CheckBrandById));
                throw;
            }
        }

        public async Task<int> DeleteBrand(Brand brand)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.ExecuteAsync(DeleteQuery, brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(DeleteBrand));
                throw;
            }
        }
    }
}
