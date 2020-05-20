using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class Frm_ChatClient : Form
    {
        public Frm_ChatClient()
        {
            InitializeComponent();
        }
        TcpClient _client;
        private void btn_Start_Click(object sender, EventArgs e)
        {
            IPEndPoint clientAddr = new IPEndPoint(IPAddress.Any,new Random().Next(IPEndPoint.MinPort, IPEndPoint.MaxPort));
            _client = new TcpClient(clientAddr);
            IPEndPoint serverAddr = new IPEndPoint(IPAddress.Parse(txt_Ip.Text), (int)nud_Port.Value);
            _client.Connect(serverAddr);
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            _client.Client.Send(Encoding.UTF8.GetBytes(txt_Msg.Text));
        }
    }
}
