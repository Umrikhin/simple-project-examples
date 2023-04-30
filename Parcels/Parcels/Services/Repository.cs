using Parcels.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using System;

namespace Parcels.Services
{
    public class Repository
    {
        private string cn;
        int _timeout;
        public Repository(string connectionString, int maxTimeOut)
        {
            cn = connectionString;
            _timeout = maxTimeOut;
        }

        //Возвращает одну запись по идентификатору из таблицы с данными
        public FileParcel Get(Guid id)
        {
            using (IDbConnection db = new SqlConnection(cn))
            {
                return db.Query<FileParcel>($"SELECT * FROM tbFileParcels WHERE Id = @Id", new { id }).FirstOrDefault() ?? new FileParcel() { Id = Guid.Empty };
            }
        }

        //Возвращает последние записи из таблицы с данными
        public IEnumerable<FileParcel> GetTop(string CTERR = "", int day = 1)
        {
            using (IDbConnection db = new SqlConnection(cn))
            {
                DateTime dbeg = DateTime.Now;
                dbeg = dbeg.AddDays(-day + 1);
                string sql = $"SELECT * FROM tbFileParcels Where DateStart>=convert(datetime, '{dbeg.ToString("dd.MM.yyyy")}', 104) Order by DateStart desc"; 
                if (!string.IsNullOrEmpty(CTERR)) sql = $"SELECT * FROM tbFileParcels Where CTERR='{CTERR}' AND DateStart>=convert(datetime, '{dbeg.ToString("dd.MM.yyyy")}', 104) Order by DateStart desc";
                return db.Query<FileParcel>(sql).ToList();
            }
        }

        //Добавляет одну запись в таблицу с данными
        public Guid AddFileParcel(FileParcel newFileParcel)
        {
            using (IDbConnection db = new SqlConnection(cn))
            {
                var values = new DynamicParameters();
                values.Add("@CTERR", newFileParcel.CTERR);
                values.Add("@StartFile", newFileParcel.StartFile);
                values.Add("@DateStart", newFileParcel.DateStart);
                values.Add("@RetFile", newFileParcel.RetFile);
                values.Add("@DateRet", newFileParcel.DateRet);
                values.Add("@Id", dbType: DbType.Guid, direction: ParameterDirection.Output);
                db.Execute("[AddFile]", values, commandType: CommandType.StoredProcedure);
                var id = values.Get<Guid>("@Id");
                newFileParcel.Id = id;
            }
            return newFileParcel.Id;
        }

        //Обновляет одну запись в таблице с данными
        public Guid UpdateFileParcel(FileParcel fileParcel)
        {
            using (IDbConnection db = new SqlConnection(cn))
            {
                var sqlQuery = "UPDATE tbFileParcels SET CTERR = @CTERR, StartFile = @StartFile, DateStart = @DateStart, RetFile = @RetFile, DateRet = @DateRet WHERE Id = @Id";
                db.Execute(sqlQuery, fileParcel, commandTimeout: _timeout);
                return fileParcel.Id;
            }
        }

        //Удаляет одну запись в таблице с данными
        public void DelFileParcel(FileParcel fileParcel)
        {
            using (IDbConnection db = new SqlConnection(cn))
            {
                var sqlQuery = "DELETE FROM tbFileParcels WHERE Id = @Id";
                db.Execute(sqlQuery, new { fileParcel.Id });
            }
        }
    }
}
