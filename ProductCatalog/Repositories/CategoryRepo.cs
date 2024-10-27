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

        public async Task<List<Category>> GetAllcategory()
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result= await connection.QueryAsync<Category>("SELECT * FROM Category");
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

        public async Task<List<SubCategory>> CheckCategory(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryAsync<SubCategory>("SELECT * FROM SubCategory WHERE CatId = @Id", new { Id = id });
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving categories.", ex);
            }
        }
    }
}
