using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class Define
    {
        public static String channel_code = "4";

        public static String Protocol = "https://";
        public static String Domain = "www.okcoin.cn";

        //public static String Protocol = "http://";
        //public static String Domain = "local.okcoin.com";
       
        public static String Url=Protocol+Domain+"/";


        public static String Reset = Url + "user/reset.do";
        public static String Login = Url + "api/userLogin.do";

        //public static String LoginQQ = "http://local.okcoin.com/link/qq/call.do?isClient="+channel_code;
        //public static String LoginSina = "http://local.okcoin.com/link/weibo/call.do?isClient="+channel_code;


        public static String LoginQQ = Url + "link/qq/call.do?isClient=" + channel_code;
        public static String LoginSina = Url + "link/weibo/call.do?isClient=" + channel_code;

        public static String GoogleCheckUrl = Url + "api/submitTotpCode.do";//登录二次验证:


        public static String checkVersion = Url + "api/checkVersion.do";//版本检查

        
        public static String Userinfo = Url + "api/userinfo.do";
        public static String UserConfig = Url + "api/userConfig.do";



        public static String Assets = Url + "api/getAssets.do";
        public static String Tickerinfo = Url + "api/tickerinfo.do";//各网站行情信息
        //public static String Trade = Domain + "api/trade.do";//
        public static String Trade = Url + "api/trade_v2.do";//

        public static String Depth = Url + "api/depth.do";//委单
        public static String Order = Url + "api/getorder.do";//获取用户挂单
        public static String OrderHistory = Url + "api/getOrderHistory.do";//获取用户历史委托



        public static String CancelOrder = Url + "api/cancelorder.do";//撤销订单
        public static String AccountRecord = Url + "api/getAccountRecord.do";//获取账单信息


        public static String Version = "3.3";
        public static String TitleBase = "小股仙交易客户端OKCoin官方认证版" + Version;


        public static String TradeLasterrorCode = "";
        public static String TradeErrorNum = "";

        public static String RecordType_0 = "0";//0:全部
        public static String RecordType_1 = "1";//1:btc买入
        public static String RecordType_2 = "2";//2:btc卖出
        public static String RecordType_3 = "3";//3:btc充值
        public static String RecordType_4 = "4";//4:btc提现
        public static String RecordType_5 = "5";//5:cny充值
        public static String RecordType_6 = "6";//6:cny提现
        public static String RecordType_8 = "8";//8:ltc买入
        public static String RecordType_9 = "9";//9:ltc卖出 
        public static String RecordType_10 = "10";//10:ltc充值
        public static String RecordType_11 = "11";//11:ltc提现 (默认0)

        public static int Login_Succeed = 0;
        public static int Login_Google = 1;
        public static int Login_Google_Check_Falied = 2;
        public static int Login_Falied = 244534;


        public static int Trade_Succeed = 0;
        public static int Trade_Falied = 1;


        public static String GoogleCheck = "";
        public static int GoogleCheckResult = 1;


        public static int coin_btc = 0;
        public static int coin_ltc = 1;
        public static int coin_cur = -1;


        public static String trade_symbol_btc_cny = "btc_cny";
        public static String trade_symbol_ltc_cny = "ltc_cny";
        public static String trade_symbol_cur = "";


        public static String trade_type_buy = "buy";
        public static String trade_type_sell = "sell";

        public static String trade_type_buy_market = "buy_market";
        public static String trade_type_sell_market = "sell_market";



        public static int IsCheckVersion = 0;


        public static String NewVersionUrl = "";
        public static String NewVersionFileName = "";


        public static String tradePasswordEnabled = "";  



        public static String getTradeTypeString(string type)
        {
            switch (type)
            {
                case "buy":
                    {
                         return "限价买单"; 
                    }
                    break;
                case "sell":
                    {
                         return "限价卖单"; 
                    }
                    break;
                case "buy_market":
                    {
                        return "市价买单"; 
                    }
                    break;
                case "sell_market ":
                    {
                        return "市价卖单"; 
                    }
                    break;
                default:
                    {
                        return "其它";
                    }
                    break;
            }
        }

        public static String getTradeStatusString(string status)
        {
            switch (status)
            {
                case "-1":
                    {
                        return "已撤销";
                    }
                    break;
                case "0":
                case "3":
                    {
                        return "未成交";
                    }
                    break;
                case "1":
                    {
                        return "部分成交";
                    }
                    break;
                case "2":
                    {
                        return "完全成交";
                    }
                    break;
                default:
                    {
                        return "其它";
                    }
                    break;
            }
        }



        public static String getErrorCodeString(String errorcode)
        {
            switch (errorcode)
            {
                case "10000":
                    {
                        return "必选参数不能为空";
                    }
                    break;
                case "10001":
                    { 
 
                        return "用户请求过于频繁";
                    }
                    break;
                case "10002":
                    {
                        return "系统错误";
                    }
                    break;
                case "10003":
                    {
                        return "未在请求限制列表中,稍后请重试";
                    }
                    break;
                case "10004":
                    {
                        return "IP限制不能请求该资源";
                    }
                    break;
                case "10005":
                    {
                        return "密钥不存在";
                    }
                    break;
                case "10006":
                    {
                        return "用户不存在 ";
                    }
                    break;
                case "10007":
                    {
                        return "签名不匹配";
                    }
                    break;
                case "10008":
                    {
                        return "非法参数";
                    }
                    break;
                case "10009":
                    {
                        return "订单不存在";
                    }
                    break;
                case "10010":
                    {
                        return "余额不足";
                    }
                    break;
                case "10011":
                    {
                        return "买卖的数量小于BTC/LTC最小买卖额度";
                    }
                    break;
                case "10012":
                    {
                        return "当前网站暂时只支持btc_cny ltc_cny";
                    }
                    break;
                case "10013":
                    {
                        return "此接口只支持https请求";
                    }
                    break;     
                case "10014":
                    {
                        return "下单价格不得≤0或≥1000000";
                    }
                    break;      
                case "10015":
                    {
                        return "下单价格与最新成交价偏差过大";
                    }
                    break;
                case "10217":
                    {
                        return "请输入资金密码！";
                    }
                    break;
            }

            return "下单失败！未知原因。";

        }


    }
}
