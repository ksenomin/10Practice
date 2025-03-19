using Auction.Models;
using System;
using System.Windows.Forms;

namespace Auction
{
    internal static class Program
    {
        public static AuctionModel context = new AuctionModel();
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!context.Database.Exists())
            {
                MessageBox.Show("Не удалось установить соединение с базой данных.");
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
