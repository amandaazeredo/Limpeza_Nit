using System.Data;

namespace API.Infra.DataAcess.Repository
{
    public interface IExcelImportRepository
    {
        void BulkInsertToDatabase(DataTable dataTable, string tableName);
    }
}
