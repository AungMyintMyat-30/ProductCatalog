using Dapper;
using Microsoft.Data.SqlClient;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Repositories
{
    public class CategoryRepo
    {
        private readonly string? _connectionString;

        public CategoryRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Category>> GetAllCategory()
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryAsync<Category>("SELECT * FROM Category WHERE DeletedDate IS NULL");
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving categories.", ex);
            }
        }

        public async Task<int> AddCategory(Category category)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.ExecuteScalarAsync<int>("INSERT INTO Category (CatName, CreatedDate, CreatedUser) VALUES (@CatName, @CreatedDate, @CreatedUser); SELECT CAST(SCOPE_IDENTITY() as int)", category); ;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving category.", ex);
            }
        }

        public async Task<Category> GetCategoryById(string id)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryFirstOrDefaultAsync<Category>("SELECT * FROM Category WHERE CatId = @Id", new { Id = id });
                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving categories.", ex);
            }
        }

        public async Task<int> UpdateCategory(Category category)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.ExecuteAsync("UPDATE Category SET CatName=@CatName,UpdatedDate=@UpdatedDate,UpdatedUser=@UpdatedUser WHERE CatId=@CatId", category);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving category.", ex);
            }
        }

        public async Task<SubCategory> CheckCategoryById(string id)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryFirstOrDefaultAsync<SubCategory>("SELECT * FROM SubCategory WHERE CatId = @Id", new { Id = id });
                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving categories.", ex);
            }
        }

        public async Task<int> DeleteCategory(Category category)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.ExecuteAsync("UPDATE Category SET DeletedDate=@DeletedDate,DeletedUser=@DeletedUser WHERE CatId=@CatId", category);
                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving categories.", ex);
            }
        }
    }
}
