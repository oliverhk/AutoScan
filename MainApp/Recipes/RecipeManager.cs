using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using Utility;
using DBCtrl.Model;
using DBCtrl.DAO;

namespace MainApp
{
    public class RecipeManager:IRecipe
    {
        #region property
        public int RecipeId { get; private set; }
        public string RecipeName { get; private set; }
        public bool IsChangeRcp { get; set; }
        private CameraRecipe _curCameraRcp;
        private Recipes _curRcp;
        private ScanRecipe _curScanRcp;
        private ControlRecipe _curControlRcp;

        #endregion
        #region instance
        //Singleton instance
        private static RecipeManager _instance;
        public static RecipeManager Instance => _instance ?? (_instance = new RecipeManager());
        public RecipeManager()
        { }
        #endregion

        #region get para

        public IList<Recipes> GetAllRcpList()
        {
            RecipesDao dao = new RecipesDao();
            return dao.GetList();
        }
        public CameraRecipe GetCameraRcp()
        {
            if (_curCameraRcp == null)
                LoadDefaultRcp();
            return _curCameraRcp;
        }
        public ScanRecipe GetScanRcp()
        {
            if (_curCameraRcp == null)
                LoadDefaultRcp();
            return _curScanRcp;
        }
        public ControlRecipe GetControlRcp()
        {
            if (_curControlRcp == null)
                LoadDefaultRcp();
            return _curControlRcp;
        }
        public Recipes GetRcp()
        {
            if (_curCameraRcp == null)
                LoadDefaultRcp();
            return _curRcp;
        }
        public void LoadDefaultRcp()
        {
            RecipesDao dao = new RecipesDao();
            Recipes rcp = dao.GetDefaultRecipe();
            if (rcp != null)
                LoadRcp(rcp.RecipeId);
            else
                LogHelper.AppLoger.Error("load default recipe failed.");
        }
        public void LoadRcp(int rcp_id)
        {
            try
            {
                RecipesDao dao = new RecipesDao();
                _curRcp = dao.GetRecipe(rcp_id);
                CameraRecipeDao camdao = new CameraRecipeDao();
                _curCameraRcp = camdao.GetCameraRecipe(rcp_id);
                ScanRecipeDao scandao = new ScanRecipeDao();
                _curScanRcp = scandao.GetScanRecipe(rcp_id);
                ControlRecipeDao ctrldao = new ControlRecipeDao();
                _curControlRcp = ctrldao.GetControlRecipe(rcp_id);
                RecipeId = _curRcp.RecipeId;
                RecipeName = _curRcp.Name;
                IsChangeRcp = true;
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        public bool IsExist(Recipes rcp)
        {
            bool rect = false;
            try
            {
                RecipesDao dao = new RecipesDao();
                _curRcp = dao.GetRecipe(rcp.RecipeId);
                if (_curRcp != null)
                    rect = true;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        #endregion
        #region set para
        public bool AddRecipe(Recipes rcp,CameraRecipe camrcp,ScanRecipe scanrcp,ControlRecipe ctrlrcp)
        {
            bool rect = false;
            try
            {
                RecipesDao dao = new RecipesDao();
                rect = dao.Insert(rcp);
                CameraRecipeDao camdao = new CameraRecipeDao();
                rect = camdao.Insert(camrcp);
                ScanRecipeDao scandao = new ScanRecipeDao();
                rect = scandao.Insert(scanrcp);
                ControlRecipeDao ctrldao = new ControlRecipeDao();
                rect = ctrldao.Insert(ctrlrcp);
            }
            catch(Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        public bool UpdateRecipe(Recipes rcp, CameraRecipe camrcp, ScanRecipe scanrcp,ControlRecipe ctrlrcp)
        {
            bool rect = false;
            try
            {
                RecipesDao dao = new RecipesDao();                
                rect = dao.Update(rcp);
                CameraRecipeDao camdao = new CameraRecipeDao();
                if(camdao.GetCameraRecipe(rcp.RecipeId)==null)
                    rect = camdao.Insert(camrcp);
                else
                    rect = camdao.Update(camrcp);
                ScanRecipeDao scandao = new ScanRecipeDao();
                if(scandao.GetScanRecipe(rcp.RecipeId)==null)
                    rect = scandao.Insert(scanrcp);
                else
                    rect = scandao.Update(scanrcp);
                ControlRecipeDao ctrldao = new ControlRecipeDao();
                if(ctrldao.GetControlRecipe(ctrlrcp.RecipeId)==null)
                    rect = ctrldao.Insert(ctrlrcp);
                else
                    rect = ctrldao.Update(ctrlrcp);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        public bool DeleteRecipe(int rcpid)
        {
            bool rect = false;
            try
            {
                RecipesDao dao = new RecipesDao();
                rect = dao.Delete(rcpid);
                CameraRecipeDao camdao = new CameraRecipeDao();
                rect = camdao.Delete(rcpid);
                ScanRecipeDao scandao = new ScanRecipeDao();
                rect = scandao.Delete(rcpid);
                ControlRecipeDao ctrldao = new ControlRecipeDao();
                rect = ctrldao.Delete(rcpid);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
            return rect;
        }
        public void SetDefault(int rcpid)
        {            
            try
            {
                RecipesDao dao = new RecipesDao();
                dao.SetDefault(rcpid);
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }            
        }
        #endregion

    }
}
