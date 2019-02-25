using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class RecipesDao : BaseSqlMapDao
    {
        public IList<Recipes> GetList()
        {
            IList<Recipes> rect = new List<Recipes>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = mapper.QueryForList<Recipes>("SelectRecipesList", null);
            }
            catch (Exception ex)
            { Logger.Error(ex); }
            return rect;
        }
        public Recipes GetRecipe(int recipeid)
        {
            Recipes rect = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = (Recipes)mapper.QueryForObject("SelectRecipes", recipeid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public Recipes GetDefaultRecipe()
        {
            Recipes rect = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = (Recipes)mapper.QueryForObject("SelectDefaultRecipe",0);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Insert(Recipes obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertRecipes", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Update(Recipes obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateRecipes", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool SetDefault(int rcpid)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateRecipesSetDefaultNull", null);
                mapper.Update("UpdateRecipesSetDefault", rcpid);
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
                mapper.Delete("DelRecipesById", rcpid);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public int MaxRecipeId()
        {
            int rect = 0;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                var rec =mapper.QueryForObject("SelectMaxRecipeId", null);
                rect = (int)rec;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
    }
}
