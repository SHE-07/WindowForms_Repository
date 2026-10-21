using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLRepository.LibraryRepository
{
    public interface IDBmanagment 
    {
        /// <summary>
        /// Метод изменяет нынешнее подключение по ссылке , принимает на вход обьект для изменения и конструктор строки подключения 
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="dbConnectionStringBuilder"></param>
        void DbConnection();
        /// <summary>
        /// Ассинхронная версия ВbConnection
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="dbConnectionStringBuilder"></param>
        /// <returns></returns>
        Task DbConnectionAsync();
        /// <summary>
        /// Метод выполняет функцию которая возвращает число
        /// </summary>
        /// <param name="nameFan"></param>
        /// <param name="parametrs"></param>
        /// <returns></returns>
        int RequestFunction(string nameFan, string[] parametrs);
        /// <summary>
        /// Асихронная версия метода RequestFunction 
        /// </summary>
        /// <param name="nameFan"></param>
        /// <returns>Возвращает число,  является результатом запроса</returns>
        Task<int> RequestFunction(string nameFan);
        /// <summary>
        /// Метод выполняет запрос на заполнение данными таблицу
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="nameTableInDB"></param>
        /// <param name="parametrs"></param>
        void RequestExcetionForTable(ref DataTable dt, string nameTableInDB, string[] parametrs);

        /// <summary>
        /// Метод принимает на вход текст, по которому будет производится замена, 
        /// и массив параметров, элементы массива всталяются  в текст команды
        /// </summary>
        /// <param name="textCmd"></param>
        /// <param name="paramenrsList"></param>
        /// <returns></returns>
        string InsertOfParametrs(string textCmd, string[] paramenrsList);
    }
    /*
     * 
     *              "Host=localhost;    Port=5432;      Database=mydb;      Username=postgres;  Password=mysecretpassword"
     *              "Server=localhost;                  Database=mydb;      User ID=sa;         Password=StrongPassword123;     TrustServerCertificate=True;"
     *              
     * 
     * 
     */
}
