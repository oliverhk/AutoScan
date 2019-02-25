using System;
using System.IO;
using System.Xml;

namespace Utility
{
    public class XmlConfig
    {
        private static string _fileName;
        private static XmlDocument _xmlDoc;
        private static string _defaultRootName = "Config";

        static XmlConfig()
        {
        }

        public static void SetConfigFile(string fileName)
        {
            SetConfigFile(fileName, _defaultRootName);
        }

        public static void SetConfigFile(string fileName, string rootName)
        {
            _fileName = fileName;
            _defaultRootName = rootName;

            try
            {
                _xmlDoc = new XmlDocument();
                if (File.Exists(_fileName))
                {
                    _xmlDoc.Load(_fileName);
                }
                else
                {
                    File.Create(_fileName);
                    CreateRoot();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetAttribute(string path, string name)
        {
            try
            {
                XmlElement element = ParsePath(path);
                if (element != null)
                {
                    return element.GetAttribute(name);
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetAttribute(string path, string name, string defaultValue)
        {
            try
            {
                string value = GetAttribute(path, name);
                if (string.IsNullOrEmpty(value))
                {
                    return defaultValue;
                }
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetAttribute(string path, string name, string value)
        {
            try
            {
                XmlElement element = ParsePath(path);
                if (element != null)
                {
                    element.SetAttribute(name, value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteAttribute(string path, string name)
        {
            try
            {
                XmlElement element = ParsePath(path);
                if (element != null)
                {
                    element.RemoveAttribute(name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetElement(string path)
        {
            try
            {
                XmlElement element = ParsePath(path);
                if (element != null)
                {
                    return element.InnerText;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetElement(string path, string defaultValue)
        {
            try
            {
                string value = GetElement(path);
                if (string.IsNullOrEmpty(value))
                {
                    return defaultValue;
                }
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetElement(string path, string value)
        {
            try
            {
                XmlElement element = ParsePath(path);
                if (element != null)
                {
                    element.InnerText = value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteAllChilds(string path)
        {
            try
            {
                XmlElement element = ParsePath(path);
                if (element != null)
                {
                    element.RemoveAll();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteAllAttributes(string path)
        {
            try
            {
                XmlElement element = ParsePath(path);
                if (element != null)
                {
                    element.RemoveAllAttributes();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Save()
        {
            try
            {
                _xmlDoc.Save(_fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static XmlElement ParsePath(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path)) return null;

                string[] nodeNames = path.Split(new char[] { '/' });
                if (nodeNames.Length <= 1) return null;

                XmlNode node = _xmlDoc.SelectSingleNode(nodeNames[0]); //root node
                if (node == null) CreateRoot();

                string nodeName = "";
                for (int i = 1; i < nodeNames.Length; i++)
                {
                    nodeName = nodeNames[i];
                    if (node.SelectSingleNode(nodeName) != null)
                    {
                        node = node.SelectSingleNode(nodeName);
                    }
                    else
                    {
                        XmlNode child = _xmlDoc.CreateElement(nodeName);
                        node.AppendChild(child);
                        node = node.SelectSingleNode(nodeName);
                    }
                }
                return node as XmlElement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateRoot()
        {
            XmlElement root = _xmlDoc.CreateElement(_defaultRootName);
            _xmlDoc.AppendChild(root);
        }

    }
}
