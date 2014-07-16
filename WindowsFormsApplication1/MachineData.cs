using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class MachineData
    {
        /// <summary>
        /// 配置杠杆，设置从哪个点开始玩,5表示能买1次，能卖5次
        /// </summary>
        public static int DGangGan = Convert.ToInt32(CommonHelper.GetConfig("StartGangGan"));
        /// <summary>
        /// 配置杠杆，默认是3，表示可以卖3倍，买0倍
        /// </summary>
        public static int GangGan = Convert.ToInt32(CommonHelper.GetConfig("DGangGan"));
        /// <summary>
        /// 设置上升利润达到多少进行卖出
        /// </summary>
        public static double RiseRate = Convert.ToDouble(CommonHelper.GetConfig("RiseRate"));
        /// <summary>
        /// 设置下降亏损达到多少进行买入
        /// </summary>
        public static double FallRate = Convert.ToDouble(CommonHelper.GetConfig("FallRate"));
        /// <summary>
        /// 开始操作的价格
        /// </summary>
        public static double PriceStart = Convert.ToDouble(CommonHelper.GetConfig("PriceStart"));
        /// <summary>
        /// 每次买卖的数量
        /// </summary>
        public static double OptNum = Convert.ToDouble(CommonHelper.GetConfig("OptNum"));
        

        //public bool IsSell { get; set; }
        public static double HighPrice = PriceStart;
        public static double LowPrice = PriceStart;
        public static double Num = OptNum;
        public static bool IsStop = false;

        //表示操作数量
        public static double BuyCount = 0;
        public static double SellCount = 0;
        public static double DiffBuySell = 0;

        public static bool IsCanBuy()
        {
            if (DGangGan < GangGan)
            {
                DGangGan++;
                return true;
            }
            return false;
        }

        public static bool IsCanSell()
        {
            if (DGangGan > 0)
            {
                DGangGan--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 计算所有的差
        /// </summary>
        public static void CalMaxDiff()
        {
            DiffBuySell = Math.Max(DiffBuySell, Math.Abs(BuyCount - SellCount));
        }
    }
}
