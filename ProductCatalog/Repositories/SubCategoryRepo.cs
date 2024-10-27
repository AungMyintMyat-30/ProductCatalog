using Dapper;
using Microsoft.Data.SqlClient;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Repositories
{
    public class SubCategoryRepo
    {
        private readonly string? _connectionString;

        public SubCategoryRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<ViSubCategory>> GetAllSubCategory()
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryAsync<ViSubCategory>("SELECT * FROM VI_SubCategory WHERE DeletedDate IS NULL");
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving subcategories.", ex);
            }
        }

        public async Task<int> AddSubCategory(SubCategory subcategory)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.ExecuteScalarAsync<int>("INSERT INTO SubCategory (CatId,SubName, CreatedDate, CreatedUser) VALUES (@CatId, @SubName, @CreatedDate, @CreatedUser); SELECT CAST(SCOPE_IDENTITY() as int)", subcategory); ;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving subcategory.", ex);
            }
        }

        //public async Task<SubCategory> GetSubCategoryById(string id)
        //{
        //    using var connection = new SqlConnection(_connectionString);
        //    try
        //    {
        //        var result = await connection.QueryFirstOrDefaultAsync<SubCategory>("SELECT * FROM SubCategory WHERE SubId = @Id", new { Id = id });
        //        return result!;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("An error occurred while retrieving subcategories.", ex);
        //    }
        //}

        public async Task<ViSubCategory> GetSubCategoryById(string id)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryFirstOrDefaultAsync<ViSubCategory>("SELECT * FROM VI_SubCategory WHERE SubId = @Id", new { Id = id });
                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving subcategories.", ex);
            }
        }

        public async Task<int> UpdateSubCategory(SubCategory subcategory)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.ExecuteAsync("UPDATE SubCategory SET SubName=@SubName,CatId=@CatId,UpdatedDate=@UpdatedDate,UpdatedUser=@UpdatedUser WHERE SubId=@SubId", subcategory);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving subcategories.", ex);
            }
        }

        public async Task<Product> CheckSubCategoryById(string id)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryFirstOrDefaultAsync<Product>("SELECT * FROM Product WHERE SubId = @Id", new { Id = id });
                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving subcategories.", ex);
            }
        }

        public async Task<int> DeleteSubCategory(SubCategory subcategory)
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.ExecuteAsync("UPDATE SubCategory SET DeletedDate=@DeletedDate,DeletedUser=@DeletedUser WHERE SubId=@SubId", subcategory);
                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving subcategories.", ex);
            }
        }
    }
}
