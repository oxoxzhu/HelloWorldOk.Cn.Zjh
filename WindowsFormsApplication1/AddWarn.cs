using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LitJson;

namespace WindowsFormsApplication1
{
    public partial class AddWarn : Form
    {


        int BTCorLTC = -1;
        int index = -1;


        public AddWarn()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            this.Text = Define.TitleBase;
            this.MaximizeBox = false;
            //this.MaximizeBox = false;

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;


            combo_greatorless.Items.Add("小于");
            combo_greatorless.Items.Add("大于");

            check_warn1.Text = "声音报警";
            check_warn2.Text = "任栏闪动";

            if (BTCorLTC == 0)
            {
                radioBTCorLTC1.Checked = true;

            }
            else
            {
                radioBTCorLTC2.Checked = true;

            }

            if (  BTCorLTC > -1 && index > -1)
            {
                WarnManagement.Instance().set_cur_warnlist(BTCorLTC);
                Warn warn= WarnManagement.Instance().get_Warn(index);
                if (warn != null)
                {


                    radioBTCorLTC1.Enabled = false;
                    radioBTCorLTC2.Enabled = false;
                    if (warn.greatorless == 0)
                    {
                        //combo_greatorless.Select(warn.greatorless);
                        combo_greatorless.SelectedIndex = warn.greatorless;
                    } 
                    else
                    {
                        //combo_greatorless.Select(warn.greatorless);
                        combo_greatorless.SelectedIndex = warn.greatorless;
                    }

                    txt_price.Text = warn.price.ToString();

                    if (warn.alerttype == 1)
                    {
                        check_warn1.Checked = true;
                    }
                    if (warn.alerttype == 2)
                    {
                        check_warn2.Checked = true;
                    }

                    if (warn.alerttype == 3)
                    {
                        check_warn1.Checked = true;
                        check_warn2.Checked = true;
                    }

                    button1.Text = "确定修改";



                }
            }
        }

        public void command_cointype(int _BTCorLTC)
        {
            BTCorLTC = _BTCorLTC;
        }


        public void command(int _BTCorLTC, int _index)
        {
            BTCorLTC =_BTCorLTC;
            index = _index;
        }

        public void resetcommand(int _BTCorLTC, int _index)
        {
            BTCorLTC = -1;
            index = -1;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if ( combo_greatorless.Text.ToString().Length == 0 
                 || txt_price.Text.ToString().Length == 0 
                 || (check_warn1.Checked == false && check_warn2.Checked == false) 
                )
            {
                return ;
            }

            int greatorless = 0;
            String price = txt_price.Text;
            int alerttype = 0;

            if (combo_greatorless.Text == "小于")
            {
                greatorless = 0;
            }
            else
            {
                greatorless = 1;
            }
            

            if (check_warn1.Checked == true )
            {
                alerttype = 1;
            }
            if (check_warn2.Checked == true)
            {
                alerttype = 2;
            }

            if (check_warn1.Checked == true && check_warn2.Checked == true)
            {
                alerttype = 3;
            }


            Warn warn = null;

            if (BTCorLTC > -1 && index > -1)//修改，
            {
                WarnManagement.Instance().set_cur_warnlist(BTCorLTC);
                warn = WarnManagement.Instance().get_Warn(index);

                warn.greatorless = greatorless;
                warn.setprice(txt_price.Text);
                warn.alerttype = alerttype;


                //WarnManagement.Instance().addtolist(warn);
                WarnManagement.Instance().write_json();
                MessageBox.Show(this, "修改预警成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            else //添加
            {

                warn = new Warn();

                warn.greatorless = greatorless;
                warn.setprice(txt_price.Text);
                warn.alerttype = alerttype;

                if (radioBTCorLTC1.Checked == true)
                {
                    WarnManagement.Instance().set_cur_warnlist(0);
                }
                else
                {
                    WarnManagement.Instance().set_cur_warnlist(1);
                }

                WarnManagement.Instance().addtolist(warn);
                WarnManagement.Instance().write_json();
                MessageBox.Show(this, "预警添加成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            this.Close();

        }





        private void button2_Click(object sender, EventArgs e)
        {
            JsonData data = new JsonData();

            string json = "{\"first\":\"one\",\"second\":\"two\"," +
                "\"third\":\"three\",\"fourth\":\"four\"}";

            for (int i = 0; i < 10; i++)
            {
                data.Clear();

                data["first"] = "one";
                data["second"] = "two";
                data["third"] = "three";
                data["fourth"] = "four";

                json += data.ToJson();
            }
        }

        private void txt_price_TextChanged(object sender, EventArgs e)
        {

        }





    }




}
