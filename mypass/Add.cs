﻿using mypass.db;
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
        int id;

        //构造函数
        public Add(List list, int id = 0)
        {            
            InitializeComponent();
            this.parent = list;
            this.id = id;

            if (this.id > 0) {
                var model = new Record();
                DataRow dr = model.get(this.id);
                if (dr != null) {
                    var des = new DEScode(Session.password);
                    textBox1.Text = des.DecryptDES(dr["标题"].ToString());
                    textBox2.Text = des.DecryptDES(dr["网址"].ToString());
                    textBox3.Text = des.DecryptDES(dr["账户"].ToString());
                    textBox4.Text = des.DecryptDES(dr["密码"].ToString());
                    textBox5.Text = des.DecryptDES(dr["二级密码"].ToString());
                    richTextBox1.Text = des.DecryptDES(dr["备注"].ToString());
                }
            }

            this.StartPosition = FormStartPosition.CenterParent;
        }

        //新增或者编辑保存
        private void button1_Click(object sender, EventArgs e)
        {
            var model = new Record();
            string[] columns = { "标题", "网址", "账户", "密码", "二级密码", "备注", "添加时间"};

            var des = new DEScode(Session.password);
            string title = des.EncryptDES(textBox1.Text);
            string website = des.EncryptDES(textBox2.Text);
            string account = des.EncryptDES(textBox3.Text);
            string password = des.EncryptDES(textBox4.Text);
            string second_password = des.EncryptDES(textBox5.Text);
            string remark = des.EncryptDES(richTextBox1.Text);

            if (textBox1.Text.Trim().Length < 1 || textBox3.Text.Trim().Length < 1 || textBox4.Text.Trim().Length < 1)
            {
                MessageBox.Show("缺少必填参数", "错误提示");
            }
            else
            {
                string[] values = { title, website, account, password, second_password, remark, DateTime.Now.ToString() };
                if (this.id > 0)
                {
                    model.update(this.id, columns, values);
                }
                else
                {
                    model.add(columns, values);
                }

                parent.List_Load(sender, e);
                this.Close();
            }

        }
    }
}
