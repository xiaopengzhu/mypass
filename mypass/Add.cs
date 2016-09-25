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
    public partial class Add : Form
    {
        List parent;
        public Add(List list)
        {            
            InitializeComponent();
            this.parent = list;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var model = new Record();
            string[] columns = { "标题", "账户", "密码", "二级密码", "备注", "添加时间"};

            var des = new DEScode();
            string title = des.EncryptDES(textBox1.Text);
            string account = des.EncryptDES(textBox3.Text);
            string password = des.EncryptDES(textBox4.Text);
            string second_password = des.EncryptDES(textBox5.Text);
            string remark = des.EncryptDES(richTextBox1.Text);

            string[] values = { title, account, password, second_password, remark, DateTime.Now.ToString()};
            model.add(columns, values);
            parent.List_Load(sender, e);
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
