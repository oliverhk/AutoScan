using System;
using System.IO;
using System.Xml;
using System.Text; 
using System.IO.Compression;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace Utility
{
    public static class XmlExSerializer
    {
        private static byte[] _key = new byte[] { 101, 111, 132, 66, 24, 0, 185, 74, 153, 211, 139, 7, 13, 247, 111, 150 };
        private static byte[] _iv = new byte[] { 123, 69, 174, 199, 204, 177, 41, 65, 130, 67, 72, 134, 126, 76, 31, 55 };

        #region Constructor

        static XmlExSerializer()
        {
        }

        #endregion

        #region Methods

        public static string Serialize<T>(T obj)
        { 
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj);
                return builder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                writer.Close();
                writer.Dispose();
            }
        }

        public static T Deserialize<T>(string data)
        {
            StringReader reader = new StringReader(data);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
        }

        public static void SerializeToFile<T>(T obj, string file)
        {
            SerializeToFile(obj, file, Encoding.UTF8);
        }

        public static void SerializeToFile<T>(T obj, string file, Encoding encoding)
        {
            SerializeToFile(obj, file, false, encoding);
        }

        public static void SerializeToFile<T>(T obj, string file, bool encrypt)
        {
            SerializeToFile(obj, file, encrypt, Encoding.UTF8);
        }

        public static void SerializeToFile<T>(T obj, string file, bool encrypt, Encoding encoding)
        {
            try
            {
                string text = Serialize<T>(obj);
                if (encrypt)
                    text = Security.AESEncrypt(text);
                File.WriteAllText(file, text, encoding);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T DeserializeByFile<T>(string path)
        {
            return DeserializeByFile<T>(path, Encoding.UTF8);
        }

        public static T DeserializeByFile<T>(string path, Encoding encoding)
        {
            return DeserializeByFile<T>(path, false, encoding);
        }

        public static T DeserializeByFile<T>(string path, bool decrypt)
        {
            return DeserializeByFile<T>(path, decrypt, Encoding.UTF8);
        }

        public static T DeserializeByFile<T>(string path, bool decrypt, Encoding encoding)
        {
            T obj = default(T);

            StringReader reader = null;
            try
            {
                try
                {
                    string text = File.ReadAllText(path, encoding);
                    if (decrypt)
                        text = Security.AESDecrypt(text);
                    reader = new StringReader(text);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    obj = (T)serializer.Deserialize(reader);
                }
                catch
                {
                    //读不成功,则加密或不加密的方式重读一次.
                    string text = File.ReadAllText(path, encoding);
                    if (!decrypt)
                        text = Security.AESDecrypt(text);
                    reader = new StringReader(text);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    obj = (T)serializer.Deserialize(reader);
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }
        }


        #endregion
    }

}
