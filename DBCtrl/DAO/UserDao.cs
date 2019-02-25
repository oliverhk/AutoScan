using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class UserDao:BaseSqlMapDao
    {
        public IList<User> GetList()
        {
            IList<User> rect = new List<User>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = mapper.QueryForList<User>("SelectUserList", null);         
            }catch(Exception ex)
            { Logger.Error(ex); }
            return rect;
        }
        public User GetUser(string userid)
        {
            User rect = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = (User)mapper.QueryForObject("SelectUser", userid);
            }catch(Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Insert(User obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertUser", obj);
                rect = true;
            }catch(Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }        
        public bool Update(User obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateUser", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Delete(string userid)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Delete("DelUserById", userid);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
    }
}
