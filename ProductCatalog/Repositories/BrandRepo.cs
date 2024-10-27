using Dapper;
using Microsoft.Data.SqlClient;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Repositories
{
    public class BrandRepo
    {
        private readonly string? _connectionString;

        public BrandRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Brand>> GetAllBrand()
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryAsync<Brand>("SELECT * FROM Brand WHERE DeletedDate IS NULL");
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving brands.", ex);
            }
        }

        public async Task<int> AddBrand(Brand brand)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.ExecuteScalarAsync<int>("INSERT INTO Brand (BrandName, CreatedDate, CreatedUser) VALUES (@BrandName, @CreatedDate, @CreatedUser); SELECT CAST(SCOPE_IDENTITY() as int)", brand); ;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving category.", ex);
            }
        }

        public async Task<Brand> GetBrandById(string id)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryFirstOrDefaultAsync<Brand>("SELECT * FROM Brand WHERE BrandId = @Id", new { Id = id });
                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving brands.", ex);
            }
        }

        public async Task<int> UpdateBrand(Brand brand)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.ExecuteAsync("UPDATE Brand SET BrandName=@BrandName,UpdatedDate=@UpdatedDate,UpdatedUser=@UpdatedUser WHERE BrandId=@BrandId", brand);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving brands.", ex);
            }
        }

        public async Task<Product> CheckBrandById(string id)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Product WHERE BrandId = @Id", new { Id = id });
                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving brands.", ex);
            }
        }

        public async Task<int> DeleteBrand(Brand brand)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.ExecuteAsync("UPDATE Brand SET DeletedDate=@DeletedDate,DeletedUser=@DeletedUser WHERE BrandId=@BrandId", brand);
                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving brands.", ex);
            }
        }
    }
}
