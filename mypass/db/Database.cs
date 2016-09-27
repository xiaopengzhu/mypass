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

        //建库表
        public static bool CreateDb()
        {
            string path = @System.Windows.Forms.Application.StartupPath + "\\res\\mypass.accdb";
            if (!File.Exists(path))
            {
                //建库
                ADOX.Catalog catalog = new Catalog();
                catalog.Create(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path
                + " ;Jet OLEDB:Engine Type=5;Jet OLEDB:Database Password=mypass2016");

                //建用户表
                var user = new User();
                user.CreateTable();

                var record = new Record();
                record.CreateTable();

                return true; 
            }
            else
            {
                return true;
            }
        }
    }
}
