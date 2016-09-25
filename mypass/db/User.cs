using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mypass.db
{
    class User
    {
        public int login(string account, string password)
        {
            var query = "select * from 用户 where 账户='" + account + "' and 密码='" + password +"'";
            OleDbConnection conn = Database.GetConnection();
            OleDbCommand comm = new OleDbCommand(query, conn);
            if (comm.ExecuteScalar()!=null)
            {
                return (int)comm.ExecuteScalar();
            }
            else {
                return 0;
            }
        }
    }
}
