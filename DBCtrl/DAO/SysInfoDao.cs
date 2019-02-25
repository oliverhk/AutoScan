using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class SysInfoDao: BaseSqlMapDao
    {
        public IList<SysInfo> GetList()
        {
            IList<SysInfo> ret = new List<SysInfo>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = mapper.QueryForList<SysInfo>("SelectSysInfoList", null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public SysInfo GetSysInfo(string fieldid)
        {
            SysInfo ret = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = (SysInfo)mapper.QueryForObject("SelectSysInfo", fieldid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Insert(SysInfo obj)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertSysInfo", obj);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Update(SysInfo obj)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateSysInfo", obj);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Delete(string fieldid)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Delete("DelSysInfoById", fieldid);
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
