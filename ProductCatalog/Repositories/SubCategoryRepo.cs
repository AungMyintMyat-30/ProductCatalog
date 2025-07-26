using Dapper;
using Microsoft.Data.SqlClient;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Repositories
{
    public class SubCategoryRepo
    {
        private readonly string? _connectionString;
        private readonly ILogger<SubCategoryRepo> _logger;

        public SubCategoryRepo(IConfiguration configuration, ILogger<SubCategoryRepo> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public async Task<List<ViSubCategory>> GetAllSubCategories()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<ViSubCategory>("SELECT * FROM VI_SubCategory WHERE DeletedDate IS NULL");
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(GetAllSubCategories));
                throw;
            }
        }

        public async Task<int> AddSubCategory(SubCategory subcategory)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.ExecuteScalarAsync<int>(@"
                INSERT INTO SubCategory (CatId,SubName, CreatedDate, CreatedUser) 
                VALUES (@CatId, @SubName, @CreatedDate, @CreatedUser); 
                SELECT CAST(SCOPE_IDENTITY() as int)", subcategory); ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(AddSubCategory));
                throw;
            }
        }

        public async Task<ViSubCategory?> GetSubCategoryBySubId(string id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryFirstOrDefaultAsync<ViSubCategory>("SELECT * FROM VI_SubCategory WHERE SubId = @Id", new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(GetSubCategoryBySubId));
                throw;
            }
        }

        public async Task<List<ViSubCategory>> GetSubCategoryByCatId(string id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<ViSubCategory>("SELECT * FROM VI_SubCategory WHERE CatId = @Id", new { Id = id });
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(GetSubCategoryByCatId));
                throw;
            }
        }

        public async Task<int> UpdateSubCategory(SubCategory subcategory)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.ExecuteAsync(@"
                UPDATE SubCategory 
                SET SubName=@SubName,CatId=@CatId,UpdatedDate=@UpdatedDate,UpdatedUser=@UpdatedUser 
                WHERE SubId=@SubId", subcategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(UpdateSubCategory));
                throw;
            }
        }

        public async Task<Product?> CheckSubCategoryById(string id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Product WHERE SubId = @Id", new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(CheckSubCategoryById));
                throw;
            }
        }

        public async Task<int> DeleteSubCategory(SubCategory subcategory)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.ExecuteAsync(@"
                UPDATE SubCategory 
                SET DeletedDate=@DeletedDate,DeletedUser=@DeletedUser 
                WHERE SubId=@SubId", subcategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(DeleteSubCategory));
                throw;
            }
        }
    }
}
