using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using LitJson;

using System.Runtime.InteropServices;
using System.Threading;

namespace WindowsFormsApplication1
{

    public partial class Main : Form
    {

        private Double ReckonBuyCny = 0;
        private Double ReckonBuyCoin = 0;

        private Double ReckonBuyCnyAll = 0;

        private Double TempBuyOrSellPrice = 0;

        //---------------------------------

        //private IDictionary<String, String> consign = new Dictionary<String, String>();

        private List<Label> CSPList = new List<Label>();
        private List<Label> CSNList = new List<Label>();

        private List<Label> CBPList = new List<Label>();
        private List<Label> CBNList = new List<Label>();
        private List<Price> PriceList = new List<Price>();

        private String Title = "";

        private int islogin = 0;

        private string LastTime = "";

        private IDictionary<int, Consign> BuyWaitList = new Dictionary<int, Consign>();
        private IDictionary<int, Consign> SellWaitList = new Dictionary<int, Consign>();

        private IDictionary<int, Consign> BuyComList = new Dictionary<int, Consign>();
        private IDictionary<int, Consign> SellComList = new Dictionary<int, Consign>();

        private IDictionary<int, Consign> BuyWaitListNew = new Dictionary<int, Consign>();
        private IDictionary<int, Consign> BuyComListNew = new Dictionary<int, Consign>();

        private IDictionary<int, HtmlElement> dataHtmlElement = new Dictionary<int, HtmlElement>();

        Boolean ListIsHasRemove = false;
        public ListView curlistwiew = null;

        public Boolean isbtcwarn = false;
        public Boolean isltcwarn = false;

        public int btcwarncount = 0;
        public int ltcwarncount = 0;

        public String BatBuyPrice = "";


        System.Media.SoundPlayer sndPlayer = new System.Media.SoundPlayer(Application.StartupPath + @"/1.wav");


        [DllImport("user32.dll")]
        public static extern bool FlashWindow(
         IntPtr hWnd,     // handle to window
         bool bInvert   // flash status
         );


        int FlashWindowCount = 0;

        public Main()
        {
            InitializeComponent();
        }

        private void cur_BTCorLTC(int flag)
        {
            Data.SwitchCurCoin(flag);
            warn_to_list(0);
            warn_to_list(1);

            if (flag == Define.coin_btc)
            {
                label23.Text = "BTC买入：";
                label6.Text = "可买BTC：";

                label20.Text = "BTC余额：";
                label25.Text = "BTC卖出：";

            }
            else
            {
                label23.Text = "LTC买入：";
                label6.Text = "可买LTC：";

                label20.Text = "LTC余额：";
                label25.Text = "LTC卖出：";

            }

        }

        private void warn_to_list(int flag)
        {
            ListView listwiew = null;
            if (flag == 0)
            {
                listwiew = listViewBTC;
            }
            else
            {
                listwiew = listViewLTC;
            }
            listwiew.Items.Clear();

            WarnManagement.Instance().set_cur_warnlist(flag);
            WarnManagement.Instance().read_json();

            List<Warn> warnlistcur = WarnManagement.Instance().warnlistcur;

            for (int i = 0; i < warnlistcur.Count; i++)
            {
                Warn warn = warnlistcur[i];
                String alertstr = "";
                //alertstr += "当";
                alertstr += (warn.greatorless == 0) ? "小于" : "大于";
                alertstr += warn.price.ToString();
                alertstr += "时";

                if (warn.alerttype == 1)
                {
                    alertstr += "声音报警";
                }
                else if (warn.alerttype == 2)
                {
                    alertstr += "任栏闪动";

                } if (warn.alerttype == 3)
                {
                    alertstr += "声音报警和任栏闪动";
                }

                ListViewItem lvi = new ListViewItem(alertstr, -1);
                FontFamily fontFamily = new FontFamily("Arial");
                Font font = new Font(fontFamily, 12, FontStyle.Regular, GraphicsUnit.Pixel);
                lvi.ForeColor = Color.FromName("#FF9900");
                lvi.Font = font;
                ImageList imgList = new ImageList();
                imgList.ImageSize = new Size(1, 14);//分别是宽和高


                listwiew.SmallImageList = imgList;   //这里设置listView的SmallImageList ,用imgList将其撑大
                listwiew.Items.Add(lvi);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wirteToLog = new WriteLog(RichTxtLog);
            Define.TitleBase +="计算比率是: "+ MachineData.RiseRate;
            //String fdsa = Cookie.GetCookies(Define.Domain);
            //MessageBox.Show(fdsa);

            Data.init();

            Login login = new Login();  //新建一个NewForm窗口(NewForm是自己定义的Form)
            login.ShowDialog();         //新窗口显现

            if (Data.GetLoginResult != Define.Login_Succeed)
            {
                this.Close();
                return;
            }



            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //String  dda = Des.GetMachineInfo(channel.ToString());

            CreateColumnHeader(listView_Order);
            CreateColumnHeader(listView_Completed);


            ColumnHeader BTCc1 = new ColumnHeader();
            BTCc1.Text = "比特币BTC预警";
            BTCc1.Width = 210;
            listViewBTC.Columns.AddRange(new ColumnHeader[] { BTCc1 });//将这两列加入listView1
            listViewBTC.View = View.Details;


            ColumnHeader LTCc1 = new ColumnHeader();
            LTCc1.Text = "莱特币LTC预警";
            LTCc1.Width = 210;
            listViewLTC.Columns.AddRange(new ColumnHeader[] { LTCc1 });//将这两列加入listView1
            listViewLTC.View = View.Details;

            Title = Define.TitleBase;
            this.Text = Title;

            String mac = "";
            NetworkInterface[] netWorks = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface netWork in netWorks)
            {
                mac += netWork.GetPhysicalAddress().ToString();
            }

            timer.Interval = 1000;
            timer.Enabled = true;

            timerWarnPlay.Interval = 60000;
            timerWarnPlay.Enabled = false;

            timerDepth.Interval = 1000;
            timerDepth.Enabled = true;

            timer_AccountRecord.Interval = 5000;
            timer_AccountRecord.Enabled = true;
            timer_order.Interval = 5000;
            timer_order.Enabled = true;

            timer_OrderHistory.Interval = 5000;
            timer_OrderHistory.Enabled = true;


            tabControl1.Width = 745;
            tabControl1.Height = 224;

            lab_alert.Text = "";
            lab_sell_alert.Text = "";

            cur_BTCorLTC(1);

            CSPList.Add(CSP5);
            CSPList.Add(CSP4);
            CSPList.Add(CSP3);
            CSPList.Add(CSP2);
            CSPList.Add(CSP1);

            CSNList.Add(CSN5);
            CSNList.Add(CSN4);
            CSNList.Add(CSN3);
            CSNList.Add(CSN2);
            CSNList.Add(CSN1);

            CBPList.Add(CBP1);
            CBPList.Add(CBP2);
            CBPList.Add(CBP3);
            CBPList.Add(CBP4);
            CBPList.Add(CBP5);

            CBNList.Add(CBN1);
            CBNList.Add(CBN2);
            CBNList.Add(CBN3);
            CBNList.Add(CBN4);
            CBNList.Add(CBN5);


            //ThreadEx threadex = new ThreadEx();
            //threadex.Start(new ThreadStart(threadex.GetCheckVersion), new EventHandler(GetCheckVersion), this);



            ThreadEx threadex2 = new ThreadEx();
            threadex2.Start(new ThreadStart(threadex2.GetUserConfig), new EventHandler(GetUserConfig), this);




        }

