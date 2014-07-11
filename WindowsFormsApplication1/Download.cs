using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Configuration;
using System.Web;
using System.Security;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

using System.Security.Cryptography;//加密服务的命名空间
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

using LitJson;
using System.Security;



namespace WindowsFormsApplication1
{
    class Download
    {
        //// <summary>
        /// WebClient上传文件至服务器
        /// </summary>
        /// <param name="fileNamePath">文件名，全路径格式</param>
        /// <param name="uriString">服务器文件夹路径</param>
        private static void UpLoadFile(string fileNamePath, string uriString)
        {
            //string fileName = fileNamePath.Substring(fileNamePath.LastIndexOf("\\") + 1);
            string NewFileName = DateTime.Now.ToString("yyMMddhhmmss") + DateTime.Now.Millisecond.ToString() + fileNamePath.Substring(fileNamePath.LastIndexOf("."));

            string fileNameExt = fileNamePath.Substring(fileNamePath.LastIndexOf(".") + 1);
            if (uriString.EndsWith("/") == false) uriString = uriString + "/";

            uriString = uriString + NewFileName;
            /**/
            /// 创建WebClient实例
            WebClient myWebClient = new WebClient();
            myWebClient.Credentials = CredentialCache.DefaultCredentials;

            // 要上传的文件
            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            //FileStream fs = OpenFile();
            BinaryReader r = new BinaryReader(fs);
            try
            {
                //使用UploadFile方法可以用下面的格式
                //myWebClient.UploadFile(uriString,"PUT",fileNamePath);
                byte[] postArray = r.ReadBytes((int)fs.Length);
                Stream postStream = myWebClient.OpenWrite(uriString, "PUT");
                if (postStream.CanWrite)
                {
                    postStream.Write(postArray, 0, postArray.Length);
                }
                else
                {
                    //MessageBox.Show("文件目前不可写！");
                }
                postStream.Close();
            }
            catch
            {
                //MessageBox.Show("文件上传失败，请稍候重试~");
            }
        }


        /**/
        /// <summary>
        /// 下载服务器文件至客户端

        /// </summary>
        /// <param name="URL">被下载的文件地址，绝对路径</param>
        /// <param name="Dir">另存放的目录</param>
        public static String GetDownload(string URL, string Dir)
        {
            WebClient client = new WebClient();
            String fileName = URL.Substring(URL.LastIndexOf("/") + 1); //被下载的文件名

            String Path = Dir + fileName;   //另存为的绝对路径＋文件名

            try
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                WebRequest myre = WebRequest.Create(URL);
            }
            catch
            {
                //MessageBox.Show(exp.Message,"Error"); 
            }

            try
            {
                client.DownloadFile(URL, Path);
                return fileName;
            }
            catch
            {
                //MessageBox.Show(exp.Message,"Error");
            }
            return "";

        }

        public static void testload()
        {
            GetDownload("http://192.168.0.69/client/OKCoinSetup.exe", "D:\\");
        }




    }
}


