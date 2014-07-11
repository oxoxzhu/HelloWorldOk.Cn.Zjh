using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;


namespace WindowsFormsApplication1
{
    class Protocol
    {
        public static JsonData GetJsonData(string Json)
        {
            if (Json == null || Json =="")
            {
                return null;
            }

            JsonReader reader = new JsonReader(Json);
            if (reader != null)
            {
                JsonData data = JsonMapper.ToObject(reader);
                if (data != null)
                {
                    if (data.Count == 0)
                    {
                        return null;
                    }
                    return data;
                }
            }
            return null;
        }


        public static int DoLogin(string loginName, string password)
        {
            next:
            try
            {

                String JsonStr = GetLoginResult(loginName, password);
                int i = 0;
                JsonData jsondata = GetJsonData(JsonStr);
                if (jsondata != null)
                {
                    if (jsondata["resultCode"].ToString() == "0")
                    {
                        return Define.Login_Succeed;
                    }
                    else if (jsondata["resultCode"].ToString() == "1")
                    {
                        i++;
                        if (i == 1)
                        {
                            return Define.Login_Google;
                        }

                    }
                    else if (jsondata["resultCode"].ToString() == "-3" || jsondata["resultCode"].ToString() == "3")
                    {
                        i++;
                        if (i == 1)
                        {
                            goto next;
                        }

                    }
                }
            }catch
            {
                return Define.Login_Falied;
            }
            return Define.Login_Falied;
        }

        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }



        public static String GetLoginResult(string loginName, string password)
        {
            String Pwmd5 = UrlEncode(password);//String Pwmd5 = Des.GetMD5Hash(password).ToString();
            String JsonStr = Http.HttpPost(Define.Login, "loginName=" + loginName + "&password=" + Pwmd5 + "&isClient=" + Define.channel_code);
            return JsonStr;
        }



        public static int GetQQLogin()
        {
            /*
            String JsonStr = Http.HttpGet(Define.QQLogin, "isClient="+channel_code);

            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {
                if (jsondata["errorCode"] != null)
                {
                    if (jsondata["errorCode"].ToString() == "0")
                    {
                        if (jsondata["url"] != null)
                        {
                            if (jsondata["url"].ToString() != "")
                            {
                                Data.GetQQLoginUrl = jsondata["url"].ToString();
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
             * */
            return Define.Login_Falied;
        }


        public static int GetGoogleCheckResult(string totpCode)
        {
            String JsonStr = Http.HttpPost(Define.GoogleCheckUrl, "totpCode=" + totpCode + "&isClient=" + Define.channel_code);

            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {
                if (jsondata["resultCode"].ToString() == "0")
                {
                    Data.GetLoginResult = Define.Login_Succeed;
                    //Define.GoogleCheckResult = Define.Login_Succeed;
                    //return Define.Login_Succeed;//0:登录成功
                }
                else 
                {
                    //1:验证码格式不正确
                    //2:验证码输错  剩余次数 errorNum

                    Define.GoogleCheckResult = Define.Login_Google_Check_Falied;

                    //return Define.Login_Google;
                }
            }


            return 0;
        }

