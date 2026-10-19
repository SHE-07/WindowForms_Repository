namespace SQLRepository.LibraryRepository
{
    public interface IDbStringConnectionBuilder
    {
        string host { get; set;  }
        string databasename { get; set; }
       

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
