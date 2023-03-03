using System.Security.Cryptography;
using System.Text;

string HashPepperPassword ( string password, string username )
{
    var sha = SHA512.Create();
    var asByteArray = Encoding.Default.GetBytes( password + username );
    var hashedPassword = sha.ComputeHash( asByteArray );

    return Convert.ToBase64String ( hashedPassword );
}

string HashPassword ( string password )
{
    var sha = SHA512.Create();
    var asByteArray = Encoding.Default.GetBytes( password );
    var hashedPassword = sha.ComputeHash( asByteArray );

    return Convert.ToBase64String ( hashedPassword );
}

Console.WriteLine ( "Enter password:" );
string password = Console.ReadLine();

Console.WriteLine ( "Enter username:" );
string username = Console.ReadLine();

Console.WriteLine ( "Hashed Password  : " + HashPassword ( password ) );
Console.WriteLine ( "Peppered Password: " + HashPepperPassword ( password, username ) );

Console.ReadLine ();