using Microsoft.Data.Sqlite;
using System.DirectoryServices;
using System.Runtime.Intrinsics.Arm;

using System.Text;


namespace MaterialLoginWpfApp;
public static class UsersDatabase
{
    private static readonly string _databaseFileName;
    private static readonly SqliteConnection _connection;
    private static readonly SqliteCommand _command;
  
    static UsersDatabase()
    {
        _databaseFileName = "../../../UserDb/UsersDatabase.db";
        _connection = new SqliteConnection("Data source = " + _databaseFileName);
        _command = _connection.CreateCommand();
        _connection.Open();
        CreateTable();
    }

    private static void CreateTable()
    {
        _command.CommandText = @$"CREATE TABLE IF NOT EXISTS UsersDatabase(
                                 id INTEGER PRIMARY KEY AUTOINCREMENT,
                                 login TEXT NOT NULL,
                                 password TEXT NOT NULL,
                                 email TEXT NOT NULL,
                                 phone_number TEXT)";
        _command.ExecuteNonQuery();
    

    }
   
    public static void AddUser(User user)
    {
        var encryptPassword=Sha256Encrypter.EncryptPassword(user.Password);
        _command.CommandText = @$"INSERT INTO UsersDatabase (login, password, email, phone_number)
                                 VALUES('{user.Login}','{encryptPassword}','{user.Email}','{user.PhoneNumber}')";
        _command.ExecuteNonQuery();
    }
    public static IEnumerable<User> GetAllUsers()
    {
        _command.CommandText = $"SELECT * FROM UsersDatabase";
        using var reader = _command.ExecuteReader();
        while(reader.Read() )
        {
            yield return new User
            {
                Id = reader.GetInt32(0),
                Login=reader.GetString(1),
                Password=reader.GetString(2),
                Email=reader.GetString(3),
                PhoneNumber=reader.GetString(4)
            };
        }
        reader.Close();
    }






}

