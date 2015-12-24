using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using BW.Data.Contract.DTOs;
using System.Net;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web;


namespace BW.Website.Common.Utilities
{
    public class ApiServiceUtilities
    {

        public static HttpClient ConnectClient()
        {
            var url = ConfigurationManager.AppSettings["BWApiServiceUrl"];
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var credentials = Encoding.ASCII.GetBytes("myUsername:myPassword");
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credentials));
            return client;
        }

        public static HttpResponseMessage GetReponse(string path)
        {
            HttpClient client = ConnectClient();
            client.DefaultRequestHeaders.Add("ApiKey", ComputeHash());
            HttpResponseMessage reponse = client.GetAsync(path).Result;
            return reponse;
        }

        public static HttpResponseMessage PostJson(string path, object value)
        {
            HttpClient client = ConnectClient();
            client.DefaultRequestHeaders.Add("ApiKey", ComputeHash());
            HttpResponseMessage reponse = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            try
            {
                reponse = client.PostAsJsonAsync(path, value).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode()).Result;
                return reponse;
            }
            catch (AggregateException e)
            {
                return reponse;
            }
        }

        public static HttpResponseMessage PostJson2(string path, object value)
        {
            HttpClient client = ConnectClient();
            client.DefaultRequestHeaders.Add("ApiKey", ComputeHash());
            HttpResponseMessage reponse = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
            string decryp = Encrypt(path);
            string encryp = Decrypt(decryp);
            try
            {                
                reponse = client.PostAsJsonAsync(path, value).Result;
                return reponse;                
            }
            catch (AggregateException e)
            {
                return reponse;
            }
        }

        public static string ComputeHash( )
        {
            string hashedPassword = ConfigurationManager.AppSettings["password"];
            string username = ConfigurationManager.AppSettings["username"];
            var key = Encoding.UTF8.GetBytes(hashedPassword.ToUpper());
            string hashString;

            using (var hmac = new HMACSHA256(key))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(username));
                hashString = Convert.ToBase64String(hash);
            }

            return hashString + "ApiKey" + DateTime.UtcNow.Ticks; ;
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string Encrypt(string toEncrypt)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            bool useHashing = true;

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("password",
                                                             typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            //ICryptoTransform cTransform = tdes.CreateEncryptor();
            ////transform the specified region of bytes array to resultArray
            //byte[] resultArray =
            //  cTransform.TransformFinalBlock(toEncryptArray, 0,
            //  toEncryptArray.Length);
            ////Release resources held by TripleDes Encryptor
            //tdes.Clear();
            ////Return the encrypted data into unreadable string format
            //return Convert.ToBase64String(resultArray, 0, resultArray.Length);

            using (ICryptoTransform cTransform = tdes.CreateEncryptor())
            {
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
        }

        public static string Decrypt(string cipherString)
        {
            byte[] keyArray;
            //get the byte code of the string
            bool useHashing = true;

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("password",
                                                         typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);

            //using (ICryptoTransform cTransform = tdes.CreateEncryptor())
            //{
            //    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //    tdes.Clear();
            //    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            //}
        }
    }
}
