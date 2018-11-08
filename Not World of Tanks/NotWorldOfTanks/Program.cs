using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotWorldOfTanks.View;

namespace NotWorldOfTanks
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AreaView area=new AreaView(new Size(500,500),numOfTanks: 5, numOfBonuses: 5, speed: 8);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(size:new System.Drawing.Size(500, 500),numOfTanks:5,numOfBonuses:5,speed:8));
        }
    }
}
