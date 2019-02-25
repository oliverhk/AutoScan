using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class CellAIResultDao: BaseSqlMapDao
    {
        public IList<CellAIResult> GetList()
        {
            IList<CellAIResult> ret = new List<CellAIResult>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = mapper.QueryForList<CellAIResult>("SelectAIResultList", null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }
    }
}
