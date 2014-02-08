using KegelApp.Ipc.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KegelApp.WinClient
{
    public partial class Form1 : Form
    {
        NancyClient client;

        public Form1()
        {
            InitializeComponent();

            textBox1.Text = "http://localhost:8008";
            client = new NancyClient(textBox1.Text);
        }

        void LoadUser()
        {
            bsUserData.DataSource = client.GetUser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadUser();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.PutUser((UserData)bsUserData.Current);
        }
    }
}
