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
using System.Security.Cryptography.X509Certificates;
using System.Security;
using System.Data;
using System.Configuration;

using System.Net.Security;




namespace WindowsFormsApplication1
{
    class Http
    {
        public static CookieContainer cookie = new CookieContainer();




        /// <summary>
        /// 获取远程页面的HTML
        /// </summary>
        /// <param name="url">远程地址</param>
        /// <returns></returns>
        public static string HttpGet(String _url, String DataStr)
        {
            string Url = _url + "?" + DataStr;
            Encoding code = Encoding.GetEncoding("UTF-8");
            StreamReader sr = null;
            String str = null;

            try
            {
                //读取远程路径HttpWebRequest
                WebRequest request = WebRequest.Create(Url);
                //request.Referer = Define.Domain;
                WebResponse myTemp = request.GetResponse();
                sr = new StreamReader(myTemp.GetResponseStream(), code);
                //读取
                sr = new StreamReader(myTemp.GetResponseStream(), code);
                str = sr.ReadToEnd();
                sr.Close();

            }
            catch (Exception ex)
            {
                //throw ex;
            }
            finally
            {


            }
            return str;
        }



        public static string HttpDownload(String Url, String DataStr)
        {
            /*
            var fullQualifiedPathToDll = Server.MapPath("/") + "/bin/mydll.dll";
            var myFileStream = new FileStream(fullQualifiedPathToDll, FileMode.Open);
            var fileSize = myFileStream.Length;
            
            var buffer = new byte[(int)FileSize];
            myFileStream.Read(buffer, 0, (int)FileSize);

            myFileStream.Close();
            Response.BinaryWrite(buffer);
             */
            return "";
        }
        public static string HttpPost(String Url, String DataStr)
        {

            try
            {
                bool ishttps = false;
                String httpstr = Url.Substring(0,Url.IndexOf(":"));

                if (httpstr.ToLower() == "https")
                {
                    ishttps = true;
                } 

                if (ishttps == true)
                {
                    //类似浏览器确认证书合法方法的绑定
                    ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidationCallback;
                }


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                if (ishttps == true)
                {
                    //这2句代码表示如果要求客户端证书，将客户端证书加入request，不需要客户端证书的https请求则不需要此代码
                    X509Certificate cer = new X509Certificate("plist.dll");
                    request.ClientCertificates.Add(cer);
                }
                
                request.Method = "POST";
                request.Referer = Define.Domain;
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentLength = Encoding.UTF8.GetByteCount(DataStr);

                request.CookieContainer = cookie;



                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                myStreamWriter.Write(DataStr);
                myStreamWriter.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cookie.GetCookies(response.ResponseUri);


                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();


                response.Close();

                return retString;
            }
            catch (System.Exception ex)
            {
                return "";
            }

  

            return "";



            /*
            StringBuilder sb = new StringBuilder();
            string _strToRequest = "send";
            try
            {
                //POST请求开始 
                byte[] bt = Encoding.Default.GetBytes("send");
                HttpWebRequest Req = (HttpWebRequest)System.Net.WebRequest.Create("https://202.108.CCC.XXX:Port//");
                Req.KeepAlive = true;
                //Req.Timeout=60000; 
                Req.ContentType = "text/xml";
                Req.ContentLength = _strToRequest.Length;
                Req.Method = "POST";
                X509CertificateStore store = X509CertificateStore.CurrentUserStore(X509CertificateStore.MyStore);
                store.OpenRead();

                //读取证书的keyid 

                Microsoft.Web.Services2.Security.X509.X509CertificateCollection certs =
                store.FindCertificateByKeyIdentifier(Convert.FromBase64String("CXv+xZ78zI3qWHGJ6Wh9BF6B23A="));

                X509SecurityToken token = null;

                if (certs.Count > 0)
                {
                    // 得到证书存储区的第1个人证书 
                    token = new X509SecurityToken(((Microsoft.Web.Services2.Security.X509.X509Certificate)certs[0]));
                }

                if (token != null)
                    Req.ClientCertificates.Add(token.Certificate);

                Req.KeepAlive = true;

                Stream ReqStream = Req.GetRequestStream();

                ReqStream.Write(bt, 0, bt.Length);

                ReqStream.Close();

                //得到响应 

                HttpWebResponse res = (HttpWebResponse)Req.GetResponse();

                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.Default);

                sb.Append(sr.ReadToEnd());

                res.Close();

                sr.Close();

            }

            catch (Exception ex)
            {

                sb.Remove(0, sb.Length);

                sb.Append("<?xml version=\"1.0\" encoding=\"gb2312\"?>\n");

                sb.Append("<slia ver=\"1.0.0\">\n");

                sb.Append("<result resid=\"501\">" + ex.Message + "</result>\n");

                sb.Append("</slia>\n");

            }

            Console.WriteLine(sb.ToString());

            Console.Read(); 
            */

            return "";

        } 
 

