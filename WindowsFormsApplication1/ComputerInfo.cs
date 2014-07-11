
using System;
using System.Management;
using LitJson;


namespace WindowsFormsApplication1
{
    /// <summary>   
    /// Computer Information   
    /// </summary>   
    public class ComputerInfo
    {
        public string CpuID;
        public string MacAddress;
        public string DiskID;
        public string IpAddress;
        public string LoginUserName;
        public string ComputerName;
        public string SystemType;
        public string TotalPhysicalMemory; //单位：M   
        private static ComputerInfo _instance;
        public static ComputerInfo Instance()
        {
            if (_instance == null)
                _instance = new ComputerInfo();
            return _instance;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComputerInfo()
        {
            //CpuID = GetCpuID();
            //MacAddress = GetMacAddress();
            //DiskID = GetDiskID();
            //IpAddress = GetIPAddress();
            //LoginUserName = GetUserName();
            //SystemType = GetSystemType();
            //TotalPhysicalMemory = GetTotalPhysicalMemory();
            //ComputerName = GetComputerName();
        }



        public static JsonData GetBaseInfoData(String channel, String platform)
        {

            ComputerInfo ci = new ComputerInfo();
            String CpuID = ci.GetCpuID();
            String DiskID = ci.GetDiskID();
            String TotalPhysicalMemory = ci.GetTotalPhysicalMemory();            

            
            JsonData data = new JsonData();

            data["channel"] = channel;
            data["platform"] = platform;
            data["cpu"] = CpuID;
            data["disk"] = DiskID;
            data["memory"] = TotalPhysicalMemory;

            /*
            JsonReader reader = new JsonReader(expected);
            JsonData data1 = JsonMapper.ToObject(reader);
            String fdsa = data1["channel"].ToString();
            */
            
            return data;

        }


        public static String GetBaseInfo(String channel, String platform)
        {
            JsonData data = GetBaseInfoData(channel,platform);
            String json = data.ToJson();
            return json;

        }

        /// <summary>
        /// 获取cpu序列号
        /// </summary>
        /// <returns></returns>
        string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码   
                string cpuInfo = "";//cpu序列号   
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }
        /// <summary>
        /// 获取网卡硬件地址  
        /// </summary>
        /// <returns></returns>
        string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址   
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        string GetIPAddress()
        {
            try
            {
                //获取IP地址   
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        //st=mo["IpAddress"].ToString();   
                        System.Array ar;
                        ar = (System.Array)(mo.Properties["IpAddress"].Value);
                        st = ar.GetValue(0).ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }
        /// <summary>
        /// 获取硬盘ID 
        /// </summary>
        /// <returns></returns>
        string GetDiskID()
        {
            try
            {
                //获取硬盘ID   
                String HDid = "";
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    HDid = (string)mo.Properties["Model"].Value;
                }
                moc = null;
                mc = null;
                return HDid;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }

        /// <summary>   
        /// 操作系统的登录用户名   
        /// </summary>   
        /// <returns></returns>   
        string GetUserName()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["UserName"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }


        /// <summary>   
        /// PC类型   
        /// </summary>   
        /// <returns></returns>   
        string GetSystemType()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["SystemType"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }

        /// <summary>   
        /// 物理内存   
        /// </summary>   
        /// <returns></returns>   
        string GetTotalPhysicalMemory()
        {
            try
            {

                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st = mo["TotalPhysicalMemory"].ToString();

                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "";
            }
            finally
            {
            }
        }
        /// <summary>   
        ///    电脑名称
        /// </summary>   
        /// <returns></returns>   
        string GetComputerName()
        {
            try
            {
                return System.Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "";
            }
            finally
            {
            }
        }



        // 得到硬盘序列号
        public static String GetHDIndex()
        {
            String _HDInfo = "";
            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                _HDInfo = (String)mo.Properties["Model"].Value.ToString();
            }
            return _HDInfo;
        }



        //获取cpu信息
        public static String GetCpuIndex()
        {


            String _cpuInfo = "";
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                _cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }
            return _cpuInfo;
        }



        /*
        //方法二：主要是通过WMI方法获取系统的相关信息。具体有关常用WMI的使用列表，本人早已经整理到蒂强网络中了请访问：http://www.hd1204.com/article/html/1559.html//获取CPU的相关信息
          
         * 
         */

        public static String GetMachineInfo()
        {
            ManagementObjectSearcher searcher02 = new ManagementObjectSearcher("select * from Win32_Processor");
            string strCPUN = "";
            string strCPUDp = "";
            string strCPUCCS = "";
            string strCPUMCS = "";
            string strCPUMf = "";
            string strCPUEC = "";
            string strCPUFy = "";
            string strCPUL2CS = "";
            string strCPUNLP = "";
            string strCPUPId = "";
            string strCPUPLP = "";

            foreach (ManagementObject Pror in searcher02.Get())  //调用ManagementObject类GET方法//获取CPU的信息
            {
                strCPUN = Pror["Name"].ToString().Trim(); //CPU的规格
                strCPUDp = Pror["Description"].ToString().Trim(); //CPU的版本
                strCPUCCS = Pror["CurrentClockSpeed"].ToString().Trim(); //CPU的主频
                strCPUMCS = Pror["MaxClockSpeed"].ToString().Trim(); //CPU的最大频率(高频)
                strCPUMf = Pror["Manufacturer"].ToString().Trim(); //CPU的厂商
                strCPUEC = Pror["ExtClock"].ToString().Trim(); //CPU的前端总线
                strCPUFy = Pror["NumberOfCores"].ToString().Trim(); //CPU的核心数
                strCPUL2CS = Pror["L2CacheSize"].ToString().Trim(); //CPU的二级缓存
                strCPUNLP = Pror["NumberOfLogicalProcessors"].ToString().Trim(); //CPU逻辑核心个数
                strCPUPId = Pror["ProcessorId"].ToString().Trim(); //CPU序列号
                strCPUPLP = Pror["LoadPercentage"].ToString().Trim(); //CPU使用率
                break;
            }                    //此处本人用Label显示了cpu的相关信息，这样显得更直观一些！


            return "";

        }





    }
}
