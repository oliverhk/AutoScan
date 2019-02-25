using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class FunctionRoleDao : BaseSqlMapDao
    {
        public IList<FunctionRole> GetList()
        {
            IList<FunctionRole> rect = new List<FunctionRole>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = mapper.QueryForList<FunctionRole>("SelectFunctionRoleList", null);
            }
            catch (Exception ex)
            { Logger.Error(ex); }
            return rect;
        }
        public IList<FunctionRole> GetListByRole(int roleid)
        {
            IList<FunctionRole> rect = new List<FunctionRole>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = mapper.QueryForList<FunctionRole>("SelectFunctionRoleByRole", roleid);
            }
            catch (Exception ex)
            { Logger.Error(ex); }
            return rect;
        }
        public FunctionRole GetFunction(string serialid)
        {
            FunctionRole rect = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = (FunctionRole)mapper.QueryForObject("SelectFunctionRole", serialid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Insert(FunctionRole obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertFunctionRole", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Update(FunctionRole obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateFunctionRole", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Delete(int serialid)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Delete("DelFunctionRoleById", serialid);
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
