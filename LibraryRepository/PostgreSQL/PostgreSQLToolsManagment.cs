using Npgsql;
using SQLRepository.LibraryRepository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SQLRepository.LibraryRepository.PostgreSQL
{
    public class PostgreSQLToolsManagment : IEventHandler, IDBmanagment
    {
        private PostgreSQLStringBiulder postgreSQLStringBiulder1 = new PostgreSQLStringBiulder();
        private Npgsql.NpgsqlCommand NpgsqlCommand = new NpgsqlCommand();
        private NpgsqlConnectionStringBuilder keyValuePairs = new NpgsqlConnectionStringBuilder();
        private NpgsqlConnection connection = new NpgsqlConnection();
        private NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter();
        private NpgsqlTransaction npgsqlTransaction;
        public event EventHandler<Exception> EventWithException;
        public event EventHandler<List<string>> EventWithList;

        /// <summary>
        /// Свойство отвечающие за объект PostgreSQLStringBiulder 
        /// </summary>
        public PostgreSQLStringBiulder PostgreSQLStringBiulder { get; set; }
        public PostgreSQLToolsManagment(PostgreSQLStringBiulder postgreSQLStringBiulder)
        {
            this.postgreSQLStringBiulder1 = postgreSQLStringBiulder;
        }
        /// <summary>
        /// Метод открывает соединение 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="dbConnectionStringBuilder"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void DbConnection(DbConnection dbConnection, DbConnectionStringBuilder dbConnectionStringBuilder)
        {
            if (dbConnection == null)
            {
                EventWithException.Invoke(this, new ArgumentNullException(" DbConnection dbConnection - ссылка на объект пустая !"));
                throw new ArgumentNullException(" DbConnection dbConnection - ссылка на объект пустая !");
            }
            dbConnection.ConnectionString = dbConnectionStringBuilder.ConnectionString;
            dbConnection.Open();
            this.connection = (NpgsqlConnection)dbConnection;
        }
        /// <summary>
        /// Асинхронное открытие соединения
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="dbConnectionStringBuilder"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DbConnectionAsync(DbConnection dbConnection , DbConnectionStringBuilder dbConnectionStringBuilder)
        {
            /* if (dbConnection == null)
             {
                 EventWithException.Invoke(this, new ArgumentNullException(" DbConnection dbConnection - ссылка на объект пустая !"));
                 throw new ArgumentNullException(" DbConnection dbConnection - ссылка на объект пустая !");
             }*/
            dbConnection = new NpgsqlConnection();
            dbConnection.ConnectionString = dbConnectionStringBuilder.ConnectionString;
            await dbConnection.OpenAsync();
        }
        /// <summary>
        /// Метод по названию функции выполняет запро и воозвращает число 
        /// </summary>
        /// <param name="nameFan"></param>
        /// <param name="parametrs"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public int RequestFunction(string nameFan, string[] parametrs)
        {
            if (nameFan == null)
            {
                EventWithException.Invoke(this, new ArgumentNullException("текст команды пуст !"));
                throw new ArgumentNullException("текст команды пуст !");
            }
            try
            {
                using (this.connection)
                {
                    this.connection.Open();
                    using (this.NpgsqlCommand.Transaction = this.connection.BeginTransaction())
                    {
                        this.NpgsqlCommand.CommandText = $"SELECT {nameFan}();";
                        int result = this.NpgsqlCommand.ExecuteNonQuery();
                        this.NpgsqlCommand.Transaction.Commit();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                this.NpgsqlCommand.Transaction.Rollback();
                this.EventWithException.Invoke(this,ex);
                throw  ex; 
            }
        }
        /// <summary>
        /// Метод по названию функции выполняет запро и воозвращает число
        /// </summary>
        /// <param name="nameFan"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<int> RequestFunction(string nameFan)
        {
            if (nameFan == null)
            {
                EventWithException.Invoke(this, new ArgumentNullException("текст команды пуст !"));
                throw new ArgumentNullException("текст команды пуст !");
            }
            try
            {

                await this.connection.OpenAsync();
                this.connection.BeginTransaction();

                this.NpgsqlCommand.CommandText = $"SELECT {nameFan}();";
                int result = await this.NpgsqlCommand.ExecuteNonQueryAsync();
                await this.NpgsqlCommand.Transaction.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await this.NpgsqlCommand.Transaction.RollbackAsync();
                this.EventWithException.Invoke(this, ex);
                throw ex;
            }
        }

        public void RequestExcetionForTable(ref DataTable dt, string nameTableInDB, string[] parametrs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Метод который вставляет параметры из массива параметров в строку
        /// </summary>
        /// <param name="textCmd"></param>
        /// <param name="paramenrsList"></param>
        /// <returns> Возвращает строку с парметрами </returns>
        /// <exception cref="Exception"></exception>
        public string InsertOfParametrs(string textCmd, string[] paramenrsList)
        {
            if(string.IsNullOrEmpty(textCmd))
            {
                EventWithException.Invoke(this,new Exception("Парметр textCmd - пуст, проверьте указание параметра и повторите попытку !"));
                throw new Exception("Парметр textCmd - пуст, проверьте указание параметра и повторите попытку !");
            }
            if(paramenrsList.Length == 0)
            {
                EventWithException.Invoke(this, new Exception("Список параметров paramenrsList - пуст, проверьте указание параметра и повторите попытку !"));
                throw new Exception("Список параметров paramenrsList - пуст, проверьте указание параметра и повторите попытку !");
            }
            if (textCmd.Count(x => x == '@') == paramenrsList.Length)
            {
                for (int i = 0; i < paramenrsList.Length; i++)
                {
                    textCmd.Replace($"@{i + 1}", paramenrsList[i]);
                }
                return textCmd;
            }
            else
            {
                EventWithException.Invoke(this, new Exception("Количество спец-элементов в строке и кол-во парметров, не одинаковое кол-во !"));
                throw new Exception("Количество спец-элементов в строке и кол-во парметров, не одинаковое кол-во !");
            }
        }
    }
}
