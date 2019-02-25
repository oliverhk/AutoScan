using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Utility.DEncrypt;
using System.Data;
using DBCtrl.DAO;
using DBCtrl.Model;

namespace MarkTool.Usr
{
    public static class UserAccess
    {
        #region property         
        #endregion
        //public UserAccess()
        //{ }
        /// <summary>
        /// 获取用户登录，成功后返回用户id
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool UserLogin(string userid,string pwd)
        {
            bool ret = false;
            try
            {
                User usr = new UserDao().GetUser(userid);
                if (usr == null)
                    return ret;
                if (usr.UserPwd == pwd)
                    ret = true;                 
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        public static bool IsUserRight(string userid,string funid)
        {
            bool ret = false;
            try
            {
                User usr = new UserDao().GetUser(userid);
                if (usr == null)
                    return ret;
                List<string> lstf = GetUserPermissions(usr.RoleId);
                if (lstf.Contains(funid))
                {
                    ret = true;
                }
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        public static  List<string> GetUserPermissions(string userid)
        {
            List<string> ret = new List<string>();
            try
            {
                User usr = new UserDao().GetUser(userid);
                if(usr !=null )
                {
                    return GetUserPermissions(usr.RoleId);
                }
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        public static List<string>GetUserPermissions(int roleid)
        {
            List<string> ret = new List<string>();
            try
            {
                IList<FunctionRole> lst= new  FunctionRoleDao().GetListByRole(roleid);
                foreach(var v in lst)
                {
                    ret.Add(v.FunctionId);
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }        
    }
}
