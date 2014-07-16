using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace WindowsFormsApplication1
{
    public class CommonHelper
    {
        /// <summary>
        /// 从网络api发送数据
        /// </summary>
        /// <param name="webClient"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetDataFromWeb(string url)
        {
            WebClient webClient = new WebClient();
            string json = string.Empty;
            json = webClient.DownloadString(url);
            return json;
        }

        ///<summary>
        ///MD5加密
        ///</summary>
        ///<param name="strPwd">被加密的字符串</param>
        ///<returns>返回被加密的字符串</returns>
        public static string GetMD5(string strPwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.Default.GetBytes(strPwd);//将字符biNMaybe为一个字节序列
            byte[] md5data = md5.ComputeHash(data); //计算data字节数组的哈希值
            md5.Clear();
            string str = "";
            for (int i = 4; i < 12; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }

        /// <summary>
        /// 用于截取double类型数字指定位数
        /// </summary>
        /// <param name="num">需要截取的数字</param>
        /// <param name="pI">设置截取的位数</param>
        /// <returns>返回截取后的数字</returns>
        public static double MyRand(double num, int pI)
        {
            if (num <= 0)
            {
                return 0;
            }
            double pNum = 0.0005;
            return Convert.ToDouble((num - pNum).ToString("0.000"));
        }

        /// <summary>
        /// 获取本机MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetLocalMac()
        {
            string strMac = string.Empty;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                    strMac += mo["MacAddress"].ToString();
            }
            return strMac.ToUpper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="host"></param>
        /// <param name="mac"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);
        /// <summary>
        /// 获取同网段的MAC
        /// </summary>
        /// <param name="clientIP">远端的IP</param>
        /// <returns></returns>
        public static string GetRemoteMac(string clientIP)
        {
            string ip = clientIP;
            if (ip == "127.0.0.1")
                ip = GetLocalIP()[0];
            Int32 ldest = inet_addr(ip);
            Int64 macinfo = new Int64();
            Int32 len = 6;
            try
            {
                SendARP(ldest, 0, ref macinfo, ref len);
            }
            catch
            {
                return "";
            }
            string originalMACAddress = Convert.ToString(macinfo, 16);
            if (originalMACAddress.Length < 12)
            {
                originalMACAddress = originalMACAddress.PadLeft(12, '0');
            }
            string macAddress;
            if (originalMACAddress != "0000" && originalMACAddress.Length == 12)
            {
                string mac1, mac2, mac3, mac4, mac5, mac6;
                mac1 = originalMACAddress.Substring(10, 2);
                mac2 = originalMACAddress.Substring(8, 2);
                mac3 = originalMACAddress.Substring(6, 2);
                mac4 = originalMACAddress.Substring(4, 2);
                mac5 = originalMACAddress.Substring(2, 2);
                mac6 = originalMACAddress.Substring(0, 2);
                macAddress = mac1 + "-" + mac2 + "-" + mac3 + "-" + mac4 + "-" + mac5 + "-" + mac6;
            }
            else
            {
                macAddress = "";
            }
            return macAddress.ToUpper();
        }
        /// <summary>
        /// 获取本地IP
        /// </summary>
        /// <returns></returns>
        public static string[] GetLocalIP()
        {
            string hostName = Dns.GetHostName();
            //IPHostEntry ipEntry=Dns.GetHostByName(hostName); 
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            IPAddress[] arr = ipEntry.AddressList;
            string[] result = new string[arr.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = arr[i].ToString();
            }
            return result;
        }

        public static string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }

}
