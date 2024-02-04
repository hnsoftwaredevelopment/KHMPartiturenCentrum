using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;

namespace KHM.Helpers;
public class RegHelper
{
    public static string GetIP()
    {
        string ip = "";

        try
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\KHM\\Partiturencentrum"))
            {
                if (key != null)
                {
                    ip = key.GetValue("Database").ToString();
                }
            }
        }
        catch (Exception ex)  //just for demonstration...it's always best to handle specific exceptions
        {
            Console.WriteLine( ex );
            //react appropriately
        }

        if (ip == "" || ip == null) { ip = "192.168.1.100"; }
        return ip;
    }

}
