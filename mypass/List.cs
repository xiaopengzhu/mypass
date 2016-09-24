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
    public partial class List : Form
    {
        public List()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void List_Load(object sender, EventArgs e)
        {
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;

            listView1.Columns.Clear();
            listView1.Columns.Add("ID",  40, HorizontalAlignment.Center);
            listView1.Columns.Add("标题", 80, HorizontalAlignment.Center);
            listView1.Columns.Add("帐号", 80, HorizontalAlignment.Center);
            listView1.Columns.Add("密码", 80, HorizontalAlignment.Center);
            listView1.Columns.Add("二级密码", 80, HorizontalAlignment.Center);
            listView1.Columns.Add("备注", 160, HorizontalAlignment.Center);

            var model = new Record();
            var data = model.select();

            listView1.Items.Clear();
            foreach (DataRow row in data)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Clear();
                item.SubItems[0].Text = row["ID"].ToString();
                item.SubItems.Add(row["标题"].ToString());
                item.SubItems.Add(row["账户"].ToString());
                item.SubItems.Add(row["密码"].ToString());
                item.SubItems.Add(row["二级密码"].ToString());
                item.SubItems.Add(row["备注"].ToString());
                listView1.Items.Add(item);
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var add = new Add(this);
            add.ShowDialog();
        }

        private void List_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