        public static int GetUserInfo()
        {

            String JsonStr = Http.HttpPost(Define.Userinfo, "isClient=" + Define.channel_code);

            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {

                if (jsondata["info"] != null)
                {
                    if (jsondata["info"]["funds"] != null)
                    {
                        if (jsondata["info"]["funds"]["free"] != null)
                        {
                            String free_btc = jsondata["info"]["funds"]["free"]["btc"].ToString();
                            String free_cny = jsondata["info"]["funds"]["free"]["cny"].ToString();
                            String free_ltc = jsondata["info"]["funds"]["free"]["ltc"].ToString();

                            lock (Data.free)
                            {
                                Data.free["btc"] = (Double)Convert.ChangeType(free_btc, typeof(Double));
                                Data.free["cny"] = (Double)Convert.ChangeType(free_cny, typeof(Double));
                                Data.free["ltc"] = (Double)Convert.ChangeType(free_ltc, typeof(Double));

                                if (Define.coin_cur == Define.coin_btc)
                                {
                                    Data.free["cur_coin_num"] = Data.free["btc"];
                                } 
                                else
                                {
                                    Data.free["cur_coin_num"] = Data.free["ltc"];
                                }
                            }



                        }
                        if (jsondata["info"]["funds"]["freezed"] != null)
                        {
                            String freezed_btc = jsondata["info"]["funds"]["freezed"]["btc"].ToString();
                            String freezed_cny = jsondata["info"]["funds"]["freezed"]["cny"].ToString();
                            String freezed_ltc = jsondata["info"]["funds"]["freezed"]["ltc"].ToString();
                            lock (Data.freezed)
                            {
                                Data.freezed["btc"] = (Double)Convert.ChangeType(freezed_btc, typeof(Double));
                                Data.freezed["cny"] = (Double)Convert.ChangeType(freezed_cny, typeof(Double));
                                Data.freezed["ltc"] = (Double)Convert.ChangeType(freezed_ltc, typeof(Double));
                            }

                        }

                        
                        if (jsondata["info"]["funds"]["freeall"] != null)
                        {
                            lock (Data.freeall)
                            {
                                String freeall_all = jsondata["info"]["funds"]["freeall"]["all"].ToString();
                                Data.freeall["all"] = (Double)Convert.ChangeType(freeall_all, typeof(Double));
                            }

                        }
                        return 0;
                    }
                }
            }
            return Define.Login_Falied;
        }


