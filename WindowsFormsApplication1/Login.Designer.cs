namespace WindowsFormsApplication1
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panel_login = new System.Windows.Forms.Panel();
            this.label_updatealert = new System.Windows.Forms.Label();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_Username = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_google = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_GoogleCheck = new System.Windows.Forms.TextBox();
            this.panel_qq = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel_login.SuspendLayout();
            this.panel_google.SuspendLayout();
            this.panel_qq.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_login
            // 
            this.panel_login.Controls.Add(this.label_updatealert);
            this.panel_login.Controls.Add(this.linkLabel4);
            this.panel_login.Controls.Add(this.linkLabel3);
            this.panel_login.Controls.Add(this.linkLabel2);
            this.panel_login.Controls.Add(this.linkLabel1);
            this.panel_login.Controls.Add(this.button1);
            this.panel_login.Controls.Add(this.txt_password);
            this.panel_login.Controls.Add(this.txt_Username);
            this.panel_login.Controls.Add(this.label3);
            this.panel_login.Controls.Add(this.label2);
            this.panel_login.Controls.Add(this.label1);
            this.panel_login.Location = new System.Drawing.Point(49, 4);
            this.panel_login.Name = "panel_login";
            this.panel_login.Size = new System.Drawing.Size(312, 181);
            this.panel_login.TabIndex = 57;
            this.panel_login.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_login_Paint);
            // 
            // label_updatealert
            // 
            this.label_updatealert.AutoSize = true;
            this.label_updatealert.ForeColor = System.Drawing.Color.Red;
            this.label_updatealert.Location = new System.Drawing.Point(77, 158);
            this.label_updatealert.Name = "label_updatealert";
            this.label_updatealert.Size = new System.Drawing.Size(41, 12);
            this.label_updatealert.TabIndex = 66;
            this.label_updatealert.Text = "label8";
            this.label_updatealert.Visible = false;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(222, 134);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(77, 12);
            this.linkLabel4.TabIndex = 63;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "新浪微博登录";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(162, 134);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(41, 12);
            this.linkLabel3.TabIndex = 63;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "QQ登录";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked_1);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(246, 93);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(53, 12);
            this.linkLabel2.TabIndex = 64;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "忘记密码";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked_1);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(248, 54);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(29, 12);
            this.linkLabel1.TabIndex = 65;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "注册";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(74, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 23);
            this.button1.TabIndex = 62;
            this.button1.Text = "登录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(74, 88);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(168, 21);
            this.txt_password.TabIndex = 61;
            this.txt_password.Text = "zhu2154967";
            this.txt_password.TextChanged += new System.EventHandler(this.txt_password_TextChanged);
            this.txt_password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_password_KeyPress);
            // 
            // txt_Username
            // 
            this.txt_Username.Location = new System.Drawing.Point(74, 51);
            this.txt_Username.Name = "txt_Username";
            this.txt_Username.Size = new System.Drawing.Size(168, 21);
            this.txt_Username.TabIndex = 60;
            this.txt_Username.Text = "oxoxzhu@163.com";
            this.txt_Username.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Username_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 59;
            this.label3.Text = "密 码:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 58;
            this.label2.Text = "Email:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 57;
            this.label1.Text = "登录OKCoin ";
            // 
            // panel_google
            // 
            this.panel_google.Controls.Add(this.label4);
            this.panel_google.Controls.Add(this.button2);
            this.panel_google.Controls.Add(this.label5);
            this.panel_google.Controls.Add(this.label6);
            this.panel_google.Controls.Add(this.label7);
            this.panel_google.Controls.Add(this.txt_GoogleCheck);
            this.panel_google.Location = new System.Drawing.Point(28, 22);
            this.panel_google.Name = "panel_google";
            this.panel_google.Size = new System.Drawing.Size(303, 171);
            this.panel_google.TabIndex = 57;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "请输入Google验证码";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(108, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 12);
            this.label6.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 12);
            this.label7.TabIndex = 12;
            // 
            // txt_GoogleCheck
            // 
            this.txt_GoogleCheck.Location = new System.Drawing.Point(23, 70);
            this.txt_GoogleCheck.Name = "txt_GoogleCheck";
            this.txt_GoogleCheck.Size = new System.Drawing.Size(261, 21);
            this.txt_GoogleCheck.TabIndex = 11;
            this.txt_GoogleCheck.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_GoogleCheck_KeyPress);
            // 
            // panel_qq
            // 
            this.panel_qq.Controls.Add(this.webBrowser1);
            this.panel_qq.Location = new System.Drawing.Point(281, 6);
            this.panel_qq.Name = "panel_qq";
            this.panel_qq.Size = new System.Drawing.Size(340, 213);
            this.panel_qq.TabIndex = 59;
            this.panel_qq.Visible = false;
            this.panel_qq.Resize += new System.EventHandler(this.panel_qq_Resize);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(331, 194);
            this.webBrowser1.TabIndex = 59;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 234);
            this.Controls.Add(this.panel_qq);
            this.Controls.Add(this.panel_login);
            this.Controls.Add(this.panel_google);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "0";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Resize += new System.EventHandler(this.Login_Resize);
            this.panel_login.ResumeLayout(false);
            this.panel_login.PerformLayout();
            this.panel_google.ResumeLayout(false);
            this.panel_google.PerformLayout();
            this.panel_qq.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_login;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_Username;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_google;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_GoogleCheck;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.Panel panel_qq;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label label_updatealert;

    }
}