using AptekaRu.DAL.Extensions;
using AptekaRu.DAL.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Text;
using AptekaRu.DAL.Models;


namespace AptekaRu.DAL.Repositories
{
    public class AptekaruRepository : IAptekaruRepository
    {
        private readonly string connectionString;
        private readonly ILogger<AptekaruRepository> logger;

        public AptekaruRepository(
            string connectionString, 
            ILogger<AptekaruRepository> logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;
        }

        public async Task<bool> Create(object model, string schemaName, string tableName)
        {
            var sb = new StringBuilder($"INSERT INTO {schemaName}.{tableName} VALUES (");

            var jsonPropertyNames = model.GetJsonPropertyName();
            int i = 0;
            foreach (var jsonPropertyName in jsonPropertyNames)
            {
                i++;
                if (i == jsonPropertyNames.Count)
                {
                    sb.Append($"@{jsonPropertyName.Key})");
                    break;
                }
                sb.Append($"@{jsonPropertyName.Key},");
            }

            using var connection = new NpgsqlConnection(connectionString);

            logger.LogInformation($"Executing query: {sb.ToString()}");
            
            await connection.ExecuteAsync(sb.ToString(), model);

            return true;
        }

        public async Task<IEnumerable<IDictionary<string, object>>> Read(string schemaName, string tableName, int limit, int offset)
        {
            string query = $"SELECT * FROM {schemaName}.{tableName} LIMIT {limit} OFFSET {offset}";

            using var connection = new NpgsqlConnection(connectionString);

            logger.LogInformation($"Executing query: {query}");

            var rows = (from row in await connection.QueryAsync(query)
                        select (IDictionary<string, object>)row).AsList();
           
            return rows;
        }

        public async Task<IDictionary<string, object>?> GetByGuid(string schemaName, string tableName, Guid identifier, string identifierName)
        {
            var query = $"SELECT * FROM {schemaName}.{tableName} WHERE {identifierName} = {identifier}";

            using var connection = new NpgsqlConnection(connectionString);

            logger.LogInformation($"Executing query: {query}");

            IDictionary<string, object>? response = await connection.QueryFirstOrDefaultAsync(query, identifier);

            return response;
        }

        public async Task<IDictionary<string, object>?> GetById(string schemaName, string tableName, int identifier, string identifierName)
        {
            var query = $"SELECT * FROM {schemaName}.{tableName} WHERE {identifierName} = {identifier}";

            using var connection = new NpgsqlConnection(connectionString);

            logger.LogInformation($"Executing query: {query}");

            IDictionary<string, object>? response = await connection.QueryFirstOrDefaultAsync(query);

            return response;
        }

        public async Task<bool> Update(object model, string schemaName, string tableName, ConstraintInfo constraintInfo, string identifier)
        {

            var sb = new StringBuilder($"UPDATE {schemaName}.{tableName} SET \n");

            var jsonPropertyNames = model.GetJsonPropertyName();
            int i = 0;
            foreach (var jsonPropertyName in jsonPropertyNames)
            {
                i++;
                if (i == jsonPropertyNames.Count)
                {
                    sb.Append($"{jsonPropertyName.Value} = @{jsonPropertyName.Key}\n");
                    break;
                }
                sb.Append($" {jsonPropertyName.Value} = @{jsonPropertyName.Key},\n");
            }

            sb.Append($"WHERE {constraintInfo.ColumnName} = {identifier}");

            using var connection = new NpgsqlConnection(connectionString);

            logger.LogInformation($"Executing query: {sb.ToString()}");

            await connection.ExecuteAsync(sb.ToString(), model);

            return true;
        }

        public async Task<int> Delete(string schemaName, string tableName, ConstraintInfo constraintInfo, string identifier)
        {
            var query = $"DELETE FROM {schemaName}.{tableName} WHERE {constraintInfo.ColumnName} = {identifier}";

            using var connection = new NpgsqlConnection(connectionString);

            logger.LogInformation($"Executing query: {query}");

            return await connection.ExecuteAsync(query);
        }
    }
}