        public static int GetAssets()
        {

            String JsonStr = Http.HttpPost(Define.Assets, "isClient=" + Define.channel_code);
            //MessageBox.Show(JsonStr);
            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {

                if (jsondata["assets"] != null)
                {
                    if (jsondata["assets"]["lending_account"] != null)
                    {

                        if (jsondata["assets"]["lending_account"]["available"] != null)
                        {
                            lock (Data.lending_account)
                            {

                                String btc = "";
                                String cny = "";
                                String ltc = "";


                                btc = jsondata["assets"]["lending_account"]["available"]["btc"].ToString();
                                cny = jsondata["assets"]["lending_account"]["available"]["cny"].ToString();
                                ltc = jsondata["assets"]["lending_account"]["available"]["ltc"].ToString();

                                Data.lending_account["available"] = new Dictionary<String, Double>();
                                Data.lending_account["available"]["btc"] = (Double)Convert.ChangeType(btc, typeof(Double));
                                Data.lending_account["available"]["cny"] = (Double)Convert.ChangeType(cny, typeof(Double));
                                Data.lending_account["available"]["ltc"]= (Double)Convert.ChangeType(ltc, typeof(Double));

                                //////////////////////////////////////////////////////////////////////////////////////////

                                btc = jsondata["assets"]["lending_account"]["frozen"]["btc"].ToString();
                                cny = jsondata["assets"]["lending_account"]["frozen"]["cny"].ToString();
                                ltc = jsondata["assets"]["lending_account"]["frozen"]["ltc"].ToString();

                                Data.lending_account["frozen"] = new Dictionary<String, Double>();
                                Data.lending_account["frozen"]["btc"] = (Double)Convert.ChangeType(btc, typeof(Double));
                                Data.lending_account["frozen"]["cny"] = (Double)Convert.ChangeType(cny, typeof(Double));
                                Data.lending_account["frozen"]["ltc"]= (Double)Convert.ChangeType(ltc, typeof(Double));

                                //////////////////////////////////////////////////////////////////////////////////////////

                                btc = jsondata["assets"]["lending_account"]["lent"]["btc"].ToString();
                                cny = jsondata["assets"]["lending_account"]["lent"]["cny"].ToString();
                                ltc = jsondata["assets"]["lending_account"]["lent"]["ltc"].ToString();

                                Data.lending_account["lent"] = new Dictionary<String, Double>();
                                Data.lending_account["lent"]["btc"] = (Double)Convert.ChangeType(btc, typeof(Double));
                                Data.lending_account["lent"]["cny"] = (Double)Convert.ChangeType(cny, typeof(Double));
                                Data.lending_account["lent"]["ltc"]= (Double)Convert.ChangeType(ltc, typeof(Double));
                                //////////////////////////////////////////////////////////////////////////////////////////

                                String total = jsondata["assets"]["lending_account"]["total"].ToString();
                                Data.lending_account_child["total"] = (Double)Convert.ChangeType(total, typeof(Double));
                                                               


                            }
                        }
                    }

                    //////////////////////////////////////////////////////


                    if (jsondata["assets"]["trading_account"] != null)
                    {
                        if (jsondata["assets"]["trading_account"]["available"] != null)
                        {
                            lock (Data.trading_account)
                            {

                                String btc = "";
                                String cny = "";
                                String ltc = "";


                                btc = jsondata["assets"]["trading_account"]["available"]["btc"].ToString();
                                cny = jsondata["assets"]["trading_account"]["available"]["cny"].ToString();
                                ltc = jsondata["assets"]["trading_account"]["available"]["ltc"].ToString();

                                Data.trading_account["available"] = new Dictionary<String, Double>();
                                Data.trading_account["available"]["btc"] = (Double)Convert.ChangeType(btc, typeof(Double));
                                Data.trading_account["available"]["cny"] = (Double)Convert.ChangeType(cny, typeof(Double));
                                Data.trading_account["available"]["ltc"] = (Double)Convert.ChangeType(ltc, typeof(Double));

                                //////////////////////////////////////////////////////////////////////////////////////////

                                btc = jsondata["assets"]["trading_account"]["frozen"]["btc"].ToString();
                                cny = jsondata["assets"]["trading_account"]["frozen"]["cny"].ToString();
                                ltc = jsondata["assets"]["trading_account"]["frozen"]["ltc"].ToString();

                                Data.trading_account["frozen"] = new Dictionary<String, Double>();
                                Data.trading_account["frozen"]["btc"] = (Double)Convert.ChangeType(btc, typeof(Double));
                                Data.trading_account["frozen"]["cny"] = (Double)Convert.ChangeType(cny, typeof(Double));
                                Data.trading_account["frozen"]["ltc"] = (Double)Convert.ChangeType(ltc, typeof(Double));

                                //////////////////////////////////////////////////////////////////////////////////////////

                                btc = jsondata["assets"]["trading_account"]["borrowed"]["btc"].ToString();
                                cny = jsondata["assets"]["trading_account"]["borrowed"]["cny"].ToString();
                                ltc = jsondata["assets"]["trading_account"]["borrowed"]["ltc"].ToString();

                                Data.trading_account["borrowed"] = new Dictionary<String, Double>();
                                Data.trading_account["borrowed"]["btc"] = (Double)Convert.ChangeType(btc, typeof(Double));
                                Data.trading_account["borrowed"]["cny"] = (Double)Convert.ChangeType(cny, typeof(Double));
                                Data.trading_account["borrowed"]["ltc"] = (Double)Convert.ChangeType(ltc, typeof(Double));

                                //////////////////////////////////////////////////////////////////////////////////////////

                                String total = jsondata["assets"]["trading_account"]["total"].ToString();
                                String net = jsondata["assets"]["trading_account"]["net"].ToString();
                                Data.trading_account_child["total"] = (Double)Convert.ChangeType(total, typeof(Double));
                                Data.trading_account_child["net"] = (Double)Convert.ChangeType(net, typeof(Double));


                            }
                        }
                    }

                    ////////////////////////////////////

                }
            }
            return Define.Login_Falied;
        }
        public static int GetTickerinfo()
        {
            //各网站行情信息:
            //https://www.okcoin.com/api/tickerinfo.do
            String JsonStr = Http.HttpGet(Define.Tickerinfo, "isClient=" + Define.channel_code);

            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {

                if (jsondata["result"] != null)
                {
                    int fdsaf = jsondata["result"].Count;
                    for (int i = 0; i < jsondata["result"].Count;i++ )
                    {
                        if (jsondata["result"][i]["market_from"]  != null)
                        {
                            if (jsondata["result"][i]["market_from"].ToString() == "0")
                            {
                                Data.btclast = (Double)Convert.ChangeType(jsondata["result"][i]["last"].ToString(), typeof(Double));
                            }
 
                            if (jsondata["result"][i]["market_from"].ToString() == "3")
                            {
                                Data.ltclast = (Double)Convert.ChangeType(jsondata["result"][i]["last"].ToString(), typeof(Double));
                            }
                        }
                    }

                    if (Define.coin_cur == 0 )
                    {
                        Data.curlast = Data.btclast;
                    } 
                    else
                    {
                        Data.curlast = Data.ltclast;
                    }

                    return 0;
                }
            }
            return Define.Login_Falied;
        }


