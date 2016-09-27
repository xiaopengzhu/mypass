using mypass.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mypass
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //检测是否需要新建库
            Database.CreateDb();

            //检测是否要重新注册
            var model = new User();
            if (model.check() == 0)
            {
                Application.Run(new Register());
            }
            else
            {
                Application.Run(new Login());
            }            
        }
    }
}
