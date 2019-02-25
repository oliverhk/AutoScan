using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class ScanRecipeDao : BaseSqlMapDao
    {
        public IList<ScanRecipe> GetList()
        {
            IList<ScanRecipe> rect = new List<ScanRecipe>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = mapper.QueryForList<ScanRecipe>("SelectScanRecipeList", null);
            }
            catch (Exception ex)
            { Logger.Error(ex); }
            return rect;
        }
        public ScanRecipe GetScanRecipe(int userid)
        {
            ScanRecipe rect = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = (ScanRecipe)mapper.QueryForObject("SelectScanRecipe", userid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Insert(ScanRecipe obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertScanRecipe", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Update(ScanRecipe obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateScanRecipe", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Delete(int rcpid)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Delete("DelScanRecipeById", rcpid);
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