        private void CreateColumnHeader(ListView listview)
        {

            //比如您在窗体上放了一个ListView，教您一些简单的操作。
            ColumnHeader c1 = new ColumnHeader();
            c1.Text = "委托时间";
            c1.Width = 110;
            ColumnHeader c2 = new ColumnHeader();
            c2.Text = "委托类别";
            c2.Width = 75;
            ColumnHeader c3 = new ColumnHeader();
            c3.Text = "委托数量";
            c3.Width = 70;
            ColumnHeader c4 = new ColumnHeader();
            c4.Text = "委托价格";
            c4.Width = 75;
            ColumnHeader c5 = new ColumnHeader();
            c5.Text = "成交数量";
            c5.Width = 60;
            ColumnHeader c6 = new ColumnHeader();
            c6.Text = "平均成交价";
            c6.Width = 90;
            ColumnHeader c7 = new ColumnHeader();
            c7.Text = "尚未成交";
            c7.Width = 85;
            ColumnHeader c8 = new ColumnHeader();
            c8.Text = "操作/状态";
            c8.Width = 70;
            ColumnHeader c9 = new ColumnHeader();
            c9.Text = "单号";
            c9.Width = 85;

            listview.Columns.AddRange(new ColumnHeader[] { c1, c2, c3, c4, c5, c6, c7, c8, c9 });//将这两列加入listView1
            listview.View = View.Details;
        }


        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)  //判断是否最小化
            {
                this.ShowInTaskbar = false;  //不显示在系统任务栏
                NotfCoin.Visible = true;  //托盘图标可见
            }
            /*
            webBrowser1.Width = this.Width - 450;
            webBrowser1.Height = this.Height - 100;
            */
        }

        private void AddtoListView(ListView listview, string[] dataset)
        {

            ListViewItem lvi = new ListViewItem(dataset, -1);
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 14, FontStyle.Regular, GraphicsUnit.Pixel);
            lvi.ForeColor = Color.FromName("#FF9900");
            lvi.Font = font;
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 16);//分别是宽和高
            listview.SmallImageList = imgList;   //这里设置listView的SmallImageList ,用imgList将其撑大
            listview.Items.Add(lvi);
        }

        private void showsellconsign()
        {
            int index = 0;
            lock (Data.SellDepthList)
            {
                for (int i = Data.SellDepthList.Count - 1; i > 0; i--)
                {

                    if (index >= 5)
                    {
                        return;
                    }
                    Consign consign = Data.SellDepthList[i];
                    CSPList[5 - index - 1].Text = consign.rate.ToString();
                    CSNList[5 - index - 1].Text = consign.amount.ToString();
                    index++;
                }
            }

        }

        private void showbuyconsign()
        {
            for (int i = 0; i < Data.BuyDepthList.Count; i++)
            {
                if (i >= 5)
                {
                    return;
                }
                Consign consign = Data.BuyDepthList[i];
                CBPList[i].Text = consign.rate.ToString();
                CBNList[i].Text = consign.amount.ToString();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            lab_alert.Text = "";
            lab_alert.Text = doBuy(Define.trade_symbol_cur, Define.trade_type_buy, txt_tradeCnyPrice.Text, txt_tradeAmount.Text, textBox_tradePassword_buy.Text);
            timer_alert.Enabled = true;

        }

        private String doBuy(String trade_symbol, String trade_type, String rate, String amount, String tradePwd)
        {
            return Submit(trade_symbol, trade_type, rate, amount, tradePwd);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            lab_sell_alert.Text = "";
            lab_sell_alert.Text = doSell(Define.trade_symbol_cur, Define.trade_type_sell, txt_Sell_tradeCnyPrice.Text, txt_Sell_tradeAmount.Text, textBox_tradePassword_sell.Text);
        }

        private String doSell(String trade_symbol, String trade_type, String rate, String amount, String tradePwd)
        {
            return Submit(trade_symbol, trade_type, rate, amount, tradePwd);
        }



        private String Submit(String trade_symbol, String trade_type, String rate, String amount, String tradePwd)
        {
            String errorstr = "";
            if (rate.Length == 0)
            {
                MessageBox.Show(this, "请输入价格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return "请输入价格!";
            }
            if (amount.Length == 0)
            {
                MessageBox.Show(this, "请输入数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return "请输入数量!";
            }

            if (amount == "0")
            {
                MessageBox.Show(this, "请输入数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return "请输入数量!";
            }

            if (Define.TradeErrorNum == "0")
            {
                errorstr = "交易密码输入错误多次，请2小时后再试。";
                return errorstr;
            }

            if (Define.tradePasswordEnabled != "" || Define.TradeLasterrorCode == "10217")
            {
                if (Define.tradePasswordEnabled == "0" || Define.TradeLasterrorCode == "10217")
                {
                    if (tradePwd == "")
                    {
                        MessageBox.Show(this, "请输入交易密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return "请输入交易密码!";
                    }
                }
            }


            if (trade_type == Define.trade_type_buy)
            {
                //NeedMoneyNumRMB = (Double)Convert.ChangeType(txt_txt_tradeAmount.Text, typeof(Double));
                lock (Data.free)
                {
                    if (ReckonBuyCny > Data.free["cny"])
                    {
                        String alerttext = "所需要金额超出总资金量!";
                        alert(Define.trade_type_buy, alerttext);

                        return "所需要金额超出总资金量!";
                    }
                }

            }
            else
            {
                ReckonBuyCoin = (Double)Convert.ChangeType(amount, typeof(Double));
                lock (Data.free)
                {
                    if (ReckonBuyCoin > Data.free["cur_coin_num"])
                    {
                        String alerttext = "所需要出售的币超出币的总量!";
                        alert(trade_type, alerttext);

                        return "所需要出售的币超出币的总量!";
                    }
                }
            }


            textBox_tradePassword_buy.Text = "";
            textBox_tradePassword_sell.Text = "";

            try
            {


                label35.Visible = false;
                textBox_tradePassword_buy.Visible = false;
                label36.Visible = false;
                textBox_tradePassword_sell.Visible = false;


                int result = Protocol.DoTrade(trade_symbol, trade_type, rate, amount, tradePwd);
                if (result == Define.Trade_Succeed)
                {
                    OnOrder();
                    Define.tradePasswordEnabled = "";
                    Define.TradeLasterrorCode = "";
                    return "下单成功！";
                }
                else
                {
                    if (Define.TradeLasterrorCode == "10217")
                    {
                        GetUserConfig(null, null);
                    }

                    if (Define.TradeErrorNum != "")
                    {
                        if (Define.TradeErrorNum == "0")
                        {
                            errorstr = "交易密码输入错误多次，请2小时后再试。";
                            label35.Visible = false;
                            textBox_tradePassword_buy.Visible = false;
                            label36.Visible = false;
                            textBox_tradePassword_sell.Visible = false;
                        }
                        else
                        {
                            errorstr = "交易密码输入错误，还剩下" + Define.TradeErrorNum + "次机会。";
                            label35.Visible = true;
                            textBox_tradePassword_buy.Visible = true;
                            label36.Visible = true;
                            textBox_tradePassword_sell.Visible = true;
                        }


                    }
                    else
                    {
                        errorstr = Define.getErrorCodeString(Define.TradeLasterrorCode);
                    }
                    return errorstr;
                }


            }
            catch (System.Exception ex)
            {

            }
            return "下单失败！可能网络无法F连接。";
        }



        private void txt_tradeAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            only_input_number(sender, e);
        }

        private void txt_tradeCnyPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            only_input_number(sender, e);
        }


        private void only_input_number(object sender, KeyPressEventArgs e)
        {
            TextBox obj = (TextBox)sender;
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;

            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (obj.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(obj.Text, out oldf);
                    b2 = float.TryParse(obj.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        private void txt_tradeAmount_TextChanged(object sender, EventArgs e)
        {
            ReckonBuyCny = 0;
            if (txt_tradeCnyPrice.Text != "" && txt_tradeAmount.Text != "")
            {
                ReckonBuyCny = get_input_RMB(Define.trade_type_buy, txt_tradeCnyPrice.Text, txt_tradeAmount.Text);
            }

            txt_txt_tradeAmount.Text = ReckonBuyCny.ToString();

        }

        private void txt_tradeCnyPrice_TextChanged(object sender, EventArgs e)
        {
            ReckonBuyCny = 0;
            if (txt_tradeCnyPrice.Text != "" && txt_tradeAmount.Text != "")
            {
                ReckonBuyCny = get_input_RMB(Define.trade_type_buy, txt_tradeCnyPrice.Text, txt_tradeAmount.Text);
            }
            txt_txt_tradeAmount.Text = ReckonBuyCny.ToString();
        }


        private Double get_input_RMB(String trade_type, String tradeCnyPrice, String tradeAmount)
        {
            alert_clear(trade_type);

            Double InputPrice = (Double)Convert.ChangeType(tradeCnyPrice, typeof(Double));
            Double InputNum = (Double)Convert.ChangeType(tradeAmount, typeof(Double));
            Double tempReckonCny = InputPrice * InputNum;
            String strneedRMB = tempReckonCny.ToString("0.000").TrimEnd('0').TrimEnd('.');
            tempReckonCny = (Double)Convert.ChangeType(strneedRMB, typeof(Double));

            if (tempReckonCny == 0)
            {
                int fdsa = 0;
            }

            if (Define.trade_type_buy == trade_type)
            {
                lock (Data.free)
                {
                    if (tempReckonCny > Data.free["cny"])
                    {
                        alert(trade_type, "所需要金额超出总资金量!");
                    }
                }

            }
            else
            {
                lock (Data.free)
                {
                    if (InputNum > Data.free["cur_coin_num"])
                    {
                        alert(trade_type, "所需要出售的币超出币的总量!");
                    }
                }

            }


            return tempReckonCny;
        }

        private void alert_clear(String trade_type)
        {
            if (trade_type == Define.trade_type_buy)
            {
                lab_alert.Text = "";
            }
            else
            {
                lab_sell_alert.Text = "";
            }
        }
        private void alert(String trade_type, String text)
        {
            if (trade_type == Define.trade_type_buy)
            {
                lab_alert.Text = text;
            }
            else
            {
                lab_sell_alert.Text = text;
            }
            timer_alert.Enabled = true;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            lock (Data.SellDepthList)
            {
                txt_tradeCnyPrice.Text = CSP1.Text;
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (Data.BuyDepthList.Count() < 5)
            {
                return;
            }
            Double price = (Double)Convert.ChangeType(CBP1.Text, typeof(Double));
            price += 0.01;
            txt_tradeCnyPrice.Text = price.ToString();
        }

        private String getneedNumWithRMB(String _tradeCnyPrice, String _tradeAmount, Double num)
        {
            if (_tradeCnyPrice == "")
            {
                return "0";
            }
            Double price = (Double)Convert.ChangeType(_tradeCnyPrice, typeof(Double));
            Double neednum = 0;
            lock (Data.free)
            {
                neednum = Data.free["cny"] / price;
            }

            if (neednum < 0)
            {
                neednum = 0;
            }

            neednum = neednum * num;
            String strneednum = neednum.ToString("0.000").TrimEnd('0').TrimEnd('.');
            neednum = (Double)Convert.ChangeType(strneednum, typeof(Double));
            return neednum.ToString();

        }

        private void getneedNumWithBTC(TextBox _tradeAmount, Double num)
        {
            Double neednum = 0;
            //Double price = (Double)Convert.ChangeType(txt_Sell_tradeCnyPrice.Text, typeof(Double));

            lock (Data.free)
            {
                neednum = Data.free["cur_coin_num"] * num;
            }

            String strneednum = neednum.ToString("0.000").TrimEnd('0').TrimEnd('.');
            neednum = (Double)Convert.ChangeType(strneednum, typeof(Double));
            _tradeAmount.Text = neednum.ToString();

            //Double TempReckonSellCny = get_input_RMB(Define.trade_type_sell, txt_Sell_tradeCnyPrice.Text, txt_Sell_tradeAmount.Text);

            //txt_txt_Sell_tradeAmount.Text = TempReckonSellCny.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txt_tradeAmount.Text = getneedNumWithRMB(txt_tradeCnyPrice.Text, txt_tradeAmount.Text, 1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            txt_tradeAmount.Text = getneedNumWithRMB(txt_tradeCnyPrice.Text, txt_tradeAmount.Text, 0.5);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            txt_tradeAmount.Text = getneedNumWithRMB(txt_tradeCnyPrice.Text, txt_tradeAmount.Text, 0.33);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txt_tradeAmount.Text = getneedNumWithRMB(txt_tradeCnyPrice.Text, txt_tradeAmount.Text, 0.25);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txt_tradeAmount.Text = getneedNumWithRMB(txt_tradeCnyPrice.Text, txt_tradeAmount.Text, 0.2);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            ThreadEx threadex = new ThreadEx();
            threadex.Start(new ThreadStart(threadex.GetUserInfo), new EventHandler(GetUserInfo), this);

            ThreadEx threadex2 = new ThreadEx();
            threadex2.Start(new ThreadStart(threadex2.GetTickerinfo), new EventHandler(GetTickerinfo), this);


        }


        private void GetUserInfo(Object o, EventArgs e)
        {
            if (Data.GetUserInfoResult == Define.Login_Succeed)
            {
                lock (Data.free)
                {
                    lbl_freecny.Text = Data.free["cny"].ToString("0.000000").TrimEnd('0').TrimEnd('.');
                    lab_freecny.Text = Data.free["cny"].ToString("0.000000").TrimEnd('0').TrimEnd('.');

                    lbl_freebtc.Text = Data.free["btc"].ToString("0.000000").TrimEnd('0').TrimEnd('.');
                    lbl_freeltc.Text = Data.free["ltc"].ToString("0.000000").TrimEnd('0').TrimEnd('.');

                    lab_Sell_coin_num.Text = Data.free["cur_coin_num"].ToString("0.000000").TrimEnd('0').TrimEnd('.');

                    lab_RemainderMoney.Text = Data.free["cny"].ToString("0.000000").TrimEnd('0').TrimEnd('.'); ;
                }



                lock (Data.freezed)
                {
                    lbl_freezedcny.Text = Data.freezed["cny"].ToString("0.000000").TrimEnd('0').TrimEnd('.');
                    lbl_freezedbtc.Text = Data.freezed["btc"].ToString("0.000000").TrimEnd('0').TrimEnd('.');
                    lbl_freezedltc.Text = Data.freezed["ltc"].ToString("0.000000").TrimEnd('0').TrimEnd('.');
                }
                lock (Data.freeall)
                {
                    lab_TotalMoney.Text = Data.freeall["all"].ToString("0.000000").TrimEnd('0').TrimEnd('.');
                }




                if (isbtcwarn == true)
                {
                    DoWarn(Define.coin_btc);
                }
                if (isltcwarn == true)
                {
                    DoWarn(Define.coin_ltc);
                }

            }
            GetReckonBuy();
        }






        private void GetTickerinfo(Object o, EventArgs e)//各网站行情信息:
        {

            if (Data.GetTickerinfoResult == Define.Login_Succeed)
            {
                try
                {
                    lock (Data.free)
                    {
                        lbl_freecny.Text = Data.free["cny"].ToString("0.000000").TrimEnd('0').TrimEnd('.');
                        lbl_freebtc.Text = Data.free["btc"].ToString("0.000000").TrimEnd('0').TrimEnd('.');
                        lbl_freeltc.Text = Data.free["ltc"].ToString("0.000000").TrimEnd('0').TrimEnd('.');
                    }


                    lab_last_btc.Text = Data.btclast.ToString("0.000000").TrimEnd('0').TrimEnd('.');
                    lab_last_ltc.Text = Data.ltclast.ToString("0.000000").TrimEnd('0').TrimEnd('.');

                    String Title = Define.TitleBase + ",　　最新价:　　BTC" + Data.btclast + "　　LTC" + Data.ltclast;

                    this.Text = Title;


                }
                catch (System.Exception ex)
                {
                }
            }
            //在这里获得了数据
            GetReckonBuy();
        }


        private void GetCheckVersion(Object o, EventArgs e)//各网站行情信息:
        {
            if (Define.NewVersionUrl != "")
            {
                DialogResult dr;
                dr = MessageBox.Show("发现新版本，是否现在升级？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    return;
                }

                ThreadEx threadex = new ThreadEx();
                threadex.Start(new ThreadStart(threadex.GetNewVersion), new EventHandler(GetNewVersion), this);
            }
        }
        private void GetUserConfig(Object o, EventArgs e)
        {
            if (Define.tradePasswordEnabled != "" || Define.TradeLasterrorCode == "10217")
            {
                if (Define.tradePasswordEnabled == "0" || Define.TradeLasterrorCode == "10217")
                {
                    label35.Visible = true;
                    textBox_tradePassword_buy.Visible = true;
                    label36.Visible = true;
                    textBox_tradePassword_sell.Visible = true;
                }
                else
                {
                    label35.Visible = false;
                    textBox_tradePassword_buy.Visible = false;
                    label36.Visible = false;
                    textBox_tradePassword_sell.Visible = false;
                }
            }
        }

        private void GetNewVersion(Object o, EventArgs e)//各网站行情信息:
        {
            if (Define.NewVersionFileName != "")
            {
                Des.Execute(Define.NewVersionFileName, "");
                //this.Close();
            }
        }

        private WriteLog wirteToLog = null;

        private void GetReckonBuy()
        {
            //try
            //{
            lock (Data.free)
            {
                lab_ReckonBuy.Text = (Data.free["cny"] / Data.curlast).ToString("0.000").TrimEnd('0').TrimEnd('.');
                lab_ReckonSell.Text = (Data.free["cur_coin_num"] * Data.curlast).ToString("0.000").TrimEnd('0').TrimEnd('.');
                label_curlast.Text = Data.curlast.ToString("0.000000").TrimEnd('0').TrimEnd('.');
                //这里开始重头戏

                //TODO:对冻结资金进行一定的监控

                //手里拿的是币，要卖掉
                double realBuyPrice = GetRealBuyPrice(MachineData.OptNum);
                //手里拿的是钱，要买币
                double realSellPrice = GetRealSellPrice(MachineData.OptNum);

                if (realBuyPrice <= 0 || realSellPrice <= 0) return;

                //计算上升和下降比例
                double rateRise = (realBuyPrice - MachineData.PriceStart) / MachineData.PriceStart;
                double rateHFall = (MachineData.PriceStart - realSellPrice) / MachineData.PriceStart;

                //显示下数据
                //wirteToLog.LogAppendMsg("当前价格-->" + Data.curlast + "--上升比例-->" + rateRise + "--下降比例-->" + rateHFall+"--购买次数:"+MachineData.BuyCount+"--出售次数"+MachineData.SellCount);
                //wirteToLog.LogAppendMsg("当前价格");

                if ((rateRise < MachineData.RiseRate && rateRise >= 0) && (rateHFall < MachineData.FallRate && rateHFall >= 0))
                {
                    //上涨利润太小,下跌利润太小   退出，继续找
                    return;
                }

                //进行挂卖单
                if (rateRise >= MachineData.RiseRate)
                {
                    //有利可图，查看是否有卖出杠杆
                    if (MachineData.IsCanSell())
                    {
                        //TODO:进行卖
                        int problem = StartSell(realBuyPrice, MachineData.OptNum);

                        //TODO:显示下交易过程
                        MachineData.CalMaxDiff();
                        wirteToLog.LogAppendMsg("当前价格-->" + Data.curlast + "--进行卖出--购买次数:" + MachineData.BuyCount + "--出售次数" + MachineData.SellCount + "--最大差距-->" + MachineData.DiffBuySell);

                        //如果没有冻结资金，买卖操作成功
                        MachineData.PriceStart = realBuyPrice;

                        //刷新监视价格
                    }
                }

                //下降空间达到要求
                //进行挂买单
                if (rateHFall >= MachineData.FallRate)
                {
                    if (MachineData.IsCanBuy())
                    {
                        //TODO:进行买
                        int problem = StartBuy(realSellPrice, MachineData.OptNum);

                        MachineData.CalMaxDiff();
                        //如果没有冻结资金，买卖操作成功
                        MachineData.PriceStart = realSellPrice;

                        wirteToLog.LogAppendMsg("当前价格-->" + Data.curlast + "--进行购买--购买次数:" + MachineData.BuyCount + "--出售次数" + MachineData.SellCount + "--最大差距-->" + MachineData.DiffBuySell);
                    }
                }
            }


            //}
            //catch (System.Exception ex)
            //{
            //}
        }
        /// <summary>
        /// 通过数量算出实际卖出的价格线
        /// </summary>
        /// <param name="optNum"></param>
        /// <returns></returns>
        private double GetRealBuyPrice(double optNum)
        {
            if (Data.BuyDepthList.Count <= 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(Data.BuyDepthList[0].rate);
            }
        }
        /// <summary>
        /// 通过数量算出实际买入的价格线
        /// </summary>
        /// <param name="optNum"></param>
        /// <returns></returns>
        private double GetRealSellPrice(double optNum)
        {
            if (Data.SellDepthList.Count <= 0)
            {
                return 0;
            }
            else
            {
                double price = 0;
                try
                {
                    price = Convert.ToDouble(Data.SellDepthList[Data.SellDepthList.Count - 1].rate);
                }
                catch (Exception)
                {
                    price = 0;
                }
                return price;
            }
        }

        /// <summary>
        /// 挂单进行卖操作
        /// </summary>
        /// <param name="realPrice">价格</param>
        /// <param name="optNum">数量</param>
        /// <returns>返回结果</returns>
        private int StartSell(double realPrice, double optNum)
        {
            MachineData.SellCount++;
            return 1;
        }

        /// <summary>
        /// 挂单进行买操作
        /// </summary>
        /// <param name="realPrice">价格</param>
        /// <param name="optNum">数量</param>
        /// <returns>返回结果</returns>
        private int StartBuy(double realPrice, double optNum)
        {
            MachineData.BuyCount++;
            return 1;
        }


        private void Show_track_change(TextBox _tradeCnyPrice, TrackBar _track_change, int SmallChane)
        {
            if (_tradeCnyPrice.Text == "")
            {
                return;
            }
            TempBuyOrSellPrice = (Double)Convert.ChangeType(_tradeCnyPrice.Text, typeof(Double));
            _track_change.SmallChange = SmallChane;



        }



        private void track_change_Scroll(object sender, EventArgs e)
        {
            txt_tradeCnyPrice.Text = track_change_Scroll_Price(sender).ToString();
        }

        private Double track_change_Scroll_Price(object sender)
        {
            TrackBar obj = (TrackBar)sender;
            Double needPrice = TempBuyOrSellPrice;
            Double needPercent = (Double)Convert.ChangeType(obj.Value, typeof(Double));
            needPercent = needPercent / 100;
            String strneedPercent = needPercent.ToString("0.000").TrimEnd('0').TrimEnd('.');
            needPercent = (Double)Convert.ChangeType(strneedPercent, typeof(Double));

            needPrice = needPrice + (needPrice * needPercent);
            String strneedPrice = needPrice.ToString("0.000").TrimEnd('0').TrimEnd('.');
            needPrice = (Double)Convert.ChangeType(strneedPrice, typeof(Double));
            //MessageBox.Show(MoneyNumRMB.ToString());
            return needPrice;
        }




        private void txt_Sell_tradeAmount_TextChanged(object sender, EventArgs e)
        {
            Double ReckonSellCny = 0;
            if (txt_Sell_tradeCnyPrice.Text != "" && txt_Sell_tradeAmount.Text != "")
            {
                ReckonSellCny = get_input_RMB(Define.trade_type_sell, txt_Sell_tradeCnyPrice.Text, txt_Sell_tradeAmount.Text);
            }
            txt_txt_Sell_tradeAmount.Text = ReckonSellCny.ToString();
        }

        private void txt_Sell_tradeCnyPrice_TextChanged(object sender, EventArgs e)
        {
            Double ReckonSellCny = 0;
            if (txt_Sell_tradeCnyPrice.Text != "" && txt_Sell_tradeAmount.Text != "")
            {
                ReckonSellCny = get_input_RMB(Define.trade_type_sell, txt_Sell_tradeCnyPrice.Text, txt_Sell_tradeAmount.Text);
            }
            txt_txt_Sell_tradeAmount.Text = ReckonSellCny.ToString();






        }

        private void txt_Sell_tradeCnyPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            only_input_number(sender, e);
        }

        private void txt_Sell_tradeAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            only_input_number(sender, e);
        }

        private void button14_Click(object sender, EventArgs e)
        {

            lock (Data.SellDepthList)
            {
                if (Data.BuyDepthList.Count() < 5)
                {
                    return;
                }
                txt_Sell_tradeCnyPrice.Text = CBP5.Text;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            lock (Data.SellDepthList)
            {
                if (Data.SellDepthList.Count() < 5)
                {
                    return;
                }
                Double price = (Double)Convert.ChangeType(CSP1.Text, typeof(Double));
                price -= 0.01;
                txt_Sell_tradeCnyPrice.Text = price.ToString();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            txt_Sell_tradeAmount.Text = Data.free["cur_coin_num"].ToString("0.000000").TrimEnd('0').TrimEnd('.');
            //getneedNumWithBTC(txt_Sell_tradeAmount, 0.9999);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            getneedNumWithBTC(txt_Sell_tradeAmount, 0.5);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            getneedNumWithBTC(txt_Sell_tradeAmount, 0.33);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getneedNumWithBTC(txt_Sell_tradeAmount, 0.25);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getneedNumWithBTC(txt_Sell_tradeAmount, 0.2);
        }







        private void track_Sell_change_Scroll(object sender, EventArgs e)
        {
            txt_Sell_tradeCnyPrice.Text = track_change_Scroll_Price(sender).ToString();
        }







        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void SetTradeCnyPrice(object sender)
        {
            Label obj = (Label)sender;
            txt_tradeCnyPrice.Text = obj.Text;
            txt_Sell_tradeCnyPrice.Text = obj.Text;
        }

        private void SetTradeCnyNum(object sender)
        {
            Label obj = (Label)sender;
            txt_tradeAmount.Text = obj.Text;
            txt_Sell_tradeAmount.Text = obj.Text;
        }

        private void CSP5_Click(object sender, EventArgs e)
        {
            SetTradeCnyPrice(sender);
        }

        private void CSP4_Click(object sender, EventArgs e)
        {
            SetTradeCnyPrice(sender);
        }

        private void CSP3_Click(object sender, EventArgs e)
        {
            SetTradeCnyPrice(sender);
        }

        private void CSP2_Click(object sender, EventArgs e)
        {
            SetTradeCnyPrice(sender);
        }

        private void CSP1_Click(object sender, EventArgs e)
        {
            SetTradeCnyPrice(sender);
        }

        private void CBP1_Click(object sender, EventArgs e)
        {
            SetTradeCnyPrice(sender);
        }

        private void CBP2_Click(object sender, EventArgs e)
        {
            SetTradeCnyPrice(sender);
        }

        private void CBP3_Click(object sender, EventArgs e)
        {
            SetTradeCnyPrice(sender);
        }

        private void CBP4_Click(object sender, EventArgs e)
        {
            SetTradeCnyPrice(sender);
        }

        private void CBP5_Click(object sender, EventArgs e)
        {
            SetTradeCnyPrice(sender);
        }
        private void CSN5_Click(object sender, EventArgs e)
        {
            SetTradeCnyNum(sender);
        }

        private void CSN4_Click(object sender, EventArgs e)
        {
            SetTradeCnyNum(sender);
        }

        private void CSN3_Click(object sender, EventArgs e)
        {
            SetTradeCnyNum(sender);
        }

        private void CSN2_Click(object sender, EventArgs e)
        {
            SetTradeCnyNum(sender);
        }

        private void CSN1_Click(object sender, EventArgs e)
        {
            SetTradeCnyNum(sender);
        }

        private void CBN1_Click(object sender, EventArgs e)
        {
            SetTradeCnyNum(sender);
        }

        private void CBN2_Click(object sender, EventArgs e)
        {
            SetTradeCnyNum(sender);
        }

        private void CBN3_Click(object sender, EventArgs e)
        {
            SetTradeCnyNum(sender);
        }

        private void CBN4_Click(object sender, EventArgs e)
        {
            SetTradeCnyNum(sender);
        }

        private void CBN5_Click(object sender, EventArgs e)
        {
            SetTradeCnyNum(sender);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            //Process process = new Process();
            process.StartInfo.FileName = "iexplore.exe";
            process.StartInfo.Arguments = Define.Url;
            process.StartInfo.UseShellExecute = true;
            process.Start();
        }


        private void button22_Click(object sender, EventArgs e)
        {
            Assets assets = new Assets();   //新建一个NewForm窗口(NewForm是自己定义的Form)
            assets.ShowDialog();            //新窗口显现
        }


        private void button27_Click(object sender, EventArgs e)
        {
            txt_tradeAmount.Text = getneedNumWithRMB(txt_tradeCnyPrice.Text, txt_tradeAmount.Text, 0.16);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            txt_tradeAmount.Text = getneedNumWithRMB(txt_tradeCnyPrice.Text, txt_tradeAmount.Text, 0.14);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            txt_tradeAmount.Text = getneedNumWithRMB(txt_tradeCnyPrice.Text, txt_tradeAmount.Text, 0.125);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            txt_tradeAmount.Text = getneedNumWithRMB(txt_tradeCnyPrice.Text, txt_tradeAmount.Text, 0.111);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            txt_tradeAmount.Text = getneedNumWithRMB(txt_tradeCnyPrice.Text, txt_tradeAmount.Text, 0.1);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            getneedNumWithBTC(txt_Sell_tradeAmount, 0.16);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            getneedNumWithBTC(txt_Sell_tradeAmount, 0.14);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            getneedNumWithBTC(txt_Sell_tradeAmount, 0.125);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            getneedNumWithBTC(txt_Sell_tradeAmount, 0.111);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            getneedNumWithBTC(txt_Sell_tradeAmount, 0.1);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            lock (Data.SellDepthList)
            {
                txt_tradeCnyPrice.Text = CSP5.Text;
            }

        }

        private void button35_Click(object sender, EventArgs e)
        {
            EditWarn(0);
        }

        private void EditWarn(int cointype)
        {
            if (cointype == 0)
            {
                curlistwiew = listViewBTC;
            }
            else
            {
                curlistwiew = listViewLTC;
            }

            if (curlistwiew == null)
            {
                return;
            }

            int BTCorLTC = -1;
            if (listViewBTC == curlistwiew)
            {
                BTCorLTC = 0;
            }
            else if (listViewLTC == curlistwiew)
            {
                BTCorLTC = 1;
            }
            if (BTCorLTC == -1)
            {
                return;
            }
            int index = -1;
            ListView.SelectedListViewItemCollection items = curlistwiew.SelectedItems;

            foreach (ListViewItem item in items)
            {
                index = item.Index;
            }
            if (index >= 0)
            {
                AddWarn form2 = new AddWarn();//新建一个NewForm窗口(NewForm是自己定义的Form)
                form2.command(BTCorLTC, index);
                form2.ShowDialog(); //新窗口显现
                warn_to_list(0);
                warn_to_list(1);
            }
            else
            {
                MessageBox.Show(this, "请选择将要修改的预警！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void button34_Click(object sender, EventArgs e)
        {
            AddWarn(0);
        }


        private void AddWarn(int cointype)
        {
            AddWarn form2 = new AddWarn();//新建一个NewForm窗口(NewForm是自己定义的Form)
            form2.command_cointype(cointype);
            form2.ShowDialog(); //新窗口显现
            warn_to_list(0);
            warn_to_list(1);
        }

        private void DoWarn(int cointype)
        {

            Double curPrice = 0;

            if (cointype == Define.coin_btc)
            {
                curPrice = Data.btclast;
            }
            else
            {
                curPrice = Data.ltclast;
            }

            List<Warn> warnlistcur = WarnManagement.Instance().get_warnlist(cointype);
            for (int i = 0; i < warnlistcur.Count; i++)
            {
                Warn warn = warnlistcur[i];

                if (warn.greatorless == 0)//小于
                {
                    if (warn.price > curPrice)
                    {
                        int warncount = 0;
                        if (cointype == Define.coin_btc)
                        {
                            warncount = btcwarncount;
                        }
                        else
                        {
                            warncount = ltcwarncount;
                        }

                        if (warncount == 0)
                        {
                            if (warn.alerttype == 1 || warn.alerttype == 3)
                            {
                                timerWarnPlay.Enabled = true;
                                sndPlayer.PlayLooping();
                            }
                            if (warn.alerttype == 2 || warn.alerttype == 3)
                            {
                                timerWarnPlay.Enabled = true;
                                FlashWindow(this.Handle, true);//闪烁 
                                timer_FlashWindow.Enabled = true;
                            }
                            warncount++;
                        }
                        else if (warncount == 25)
                        {

                            if (cointype == Define.coin_btc)
                            {
                                btcwarncount = 0;
                            }
                            else
                            {
                                ltcwarncount = 0;
                            }
                            warncount = 0;
                        }
                        if (warncount > 0)
                        {
                            if (cointype == Define.coin_btc)
                            {
                                btcwarncount++;
                            }
                            else
                            {
                                ltcwarncount++;
                            }
                        }
                        //MessageBox.Show("小于");
                        //报警
                    }
                }

                if (warn.greatorless == 1)//大于
                {

                    if (warn.price < curPrice)
                    {
                        int warncount = 0;
                        if (cointype == Define.coin_btc)
                        {
                            warncount = btcwarncount;
                        }
                        else
                        {
                            warncount = ltcwarncount;
                        }

                        if (warncount == 0)
                        {
                            if (warn.alerttype == 1 || warn.alerttype == 3)
                            {
                                timerWarnPlay.Enabled = true;
                                sndPlayer.PlayLooping();
                            }
                            if (warn.alerttype == 2 || warn.alerttype == 3)
                            {
                                timerWarnPlay.Enabled = true;
                                FlashWindow(this.Handle, true);//闪烁
                                timer_FlashWindow.Enabled = true;
                            }
                            warncount++;
                        }
                        else if (warncount == 100)
                        {

                            if (cointype == Define.coin_btc)
                            {
                                btcwarncount = 0;
                            }
                            else
                            {
                                ltcwarncount = 0;
                            }
                            warncount = 0;
                        }
                        if (warncount > 0)
                        {
                            if (cointype == Define.coin_btc)
                            {
                                btcwarncount++;
                            }
                            else
                            {
                                ltcwarncount++;
                            }
                        }


                        //MessageBox.Show("小于");
                        //报警
                    }
                }
            }
        }

        private void listViewBTC_SelectedIndexChanged(object sender, EventArgs e)
        {
            curlistwiew = listViewBTC;
        }

        private void listViewLTC_SelectedIndexChanged(object sender, EventArgs e)
        {
            curlistwiew = listViewLTC;
        }




        private void button36_Click(object sender, EventArgs e)
        {
            DeleteWarn(0);
        }


        private void DeleteWarn(int cointype)
        {

            if (cointype == 0)
            {
                curlistwiew = listViewBTC;
            }
            else
            {
                curlistwiew = listViewLTC;
            }

            if (curlistwiew == null)
            {
                return;
            }

            int BTCorLTC = -1;
            if (listViewBTC == curlistwiew)
            {
                BTCorLTC = 0;
            }
            else if (listViewLTC == curlistwiew)
            {
                BTCorLTC = 1;
            }
            if (BTCorLTC == -1)
            {
                return;
            }
            int index = -1;
            ListView.SelectedListViewItemCollection items = curlistwiew.SelectedItems;

            foreach (ListViewItem item in items)
            {
                index = item.Index;
            }

            if (index >= 0)
            {
                DialogResult dr;
                dr = MessageBox.Show("确定要删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                WarnManagement.Instance().set_cur_warnlist(BTCorLTC);
                WarnManagement.Instance().delete_Warn(index);

                warn_to_list(0);
                warn_to_list(1);
            }
            else
            {
                MessageBox.Show(this, "请选择将要删除的预警！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 撤销单个订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_BuyWait_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedListViewItemCollection items = listView_Order.SelectedItems;

            foreach (ListViewItem item in items)
            {
                CancelOrderParm cancelorderparm = new CancelOrderParm();
                cancelorderparm.Order_id = item.SubItems[8].Text;
                cancelorderparm.symbol = Define.trade_symbol_cur;

                ThreadEx threadex = new ThreadEx();
                threadex.Start(new ParameterizedThreadStart(threadex.GetCancelOrder), (Object)cancelorderparm, new EventHandler(GetCancelOrder), this);

                //GetTradeList(BuyOrSell.ENTRUST,ConsignState.WAIT, webBrowser3.Document, item.Index);
            }
            OnOrder();

        }

        private void GetCancelOrder(Object o, EventArgs e)
        {

        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            listView_Order.Left = 0;
            listView_Completed.Left = 0;


            listView_Order.Top = 0;
            listView_Completed.Top = 0;


            listView_Order.Width = tabControl1.Width - 8;
            listView_Completed.Width = tabControl1.Width - 8;

            listView_Order.Height = tabControl1.Height - 25;
            listView_Completed.Height = tabControl1.Height - 25;

        }



        private void timerDepth_Tick(object sender, EventArgs e)
        {

            DepthParm depthparm = new DepthParm();
            depthparm.symbol = Define.trade_symbol_cur;

            ThreadEx threadex = new ThreadEx();
            threadex.Start(new ParameterizedThreadStart(threadex.GetDepth), (Object)depthparm, new EventHandler(GetDepth), this);
        }

        private void GetDepth(Object o, EventArgs e)
        {
            if (Data.GetDepthResult == 0)
            {
                //Protocol.GetDepth();
                showsellconsign();
                showbuyconsign();
            }
        }


        private void timer_AccountRecord_Tick(object sender, EventArgs e)
        {
            //public static int DoAccountRecord(string recordType, string currentPage, string pageLength)
            /*
            int result = Protocol.DoAccountRecord(Define.RecordType_0, "1", "100");

            if (result == Define.Login_Succeed)
            {
            }
            */
        }

        private void timer_order_Tick(object sender, EventArgs e)
        {
            OnOrder();
        }

        private void OnOrder()
        {
            //timer_order.Enabled = false;

            OrderParm orderparm = new OrderParm();
            orderparm.status = "0";
            orderparm.symbol = Define.coin_cur.ToString();
            orderparm.currentPage = "1";
            orderparm.pageLength = "100";

            ThreadEx threadex = new ThreadEx();
            threadex.Start(new ParameterizedThreadStart(threadex.GetOrderHistory), (Object)orderparm, new EventHandler(GetOrder), this);


        }

        private void GetOrder(Object o, EventArgs e)
        {

        }



        private void timer_OrderHistory_Tick(object sender, EventArgs e)
        {

            OrderParm orderparm = new OrderParm();
            orderparm.status = "2";
            orderparm.symbol = Define.coin_cur.ToString();
            orderparm.currentPage = "1";
            orderparm.pageLength = "100";

            ThreadEx threadex = new ThreadEx();
            threadex.Start(new ParameterizedThreadStart(threadex.GetOrderHistory), (Object)orderparm, new EventHandler(GetOrderHistory), this);
        }


        private void GetOrderHistory(Object o, EventArgs e)
        {

            if (Data.GetOrderHistoryResult == Define.Login_Succeed)
            {
                lock (Data.OrderList)
                {
                    ShowList(listView_Completed, Data.OrderHistoryList);
                }
            }
        }


        private void ShowList(ListView listview, List<Consign> ListData)
        {

            listview.Items.Clear();

            int fdsaf = ListData.Count;
            for (int i = 0; i < ListData.Count; i++)
            {

                Consign consign = ListData[i];

                string[] dataset = new string[9];


                Double amount = (Double)Convert.ChangeType(consign.amount, typeof(Double));
                Double deal_amount = (Double)Convert.ChangeType(consign.deal_amount, typeof(Double));

                Double remain = amount - deal_amount;

                dataset[0] = Des.getTimeString(consign.createDate);
                dataset[1] = Define.getTradeTypeString(consign.type);
                dataset[2] = consign.amount;
                dataset[3] = consign.rate;
                dataset[4] = consign.deal_amount;
                dataset[5] = consign.avg_rate;
                dataset[6] = remain.ToString();
                dataset[7] = Define.getTradeStatusString(consign.status);
                dataset[8] = consign.orders_id;


                ListViewItem lvi = new ListViewItem(dataset, -1);
                FontFamily fontFamily = new FontFamily("Arial");
                Font font = new Font(fontFamily, 14, FontStyle.Regular, GraphicsUnit.Pixel);
                lvi.ForeColor = Color.FromName("#FF9900");
                lvi.Font = font;
                ImageList imgList = new ImageList();
                imgList.ImageSize = new Size(1, 16);    //分别是宽和高
                listview.SmallImageList = imgList;      //这里设置listView的SmallImageList ,用imgList将其撑大
                listview.Items.Add(lvi);
            }

        }

        private void label43_Click(object sender, EventArgs e)
        {

        }

        private void listView_Order_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                panel_cancel.Visible = true;
            }
            else
            {
                panel_cancel.Visible = false;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_tradeCnyPrice.Text = "";
            txt_Sell_tradeCnyPrice.Text = "";
            txt_tradeAmount.Text = "";
            txt_Sell_tradeAmount.Text = "";
            if (tabControl2.SelectedIndex == 0)
            {

                cur_BTCorLTC(0);
            }
            if (tabControl2.SelectedIndex == 1)
            {
                cur_BTCorLTC(1);
            }

        }

        private void lab_last_btc_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            AddWarn(1);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            EditWarn(1);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            DeleteWarn(1);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isltcwarn == false)
            {
                isltcwarn = true;
                ltcwarncount = 0;
                checkBox1.Text = "关闭LTC预警";
                //timerWarn.Interval = 500;
                //timerWarn.Enabled = true;
                //sndPlayer.PlayLooping();
            }
            else
            {
                isltcwarn = false;
                ltcwarncount = 0;
                checkBox1.Text = "启动LTC预警";
                //timerWarn.Interval = 500;
                //timerWarn.Enabled = false;
                sndPlayer.Stop();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (isbtcwarn == false)
            {
                isbtcwarn = true;
                btcwarncount = 0;

                checkBox2.Text = "关闭BTC预警";
                //timerWarn.Interval = 500;
                //timerWarn.Enabled = true;
                //sndPlayer.PlayLooping();
            }
            else
            {
                isbtcwarn = false;
                btcwarncount = 0;
                checkBox2.Text = "启动BTC预警";
                //timerWarn.Interval = 500;
                //timerWarn.Enabled = false;
                sndPlayer.Stop();
            }
        }

        private void listViewBTC_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listViewBTC.SelectedItems.Count > 0)
            {
                button35.Enabled = true;
                button36.Enabled = true;
            }
            else
            {
                //button35.Enabled = false;
                //button36.Enabled = false;
            }

        }

        private void listViewLTC_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listViewBTC.SelectedItems.Count > 0)
            {
                button17.Enabled = true;
                button20.Enabled = true;
            }
            else
            {
                //button17.Enabled = false;
                //button20.Enabled = false;
            }
        }

        private void timerWarnPlay_Tick(object sender, EventArgs e)
        {
            timerWarnPlay.Enabled = false;
            sndPlayer.Stop();
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            lock (Data.SellDepthList)
            {
                txt_Sell_tradeCnyPrice.Text = CBP1.Text;
            }
        }

        private void lab_RemainderMoney_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void timer_alert_Tick(object sender, EventArgs e)
        {
            lab_alert.Text = "";
            lab_sell_alert.Text = "";
            timer_alert.Enabled = false;
        }

        private void timer_FlashWindow_Tick(object sender, EventArgs e)
        {
            FlashWindowCount++;
            if (FlashWindowCount >= 6)
            {
                timer_FlashWindow.Enabled = false;
                FlashWindowCount = 0;
                return;
            }
            FlashWindow(this.Handle, true);//闪烁 
        }






        private void GetOrderForId(Object o, EventArgs e)
        {

            if (Data.GetOrderHistoryResult == Define.Login_Succeed)
            {
                lock (Data.OrderList)
                {
                    ShowList(listView_Order, Data.OrderList);
                }

            }
        }

        private void NotfCoin_DoubleClick(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;  //显示在系统任务栏
            this.WindowState = FormWindowState.Normal;  //还原窗体
            NotfCoin.Visible = false;  //托盘图标隐藏
        }




    }

    public partial class Price
    {
        public int time;
        public Double price;
        public void setprice(String _price)
        {
            price = (Double)Convert.ChangeType(_price, typeof(Double));
        }
    }






}
