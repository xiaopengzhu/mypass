using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using mypass.lib;

namespace mypass.db
{
    class Record
    {
        //建表
        public void CreateTable()
        {
            OleDbConnection conn = Database.GetConnection();

            if (conn.ToString().Length > 0)
            {
                string sql = @"CREATE TABLE [记录] "
                + "([ID] Counter primary key, [标题] VarChar(255), "
                + "[网址] VarChar(255), [账户] VarChar(255), "
                + "[密码] VarChar(255), [二级密码] VarChar(255), "
                + "[备注] Text, [添加时间] DateTime);";

                OleDbCommand comm = new OleDbCommand(sql, conn);
                comm.ExecuteNonQuery();
            }
            conn.Close();
        }

        //列表
        public DataRowCollection select()
        {
            DataSet ds = new DataSet();
            OleDbConnection conn = Database.GetConnection();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from 记录", conn);
            da.Fill(ds);
            conn.Close();
            DataTable dt = ds.Tables[0];
            return dt.Rows;
        }

        //增
        public int add(string[] columns, string[] values)
        {
            String query = "insert into 记录 ("+ string.Join(",", columns) +") " +
                "values ('"+ string.Join("','", values) +"')";

            OleDbConnection conn = Database.GetConnection();
            OleDbCommand comm = new OleDbCommand(query, conn);
            int num = comm.ExecuteNonQuery();
            conn.Close();
            return num;
        }

        //删
        public int delete(int id)
        {

            String query = "delete from 记录 where id=" + id;

            OleDbConnection conn = Database.GetConnection();
            OleDbCommand comm = new OleDbCommand(query, conn);
            int num = comm.ExecuteNonQuery();
            conn.Close();
            return num;
        }

        //查
        public DataRow get(int id)
        {
            DataSet ds = new DataSet();
            OleDbConnection conn = Database.GetConnection();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from 记录 where id=" + id, conn);
            da.Fill(ds);
            conn.Close();
            DataTable dt = ds.Tables[0];
            return dt.Rows[0];
        }

        //改
        public int update(int id, string[] columns, string[] values)
        {
            if (columns.Length == values.Length)
            {
                string str = "";
                for (int i = 0; i < columns.Length; i++) {
                    str += columns[i] +"='"+values[i] + "',";
                }
                str = str.Substring(0, str.Length - 1);
                
                String query = "update 记录 set " + str + " where id=" + id;

                OleDbConnection conn = Database.GetConnection();
                OleDbCommand comm = new OleDbCommand(query, conn);
                int num = comm.ExecuteNonQuery();
                conn.Close();
                return num;
            }
            else
            {
                return 0;
            }
        }

        //清空
        public int clear()
        {
            String query = "delete * from 记录";

            OleDbConnection conn = Database.GetConnection();
            OleDbCommand comm = new OleDbCommand(query, conn);
            int num = comm.ExecuteNonQuery();
            conn.Close();
            return num;
        }

        //重新加密
        public void rebuild(string old_password, string new_password)
        {
            DataRowCollection drc = this.select();
            var old_des = new DEScode(old_password);
            var new_des = new DEScode(new_password);
            foreach (DataRow row in drc)
            {
                int id = int.Parse(row["ID"].ToString());
                string title = new_des.EncryptDES(old_des.DecryptDES(row["标题"].ToString()));
                string website = new_des.EncryptDES(old_des.DecryptDES(row["网址"].ToString()));
                string account = new_des.EncryptDES(old_des.DecryptDES(row["账户"].ToString()));
                string password = new_des.EncryptDES(old_des.DecryptDES(row["密码"].ToString()));
                string second_password = new_des.EncryptDES(old_des.DecryptDES(row["二级密码"].ToString()));
                string remark = new_des.EncryptDES(old_des.DecryptDES(row["备注"].ToString()));
                
                string[] columns = { "标题", "网址", "账户", "密码", "二级密码", "备注"};
                string[] values = {title, website, account, password, second_password, remark };

                this.update(id, columns, values);
            }
        }
    }
}
