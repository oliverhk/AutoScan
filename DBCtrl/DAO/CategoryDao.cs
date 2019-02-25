using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBCtrl.Model;
using IBatisNet.DataMapper;

namespace DBCtrl.DAO
{
    public class CategoryDao: BaseSqlMapDao
    {
        public IList<Category> GetList()
        {
            IList<Category> ret = new List<Category>();
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = mapper.QueryForList<Category>("SelectCategoryList", null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public Category GetCategory(int cateid)
        {
            Category ret = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = (Category)mapper.QueryForObject("SelectCategory", cateid);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public Category GetCategoryByName(string catename)
        {
            Category ret = null;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                ret = (Category)mapper.QueryForObject("SelectCategoryByName", catename);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Insert(Category obj)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Insert("InsertCategory", obj);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }

        public bool Update(Category obj)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Update("UpdateCategory", obj);
                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return ret;
        }
        public bool Delete(int cateid)
        {
            bool ret = false;
            try
            {
                ISqlMapper mapper = Mapper.Instance();
                mapper.Delete("DelCategoryById", cateid);
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
