using Syncfusion.Licensing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using KHMPartiturenCentrum.Helpers;
using KHMPartiturenCentrum.Models;
using KHMPartiturenCentrum.ViewModels;
using Microsoft.VisualBasic.ApplicationServices;

namespace KHMPartiturenCentrum;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        //Register Syncfusion license
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhlX1pFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jSn9QdkZgX3tccXNWRA==;Mgo+DSMBPh8sVXJ0S0J+XE9BdVRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TdURgWXxddnBXR2hUUw==;ORg4AjUWIQA/Gnt2VVhkQlFac1xJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkZiWn9dc3JRRGJcVEY=;MTMwOTMzM0AzMjMwMmUzNDJlMzBCVXdKd3d4YU9jWUF4Wm5GMENITTJvQnNkZzVpVUpseG56OTIrS25MaFM4PQ==;MTMwOTMzNEAzMjMwMmUzNDJlMzBHRjFQSXI1T1E0UEdNeWJHcmc2S3lqN1R0SFM4WWlTL0N2WitsbjVvcmM4PQ==;NRAiBiAaIQQuGjN/V0Z+WE9EaFpBVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdUVgWHxec3dTQmJeVkN0;MTMwOTMzNkAzMjMwMmUzNDJlMzBVMW9pZFY1c2x5d05CdmU3MGNLTWw2YS9PdWhlM0RDOUcrYXFOdFRXNElzPQ==;MTMwOTMzN0AzMjMwMmUzNDJlMzBsd0Z0ajM2MmZKS0UxU0xDcTArUGRvSUk3Mm4wQ2d1eGlzMnBqZVhYOHZJPQ==;Mgo+DSMBMAY9C3t2VVhkQlFac1xJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkZiWn9dc3JRRGNZUUY=;MTMwOTMzOUAzMjMwMmUzNDJlMzBnZHM4akU4ZlA1RFZxWnEyV1FxY0wrMGJxRnhTODFCbkl3ZzAzNmhyVngwPQ==;MTMwOTM0MEAzMjMwMmUzNDJlMzBPS2ZFR1VYQU93K1lMdzVVODZGYjJwWVZYRFpEZTBsUWw4T1RJbnkwOC9BPQ==;MTMwOTM0MUAzMjMwMmUzNDJlMzBVMW9pZFY1c2x5d05CdmU3MGNLTWw2YS9PdWhlM0RDOUcrYXFOdFRXNElzPQ==");
    }
    public class ScoreUsers
    {
        public static int SelectedUserId { get; set; }
        public static string SelectedUserName { get; set; }
        public static string SelectedUserFullName { get; set; }
        public static string SelectedUserEmail { get; set; }
        public static string SelectedUserPassword { get; set; }
        public static int SelectedUserRoleId { get; set; }
        public static int SelectedUserRoleName { get; set; }
        public static int SelectedUserRoleDescription { get; set; }
        public static ObservableCollection<UserModel> User { get; set; }
    }
}
