using ADOX;
using mypass.lib;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace mypass.db
{
    class User
    {
        //建表
        public void CreateTable()
        {
            OleDbConnection conn = Database.GetConnection();

            if (conn.ToString().Length > 0)
            {
                string sql = @"CREATE TABLE [用户] "
                + "([ID] Counter primary key, [账户] VarChar(255), "
                + "[密码] VarChar(255), [登录时间] DateTime);";

                OleDbCommand comm = new OleDbCommand(sql, conn);
                comm.ExecuteNonQuery();
            }
            conn.Close();
        }

        //判断是否注册
        public int check()
        {
            var query = "select * from 用户";
            OleDbConnection conn = Database.GetConnection();
            OleDbCommand comm = new OleDbCommand(query, conn);

            var obj = comm.ExecuteScalar();
            conn.Close();

            if (obj != null)
            {
                return (int)obj;
            }
            else
            {
                return 0;
            }
        }

        //注册
        public int register(string account, string password)
        {
            if (password.Length < 8) return 0;
            byte[] tmp = Encoding.Default.GetBytes(password.Trim());
            MD5 md5 = new MD5CryptoServiceProvider();
            tmp = md5.ComputeHash(tmp);
            string md5pass = BitConverter.ToString(tmp).Replace("-", "").ToLower();

            String query = "insert into 用户 (账户, 密码) values ('" + account + "', '" + md5pass + "')";

            OleDbConnection conn = Database.GetConnection();
            OleDbCommand comm = new OleDbCommand(query, conn);
            int num = comm.ExecuteNonQuery();
            conn.Close();
            return num;
        }

        //登录
        public int login(string account, string password)
        {
            byte[] tmp = Encoding.Default.GetBytes(password.Trim());
            MD5 md5 = new MD5CryptoServiceProvider();
            tmp = md5.ComputeHash(tmp);
            string md5pass = BitConverter.ToString(tmp).Replace("-", "").ToLower();

            var query = "select * from 用户 where 账户='" + account + "' and 密码='" + md5pass + "'";
            OleDbConnection conn = Database.GetConnection();
            OleDbCommand comm = new OleDbCommand(query, conn);

            var obj = comm.ExecuteScalar();
            int tag = 0;
            
            if (obj!=null)
            {
                Session.account = account;
                Session.password = password;

                var update = "update 用户 set 登录时间='" + DateTime.Now + "' where 账户='" + account + "'";
                OleDbCommand comm2 = new OleDbCommand(update, conn);
                comm2.ExecuteNonQuery();
                
                tag = 1;
            }

            conn.Close();
            return tag;
        }

        //重设密码
        public int reset(string account, string password)
        {
            if (password.Length < 8) return 0;
            byte[] tmp = Encoding.Default.GetBytes(password.Trim());
            MD5 md5 = new MD5CryptoServiceProvider();
            tmp = md5.ComputeHash(tmp);
            string md5pass = BitConverter.ToString(tmp).Replace("-", "").ToLower();

            var query = "update 用户 set 密码='" +md5pass + "' where 账户='" + account + "'";
            OleDbConnection conn = Database.GetConnection();
            OleDbCommand comm = new OleDbCommand(query, conn);

            if (comm.ExecuteNonQuery() > 0)
            {
                Session.account = account;
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
