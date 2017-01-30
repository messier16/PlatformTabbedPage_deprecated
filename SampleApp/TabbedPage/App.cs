using System;

using Xamarin.Forms;

namespace TabbedPage
{
    public class App : Application
    {
        public static Color[] SelectedColors =
        {
            Color.FromHex("3C8A3F"),
            Color.Green,
            Color.Fuchsia,
            Color.Orange,
            Color.Teal,
            Color.White,
			Color.Default
        };

        public static Color[] BarBackgroundColors =
        {
            Color.FromHex("eeeef2"),
			Color.Black,
            Color.FromHex("C8C8C8"),
            Color.FromHex("F0F0F0"),
            Color.FromHex("909090"),
            Color.FromHex("A8A8A8"),
			Color.Default
        };

        public static HomeTabbedPage HomeTabbedPage;

        public App()
        {
            HomeTabbedPage = new HomeTabbedPage();
            MainPage = new NavigationPage(HomeTabbedPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
