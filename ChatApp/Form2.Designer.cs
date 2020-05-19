namespace ChatApp
{
    partial class Frm_ChatClient
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
            this.txt_Ip = new System.Windows.Forms.TextBox();
            this.nud_Port = new System.Windows.Forms.NumericUpDown();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Send = new System.Windows.Forms.Button();
            this.txt_Msg = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Port)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_Ip
            // 
            this.txt_Ip.Location = new System.Drawing.Point(118, 68);
            this.txt_Ip.Name = "txt_Ip";
            this.txt_Ip.Size = new System.Drawing.Size(100, 21);
            this.txt_Ip.TabIndex = 0;
            // 
            // nud_Port
            // 
            this.nud_Port.Location = new System.Drawing.Point(118, 118);
            this.nud_Port.Name = "nud_Port";
            this.nud_Port.Size = new System.Drawing.Size(120, 21);
            this.nud_Port.TabIndex = 1;
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(337, 93);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 2;
            this.btn_Start.Text = "连接";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(337, 144);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 23);
            this.btn_Send.TabIndex = 3;
            this.btn_Send.Text = "发送";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // txt_Msg
            // 
            this.txt_Msg.Location = new System.Drawing.Point(154, 178);
            this.txt_Msg.Name = "txt_Msg";
            this.txt_Msg.Size = new System.Drawing.Size(100, 21);
            this.txt_Msg.TabIndex = 4;
            // 
            // Frm_ChatClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt_Msg);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.nud_Port);
            this.Controls.Add(this.txt_Ip);
            this.Name = "Frm_ChatClient";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.nud_Port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Ip;
        private System.Windows.Forms.NumericUpDown nud_Port;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox txt_Msg;
    }
}