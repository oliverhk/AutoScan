using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class MarkCellDao: BaseSqlMapDao
    {
        public IList<MarkCell> GetList()
        {
            IList<MarkCell> ret = new List<MarkCell>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = mapper.QueryForList<MarkCell>("SelectMarkCellList", null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public IList<MarkCell> GetSpecificList(string slideid, string swathid, string fieldid)
        {
            IList<MarkCell> ret = new List<MarkCell>();
            IList<MarkCell> ret2 = new List<MarkCell>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = mapper.QueryForList<MarkCell>("SelectMarkListBySlideId", slideid);

                foreach(MarkCell mark in ret)
                {
                    if (mark.SwathId == swathid && mark.FieldId == fieldid)
                    {
                        ret2.Add(mark);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret2;
        }

        public MarkCell GetMarkCellById(int id)
        {
            MarkCell ret = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = (MarkCell)mapper.QueryForObject("SelectMarkCellById", id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public MarkCell GetMarkCellByTime(DateTime time)
        {
            MarkCell ret = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = (MarkCell)mapper.QueryForObject("SelectMarkCellByTime", time);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Insert(MarkCell obj)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertMarkCell", obj);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Update(MarkCell obj)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateMarkCell", obj);
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
                mapper.Delete("DelMarkCellById", id);
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
