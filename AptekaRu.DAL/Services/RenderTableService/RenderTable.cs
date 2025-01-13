using AptekaRu.DAL.Interfaces;
using AptekaRu.DAL.Models;
using AptekaRu.DAL.Enums;
using AptekaRu.DAL.Extensions;
using Dapper;
using Npgsql;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AptekaRu.DAL.Services.RenderTableService
{
    public class RenderTable : IRenderTable
    {
        private readonly string connectionString;
        private readonly ILogger<RenderTable> logger;
        public RenderTable(string connectionString, ILogger<RenderTable> logger)
        {
            this.connectionString = connectionString;
            this.logger = logger;
        }
        public async Task<IEnumerable<TableInfo>> GetTablesInformation(string schema, TableType tableType)
        {
            string query = @"SELECT 
                                 table_schema AS TableSchema, 
                                 table_name AS TableName, 
                                 table_type AS TableType
                             FROM information_schema.tables
                             WHERE table_schema = @schema AND table_type = @tableType";

            using var connection = new NpgsqlConnection(connectionString);

            var response = await connection.QueryAsync<TableInfo>(query, new { schema, tableType = tableType.Description() });

            logger.LogInformation($"Executing query: {query}");

            return response;
        }

        public async Task<IEnumerable<ColumnInfo>> GetColumnsInformation(string schema, TableType tableType, string tableName)
        {

            string query = @"
            SELECT 
                column_name AS ColumnName, 
                data_type AS DataType, 
                is_nullable AS IsNullable, 
                character_maximum_length AS CharacterMaximumLength, 
                col.table_name AS TableName
            FROM information_schema.columns AS col
            INNER JOIN information_schema.tables AS tab
                ON col.table_name = tab.table_name
            WHERE 
                tab.table_schema = @schema AND                     
                tab.table_type = @tableType AND 
                col.table_name = @tableName";
            using var connection = new NpgsqlConnection(connectionString);

            var response = await connection.QueryAsync<ColumnInfo>(query,
                new { schema, tableName, tableType = tableType.Description() });

            logger.LogInformation($"Executing query: {query}");

            return response;

        }

        public async Task<IEnumerable<ConstraintInfo>> GetConstraintInfo(string schema, string tableName, ConstraintType constraintType)
        {
            string query = @"
            SELECT 
	            cons.constraint_type AS ConstraintType,
	            ke.column_name AS ColumnName,
	            col.data_type AS DataType
            FROM information_schema.table_constraints AS cons
            INNER JOIN information_schema.key_column_usage AS ke
	            ON ke.constraint_name = cons.constraint_name
            INNER JOIN information_schema.columns AS col
	            ON col.column_name = ke.column_name
            WHERE 
	            cons.constraint_schema = @schema AND 
	            cons.table_name = @tableName AND
	            cons.constraint_type = @constraintType";

            using var connection = new NpgsqlConnection(connectionString);

            var response = await connection.QueryAsync<ConstraintInfo>(query, 
                new { schema, tableName, constraintType = constraintType.Description() });

            logger.LogInformation($"Executing query: {query}");

            return response;
        }
    }
}
