using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace mypass.db
{
    class Record
    {
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
    }
}
