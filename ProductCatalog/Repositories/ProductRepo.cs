using Dapper;
using Microsoft.Data.SqlClient;
using ProductCatalogCore.Entities;

namespace ProductCatalog.Repositories
{
    public class ProductRepo
    {
        private readonly string? _connectionString;

        public ProductRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<ViProduct>> GetAllProduct()
        {
            using var connection = new SqlConnection(_connectionString);
            try
            {
                var result = await connection.QueryAsync<ViProduct>("SELECT * FROM VI_Product WHERE DeletedDate IS NULL");
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }
    }
}
