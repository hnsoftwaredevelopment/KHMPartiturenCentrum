using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KHM.Models;
public class UserRoleModel
{
    public int RoleId { get; set; }
    public int RoleOrder { get; set; }
    public string RoleName { get; set; }
    public string RoleDescription { get; set; }
}
