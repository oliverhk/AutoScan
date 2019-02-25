using System.IO;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Utility
{
    public class FileManager
    {

        private static SortedDictionary<int, List<string>> _pathList = new SortedDictionary<int, List<string>>();
        public FileManager()
        {
        }

        public static void CopyFolder(string srcPath, string desPath, bool isRoot, string parts)
        {
            //if workStation is missed ,return
            if (!Directory.Exists(srcPath))
            {
                return;
            }
            //current copy folder
            string curFolder = srcPath.Substring(srcPath.LastIndexOf("\\") + 1, srcPath.Length - srcPath.LastIndexOf("\\") - 1);

            //create current folder to local
            if (!Directory.Exists(desPath + "\\" + curFolder))
            {
                Directory.CreateDirectory(desPath + "\\" + curFolder);
            }
            string[] files = Directory.GetFiles(srcPath);
            //cycle copy
            for (int i = 0; i < files.Length; i++)
            {
                string name = files[i].Substring(files[i].LastIndexOf("\\") + 1, files[i].Length - files[i].LastIndexOf("\\") - 1);
                string tempCopyPath = desPath + "\\" + curFolder + "\\" + name;
                //if (!File.Exists(tempCopyPath))
                {
                    File.Copy(files[i], tempCopyPath, true);
                }
            }

            //get work station recipe data
            DirectoryInfo dirInfo = new DirectoryInfo(srcPath);

            foreach (DirectoryInfo fileItem in dirInfo.GetDirectories())
            {
                if (isRoot && parts == fileItem.Name || parts == "")
                {
                    //_delegateCallNo++;
                    //CopyDelegate caller = CopyFolder;
                    //caller.BeginInvoke(srcPath + "\\" + fileItem.Name, desPath + "\\" + curFolder, false, "", new AsyncCallback(CopyCallBack), caller);
                    CopyFolder(srcPath + "\\" + fileItem.Name, desPath + "\\" + curFolder, false, "");
                }
            }
        }
        //private static int _delegateCallNo = 0;
        //private static int _delegateCallBackNo = 0;
        //private delegate void CopyDelegate(string srcPath, string desPath, bool isRoot, string parts);
        //private static void CopyCallBack(IAsyncResult ar)
        //{
        //    _delegateCallBackNo++;
        //    CopyDelegate callback = (CopyDelegate)ar.AsyncState;
        //    callback.EndInvoke(ar);
        //} 

        public static void DeleteFile(string srcPath, DateTime createTime)
        {
            _pathList.Clear();
            DeleteFileItems(srcPath, 0, createTime);
            DeleteDirectory();
        }
        private static void DeleteFileItems(string path, int pathLevel, DateTime createTime)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(path);
                DirectoryInfo[] diretories = dInfo.GetDirectories();
                List<string> curList = new List<string>();
                if (_pathList.ContainsKey(pathLevel))
                {
                    curList = _pathList[pathLevel];
                }
                foreach (DirectoryInfo item in diretories)
                {
                    foreach (FileInfo fInfo in item.GetFiles())
                    {
                        if (fInfo.CreationTime < createTime)
                        {
                            File.Delete(fInfo.FullName);
                        }
                    }
                    curList.Add(item.FullName);
                    DeleteFileItems(item.FullName, pathLevel + 1, createTime);

                }
                if (_pathList.ContainsKey(pathLevel))
                {
                    _pathList[pathLevel] = curList;
                }
                else
                {
                    _pathList.Add(pathLevel, curList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void DeleteDirectory()
        {
            //根目录下的第一层，不删除。
            for (int i = _pathList.Count - 1; i >= 1; i--)
            {
                foreach (string path in _pathList[i])
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    if (Directory.Exists(path) && di.GetDirectories().Length == 0)
                    {
                        Directory.Delete(path);
                    }
                }
            }
        }
    }
}
