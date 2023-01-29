using MySql.Data.MySqlClient;

namespace KHMPartiturenCentrum.Helpers;

public class DBConnect
{
    public MySqlConnection? connection;

    public static readonly string server = "82.174.176.162";
    public static readonly string database = "AlureFactsheets";
    public static readonly string port = "3306";
    public static readonly string uid = "root";
    public static readonly string password = "OefenenKHMK24!";
    public static string ConnectionString = "SERVER=" + server + ";PORT=" + port + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";
}
