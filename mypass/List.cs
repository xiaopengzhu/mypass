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
            //渲染listview
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            listView1.Columns.Clear();
            listView1.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("标题", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("网址", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("帐号", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("remark", "备注");

            listView1.Columns["remark"].Width = -2;

            var model = new Record();
            var data = model.select();

            var des = new DEScode(Session.password);
            listView1.Items.Clear();
            foreach (DataRow row in data)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems.Clear();
                item.SubItems[0].Text = row["ID"].ToString();
                item.SubItems.Add(des.DecryptDES(row["标题"].ToString()));
                item.SubItems.Add(des.DecryptDES(row["网址"].ToString()));
                item.SubItems.Add(des.DecryptDES(row["账户"].ToString()));
                item.SubItems.Add(des.DecryptDES(row["备注"].ToString()));
                listView1.Items.Add(item);
            }
            //渲染状态栏
            toolStripStatusLabel1.Text = "欢迎光临，" + Session.account;
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
            Process.Start("iexplore.exe", "http://www.xpcms.net/mypass/donate");
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

        //在线升级
        private void 在线升级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = "http://www.xpcms.net/mypass/upgrade";
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            string version = fvi.FileVersion;

            string new_version = Http.get(url);

            if (new_version == version)
            {
                MessageBox.Show("当前版本已经是最新，无需更新", "提示信息");
            }
            else
            {
                DialogResult dr = MessageBox.Show("有可用更新，是否要进行升级", "升级确认", MessageBoxButtons.OKCancel);

                if (dr == DialogResult.OK)
                {
                    Console.WriteLine("升级中");
                }
            }

        }

        //关于
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.Show();
        }

        //查看详情
        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string idstr = listView1.SelectedItems[0].SubItems[0].Text;
            int id = int.Parse(idstr);

            var view = new Detail(id);
            view.ShowDialog();
        }

        //导出
        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