        public static int GetUserConfig()
        {
            String JsonStr = Http.HttpPost(Define.UserConfig, "isClient=" + Define.channel_code);

            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {

                if (jsondata["config"] != null)
                {
                    if (jsondata["config"]["tradePasswordEnabled"] != null)
                    {
                        Define.tradePasswordEnabled = jsondata["config"]["tradePasswordEnabled"].ToString();
                    }
                    return 0;
                }
            }
            return Define.Login_Falied;
        }
        public static int GetCheckVersion()
        {
            //各网站行情信息:
            //https://www.okcoin.com/api/tickerinfo.do

            String JsonStr = Http.HttpPost(Define.checkVersion, "isClient=" + Define.channel_code);

            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {

                if (jsondata["version"] != null)
                {
                    String version = jsondata["version"].ToString();
                                         
                    if (Define.Version != version)
                    {
                        Define.NewVersionUrl = jsondata["path"].ToString();

                        //Des.Execute("update", url);
                    }
                    return 0;
                }
            }
            return Define.Login_Falied;
        }


        public static int GetNewVersion()
        {
            Define.NewVersionFileName = Download.GetDownload(Define.NewVersionUrl, "");
            
            return 0;
        }

        public static int DoTrade(String trade_symbol, String trade_type, String rate, String amount, String tradePwd)
        {
            String JsonStr = Http.HttpPost(Define.Trade, "isClient=" + Define.channel_code + "&symbol=" + trade_symbol + "&type=" + trade_type + "&rate=" + rate + "&amount=" + amount + "&tradePwd=" + tradePwd);

            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {

                if (jsondata["errorCode"] != null)
                {
                    Define.TradeLasterrorCode = jsondata["errorCode"].ToString();
                }
                if (jsondata["errorNum"] != null)
                {
                    Define.TradeErrorNum = jsondata["errorNum"].ToString();
                }


　               if (jsondata["result"].ToString() == "True")
                {
                    return Define.Trade_Succeed;
                }

            }
            return Define.Trade_Falied;
        }
        public static int GetDepth(string symbol)
        {
            //委单
            String JsonStr = Http.HttpGet(Define.Depth, "symbol=" + symbol + "&isClient=" + Define.channel_code);

            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {

                if (jsondata["asks"] != null)
                {
                    lock(Data.SellDepthList)
                    {
                        Data.SellDepthList.Clear();
                        int fdsaf = jsondata["asks"].Count;
                        for (int i = 0; i < jsondata["asks"].Count; i++)
                        {
                            Consign consign = new Consign();
                            //consign.c1 = c1.InnerText;
                            if (jsondata["asks"][i] != null)
                            {
                                consign.setprice(jsondata["asks"][i][0].ToString());
                            }
                            if (jsondata["asks"][i] != null)
                            {
                                consign.setnum(jsondata["asks"][i][1].ToString());
                            }
                            Data.SellDepthList.Add(consign);
                        }
                    }

                }

                if (jsondata["bids"] != null)
                {
                    Data.BuyDepthList.Clear();
                    int fdsaf = jsondata["bids"].Count;
                    for (int i = 0; i < jsondata["bids"].Count; i++)
                    {
                        Consign consign = new Consign();
                        //consign.c1 = c1.InnerText;
                        if (jsondata["bids"][i] != null)
                        {
                            consign.setprice(jsondata["bids"][i][0].ToString());
                        }
                        if (jsondata["bids"][i] != null)
                        {
                            consign.setnum(jsondata["bids"][i][1].ToString());
                        }
                        Data.BuyDepthList.Add(consign);
                    }
                }


            }
            return Define.Login_Succeed;
        }


