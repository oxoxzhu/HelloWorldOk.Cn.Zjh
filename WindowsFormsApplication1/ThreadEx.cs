using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public delegate void ReadParamEventHandler(string sParam);  
    class ThreadEx
    {
        //new ThreadStart(Run)
        public Thread thread;
        private ReadParamEventHandler OnReadParamEvent;
        public Form form;
        public EventHandler eventhandler;

        public ThreadEx()
        {
        }
        public void Start(ThreadStart threadstart, EventHandler _eventhandler,  Form _form)
        {
            ReadParam += this.OnRead;
            eventhandler = _eventhandler;
            form = _form;
            thread = new Thread(threadstart);
            thread.IsBackground = true;
            thread.Start();
        }
        public void Start(ParameterizedThreadStart threadstart, Object Parameter, EventHandler _eventhandler, Form _form)
        {
            ReadParam += this.OnRead;
            eventhandler = _eventhandler;
            form = _form;
            thread = new Thread(threadstart);
            thread.IsBackground = true;
            thread.Start(Parameter);
        }
        public event ReadParamEventHandler ReadParam  
        {  
            add { OnReadParamEvent += new ReadParamEventHandler(value);}  
            remove{ OnReadParamEvent -= new ReadParamEventHandler(value);}  
        }  
        private void OnRead(string sParam)  
        {

            try
            {
                Object[] list = { this, System.EventArgs.Empty, sParam };
                form.BeginInvoke(eventhandler, list);  
            }
            catch (System.Exception ex)
            {
            	
            }

        }
        public void Abort()
        {
            thread.Abort();
        }

        public void GetUserInfo()
        {
            Data.GetUserInfoResult = Protocol.GetUserInfo();
            OnReadParamEvent(Data.GetUserInfoResult.ToString());//触发事件   
        }

        public void GetAssets()
        {
            Data.GetAssetsResult = Protocol.GetAssets();
            OnReadParamEvent(Data.GetAssetsResult.ToString());//触发事件   
        }

        public void GetTickerinfo()//各网站行情信息:
        {
            Data.GetTickerinfoResult = Protocol.GetTickerinfo();
            OnReadParamEvent(Data.GetTickerinfoResult.ToString());//触发事件   
        }
        public void GetUserConfig()//各网站行情信息:
        {
            Data.GetUserConfigResult = Protocol.GetUserConfig();
            OnReadParamEvent(Data.GetUserConfigResult.ToString());//触发事件   
        }
        public void GetCheckVersion()//各网站行情信息:
        {
            Data.GetCheckVersionResult = Protocol.GetCheckVersion();
            OnReadParamEvent(Data.GetCheckVersionResult.ToString());//触发事件   
        }

        public void GetNewVersion()//各网站行情信息:
        {
            Data.GetNewVersionResult = Protocol.GetNewVersion();
            OnReadParamEvent(Data.GetNewVersionResult.ToString());//触发事件   
        }

        public void GetDepth(object obj)
        {
            DepthParm depthparm = obj as DepthParm;
            Data.GetDepthResult = Protocol.GetDepth(depthparm.symbol);
            OnReadParamEvent(Data.GetDepthResult.ToString());//触发事件  
        }
        public void GetOrderHistory(object obj)
        {
            OrderParm orderparm = obj as OrderParm;

            Data.GetOrderHistoryResult = Protocol.DoOrderHistory( orderparm.status,orderparm.symbol, orderparm.currentPage, orderparm.pageLength);
            OnReadParamEvent(Data.GetOrderHistoryResult.ToString());//触发事件  
        }
        public void GetOrder(object obj)
        {
            OrderParm orderparm = obj as OrderParm;
            Data.GetOrderResult = Protocol.DoOrder(orderparm.symbol, orderparm.Order_id);
            OnReadParamEvent(Data.GetOrderResult.ToString());//触发事件  

            //OrderParm orderparm = obj as OrderParm;
            //Data.GetOrderResult = Protocol.DoOrderHistory(orderparm.symbol, orderparm.status, orderparm.currentPage, orderparm.pageLength);
            //OnReadParamEvent(Data.GetOrderResult.ToString());//触发事件  
        }

        public void GetCancelOrder(object obj)
        {
            CancelOrderParm cancelorderparm = obj as CancelOrderParm;
            Data.GetOrderResult = Protocol.GetCancelOrder(cancelorderparm.symbol, cancelorderparm.Order_id);
            OnReadParamEvent(Data.GetOrderResult.ToString());//触发事件  
        }

        public void GetLogin(object obj)
        {
            LoginParm loginparm = obj as LoginParm;

            Data.GetLoginResult = Protocol.DoLogin(loginparm.Username, loginparm.Password);
            OnReadParamEvent(Data.GetLoginResult.ToString());//触发事件  
        }

        public void GetQQLogin()
        {
            Data.GetQQLoginResult = Protocol.GetQQLogin();
            OnReadParamEvent(Data.GetQQLoginResult.ToString());//触发事件  
        }

        public void GetGoogleCheck(object obj)
        {
            GoogleCheckParm googlecheckparm = obj as GoogleCheckParm;

            Define.GoogleCheckResult = Protocol.GetGoogleCheckResult(googlecheckparm.totpCode);
            OnReadParamEvent(Define.GoogleCheckResult.ToString());//触发事件  
        }

        
    }
    public class GoogleCheckParm
    {
        public string totpCode { get; set; }
    }


    public class LoginParm
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class OrderParm
    {
        public string status { get; set; }
        public string symbol { get; set; }
        public string currentPage { get; set; }
        public string pageLength { get; set; }


        public string Order_id { get; set; }
        public string deal_amount { get; set; }//成交数量
        public string avg_rate { get; set; }// = "";//平均成交价



    }


   

    public class DepthParm
    {
        public string symbol { get; set; }
    }


    public class CancelOrderParm
    {
        public string symbol { get; set; }
        public string Order_id { get; set; }
    }





}
