using System;
using Messier16.Forms.Controls;
using Xamarin.Forms;

namespace TabbedPage
{
	public class HomeTabbedPage : PlatformTabbedPage
	{
		public HomeTabbedPage()
		{
			HighlightedColor = Color.FromHex("3C8A3F");
			Children.Add(new BasicContentPage("Feed") { Icon = "feed" });
			Children.Add(new BasicContentPage("YouTube") { Icon = "youtube" });
			Children.Add(new BasicContentPage("Twitter") { Icon = "twitter" });
			Children.Add(new BasicContentPage("Info") { Icon = "info" });
		}
	}
}
