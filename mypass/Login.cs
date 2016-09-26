using mypass.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mypass
{
    public partial class Login : Form
    {
        //构造函数
        public Login()
        {            
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Database.CheckDatabase();                     
        }

        //登录
        private void button1_Click(object sender, EventArgs e)
        {
            string account = textBox1.Text;
            string password = textBox2.Text;
            
            var model = new User();
            if (model.login(account, password) == 0)
            {
                var list = new List();
                list.Show();
                this.Visible = false;
            }
            else {
                MessageBox.Show("用户名或密码错误", "错误提示");
            }
        }

        //关闭
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
