using System.Text.RegularExpressions;

string filePath = @"c:\DevOps\khm.config", contentToWrite = "3924, 2400, 2400, 2412", ipAddress = "";

if ( !File.Exists( filePath ) )
{
	File.WriteAllText( filePath, contentToWrite );
	Console.WriteLine( $"Bestand ({filePath}) is aangemaakt." );
}
else
{
	string[] lines = File.ReadAllLines(filePath);
	foreach ( string line in lines )
	{

		string[] data = line.Split(", ");
		var ipPart1 = ((int.Parse(data[0])/2)-1200)/6;
		var ipPart2 = ((int.Parse(data[1])/2)-1200)/6;
		var ipPart3 = ((int.Parse(data[2])/2)-1200)/6;
		var ipPart4 = ((int.Parse(data[3])/2)-1200)/6;

		ipAddress = $"{ipPart1}.{ipPart2}.{ipPart3}.{ipPart4}";
		Console.WriteLine( "Huidige IP Adres: " + ipAddress );
	}
}

Console.Write( "Welk IP adres moet worden gconverteerd: " );
var ip = Console.ReadLine();

if ( ip != "" && ip != ipAddress )
{
	if ( IsValidIPv4( ip ) )
	{
		Console.WriteLine( "Geldig IP-adres." );
		string[] ipParts = ip.Split(".");
		int[] Data = new int[ipParts.Length];

		for ( int i = 0; i < ipParts.Length; i++ )
		{
			Data [ i ] = ( ( int.Parse( ipParts [ i ] ) * 6 ) + 1200 ) * 2;
		}

		Console.WriteLine( "Encrypted data: {0}", String.Join( ", ", Data ) );

		File.WriteAllText( filePath, String.Join( ", ", Data ) );
		Console.WriteLine( $"Bestand ({filePath}) is aangepast met nieuwe IP Adres." );
	}
	else
	{
		Console.WriteLine( "Ongeldig IP-adres." );
	}
}
else { Console.WriteLine( "Geen actie nodig!" ); }

static bool IsValidIPv4( string ipAddressString )
{
	// Controleer of de invoer voldoet aan het patroon van een IPv4-adres
	Regex ipv4Pattern = new(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
	return ipv4Pattern.IsMatch( ipAddressString );
}