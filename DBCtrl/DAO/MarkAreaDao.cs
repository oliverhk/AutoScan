using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class MarkAreaDao : BaseSqlMapDao
    {
        public IList<MarkArea> GetList()
        {
            IList<MarkArea> ret = new List<MarkArea>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = mapper.QueryForList<MarkArea>("SelectMarkAreaList", null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public IList<MarkArea> GetSpecificList(string slideid, string swathid, string fieldid)
        {
            IList<MarkArea> ret = new List<MarkArea>();
            IList<MarkArea> ret2 = new List<MarkArea>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = mapper.QueryForList<MarkArea>("SelectMarkAreaListBySlideId", slideid);

                foreach (MarkArea markarea in ret)
                {
                    if (markarea.SwathId == swathid && markarea.FieldId == fieldid)
                    {
                        ret2.Add(markarea);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret2;
        }

        public MarkArea GetMarkAreaById(int id)
        {
            MarkArea ret = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = (MarkArea)mapper.QueryForObject("SelectMarkAreaById", id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public MarkArea GetMarkAreaByTime(DateTime time)
        {
            MarkArea ret = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = (MarkArea)mapper.QueryForObject("SelectMarkAreaByTime", time);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Insert(MarkArea obj)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertMarkArea", obj);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Update(MarkArea obj)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateMarkArea", obj);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Delete(int id)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Delete("DelMarkAreaById", id);
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
