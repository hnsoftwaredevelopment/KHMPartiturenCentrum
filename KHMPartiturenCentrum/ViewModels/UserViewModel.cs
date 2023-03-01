using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHMPartiturenCentrum.ViewModels;
public partial class UserViewModel : ObservableObject
{
    [ObservableProperty]
    public int userId;

    [ObservableProperty]
    public string userName;

    [ObservableProperty]
    public int userRole;
}
