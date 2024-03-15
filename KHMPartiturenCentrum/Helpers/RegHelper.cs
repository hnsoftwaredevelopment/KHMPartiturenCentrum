using Microsoft.Win32;

namespace KHM.Helpers;
public class RegHelper
{
	private static string keyPath = @"Software\KHM\Partiturencentrum";
	private static string valueName = "Database";
	private static string defaultValue = "localhost";

	public static string GetIPFromReg()
	{
		string databaseValue = "192.168.1.100";

		using ( RegistryKey baseKey = RegistryKey.OpenBaseKey( RegistryHive.CurrentUser, RegistryView.Registry64 ) )
		{
			RegistryKey? currentUserKey = baseKey.OpenSubKey(keyPath, true);

			if ( currentUserKey == null )
			{
				// Registry key does not exist, create it with default setting
				currentUserKey = baseKey.CreateSubKey( keyPath );
				currentUserKey = Registry.CurrentUser.CreateSubKey( keyPath );
				currentUserKey.SetValue( valueName, defaultValue );
				databaseValue = defaultValue;
			}
			else
			{
				// Read the IP Address
				object value = currentUserKey.GetValue ( valueName );

				if ( value != null && value is string stringValue )
				{
					databaseValue = stringValue;
				}
				else
				{
					// Database setting does not exist, create it with default setting
					currentUserKey.SetValue( valueName, defaultValue );
					databaseValue = defaultValue;
				}
			}

			currentUserKey.Close();
		}

		return databaseValue;
	}
}
