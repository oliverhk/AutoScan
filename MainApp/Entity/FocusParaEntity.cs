using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace MainApp.Entity
{
    [Serializable]
    public class FocusParaEntity
    {
        private const string MEMORY_FILE = @"\configure\focuspara.dat";
        public FocusParaEntity()
        {
            Version = "V1.0";
        }
        public string Version { get; set; }
        private readonly Dictionary<string, ParaEntity> parpDict = new Dictionary<string, ParaEntity>();
        #region method
        private static readonly object sync = new object();
        public void WriteToFile()
        {
            try
            {
                lock (sync)
                {
                    var fileName = Directory.GetParent(System.Environment.CurrentDirectory) + MEMORY_FILE;
                    Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                    var binFormat = new BinaryFormatter();//创建二进制序列化器
                    binFormat.Serialize(fStream, this);
                    fStream.Close();
                }
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
            }
        }
        public static FocusParaEntity ReadFromFile()
        {
            try
            {
                var fileName = Directory.GetParent(System.Environment.CurrentDirectory) + MEMORY_FILE;
                if (File.Exists(fileName))
                {
                    var fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    var b = new BinaryFormatter();
                    var d = b.Deserialize(fStream) as FocusParaEntity;
                  
                    fStream.Close();
                    return d;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.AppLoger.Error(ex);
                return null;
            }
        }
        #endregion
        #region para list
        public void AddPara(ParaEntity block)
        {
            if (!parpDict.ContainsKey(block.Name))
            {
                lock (this)
                {
                    parpDict.Add(block.Name, block);
                }
                LogHelper.AppLoger.InfoFormat("Add Para Data to Dict: {0}", block);
            }
            else
            {
                lock (this)
                {
                    parpDict[block.Name] = block;
                }
                LogHelper.AppLoger.InfoFormat("Update Para Data in Dict: {0}", block);
            }
            WriteToFile();
        }
        public ParaEntity GetParaBlock(string key)
        {
            if (parpDict.ContainsKey(key))
                return parpDict[key];
            return null;
        }
        public bool ContainsParaBlock(string key)
        {
            return parpDict.ContainsKey(key);
        }
        
        public IList<string> GetParaBlockKeys()
        {
            return parpDict.Keys.ToList();
        }
        public void RemoveParaBlock(string key)
        {
            if (parpDict.ContainsKey(key))
            {
                lock (this)
                {
                    parpDict.Remove(key);
                }
                WriteToFile();
            }
        }
        #endregion
    }
}
