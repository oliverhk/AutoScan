using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utility
{
    public static class BinSerializer
    {
        private static byte[] _key = new byte[] { 101, 111, 132, 66, 24, 0, 185, 74, 153, 211, 139, 7, 13, 247, 111, 150 };
        private static byte[] _iv = new byte[] { 123, 69, 174, 199, 204, 177, 41, 65, 130, 67, 72, 134, 126, 76, 31, 55 };

        public static void Serialize<T>(T obj, string targetFile)
        {
            Serialize<T>(obj, targetFile, false);
        }

        public static void Serialize<T>(T obj, string targetFile, bool encrypt)
        {
            if (encrypt)
            {
                MemoryStream ms = new MemoryStream();
                RijndaelManaged rm = new RijndaelManaged();
                CryptoStream cryptoStream = new CryptoStream(ms, rm.CreateEncryptor(_key, _iv), CryptoStreamMode.Write);
                DeflateStream zip = new DeflateStream(cryptoStream, CompressionMode.Compress, true);
                try
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(zip, obj);
                    zip.Close();                        // 在返回之前一定要进行关闭
                    cryptoStream.FlushFinalBlock();     // 在返回之前一定要进行调用
                    File.WriteAllBytes(targetFile, ms.ToArray());
                }
                finally
                {
                    cryptoStream.Close();
                    ms.Close();
                }
            }
            else
            {
                using (FileStream fs = new FileStream(targetFile, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, obj);
                }
            }
        }

        public static T Deserialize<T>(string originFile)
        {
            return Deserialize<T>(originFile, false);
        }

        public static T Deserialize<T>(string originFile, bool decrypt)
        {
            T obj = default(T);
            if (decrypt)
            {
                byte[] data = File.ReadAllBytes(originFile);
                MemoryStream ms = new MemoryStream(data);
                RijndaelManaged rm = new RijndaelManaged();
                CryptoStream cryptoStream = new CryptoStream(ms, rm.CreateDecryptor(_key, _iv), CryptoStreamMode.Read);
                DeflateStream unZip = new DeflateStream(cryptoStream, CompressionMode.Decompress);
                try
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    obj = (T)serializer.Deserialize(unZip);
                }
                finally
                {
                    unZip.Close();
                    cryptoStream.Close();
                    ms.Close();
                }
            }
            else
            {
                using (FileStream fs = new FileStream(originFile, FileMode.Open, FileAccess.Read, FileShare.Read, 2 * 1024 * 1024))  //2M Buffer 
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    obj = (T)formatter.Deserialize(fs);
                }
            }
            return obj;
        }

    }
}
