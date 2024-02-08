using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace KHM.Helpers;
public class RegHelper
{
    private static string keyPath = @"Software\KHM\Partiturencentrum";
    private static string valueName = "Database";
    private static string defaultValue = "localhost";

    public static string GetIP()
    {
        string databaseValue = "";

        using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
        {
            RegistryKey? currentUserKey = baseKey.OpenSubKey(keyPath, true);

            if (currentUserKey == null)
            {
                // Registry key does not exist, create it with default setting
                currentUserKey = baseKey.CreateSubKey(keyPath);
                currentUserKey = Registry.CurrentUser.CreateSubKey(keyPath);
                currentUserKey.SetValue(valueName, defaultValue);
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
                    currentUserKey.SetValue(valueName, defaultValue);
                    databaseValue = defaultValue;
                }
            }

            currentUserKey.Close();
        }

        return databaseValue;
    }
}
