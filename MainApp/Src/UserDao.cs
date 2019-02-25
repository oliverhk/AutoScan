using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using DBUtility;
using Utility.DEncrypt;
using System.Data;

namespace MainApp.Src
{
    public class UserDAO
    {
        #region property
        private const string querysql = "select userid,username,userpwd,roleid,createtime from user ";
        private const string permissionsql = "select ";
        #endregion
        public UserDAO()
        { }
        /// <summary>
        /// 获取用户登录，成功后返回用户id
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string UserLogin(string userid,string pwd)
        {
            string ret = string.Empty;
            try
            {
                string sql = string.Format("{0} where userid ={1} and userpwd ={2}",querysql,userid,DEncrypt.Encrypt(pwd));
                DataTable dt = DbHelperMySQL.Query(sql).Tables[0];
                if(dt!=null && dt.Rows.Count>0)
                {
                    ret = userid;
                }
            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        public List<string> GetUserPermissions(string userid)
        {
            List<string> ret = new List<string>();
            try
            {

            }catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
        public List<string>GetUserPermissions(int roleid)
        {
            List<string> ret = new List<string>();
            try
            {

            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return ret;
        }
    }
}
