
using System.Security.Cryptography;
using System.Text;

string HashSaltedPassword(string password, string username )
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

Console.WriteLine("Salted Password: " + HashSaltedPassword(password, username));
Console.WriteLine ( "Hashed Password: " + HashPassword ( password ) );

//123: bHid7gw+c3qWkt6T6jMyVxoCq5g9Aonlbn0j2x2FkKZISGZRfwZxZBppCaz/4qnTmR94eI92GLNuE+BRCzrFaQ==