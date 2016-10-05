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
    public partial class Detail : Form
    {
        public Detail(int id)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            var model = new Record();
            DataRow dr = model.get(id);

            if (dr != null)
            {
                var des = new DEScode(Session.password);
                label7.Text = des.DecryptDES(dr["标题"].ToString());
                label8.Text = des.DecryptDES(dr["网址"].ToString());
                label9.Text = des.DecryptDES(dr["账户"].ToString());
                label10.Text = des.DecryptDES(dr["密码"].ToString());
                label11.Text = des.DecryptDES(dr["二级密码"].ToString());
                label12.Text = des.DecryptDES(dr["备注"].ToString());
            }

        }
    }
}
