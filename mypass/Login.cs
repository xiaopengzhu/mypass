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
            //如果没有用户则启动创建用户窗体
        }

        //登录
        private void button1_Click(object sender, EventArgs e)
        {
            string account = textBox1.Text;
            byte[] password = Encoding.Default.GetBytes(textBox2.Text.Trim());
            MD5 md5 = new MD5CryptoServiceProvider();
            password = md5.ComputeHash(password);
            string md5pass = BitConverter.ToString(password).Replace("-", "").ToLower();
            
            var model = new User();
            if (model.login(account, md5pass) == 0)
            {
                var list = new List();
                list.Show();
                this.Visible = false;
            }
            else {
                MessageBox.Show("用户名或密码错误");
            }
        }

        //关闭
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
