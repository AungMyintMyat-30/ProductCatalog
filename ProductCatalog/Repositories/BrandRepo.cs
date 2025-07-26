using Dapper;
using Microsoft.Data.SqlClient;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Repositories
{
    public class BrandRepo
    {
        private readonly string? _connectionString;
        private readonly ILogger<BrandRepo> _logger;

        private const string CheckProductByBrandIdQuery = "SELECT * FROM Product WHERE BrandId = @Id";

        public BrandRepo(IConfiguration configuration,
                         ILogger<BrandRepo> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public async Task<List<Brand>> GetAllBrands()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<Brand>("SELECT * FROM Brand WHERE DeletedDate IS NULL");
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(GetAllBrands));
                throw;
            }
        }

        public async Task<int> AddBrand(Brand brand)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.ExecuteScalarAsync<int>(@"
                INSERT INTO Brand (BrandName, CreatedDate, CreatedUser) 
                VALUES (@BrandName, @CreatedDate, @CreatedUser); 
                SELECT CAST(SCOPE_IDENTITY() as int)", brand);
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
                return await connection.QueryFirstOrDefaultAsync<Brand>("SELECT * FROM Brand WHERE BrandId = @Id", new { Id = id });
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
                return await connection.ExecuteAsync(@"
                UPDATE Brand 
                SET BrandName = @BrandName, UpdatedDate = @UpdatedDate, UpdatedUser = @UpdatedUser 
                WHERE BrandId = @BrandId", brand);
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
                return await connection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Product WHERE BrandId = @Id", new { Id = id });
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
                return await connection.ExecuteAsync(@"
                UPDATE Brand 
                SET DeletedDate = @DeletedDate, DeletedUser = @DeletedUser 
                WHERE BrandId = @BrandId", brand);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(DeleteBrand));
                throw;
            }
        }
    }
}
