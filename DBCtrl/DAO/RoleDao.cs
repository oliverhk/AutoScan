using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class RoleDao : BaseSqlMapDao
    {
        public IList<Role> GetList()
        {
            IList<Role> rect = new List<Role>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = mapper.QueryForList<Role>("SelectRoleList", null);
            }
            catch (Exception ex)
            { Logger.Error(ex); }
            return rect;
        }
        public Role GetUser(int roleid)
        {
            Role rect = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = (Role)mapper.QueryForObject("SelectRole", roleid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Insert(Role obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertRole", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Update(Role obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateRole", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Delete(int roleid)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Delete("DelRoleById", roleid);
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
