using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class Frm_ChatServer : Form
    {
        public TcpListener _listener;
        public Frm_ChatServer()
        {
            InitializeComponent();
        }

        private void Frm_ChatServer_Load(object sender, EventArgs e)
        {

            txt_Ip.Text = IPAddress.Any.ToString();

        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            IPAddress addr = IPAddress.Parse(txt_Ip.Text);
            _listener = new TcpListener(addr, (int)nud_Ip.Value);
            _listener.Start();

            Task.Run(() =>
            {

                while (true)
                {

                    var curClient = _listener.AcceptTcpClient();

                    Thread curThread = new Thread(Recive);

                    curThread.Start(curClient);
                }


            });
        }


        public void Recive(object client)
        {
            TcpClient curClient = client as TcpClient;
            NetworkStream curStream = curClient.GetStream();
            byte[] buffArr = new byte[curStream.Length];

            curStream.Read(buffArr, 0, buffArr.Length);
            curStream.Flush();
            curStream.Close();
            curStream.Dispose();

            string content= Encoding.UTF8.GetString(buffArr);

            MessageBox.Show(content);

        }

        private void btn_Conn_Click(object sender, EventArgs e)
        {
            Frm_ChatClient client = new Frm_ChatClient();

            client.Show();

        }
    }
}
