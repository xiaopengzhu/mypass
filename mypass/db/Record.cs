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
            comm.ExecuteNonQuery();
            conn.Close();
            return 1;
        }

        //删
        public int delete(int id)
        {

            String query = "delete from 记录 where id=" + id;

            OleDbConnection conn = Database.GetConnection();
            OleDbCommand comm = new OleDbCommand(query, conn);
            comm.ExecuteNonQuery();
            conn.Close();
            return 1;
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
                comm.ExecuteNonQuery();
                conn.Close();
                
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
