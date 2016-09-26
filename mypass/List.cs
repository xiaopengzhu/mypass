using mypass.db;
using mypass.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mypass
{
    public partial class List : Form
    {
        //构造函数
        public List()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //退出
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //页面加载
        public void List_Load(object sender, EventArgs e)
        {
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;

            listView1.Columns.Clear();
            listView1.Columns.Add("ID",  60, HorizontalAlignment.Center);
            listView1.Columns.Add("标题", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("网址", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("帐号", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("密码", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("二级密码", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("备注", 196, HorizontalAlignment.Center);

            var model = new Record();
            var data = model.select();

            var des = new DEScode();
            listView1.Items.Clear();
            foreach (DataRow row in data)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Clear();
                item.SubItems[0].Text = row["ID"].ToString();
                item.SubItems.Add(des.DecryptDES(row["标题"].ToString()));
                item.SubItems.Add(des.DecryptDES(row["网址"].ToString()));
                item.SubItems.Add(des.DecryptDES(row["账户"].ToString()));
                item.SubItems.Add(des.DecryptDES(row["密码"].ToString()));
                item.SubItems.Add(des.DecryptDES(row["二级密码"].ToString()));
                item.SubItems.Add(des.DecryptDES(row["备注"].ToString()));
                listView1.Items.Add(item);
            }
        }

        //新建
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var add = new Add(this);
            add.ShowDialog();
        }

        //关闭
        private void List_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //右键菜单
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location) == true)
                {

                    

                    contextMenuStrip2.Show(Cursor.Position);
                }
            }
        }

        //右键删除
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要删除该项，该操作不可恢复", "删除确认", MessageBoxButtons.OKCancel);

            if (dr == DialogResult.OK)
            {
                string idstr = listView1.SelectedItems[0].SubItems[0].Text;
                int id = int.Parse(idstr);

                var model = new Record();
                model.delete(id);

                List_Load(sender, e);

            }
            else
            {

            }
        }

        //右键编辑
        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string idstr = listView1.SelectedItems[0].SubItems[0].Text;
            int id = int.Parse(idstr);

            var update = new Add(this, id);
            update.ShowDialog();
        }

        //捐赠
        private void 捐赠ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("iexplore.exe", "http://www.baidu.com");
        }


        //清空
        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要清空记录，该操作不可恢复", "删除确认", MessageBoxButtons.OKCancel);

            if (dr == DialogResult.OK)
            {
                var model = new Record();
                model.clear();

                List_Load(sender, e);

            }

        }

        //修改密码
        private void 修改密码ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var reset = new Resetpwd();
            reset.ShowDialog();
        }
    }
}
