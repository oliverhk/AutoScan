using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class ControlRecipeDao : BaseSqlMapDao
    {
        public IList<ControlRecipe> GetList()
        {
            IList<ControlRecipe> rect = new List<ControlRecipe>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = mapper.QueryForList<ControlRecipe>("SelectCtrlRecipeList", null);
            }
            catch (Exception ex)
            { Logger.Error(ex); }
            return rect;
        }
        public ControlRecipe GetControlRecipe(int recipeid)
        {
            ControlRecipe rect = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = (ControlRecipe)mapper.QueryForObject("SelectCtrlRecipe", recipeid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Insert(ControlRecipe obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertCtrlRecipe", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Update(ControlRecipe obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateCtrlRecipe", obj);
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
                mapper.Delete("DelCtrlRecipeById", rcpid);
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
