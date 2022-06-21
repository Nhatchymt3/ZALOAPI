using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Globalization;
using log4net;
using System.Security.Cryptography;
using System.IO;

namespace XProject.Core.Utils
{
    public static class Common
    {
        

        #region AppSetting
        public static string AppSettingKey(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string GetConnectString()
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[Constant.FTSS_CONNECT_STRING].ConnectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Resource
        public static string GetResourceString(string key)
        {
            try
            {
                return ResourceUtil.Instance.GetString(key);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

       

        #region Size
        public static string ConvertBytesToDisplayText(long byteCount)
        {
            string result = "";
            if(byteCount < 0) //?GB
            {
                // display as gb
                result = (Math.Abs(byteCount)/(float)100.0).ToString("#,#.##", CultureInfo.InvariantCulture) + " GB";
            }
            else if (byteCount > Math.Pow(1024, 3))
            {
                // display as gb
                result = (byteCount / Math.Pow(1024, 3)).ToString("#,#.##", CultureInfo.InvariantCulture) + " GB";
            }
            else if (byteCount > Math.Pow(1024, 2))
            {
                // display as mb
                result = (byteCount / Math.Pow(1024, 2)).ToString("#,#.##", CultureInfo.InvariantCulture) + " MB";
            }
            else if (byteCount > 1024)
            {
                // display as kb
                result = (byteCount / 1024).ToString("#,#.##", CultureInfo.InvariantCulture) + " KB";
            }
            else
            {
                // display as bytes
                if (byteCount == 0)
                    result = "0 Bytes";
                else
                    result = byteCount.ToString("#,#.##", CultureInfo.InvariantCulture) + " Bytes";
            }
            return result;
        }
        #endregion
        #region Get row string
        public static string GetRowString(string textString)
        {
            return textString.Replace("&nbsp;", "");
        }
        #endregion
        #region Contains Upper & Lower
        public static void Contains()
        {

        }
        #endregion
        public static string NullString(this string s)
        {
            if (s == "")
                return null;
            return s;
        }

        #region hash and encrypt text
        //hash password
        public static string GenerateHashWithSalt(string password, string salt)
        {
            string sHashWithSalt = password + salt;
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            // use hash algorithm to compute the hash
            System.Security.Cryptography.HashAlgorithm algorithm = new System.Security.Cryptography.SHA256Managed();
            // convert merged bytes to a hash as byte array
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            return Convert.ToBase64String(hash);
        }
        //encrypt
        public static string EncryptText(string password, string inputsalt)
        {
            string salt = "ddvddv123" + inputsalt;
            byte[] utfData = UTF8Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            string encryptedString = string.Empty;
            using (AesManaged aes = new AesManaged())
            {
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(salt, saltBytes);
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = rfc.GetBytes(aes.KeySize / 8);
                aes.IV = rfc.GetBytes(aes.BlockSize / 8);
                using (ICryptoTransform encryptTransform = aes.CreateEncryptor())
                {
                    using (MemoryStream encryptedStream = new MemoryStream())
                    {
                        using (CryptoStream encryptor =
                        new CryptoStream(encryptedStream, encryptTransform, CryptoStreamMode.Write))
                        {
                            encryptor.Write(utfData, 0, utfData.Length);
                            encryptor.Flush();
                            encryptor.Close();
                            byte[] encryptBytes = encryptedStream.ToArray();
                            encryptedString = Convert.ToBase64String(encryptBytes);
                        }
                    }
                }
            }
            if(encryptedString.EndsWith(Constant.STR_DECODE_64))
            {
                encryptedString = encryptedString.Remove(encryptedString.Length - 2, 2);
            }
            return encryptedString;
        }

        //decrypt
        public static string DecryptText(string decrypt, string inputsalt)
        {
            string salt = "ddvddv123" + inputsalt;
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            if(decrypt.Contains(" "))
              decrypt = decrypt.Replace(" ", "+");

            byte[] encryptedBytes = Convert.FromBase64String(decrypt);

            string decryptedString = string.Empty;
            using (var aes = new AesManaged())
            {
                //System.Security.Cryptography.SHA256 rfc = new SHA256(salt, saltBytes);
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(salt, saltBytes);
                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = rfc.GetBytes(aes.KeySize / 8);
                aes.IV = rfc.GetBytes(aes.BlockSize / 8);
                using (ICryptoTransform decryptTransform = aes.CreateDecryptor())
                {
                    using (MemoryStream decryptedStream = new MemoryStream())
                    {
                        CryptoStream decryptor = new CryptoStream(decryptedStream, decryptTransform, CryptoStreamMode.Write);
                        decryptor.Write(encryptedBytes, 0, encryptedBytes.Length);
                        decryptor.Flush();
                        decryptor.Close();
                        byte[] decryptBytes = decryptedStream.ToArray();
                        decryptedString = UTF8Encoding.UTF8.GetString(decryptBytes, 0, decryptBytes.Length);
                    }
                }
            }
            return decryptedString;
        }
        public static string AddEqualDescriptText(string descriptText)//
        {
            if (!string.IsNullOrEmpty(descriptText))
            {
                if (!descriptText.EndsWith(Constant.STR_DECODE_64) && descriptText.Contains(Constant.STR_SPLIT_PASS))
                {
                    return descriptText + Constant.STR_DECODE_64;
                }
            }
            return descriptText;            
        }
        #endregion
    }
}
