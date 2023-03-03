using System;
using System.Security.Cryptography;
using System.Text;

namespace KHMPartiturenCentrum.Converters;

public class Helper
{
    public static string HashPassword(string password )
    {
        var sha = SHA512.Create();
        var asByteArray = Encoding.Default.GetBytes( password );
        var hashedPassword = sha.ComputeHash( asByteArray );

        return Convert.ToBase64String ( hashedPassword );
    }

    public static string HashPepperPassword ( string password, string username )
    {
        var sha = SHA512.Create();
        var asByteArray = Encoding.Default.GetBytes( password + username.ToLower() );
        var hashedPassword = sha.ComputeHash( asByteArray );

        return Convert.ToBase64String ( hashedPassword );
    }
}
