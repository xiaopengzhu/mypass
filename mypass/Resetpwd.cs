using mypass.db;
using mypass.lib;
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
    public partial class Resetpwd : Form
    {
        public Resetpwd()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //重设密码
        private void button1_Click(object sender, EventArgs e)
        {
            string pwd = textBox1.Text.ToString();
            string new_pwd = textBox2.Text.ToString();
            string confirm_pwd = textBox3.Text.ToString();

            var model = new User();
            string account = Session.account;
            int check = model.login(account, pwd);

            if (check > 0)
            {
                if (new_pwd.Length >=8 && new_pwd == confirm_pwd)
                {
                    if (model.reset(account, new_pwd) > 0)
                    {
                        //重新加密整个记录表
                        var record = new Record();
                        record.rebuild(pwd, new_pwd);
                        //重启程序
                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show("重置失败");
                    }
                    
                }
                else
                {
                    MessageBox.Show("新密码长度小于8或二次密码不匹配");
                }
            }
            else
            {
                MessageBox.Show("密码不对!");
            }                        
        }
    }
}
