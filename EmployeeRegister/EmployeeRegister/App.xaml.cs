using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EmployeeRegister.Models;
using EmployeeRegister.Data;
using System.IO;

namespace EmployeeRegister
{
    public partial class App : Application
    {

        static SQLiteHelper db;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static SQLiteHelper SQLiteDB
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Employee.db3"));
                }
                return db;
            }
        }




        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
