using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOX;
using System.Data.OleDb;
using System.IO;

namespace mypass.db
{
    class Database
    {
        //建立连接
        public static OleDbConnection GetConnection()
        {
            OleDbConnection conn = new OleDbConnection();
            try
            {
                String connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;"
                + @"Data Source=" + System.Windows.Forms.Application.StartupPath + "\\res\\mypass.accdb;"
                + "Jet OLEDB:Database Password=mypass2016";
                conn = new OleDbConnection(connectionString);
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return conn;
        }

        //判断数据库是否存在
        //没有则新建
        public static bool CheckDatabase()
        {
            string path = @System.Windows.Forms.Application.StartupPath + "\\res\\mypass2.accdb";
            if (!File.Exists(path))
            {
                //建库
                ADOX.Catalog catalog = new Catalog();
                catalog.Create(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + " ;Jet OLEDB:Engine Type=5");
                //建表
                OleDbConnection conn = GetConnection();
                if (conn.ToString().Length > 0)
                {
                    string query = "create table 用户 ([ID] AutoNumber , [账户] Text, [密码] Text)";
                    OleDbCommand comm = new OleDbCommand(query, conn);
                    comm.ExecuteNonQuery();


                    conn.Close();
                    return true;
                }
                else
                {
                    return false;
                }      
            }
            else
            {
                return true;
            }
        }
    }
}
