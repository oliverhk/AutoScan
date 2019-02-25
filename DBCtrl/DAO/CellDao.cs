using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class CellDao: BaseSqlMapDao
    {
        public IList<Cell> GetList()
        {
            IList<Cell> ret = new List<Cell>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = mapper.QueryForList<Cell>("SelectCellList", null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }
    }
}
