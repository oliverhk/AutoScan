using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCtrl.Model;
namespace MainApp
{
    public interface IRecipe
    {        
        int RecipeId { get;}
        string RecipeName { get;}
        bool IsChangeRcp { get; set; }
        IList<Recipes> GetAllRcpList();
        CameraRecipe GetCameraRcp();
        ScanRecipe GetScanRcp();
        ControlRecipe GetControlRcp();
        Recipes GetRcp();
        void LoadDefaultRcp();
        void LoadRcp(int rcpid);
        bool AddRecipe(Recipes rcp, CameraRecipe camrcp, ScanRecipe scanrcp,ControlRecipe ctrlrcp);
        bool UpdateRecipe(Recipes rcp, CameraRecipe camrcp, ScanRecipe scanrcp,ControlRecipe ctrlrcp);
        bool DeleteRecipe(int rcpid);
        void SetDefault(int rcpid);
        bool IsExist(Recipes rcp);
    }
}
