using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class CameraRecipeDao : BaseSqlMapDao
    {
        public IList<CameraRecipe> GetList()
        {
            IList<CameraRecipe> rect = new List<CameraRecipe>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = mapper.QueryForList<CameraRecipe>("SelectCameraRecipeList", null);
            }
            catch (Exception ex)
            { Logger.Error(ex); }
            return rect;
        }
        public CameraRecipe GetCameraRecipe(int recipeid)
        {
            CameraRecipe rect = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                rect = (CameraRecipe)mapper.QueryForObject("SelectCameraRecipe", recipeid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Insert(CameraRecipe obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertCameraRecipe", obj);
                rect = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return rect;
        }
        public bool Update(CameraRecipe obj)
        {
            bool rect = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateCameraRecipe", obj);
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
                mapper.Delete("DelCameraRecipeById", rcpid);
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
