using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class UserLevelDao: BaseSqlMapDao
    {
        public IList<UserLevel> GetList()
        {
            IList<UserLevel> rect = new List<UserLevel>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = mapper.QueryForList<UserLevel>("SelectUserLevelList", null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }

        public UserLevel GetUserLevel(int levelid)
        {
            UserLevel rect = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = (UserLevel)mapper.QueryForObject("SelectUserLevel", levelid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }

        public bool Insert(UserLevel obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertUserLevel", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }

        public bool Update(UserLevel obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateUserLevel", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }

        public bool Delete(int levelid)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Delete("DelUserLevelById", levelid);
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
