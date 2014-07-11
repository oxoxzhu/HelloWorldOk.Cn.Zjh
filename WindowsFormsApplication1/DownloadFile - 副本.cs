using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Security.Cryptography;//加密服务的命名空间
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

using System.Net;

using LitJson;
using System.Web;

using System.Security.Cryptography.X509Certificates;


using System.Security;

//using System.Security.Tokens;

//using System.Security.X509; 



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
using System.IO;
using System.Security.Cryptography;//加密服务的命名空间
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

using System.Net;

using LitJson;


using System.Security.Cryptography.X509Certificates;



namespace WindowsFormsApplication1
{
    class DownloadFile
    {

        HttpWebRequest request = null;
        HttpWebResponse Response = null;

        DownloadFile()
        {
            request = (HttpWebRequest)WebRequest.Create(Url);
            Response = (HttpWebResponse)request.GetResponse();
        }

        //一、TransmitFile实现下载     
        protected void Button1_Click(object sender, EventArgs e)
        {
            /*         微软为Response对象提供了一个新的方法TransmitFile来解决使用Response.BinaryWrite 
                * 下载超过400mb的文件时导致Aspnet_wp.exe进程回收而无法成功下载的问题。    
                * 代码如下：         
                */ 

            Response.ContentType = "application/x-zip-compressed";   
            Response.AddHeader("Content-Disposition", "attachment;filename=z.zip");
            string filename = Server.MapPath("DownLoad/z.zip");
            Response.TransmitFile(filename);    
        }

        //二、WriteFile实现下载     
        protected void Button2_Click(object sender, EventArgs e)
        {
            /*         using System.IO;        */
            string fileName = "asd.txt";
            //客户端保存的文件名   
            string filePath = Server.MapPath("DownLoad/aaa.txt");
            //路径       
            FileInfo fileInfo = new FileInfo(filePath);   
            Response.Clear();    
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary"); 
            Response.ContentType = "application/octet-stream"; 
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");  
            Response.WriteFile(fileInfo.FullName);    
            Response.Flush();    
            Response.End();   
        }

        //三、   WriteFile分块下载     
        protected void Button3_Click(object sender, EventArgs e)  
        {      
            string fileName = "aaa.txt";//客户端保存的文件名  
            string filePath = Server.MapPath("DownLoad/aaa.txt");//路径  
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);  
            if (fileInfo.Exists == true)  
            {     
                const long ChunkSize = 102400;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力   
                byte[] buffer = new byte[ChunkSize];   
                Response.Clear();      
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);  
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小    
                Response.ContentType = "application/octet-stream";



                String s = fileName; 
                byte[] buffer = Encoding.UTF8.GetBytes(s);
                String Text = Encoding.GetEncoding("GBK ").GetString(buffer);//HttpUtility.UrlEncode(fileName)



                Response.AddHeader("Content-Disposition", "attachment; filename=" + Text);     
                while (dataLengthToRead > 0 && Response.IsClientConnected)      
                {        
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小    
                    Response.OutputStream.Write(buffer, 0, lengthRead);       
                    Response.Flush();      
                    dataLengthToRead = dataLengthToRead - lengthRead;   
                }    
                Response.Close();     
            }   
        }

        //四、流方式下载
        protected void Button4_Click(object sender, EventArgs e)
        {    
            string fileName = "aaa.txt";//客户端保存的文件名    
            string filePath = Server.MapPath("DownLoad/aaa.txt");//路径   
            //以字符流的形式下载文件    
            FileStream fs = new FileStream(filePath, FileMode.Open);    
            byte[] bytes = new byte[(int)fs.Length];    
            fs.Read(bytes, 0, bytes.Length);  
            fs.Close();     
            Response.ContentType = "application/octet-stream";   
            //通知浏览器下载文件而不是打开   


            String s = fileName;
            byte[] buffer = Encoding.UTF8.GetBytes(s);
            String Text = Encoding.GetEncoding("GBK ").GetString(buffer);//HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8)


            Response.AddHeader("Content-Disposition", "attachment; filename=" + Text); 
            Response.BinaryWrite(bytes);  
            Response.Flush();     
            Response.End(); 
        }

        //----------------------------------------------------------
        public void DownloadFile1( String FileNameWhenUserDownload, String FileBody)
        {
            WebForm.Response.ClearHeaders();　
            WebForm.Response.Clear();　
            WebForm.Response.Expires = 0;　
            WebForm.Response.Buffer = true;　
            WebForm.Response.AddHeader("Accept-Language", "zh-tw");　
            //'文件名称
            WebForm.Response.AddHeader("content-disposition", "attachment; filename='"+System.Web.HttpUtility.UrlEncode(FileNameWhenUserDownload, System.Text.Encoding.UTF8)+"'");　
            WebForm.Response.ContentType = "Application/octet-stream";　
            //'文件内容　　WebForm.Response.Write(FileBody);
            //----------- 
            WebForm.Response.End();
        }

        //上面这段代码是下载一个动态产生的文本文件，若这个文件已经存在于服务器端的实体路径，则可以通过下面的函数：
        public void DownloadFileByFilePath( String FileNameWhenUserDownload ,String FilePath )
        {　
            WebForm.Response.ClearHeaders();
            WebForm.Response.Clear();
            WebForm.Response.Expires = 0; 
            WebForm.Response.Buffer = true;　
            WebForm.Response.AddHeader("Accept-Language", "zh-tw");　
            //文件名称
            WebForm.Response.AddHeader("content-disposition", "attachment; filename='" + System.Web.HttpUtility.UrlEncode(FileNameWhenUserDownload, System.Text.Encoding.UTF8) +"'" );　
            WebForm.Response.ContentType = "Application/octet-stream";　
            //文件内容　　
            WebForm.Response.Write(System.IO.File.ReadAllBytes(FilePath));
            //---------　　
            WebForm.Response.End();
        }


    }
}