        public static int DoOrder(string symbol, string order_id)
        {          
            //{"orders":[{"amount":1.112,"avg_rate":70.81,"createDate":1397038807000,"deal_amount":1.112,"orders_id":23531510,"rate":70.81,"status":2,"symbol":"ltc_cny","type":"buy"}],"result":true}
            String JsonStr = Http.HttpPost(Define.Order, "symbol=" + symbol + "&order_id=" + order_id + "&isClient=" + Define.channel_code);
            
            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {
                
                    if (jsondata["result"] != null)
                    {

                    }
                    return Define.Login_Succeed;
               
            }
            return Define.Login_Falied;
        }

        

        public static int GetCancelOrder(String symbol,String Order_id)
        {

            //委单
            String JsonStr = Http.HttpPost(Define.CancelOrder, "symbol=" + symbol + "&order_id=" + Order_id + "&isClient=" + Define.channel_code);

            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {
                if (jsondata["result"] != null)
                {
                    if (jsondata["result"].ToString() == "True")
                    {
                        return Define.Login_Succeed;
                    }
                }
            }
            return Define.Login_Falied;
        }


        public static int DoOrderHistory(String status, String symbol, String currentPage, String pageLength)
        {
            String JsonStr = Http.HttpPost(Define.OrderHistory, "symbol=" + symbol + "&status=" + status + "&currentPage=" + currentPage + "&pageLength=" + pageLength + "&isClient=" + Define.channel_code);

            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {
                if (jsondata["result"] != null)
                {
                    if (jsondata["result"].ToString() == "True")
                    {
                        if (jsondata["total"] != null)
                        {
                            int total = Convert.ToInt32(jsondata["total"].ToString());
                            if (total > 0)
                            {
                                int Count = jsondata["orders"].Count;
                                if (Count > 0)
                                {
                                    lock (Data.OrderList)
                                    {
                                        if (status == "0")
                                        {
                                            Data.OrderList.Clear();
                                            //Data.OrderMap.Clear();
                                        }
                                        else if (status == "2")
                                        {
                                            Data.OrderHistoryList.Clear();
                                            //Data.OrderHistoryMap.Clear();
                                        }
                                    }

                                }


                                for (int i = 0; i < jsondata["orders"].Count; i++)
                                {


                                    //consign.c1 = c1.InnerText;
                                    if (jsondata["orders"][i] != null)
                                    {
                                        String orders_id = jsondata["orders"][i]["orders_id"].ToString();

                                        lock (Data.OrderList)
                                        {
                                            if (status == "0")
                                            {
                                                //Data.OrderMap.TryGetValue(orders_id, out orders_id);
                                            }
                                            else if(status == "2")
                                            {
                                                //Data.OrderHistoryMap.TryGetValue(orders_id, out orders_id);
                                            }
                                        }


                                        
                                        if (orders_id != null && orders_id != "")
                                        {

                                            Consign consign = new Consign();
                                            //consign.setprice(jsondata["orders"][i][0].ToString());

                                            consign.amount = jsondata["orders"][i]["amount"].ToString();                            //委单币的数量
                                            consign.avg_rate = jsondata["orders"][i]["avg_rate"].ToString();                        //平均成交价
                                            consign.createDate = jsondata["orders"][i]["createDate"].ToString().Substring(0,10);    //时间
                                            consign.deal_amount = jsondata["orders"][i]["deal_amount"].ToString();                  //成交数量
                                            consign.orders_id = jsondata["orders"][i]["orders_id"].ToString();                      //交易单号
                                            consign.rate = jsondata["orders"][i]["rate"].ToString();                                //价格
                                            consign.status = jsondata["orders"][i]["status"].ToString();                            //挂单状态－1:已撤销 0/3:未成交 1:部分成交 2:完全成交
                                            consign.type = jsondata["orders"][i]["type"].ToString();                                //sell_market

                                            lock (Data.OrderList)
                                            {
                                                if (status == "0")
                                                {
                                                    Data.OrderList.Add(consign);
                                                    //Data.OrderMap.Add(consign.orders_id, "1");
                                                }
                                                else if (status == "2")
                                                {
                                                    Data.OrderHistoryList.Add(consign);
                                                    //Data.OrderHistoryMap.Add(consign.orders_id, "1");
                                                }
                                            }

                                        }
                                    }

                                    //{"orders":[{"amount":0.21,"avg_rate":0,"createDate":1391742319000,"deal_amount":0,"orders_id":66081,"rate":0,"status":0,"type":"sell_market"}],"result":true}


                                }
                            }

                        }
                    }
                    else
                    {
                        lock (Data.OrderList)
                        {
                            if (status == "0")
                            {
                                Data.OrderList.Clear();
                                //Data.OrderMap.Clear();
                            }
                            else if (status == "2")
                            {
                                Data.OrderHistoryList.Clear();
                                //Data.OrderHistoryMap.Clear();
                            }
                        }

                    }
                }
            }
            return Define.Login_Succeed;
        }

