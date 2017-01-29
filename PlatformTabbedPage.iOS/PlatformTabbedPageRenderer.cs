using System;
using Messier16.Forms.Controls;
using Messier16.Forms.Controls.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PlatformTabbedPage), typeof(PlatformTabbedPageRenderer))]
namespace Messier16.Forms.Controls.iOS
{
	public class PlatformTabbedPageRenderer : TabbedRenderer
	{
		public static void Init()
		{
			var test = DateTime.UtcNow;
		}

		public override void ViewWillAppear(bool animated)
		{

			if (TabBar?.Items == null)
				return;

			var formsTabbedPage = Element as PlatformTabbedPage;

			if (formsTabbedPage.SelectedColor != default(Color))
				TabBar.TintColor = formsTabbedPage.SelectedColor.ToUIColor();

			if (formsTabbedPage != null)
			{
				for (int i = 0; i < TabBar.Items.Length; i++)
				{
					SetActiveImage(TabBar.Items[i], formsTabbedPage.Children[i].Icon);
				}
			}

			base.ViewWillAppear(animated);
		}

		void SetActiveImage(UITabBarItem item, string icon)
		{
			if (item == null)
				return;
			try
			{
				icon = icon + "_active";
				if (item?.SelectedImage?.AccessibilityIdentifier == icon)
					return;
				item.SelectedImage = UIImage.FromBundle(icon);
				item.SelectedImage.AccessibilityIdentifier = icon;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unable to set selected icon: " + ex);
			}
		}
	}
}
