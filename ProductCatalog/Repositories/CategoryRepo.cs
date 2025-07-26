using Dapper;
using Microsoft.Data.SqlClient;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Repositories
{
    public class CategoryRepo
    {
        private readonly string? _connectionString;
        private readonly ILogger<CategoryRepo> _logger;

        public CategoryRepo(IConfiguration configuration, ILogger<CategoryRepo> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                var result = await connection.QueryAsync<Category>("SELECT * FROM Category WHERE DeletedDate IS NULL");
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(GetAllCategories));
                throw;
            }
        }

        public async Task<int> AddCategory(Category category)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.ExecuteScalarAsync<int>(@"
                INSERT INTO Category (CatName, CreatedDate, CreatedUser)
                VALUES (@CatName, @CreatedDate, @CreatedUser);
                SELECT CAST(SCOPE_IDENTITY() as int)", category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(AddCategory));
                throw;
            }
        }

        public async Task<Category?> GetCategoryById(string id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryFirstOrDefaultAsync<Category>("SELECT * FROM Category WHERE CatId = @Id", new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(GetCategoryById));
                throw;
            }
        }

        public async Task<int> UpdateCategory(Category category)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.ExecuteAsync(@"
                UPDATE Category 
                SET CatName = @CatName, UpdatedDate = @UpdatedDate, UpdatedUser = @UpdatedUser 
                WHERE CatId = @CatId", category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(UpdateCategory));
                throw;
            }
        }

        public async Task<SubCategory?> CheckCategoryById(string id)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryFirstOrDefaultAsync<SubCategory>("SELECT * FROM SubCategory WHERE CatId = @Id", new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(CheckCategoryById));
                throw;
            }
        }

        public async Task<int> DeleteCategory(Category category)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.ExecuteAsync(@"
                UPDATE Category 
                SET DeletedDate = @DeletedDate, DeletedUser = @DeletedUser 
                WHERE CatId = @CatId", category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(DeleteCategory));
                throw;
            }
        }
    }
}
