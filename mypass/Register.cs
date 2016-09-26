using mypass.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mypass
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var model = new User();

            string account = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string confirm_password = textBox3.Text.Trim();

            if (account.Length < 1 || password.Length < 1)
            {
                MessageBox.Show("必填参数不能为空");
            } else if (password != confirm_password)
            {
                MessageBox.Show("二次密码不匹配");
            }
            else
            {
                if (model.register(account, password) > 0)
                {
                    var login = new Login();
                    login.Show();
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("注册失败");
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
