using System;
using System.IO;
using MauiAppMinhasCompras.Helpers;
using MauiAppMinhasCompras.Views;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        private static SQLiteDatabaseHelper _db;

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        public static SQLiteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3");

                    _db = new SQLiteDatabaseHelper(path);
                }

                return _db;
            }
        }
    }
}