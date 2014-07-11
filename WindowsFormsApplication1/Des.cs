using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Security.Cryptography;//加密服务的命名空间
using System.Net.NetworkInformation;

using System.Net;

using System.Collections;
using System.Collections.Specialized;
using System.Reflection;

using LitJson;
using System.Security.Cryptography;

namespace WindowsFormsApplication1
{

    class Des
    {
        const string KEY_64 = "ldh562lx";
        const string IV_64 = "ldh562lx"; //注意了，是8个字符，64位

        public static String[] strset = { "฿", "】", "］", "『", "‖", "∶", "？", "！", "·", "ˇ", ";", "“", "々", "②", "⑤", "⑧", "⒏", "⒌", "⊙", "∈", "∪", "∴", "∷", "Ｖ" };

        public Des()
        {
            
        }


        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        
        public static int getTime(string _time) 
        {
            DateTime dtTime1 = new DateTime();
            if (_time.Length > 0)
            {
               dtTime1 = Convert.ToDateTime(_time);
            }
            else
            {
                dtTime1 = DateTime.Now;
            }
            
            return ConvertDateTimeInt(dtTime1);
        }

        public static int getTimeMinute(string _time)
        {
            DateTime dtTime1 = new DateTime();
            if (_time.Length > 0)
            {
                dtTime1 = Convert.ToDateTime(_time);
            }
            else
            {
                dtTime1 = DateTime.Now;
            }
            DateTime dtTime2 = new DateTime(dtTime1.Year, dtTime1.Month, dtTime1.Day, dtTime1.Hour, dtTime1.Minute, 0); ;


            return ConvertDateTimeInt(dtTime2);
        }

        public static string getTimeString(string _time)
        {
            if (_time.Length > 0)
            {
                DateTime dt = GetTime(_time);
                return dt.ToString("MM-dd hh:mm:ss");
            }
            return "";
        }


        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }


        public static string GetFullStr()
        {
            
            Random ro = new Random(10);
            long tick = DateTime.Now.Millisecond;

            Random ran = new Random((int)(tick));


            //Random Random1 = new Random();
            int i = ran.Next(1, strset.Length - 1);
            return strset[i];
            
        }


        public static string Encode(string data)
        {
            string reslut = "";

            try
            {
                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);//加密钥匙转换为
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);//初始向量
                //二者共同影响加密后的数据
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();//加密算法的执行者
                int i = cryptoProvider.KeySize;
                MemoryStream ms = new MemoryStream();//这是向内存中写入的流
                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);//用于加密的流
                //接入内存流
                //使用根据密匙和初始向量创建加密因子
                //通过加密流写入到内存中，在内村中以byte数组的形式存在

                StreamWriter sw = new StreamWriter(cst);//再将加密流接到文本流中
                sw.Write(data);//SW--->加密流----->内存流
                sw.Flush();//清除文本流
                cst.FlushFinalBlock();//清除加密流中的内容
                sw.Flush();
                reslut = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);//将内存中存在的byte【】数组，取出来，转换为经过加密的文本！

            }
            catch
            {
                return reslut;
            }
            return reslut;

        }

        public static string Decode(string data)
        {   
            string reslut = "";

            try
            {

                byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);//将密匙转换成二进制数组
                byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);//将初始初始向量转换成二进制数据

                byte[] byEnc;

                byEnc = Convert.FromBase64String(data);//将加过秘的文字转换成数组

                MemoryStream ms = new MemoryStream(byEnc);//将内存流接到上面去
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();//创建des加密算法执行类

                CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);//解密流接到内存流上
                StreamReader sr = new StreamReader(cst);//文本读取流

                reslut = sr.ReadToEnd();
            }
            catch
            {
                return reslut;
            }

            return reslut;//加过秘的文字转化过来的数组------>内存流---------->解密流------------>原始文字
        }

       


        public static string GetMD5Hash(String input)
        {
            byte[] result = Encoding.Default.GetBytes(input);

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] output = md5.ComputeHash(result);

            string encryptResult = BitConverter.ToString(output).Replace("-", "");

            return encryptResult;


        }


        public static void OpenUrl(String url)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            //Process process = new Process();
            process.StartInfo.FileName = "iexplore.exe";
            process.StartInfo.Arguments = url;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            return;
        }

        public static void Execute(String filename, String Arguments)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            //Process process = new Process();
            process.StartInfo.FileName = filename;
            process.StartInfo.Arguments = Arguments;
            process.StartInfo.UseShellExecute = true;
            process.Start();
            return;
        }

    }


}




