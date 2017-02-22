using System;
using Messier16.Forms.Controls;
using Xamarin.Forms;

namespace TabbedPage
{
	public class HomeTabbedPage : PlatformTabbedPage
    {
        public HomeTabbedPage()
        {
            BarBackgroundColor = App.BarBackgroundColors[3];
            SelectedColor = App.SelectedColors[0];
			BarBackgroundApplyTo = BarBackgroundApplyTo.Android;

			var page = new ConfigurationPage { Icon = "feed" };

            Children.Add(page);
            Children.Add(new BasicContentPage("YouTube") { Icon = "youtube" });
            Children.Add(new BasicContentPage("Twitter") { Icon = "twitter" });
            Children.Add(new BasicContentPage("Info") { Icon = "info" });
        }
    }
}
