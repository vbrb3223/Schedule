using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule
{
    public partial class App : Application
    {
        public static string FilePath;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public App(string filePath)
        {
            InitializeComponent();

            FilePath = filePath;

            MainPage = new MainPage();
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
