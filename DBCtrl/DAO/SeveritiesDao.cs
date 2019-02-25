using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class SeveritiesDao : BaseSqlMapDao
    {
        public IList<Severities> GetList()
        {
            IList<Severities> rect = new List<Severities>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = mapper.QueryForList<Severities>("SelectSeveritiesList", null);         
            }catch(Exception ex)
            { Logger.Error(ex); }
            return rect;
        }
        public Severities GetUser(string userid)
        {
            Severities rect = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = (Severities)mapper.QueryForObject("SelectSeverities", userid);
            }catch(Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Insert(Severities obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertSeverities", obj);
                rect = true;
            }catch(Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }        
        public bool Update(Severities obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateSeverities", obj);
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
                mapper.Delete("DelSeveritiesById", userid);
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
