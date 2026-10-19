using SQLRepository.LibraryRepository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLRepository.LibraryRepository.PostgreSQL
{
    public class PostgreSQLStringBiulder : DbConnectionStringBuilder , IDbStringConnectionBuilder , IEventHandler
    {
        private string name = "postgresql";
        private string Host = "localhost";
        private string userName = "postgres";
        private string userPassword = "0";
        private string port = "5432";
      
        /// <summary>
        /// Свойсво отвечающие за получение и указание хоста
        /// </summary>
        public string host
        {
            get
            {
                return !string.IsNullOrEmpty(this.Host) ? this.Host : throw new Exception("Поле ХОСТ - пустое, проверьте указание параметров в настрйоках и повторите попытку !");
            }
            set
            {
                if (value != string.Empty)
                {
                    Host = value;
                }
                else
                {
                    throw new Exception("ХОСТ - Параметр является пустой или null строкой !");
                }
            }
        }
        /// <summary>
        /// Имя пользователя для подключения к базе данных.
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя пользователя не может быть пустым.");

                userName = value.Trim();
            }
        }
        /// <summary>
        /// Свойство отвечающие за наименование БД
        /// </summary>
        public string databasename 
        { 
            get
            {
                return name;
            } 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя базы данных не может быть пустым.");
                this.name = value;
            }
        }
        /// <summary>
        /// Пароль пользователя для подключения к базе данных.
        /// </summary>
        public string UserPassword
        {
            get { return userPassword; }
            set
            {
                userPassword = value ?? string.Empty;
            }
        }

        public event EventHandler<Exception> EventWithException;
        public event EventHandler<List<string>> EventWithList;

        public PostgreSQLStringBiulder(Dictionary<string,string> parametrs)
        {
            this.name = parametrs["namedb"];
            this.Host = parametrs["host"];
            this.userName = parametrs["nameuser"];
            this.userPassword = parametrs["password"];
            this.port = parametrs["port"];
        }
        public PostgreSQLStringBiulder() { }




        public override string ToString()
        {
            return $"Host={this.Host};Port={this.port};Database={this.name};Username={this.userName};Password={this.userPassword}";
        }
    }
}
