using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCtrl.Model
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
