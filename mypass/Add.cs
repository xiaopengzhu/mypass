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
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var model = new Record();
            string[] columns = { "标题", "账户", "密码", "二级密码", "备注", "添加时间"};
            string[] values = {textBox1.Text, textBox3.Text, textBox4.Text, textBox5.Text, richTextBox1.Text, DateTime.Now.ToString()};
            model.add(columns, values);
            this.Close();
        }
    }
}
