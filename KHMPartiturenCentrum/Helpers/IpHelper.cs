namespace KHM.Helpers;
public class IpHelper
{
	static string filePath = @".\Resources\Config\KHM.Config";
	static string defaultContent = "3924, 2400, 2400, 2412";
	static string ipAddress = "";

	public static string GetIP()
	{
		int[] ipPart = new int [4];
		if ( !File.Exists( filePath ) )
		{
			File.WriteAllText( filePath, defaultContent );
		}
		else
		{
			string[] lines = File.ReadAllLines(filePath);

			foreach ( string line in lines )
			{
				string[] data = line.Split(", ");
				ipPart [ 0 ] = ( ( int.Parse( data [ 0 ] ) / 2 ) - 1200 ) / 6;
				ipPart [ 1 ] = ( ( int.Parse( data [ 1 ] ) / 2 ) - 1200 ) / 6;
				ipPart [ 2 ] = ( ( int.Parse( data [ 2 ] ) / 2 ) - 1200 ) / 6;
				ipPart [ 3 ] = ( ( int.Parse( data [ 3 ] ) / 2 ) - 1200 ) / 6;
			}
		}
		return $"{ipPart [ 0 ]}.{ipPart [ 1 ]}.{ipPart [ 2 ]}.{ipPart [ 3 ]}";
	}
}

