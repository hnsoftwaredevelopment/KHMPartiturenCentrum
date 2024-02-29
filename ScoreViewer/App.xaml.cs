using System.Configuration;
using System.Data;
using System.Windows;

namespace ScoreViewer
	{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
		{
		  public App ( )
    {
        //Register Syncfusion license
        // 23.1.30: Ngo9BigBOggjHTQxAR8/V1NHaF5cWWNCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWH9fdXVURWJeUkBzW0Y=
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense ("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWX1cd3RcQ2dfWUJxWUs=");
    }

		public static string[] Args;  
        void app_Startup(object sender, StartupEventArgs e) {  
            // If no command line arguments were provided, don't process them if (e.Args.Length == 0) return;  
            if (e.Args.Length > 0) {  
                Args = e.Args;  }
		}
	} }
