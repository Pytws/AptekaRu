using AptekaRu.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptekaRu.DAL.Interfaces
{
    public interface IAptekaruRepository
    {
        Task<bool> Create(object model, string schemaName, string tableName);
        Task<IEnumerable<IDictionary<string, object>>> Read(string schemaName, string tableName, int limit, int offset);
        Task<IDictionary<string, object>?> GetById(string schemaName, string tableName, int identifier, string identifierName);
        Task<IDictionary<string, object>?> GetByGuid(string schemaName, string tableName, Guid identifier, string identifierName);
        Task<bool> Update(object model, string schemaName, string tableName, ConstraintInfo constraintInfo, string identifier);
        Task<bool> Delete(string schemaName, string tableName, ConstraintInfo constraintInfo, string identifier);
    }
}
