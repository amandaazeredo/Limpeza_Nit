using System.Data;
using API.Infra.DataAcess.Connection;

namespace API.Infra.DataAcess.Repository
{
    public class ExcelImportRepository(DbSession session) : IExcelImportRepository
    {
        private readonly DbSession _session = session;

        public void BulkInsertToDatabase(DataTable dataTable, string tableName)
        {
            _session.Connection.DestinationTableName = tableName;
            _session.Connection.WriteToServer(dataTable);
        }
    }
}
