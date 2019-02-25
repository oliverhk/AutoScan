using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class TypesDao: BaseSqlMapDao
    {
        public IList<Types> GetList()
        {
            IList<Types> ret = new List<Types>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = mapper.QueryForList<Types>("SelectTypesList", null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public IList<Types> GetListByCategory(int cateid)
        {
            IList<Types> ret = new List<Types>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = mapper.QueryForList<Types>("SelectTypesListByCategory", cateid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public Types GetTypeById(int typeid)
        {
            Types ret = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = (Types)mapper.QueryForObject("SelectTypesById", typeid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public Types GetTypeByName(string celltype)
        {
            Types ret = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = (Types)mapper.QueryForObject("SelectTypesByName", celltype);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Insert(Types obj)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertTypes", obj);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Update(Types obj)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateTypes", obj);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool DeleteById(int typeid)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Delete("DelTypesById", typeid);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool DeleteByCellType(string celltype)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Delete("DelTypesByCellType", celltype);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool DeleteByCateId(int cateid)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Delete("DelTypesByCateId", cateid);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }
    }
}
