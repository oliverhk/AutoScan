using System;
using System.Text;
using System.Security.Cryptography;

namespace Utility
{
    public class Security
    {
        private static readonly string AES_KEY = "57e8cd77-1f93-47ee-9e1f-cc91258e6980";

        private Security()
        {
        }

        public static string PasswordEncrypt(string number, string password)
        {
            string encrypt = "";
            if (!string.IsNullOrEmpty(password))
                encrypt = password;
            else
                encrypt = number;

            return MD5Encrypt(encrypt, 3);
        }

        public static string MD5Encrypt(string data, int times)
        {
            try
            {
                if (string.IsNullOrEmpty(data)) return string.Empty;

                MD5 md5 = MD5.Create();
                string result = data;
                for (int i = 0; i < times; i++)
                {
                    byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(result));
                    result = Convert.ToBase64String(hash);
                }
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string MD5Encrypt(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data)) return string.Empty;

                MD5 md5 = MD5.Create();
                byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(data));
                return Convert.ToBase64String(hash);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string AESEncrypt(string data)
        {
            return AESEncrypt(data, AES_KEY);
        }

        public static string AESEncrypt(string data, string key)
        {
            try
            {
                byte[] keyArray = Encoding.ASCII.GetBytes(MD5Encrypt(key));
                byte[] dataArray = Encoding.UTF8.GetBytes(data);
                byte[] result = null;

                using (RijndaelManaged rij = new RijndaelManaged())
                {
                    rij.Key = keyArray;
                    rij.Mode = CipherMode.ECB;
                    rij.Padding = PaddingMode.PKCS7;

                    ICryptoTransform transform = rij.CreateEncryptor();
                    result = transform.TransformFinalBlock(dataArray, 0, dataArray.Length);
                }
                return Convert.ToBase64String(result, 0, result.Length);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string AESDecrypt(string data)
        {
            return AESDecrypt(data, AES_KEY);
        }

        public static string AESDecrypt(string data, string key)
        {
            try
            {
                byte[] keyArray = Encoding.ASCII.GetBytes(MD5Encrypt(key));
                byte[] dataArray = Convert.FromBase64String(data);
                byte[] result = null;

                using (RijndaelManaged rij = new RijndaelManaged())
                {
                    rij.Key = keyArray;
                    rij.Mode = CipherMode.ECB;
                    rij.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = rij.CreateDecryptor();
                    result = cTransform.TransformFinalBlock(dataArray, 0, dataArray.Length);
                }
                return Encoding.UTF8.GetString(result);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static ushort CalculateCRC16(byte[] data)
        {
            ushort result = 0xFFFF;

            for (int i = 0; i < data.Length; i++)
            {
                result ^= data[i];

                for (int j = 0; j < 8; j++)
                {
                    if ((result & 0x0001) != 0)
                        result = (ushort)((result >> 1) ^ 0xA001);
                    else
                        result = (ushort)(result >> 1);
                }
            }
            return result;
        }

        public static ushort CalculateCRC16(byte[] data, ushort length)
        {
            ushort result = 0xFFFF;

            for (int i = 0; i < length; i++)
            {
                result ^= data[i];

                for (int j = 0; j < 8; j++)
                {
                    if ((result & 0x0001) != 0)
                        result = (ushort)((result >> 1) ^ 0xA001);
                    else
                        result = (ushort)(result >> 1);
                }
            }
            return result;
        }
    }
}
