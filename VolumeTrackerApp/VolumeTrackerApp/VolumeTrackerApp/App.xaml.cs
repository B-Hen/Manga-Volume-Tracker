using System;
using System.Collections.ObjectModel;
using System.IO;
using VolumeTrackerApp.Database;
using VolumeTrackerApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VolumeTrackerApp
{
    public partial class App : Application
    {
        private static MangaDatabase mangaDatabase;

        public static MangaDatabase MangaDatabase
        {
            get
            {
                if(mangaDatabase == null)
                {
                    mangaDatabase = new MangaDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MangaData.db3"));
                }

                return mangaDatabase;
            }
        }

        public App()
        {
            InitializeComponent();
            mangaDatabase = MangaDatabase;
            MainPage = new NavigationPage(new MainPage());
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