        /*
        recordType: 0:全部1:btc买入 2:btc卖出 3:btc充值 4:btc提现 5:cny充值 6:cny提现 8:ltc买入 9:ltc卖出 10:ltc充值 11:ltc提现 (默认0)
        currentPage :页码数 (默认1)
        pageLength :一页显示条数 (默认15)
         * */
       
        public static int DoAccountRecord(string recordType, string currentPage, string pageLength)
        {
            return 0;

            String JsonStr = Http.HttpPost(Define.AccountRecord, "recordType=" + recordType + "&currentPage=" + currentPage + "&pageLength=" + pageLength + "&isClient=" + Define.channel_code);

            JsonData jsondata = GetJsonData(JsonStr);
            if (jsondata != null)
            {
                if (jsondata["resultCode"].ToString() == "0")
                {
                    return Define.Login_Succeed;
                }
            }
            return Define.Login_Falied;
        }

        /*
        {"accountRecords":[

        {"amount":0,"btcBalance":10,"cnyBalance":412,"date":1393831363000,"id":3014444,"ltcBalance":100,"price":0,"turnover":0,"type":20},
        {"amount":0,"btcBalance":10,"cnyBalance":412,"date":1393831354000,"id":3014443,"ltcBalance":100,"price":0,"turnover":0,"type":21},
        {"amount":0,"btcBalance":10,"cnyBalance":412,"date":1393831350000,"id":3014441,"ltcBalance":100,"price":0,"turnover":0,"type":19},
        {"amount":0,"btcBalance":10,"cnyBalance":412,"date":1393831301000,"id":3014440,"ltcBalance":100,"price":0,"turnover":0,"type":5},
        {"amount":0,"btcBalance":10,"cnyBalance":412,"date":1393831292000,"id":3014439,"ltcBalance":100,"price":0,"turnover":0,"type":4},
        {"amount":0,"btcBalance":10,"cnyBalance":412,"date":1393831282000,"id":3014438,"ltcBalance":100,"price":0,"turnover":0,"type":3},
        {"amount":0,"btcBalance":10,"cnyBalance":412,"date":1393831259000,"id":3014437,"ltcBalance":100,"price":0,"turnover":0,"type":2},
        {"amount":0,"btcBalance":10,"cnyBalance":412,"date":1393831200000,"id":3014436,"ltcBalance":100,"price":0,"turnover":0,"type":1}],
    
        "currentPage":1,"pageLength":100,"recordType":0,"total":8}
         * */



    }
}
