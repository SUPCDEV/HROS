using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices; 


namespace HROUTOFFICE
{
    class ClassCryptography
    {        
        #region TripleDES

        public static string TripleDESGenerateKey()
        {
            TripleDESCryptoServiceProvider tripleDes = (TripleDESCryptoServiceProvider)TripleDESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(tripleDes.Key);
        }

        public static string TripleDESEncrypt(string DataToEncrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(DataToEncrypt);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string TripleDESDecrypt(string DataToEncrypt, string key, bool useHashing)
        {
            string ret = "-1";
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(DataToEncrypt);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();

            try
            {
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception)
            {
                return ret;
            }
        }

        public static bool FuctionChangePassword(string _emplId, string _password, string _key)
        {

            bool ret = false;
            string queryRun = @"";
            string newPassword = ClassCryptography.TripleDESEncrypt(_password, _key, true);

            queryRun = string.Format(@"UPDATE HROS_TSYSUSER SET PWPASS = @PWPASS, UPDATEPASSDATE = CONVERT(NVARCHAR, GETDATE(), 23), UPDATEPASSTIME = CONVERT(NVARCHAR, GETDATE(), 108) WHERE PWEMPLOYEE = @EMPLID", _emplId);

            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(DatabaseConfig.ServerConStr))
            {
                System.Data.SqlClient.SqlTransaction tranLocal = null;
                try
                {
                    con.Open();
                    tranLocal = con.BeginTransaction("UPDATEPASS");
                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand())
                    {
                        command.Connection = con;
                        command.Transaction = tranLocal;
                        command.CommandType = CommandType.Text;
                        command.CommandText = @queryRun;

                        command.Parameters.AddWithValue(@"PWPASS", newPassword);
                        command.Parameters.AddWithValue(@"EMPLID", _emplId);

                        if (command.ExecuteNonQuery() > 0)
                        {
                            try
                            {
                                tranLocal.Commit();
                                ret = true;
                            }
                            catch (Exception)
                            {
                                ret = false;
                            }
                        }
                        else
                        {
                            ret = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        tranLocal.Rollback();                        
                    }
                    catch (Exception) { }
                    ret = false;
                }
                finally
                {
                    con.Close();
                }            
            }

            return ret;
        }

        #endregion 

    }
}
