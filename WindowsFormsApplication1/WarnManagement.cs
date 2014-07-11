using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;
using System.IO;

namespace WindowsFormsApplication1
{
    class WarnManagement
    {
        private List<Warn> warnlistbtc = new List<Warn>();
        private List<Warn> warnlistltc = new List<Warn>();

        public List<Warn> warnlistcur = new List<Warn>();

        private String curwarnfilename = "";

        private static WarnManagement _instance;
        public static WarnManagement Instance()
        {
            if (_instance == null)
                _instance = new WarnManagement();
            return _instance;
        }

        public void LoadWarn()
        {

        }

        public List<Warn> get_warnlist(int flag)
        {
            if (flag == 0)
            {
                return warnlistbtc;
            }
            else
            {
                return warnlistltc;
            }
        }

        public void set_cur_warnlist(int flag)
        {
            if (flag == 0)
            {
                warnlistcur = warnlistbtc;
                curwarnfilename = @"warnbtc.dat";
            } 
            else
            {
                warnlistcur = warnlistltc;
                curwarnfilename = @"warnltc.dat";
            }
        }

        public void addtolist(Warn warn)
        {
            warnlistcur.Add(warn);
        }

        public void clearlist()
        {
            warnlistcur.Clear();
        }

        public Warn get_Warn(int index)
        {
            return warnlistcur[index];
        }

        public void delete_Warn(int index)
        {
            warnlistcur.RemoveAt(index);
            write_json();
            return;

        }

        public void write_json()
        {

            String OutJson = "";
            JsonData data = new JsonData();
            data["warncount"] = warnlistcur.Count.ToString();

            for (int i = 0; i < warnlistcur.Count; i++)
            {
                JsonData datasub = new JsonData();
                datasub["greatorless"] = warnlistcur[i].greatorless.ToString();
                datasub["price"] = warnlistcur[i].price.ToString();
                datasub["alerttype"] = warnlistcur[i].alerttype.ToString();

                data["warnlist" + i.ToString()] = datasub;
            }

            String json = data.ToJson();
            File.WriteAllText(curwarnfilename, json, Encoding.ASCII);
        }


        public void read_json()
        {
            if (File.Exists(curwarnfilename) == true)
            {
                string json = File.ReadAllText(curwarnfilename, Encoding.ASCII);

                JsonReader reader = new JsonReader(json);
                JsonData data = JsonMapper.ToObject(reader);
                int count = (int)Convert.ChangeType(data["warncount"].ToString(), typeof(int));
                if (count > 0)
                {
                    WarnManagement.Instance().clearlist();
                }
                for (int i = 0; i < count; i++ )
                {
                    JsonData datasub = data["warnlist" + i.ToString()];
                    Warn warn = new Warn();

                    warn.setgreatorless(datasub["greatorless"].ToString());
                    warn.setprice(datasub["price"].ToString());
                    warn.setalerttype(datasub["alerttype"].ToString());

                    WarnManagement.Instance().addtolist(warn);
                }
            }
        }
        

    }

    public partial class Warn
    {
        public int greatorless;
        public Double price;
        public int alerttype;

        public void setprice(String _price)
        {
            price = (Double)Convert.ChangeType(_price, typeof(Double));
            price = (Double)Convert.ChangeType(price.ToString("0.000").TrimEnd('0').TrimEnd('.'), typeof(Double));
        }
        public void setgreatorless(String _value)
        {
            greatorless = (int)Convert.ChangeType(_value, typeof(int));
        }
        public void setalerttype(String _value)
        {
            alerttype = (int)Convert.ChangeType(_value, typeof(int));
        }

    }

}