        public static string HttpPost2(string Url, string DataStr) 
        {
           
                        
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request2.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request2.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            request.ContentLength = Encoding.UTF8.GetByteCount(DataStr);
            request2.ContentLength = Encoding.UTF8.GetByteCount(DataStr);

            
            request.CookieContainer = cookie;
            

            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(DataStr);
            myStreamWriter.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Cookies = cookie.GetCookies(response.ResponseUri);


            request2.CookieContainer = cookie;
            Stream myRequestStream2 = request2.GetRequestStream();
            StreamWriter myStreamWriter2 = new StreamWriter(myRequestStream2, Encoding.GetEncoding("gb2312"));
            myStreamWriter2.Write(DataStr);
            myStreamWriter2.Close();
            HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
            response2.Cookies = cookie.GetCookies(response2.ResponseUri);

            Stream myResponseStream = response2.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();


            response.Close();
            response2.Close();

            return retString; 
            


            /*
            StringBuilder sb = new StringBuilder();
            string _strToRequest = "send";
            try
            {
                //POST请求开始 
                byte[] bt = Encoding.Default.GetBytes("send");
                HttpWebRequest Req = (HttpWebRequest)System.Net.WebRequest.Create("https://202.108.CCC.XXX:Port//");
                Req.KeepAlive = true;
                //Req.Timeout=60000; 
                Req.ContentType = "text/xml";
                Req.ContentLength = _strToRequest.Length;
                Req.Method = "POST";
                X509CertificateStore store = X509CertificateStore.CurrentUserStore(X509CertificateStore.MyStore);
                store.OpenRead();

                //读取证书的keyid 

                Microsoft.Web.Services2.Security.X509.X509CertificateCollection certs =
                store.FindCertificateByKeyIdentifier(Convert.FromBase64String("CXv+xZ78zI3qWHGJ6Wh9BF6B23A="));

                X509SecurityToken token = null;

                if (certs.Count > 0)
                {
                    // 得到证书存储区的第1个人证书 
                    token = new X509SecurityToken(((Microsoft.Web.Services2.Security.X509.X509Certificate)certs[0]));
                }

                if (token != null)
                    Req.ClientCertificates.Add(token.Certificate);

                Req.KeepAlive = true;

                Stream ReqStream = Req.GetRequestStream();

                ReqStream.Write(bt, 0, bt.Length);

                ReqStream.Close();

                //得到响应 

                HttpWebResponse res = (HttpWebResponse)Req.GetResponse();

                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.Default);

                sb.Append(sr.ReadToEnd());

                res.Close();

                sr.Close();

            }

            catch (Exception ex)
            {

                sb.Remove(0, sb.Length);

                sb.Append("<?xml version=\"1.0\" encoding=\"gb2312\"?>\n");

                sb.Append("<slia ver=\"1.0.0\">\n");

                sb.Append("<result resid=\"501\">" + ex.Message + "</result>\n");

                sb.Append("</slia>\n");

            }

            Console.WriteLine(sb.ToString());

            Console.Read(); 
            */

            return "";

        } 


        public static string GetNewPrice(string jsonstr)
        {
            if (jsonstr == null)
            {
                return "";
            }

            String result = "";
            if (jsonstr.Length == 0)
            {
                return result;
            }

            try
            {
                JsonReader reader = new JsonReader(jsonstr);
                if (reader != null)
                {
                    JsonData data = JsonMapper.ToObject(reader);
                    if (data != null)
                    {

                        if (data["ticker"] != null)
                        {
                            JsonData datass = data["ticker"];
                            if (data["ticker"] != null)
                            {
                                result = datass["last"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            return result;
        }




        public static string HttpPost999(string Url, string DataStr)
        {
            //类似浏览器确认证书合法方法的绑定
            ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidationCallback;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

            string param = DataStr;
            byte[] bs = Encoding.UTF8.GetBytes(param);

            //这2句代码表示如果要求客户端证书，将客户端证书加入request，不需要客户端证书的https请求则不需要此代码
            X509Certificate cer = new X509Certificate("C:\\Users\\Administrator\\Desktop\\see.cer");
            request.ClientCertificates.Add(cer);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            request.ContentLength = Encoding.UTF8.GetByteCount(DataStr);


            request.CookieContainer = cookie;

           


            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(DataStr);
            myStreamWriter.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Cookies = cookie.GetCookies(response.ResponseUri);


            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();


            response.Close();

            /*
            using (Stream reqStram = request.GetRequestStream())
            {
                reqStram.Write(bs, 0, bs.Length);
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    String fdsa = reader.ReadToEnd();
                }
            }
             * */
            return "";
            //Console.ReadKey();
        }
        //该方法用于验证服务器证书是否合法，当然可以直接返回true来表示验证永远通过。服务器证书具体内容在参数certificate中。可根据个人需求验证
        //该方法在request.GetResponse()时触发
        public static bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            return false;
        }







    }
}
