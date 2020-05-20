namespace ChatApp
{
    partial class Frm_ChatServer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Start = new System.Windows.Forms.Button();
            this.txt_Ip = new System.Windows.Forms.TextBox();
            this.nud_Ip = new System.Windows.Forms.NumericUpDown();
            this.btn_Conn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Ip)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(353, 48);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "启动";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // txt_Ip
            // 
            this.txt_Ip.Location = new System.Drawing.Point(46, 47);
            this.txt_Ip.Name = "txt_Ip";
            this.txt_Ip.Size = new System.Drawing.Size(100, 21);
            this.txt_Ip.TabIndex = 1;
            // 
            // nud_Ip
            // 
            this.nud_Ip.Location = new System.Drawing.Point(205, 48);
            this.nud_Ip.Name = "nud_Ip";
            this.nud_Ip.Size = new System.Drawing.Size(100, 21);
            this.nud_Ip.TabIndex = 2;
            // 
            // btn_Conn
            // 
            this.btn_Conn.Location = new System.Drawing.Point(468, 48);
            this.btn_Conn.Name = "btn_Conn";
            this.btn_Conn.Size = new System.Drawing.Size(75, 23);
            this.btn_Conn.TabIndex = 3;
            this.btn_Conn.Text = "创建";
            this.btn_Conn.UseVisualStyleBackColor = true;
            this.btn_Conn.Click += new System.EventHandler(this.btn_Conn_Click);
            // 
            // Frm_ChatServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Conn);
            this.Controls.Add(this.nud_Ip);
            this.Controls.Add(this.txt_Ip);
            this.Controls.Add(this.btn_Start);
            this.Name = "Frm_ChatServer";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Frm_ChatServer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud_Ip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.TextBox txt_Ip;
        private System.Windows.Forms.NumericUpDown nud_Ip;
        private System.Windows.Forms.Button btn_Conn;
    }
}

