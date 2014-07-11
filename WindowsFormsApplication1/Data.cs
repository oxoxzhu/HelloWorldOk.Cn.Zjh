using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Data
    {


        public static int GetUserInfoResult = 0;
        public static int GetAssetsResult = 0;
        public static int GetTickerinfoResult = 0;

        public static int GetUserConfigResult = 0;
        public static int GetCheckVersionResult = 0;

        public static int GetDepthResult = 0;
        public static int GetOrderHistoryResult = 0;
        public static int GetOrderResult = 0;

        public static int GetLoginResult = 244534;

        public static int GetNewVersionResult = 0;

        public static int GetQQLoginResult = 0;
        public static String GetQQLoginUrl = "";

        /// <summary>
        /// //////////////////////////
        /// </summary>

        //public static IDictionary<String, String> OrderMap = new Dictionary<String, String>();
        //public static IDictionary<String, String> OrderHistoryMap = new Dictionary<String, String>();


        public static List<Consign> OrderList = new List<Consign>();
        public static List<Consign> OrderHistoryList = new List<Consign>();


        public static IDictionary<String, Double> free = new Dictionary<String, Double>();
        public static IDictionary<String, Double> freezed = new Dictionary<String, Double>();
        public static IDictionary<String, Double> freeall = new Dictionary<String, Double>();




        public static IDictionary<String, IDictionary<String, Double>> lending_account = new Dictionary<String, IDictionary<String, Double>>();
        public static IDictionary<String, Double> lending_account_child = new Dictionary<String, Double>();
        public static IDictionary<String, IDictionary<String, Double>> trading_account = new Dictionary<String, IDictionary<String, Double>>();
        public static IDictionary<String, Double> trading_account_child = new Dictionary<String, Double>();


        public static List<Consign> SellDepthList = new List<Consign>();
        public static List<Consign> BuyDepthList = new List<Consign>();






        public static Double btclast = 0;
        public static Double ltclast = 0;
        public static Double curlast = 0;


        public static void init()
        {

            lock (Data.free)
            {
                Data.free["cny"] = 0;
                Data.free["btc"] = 0;
                Data.free["ltc"] = 0;

                Data.free["cur_coin_num"] = 0;                
            }

            lock (Data.freezed)
            {
                Data.freezed["cny"] = 0;
                Data.freezed["btc"] = 0;
                Data.freezed["ltc"] = 0;
            }



            lock (Data.freeall)
            {
                Data.freeall["all"] = 0;
            }

            lock (Data.freeall)
            {
                Data.btclast = 0;
                Data.ltclast = 0;
                Data.curlast = 0;
            }

            Data.SwitchCurCoin(0);

        }
        public static void SwitchCurCoin(int flag)
        {
            Define.coin_cur = flag;
            if (flag == 0)
            {
                Define.coin_cur = Define.coin_btc;
                Define.trade_symbol_cur = Define.trade_symbol_btc_cny;
                lock (Data.free)
                {
                    Data.free["cur_coin_num"] = Data.free["btc"];
                }
                
            }
            else
            {
                Define.trade_symbol_cur = Define.trade_symbol_ltc_cny;
                Define.coin_cur = Define.coin_ltc;
                lock (Data.free)
                {
                    Data.free["cur_coin_num"] = Data.free["ltc"];
                }

            }
        }


        public static void SwitchCurCoinPrice(int flag)
        {
            if (flag == 0)
            {
                free["cur"] = free["btc"];
            }
            else
            {
                free["cur"] = free["ltc"];
            }
        }


    }


    public partial class Consign
    {
                     
                                        
        public String amount = "";//委单币的数量
        public String avg_rate = "";//平均成交价
        public String createDate = "";//时间
        public String deal_amount = "";//成交数量
        public String orders_id = "";//交易单号
        public String rate = "";//价格
        public String status = "";//挂单状态－1:已撤销 0/3:未成交 1:部分成交 2:完全成交
        public String type = "";//sell_market




        public void setprice(String _rate)
        {
            rate = _rate;
            //rate = (Double)Convert.ChangeType(_price, typeof(Double));
        }

        public void setnum(String _amount)
        {
            amount = _amount;
            /*
            _amount = _amount.Replace("฿", "");
            _amount = _amount.Replace("Ł", "");
            amount = (Double)Convert.ChangeType(_amount, typeof(Double));
             * */
        }
        static public String FormatNumber(String _v)
        {
            String value = _v.Replace("฿", "");
            value = value.Replace("Ł", "");
            value = value.Replace("￥", "");
            return value;
        }
    }



}
