using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class JobsDao : BaseSqlMapDao
    {
        public IList<Jobs> GetList()
        {
            IList<Jobs> rect = new List<Jobs>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = mapper.QueryForList<Jobs>("SelectAllJobs", null);
            }
            catch (Exception ex)
            { Logger.Error(ex); }
            return rect;
        }
        public Jobs GetJob(long jobid)
        {
            Jobs rect = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = (Jobs)mapper.QueryForObject("SelectJobs", jobid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Insert(Jobs obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertJobs", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Update(Jobs obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateJobs", obj);
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
                mapper.Delete("DelJobsById", userid);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public long MaxJobId()
        {
            long rect = 0;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                var rec = mapper.QueryForObject("SelectMaxJobId", null);
                rect = (long)rec;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
    }
}
