using System;
using System.IO;
using System.Drawing;
using System.Resources;
using System.Collections;
using System.Collections.Generic;

namespace Utility 
{
    public static class Resource
    {
        #region Fields

        private static readonly object _lockObj = new object();
        private static Dictionary<string, string> _stringRes;
        private static Dictionary<string, object> _iconRes;
        private static Dictionary<string, object> _bitmapRes;
        private static Dictionary<string, string> _languageTable;
        private static Stream _manifestResource;
        private static string _path;
        private static string _currentLanguage = "zh_CN";
        private static SerializableDictionary<string, string> _lossRes;
        private static string _lossResFile;

        public static Dictionary<string, string> LanguageTable
        {
            get { return _languageTable; }
        }

        public static string DefaultLanguage
        {
            get { return "zh_CN"; }
        }

        #endregion

        #region Constructor/Init

        static Resource()
        {
            _stringRes = new Dictionary<string, string>(new StringKeyIgnoreCaseCompare());
            _iconRes = new Dictionary<string, object>(new StringKeyIgnoreCaseCompare());
            _bitmapRes = new Dictionary<string, object>(new StringKeyIgnoreCaseCompare());
            _languageTable = new Dictionary<string, string>(new StringKeyIgnoreCaseCompare());
            _manifestResource = typeof(Resource).Assembly.GetManifestResourceStream("JN.Utility.Resources.Disable.ico");
            _lossRes = new SerializableDictionary<string, string>(new StringKeyIgnoreCaseCompare());            
            InitLanguageTable();
        }

        public static void ConfigResourcePath(string path)
        {
            _path = path;
            _lossResFile = Path.Combine(_path, "LossResource.txt");
            if (File.Exists(_lossResFile))
            {
                File.Delete(_lossResFile);
            }
        }

        private static void InitLanguageTable()
        {
            _languageTable.Add("zh_CN", "简体中文");
            _languageTable.Add("zh_TW", "繁體中文");
            _languageTable.Add("en_US", "English");
        }

        public static void LoadLanguage(string language )
        {
            if (Directory.Exists(_path))
            {
                string fileName = Path.GetFullPath(_path + language + ".resources");
                if (File.Exists(fileName))
                {
                    _currentLanguage = language;
                    _stringRes.Clear();
                    AddStringResource(fileName);

                    if (LanguageChanged != null)
                    {
                        LanguageChanged(null, EventArgs.Empty);
                    }
                }
            }
        }

        public static void LoadImages()
        {
            if (Directory.Exists(_path))
            {
                foreach (string fileName in Directory.GetFiles(_path, "Icons.resources", SearchOption.TopDirectoryOnly))
                {
                    AddIconResource(fileName);
                }

                foreach (string fileName in Directory.GetFiles(_path, "Bitmaps.resources", SearchOption.TopDirectoryOnly))
                {
                    AddBitmapResource(fileName);
                }
            }
        }

        #endregion

        #region Methods

        private static void AddStringResource(string fileName)
        {
            if (File.Exists(fileName))
            {
                ResourceReader reader = new ResourceReader(fileName);
                foreach (DictionaryEntry entry in reader)
                {
                    if (!_stringRes.ContainsKey(entry.Key.ToString()))
                        _stringRes.Add(entry.Key.ToString(), entry.Value.ToString());
                }
                reader.Close();
            }
        }

        private static void AddIconResource(string fileName)
        {
            if (File.Exists(fileName))
            {
                ResourceReader reader = new ResourceReader(fileName);
                foreach (DictionaryEntry entry in reader)
                {
                    if (!_iconRes.ContainsKey(entry.Key.ToString()))
                        _iconRes.Add(entry.Key.ToString(), entry.Value);
                }
                reader.Close();
            }
        }

        private static void AddBitmapResource(string fileName)
        {
            if (File.Exists(fileName))
            {
                ResourceReader reader = new ResourceReader(fileName);
                foreach (DictionaryEntry entry in reader)
                {
                    if (!_bitmapRes.ContainsKey(entry.Key.ToString()))
                        _bitmapRes.Add(entry.Key.ToString(), entry.Value);
                }
                reader.Close();
            }
        }

        public static string GetString(string keyword)
        {
            if (string.IsNullOrEmpty(keyword)) return keyword;

            if (_stringRes != null)
            {
                if (_stringRes.ContainsKey(keyword))
                {   
                    return _stringRes[keyword];
                }
                else if (_currentLanguage == DefaultLanguage)
                {
                    if (!_lossRes.ContainsKey(keyword))
                    {
                        _lossRes.Add(keyword, keyword);

                        if (!string.IsNullOrEmpty(_lossResFile))
                        {
                            lock (_lockObj)
                            {
                                StreamWriter sw = File.AppendText(_lossResFile);
                                sw.WriteLine(keyword);
                                sw.Flush();
                                sw.Close();
                            }
                        }
                    }
                }
            }
            return keyword;
        }

        public static string GetString(string keyword, params object[] paras)
        {
            return string.Format(GetString(keyword), paras);
        }

        public static Bitmap GetBitmap(string keyword)
        {
            if (_bitmapRes != null && _stringRes.ContainsKey(keyword))
            {
                object obj = _bitmapRes[keyword];
                if (obj != null && obj is Bitmap)
                    return (Bitmap)obj;
            }
            return new Icon(_manifestResource).ToBitmap();
        }

        public static Icon GetIcon(string keyword)
        {
            if (_iconRes != null && _stringRes.ContainsKey(keyword))
            {
                object obj = _iconRes[keyword];
                if (obj != null && obj is Icon)
                    return (Icon)obj;
            }
            return new Icon(_manifestResource);
        }

        #endregion

        public static event EventHandler LanguageChanged;
    }

    public class StringKeyIgnoreCaseCompare : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return (x.ToLower() == y.ToLower());
        }

        public int GetHashCode(string obj)
        {
            return obj.ToLower().GetHashCode();
        }
    }
}
