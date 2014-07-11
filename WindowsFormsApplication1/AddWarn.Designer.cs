namespace WindowsFormsApplication1
{
    partial class AddWarn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddWarn));
            this.label3 = new System.Windows.Forms.Label();
            this.radioBTCorLTC2 = new System.Windows.Forms.RadioButton();
            this.radioBTCorLTC1 = new System.Windows.Forms.RadioButton();
            this.check_warn2 = new System.Windows.Forms.CheckBox();
            this.check_warn1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.combo_greatorless = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_price = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 8;
            // 
            // radioBTCorLTC2
            // 
            this.radioBTCorLTC2.AutoSize = true;
            this.radioBTCorLTC2.Location = new System.Drawing.Point(145, 11);
            this.radioBTCorLTC2.Name = "radioBTCorLTC2";
            this.radioBTCorLTC2.Size = new System.Drawing.Size(113, 16);
            this.radioBTCorLTC2.TabIndex = 58;
            this.radioBTCorLTC2.Text = "莱特币(LTC)交易";
            this.radioBTCorLTC2.UseVisualStyleBackColor = true;
            // 
            // radioBTCorLTC1
            // 
            this.radioBTCorLTC1.AutoSize = true;
            this.radioBTCorLTC1.Checked = true;
            this.radioBTCorLTC1.Location = new System.Drawing.Point(12, 12);
            this.radioBTCorLTC1.Name = "radioBTCorLTC1";
            this.radioBTCorLTC1.Size = new System.Drawing.Size(113, 16);
            this.radioBTCorLTC1.TabIndex = 57;
            this.radioBTCorLTC1.TabStop = true;
            this.radioBTCorLTC1.Text = "比特币(BTC)交易";
            this.radioBTCorLTC1.UseVisualStyleBackColor = true;
            // 
            // check_warn2
            // 
            this.check_warn2.AutoSize = true;
            this.check_warn2.Location = new System.Drawing.Point(404, 43);
            this.check_warn2.Name = "check_warn2";
            this.check_warn2.Size = new System.Drawing.Size(78, 16);
            this.check_warn2.TabIndex = 64;
            this.check_warn2.Text = "checkBox1";
            this.check_warn2.UseVisualStyleBackColor = true;
            // 
            // check_warn1
            // 
            this.check_warn1.AutoSize = true;
            this.check_warn1.Location = new System.Drawing.Point(302, 43);
            this.check_warn1.Name = "check_warn1";
            this.check_warn1.Size = new System.Drawing.Size(78, 16);
            this.check_warn1.TabIndex = 65;
            this.check_warn1.Text = "checkBox1";
            this.check_warn1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 28);
            this.button1.TabIndex = 63;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(266, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 62;
            this.label2.Text = "时";
            // 
            // combo_greatorless
            // 
            this.combo_greatorless.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_greatorless.FormattingEnabled = true;
            this.combo_greatorless.Location = new System.Drawing.Point(89, 40);
            this.combo_greatorless.Name = "combo_greatorless";
            this.combo_greatorless.Size = new System.Drawing.Size(69, 20);
            this.combo_greatorless.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 60;
            this.label1.Text = "当价格:";
            // 
            // txt_price
            // 
            this.txt_price.Location = new System.Drawing.Point(173, 39);
            this.txt_price.MaxLength = 10;
            this.txt_price.Name = "txt_price";
            this.txt_price.Size = new System.Drawing.Size(84, 21);
            this.txt_price.TabIndex = 59;
            this.txt_price.TextChanged += new System.EventHandler(this.txt_price_TextChanged);
            // 
            // AddWarn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 123);
            this.Controls.Add(this.check_warn2);
            this.Controls.Add(this.check_warn1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.combo_greatorless);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_price);
            this.Controls.Add(this.radioBTCorLTC2);
            this.Controls.Add(this.radioBTCorLTC1);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddWarn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioBTCorLTC2;
        private System.Windows.Forms.RadioButton radioBTCorLTC1;
        private System.Windows.Forms.CheckBox check_warn2;
        private System.Windows.Forms.CheckBox check_warn1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combo_greatorless;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_price;
    }
}