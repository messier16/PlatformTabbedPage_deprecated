using System;
using System.ComponentModel;
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

		PlatformTabbedPage FormsTabbedPage => Element as PlatformTabbedPage;
		UIColor DefaultTintColor;
		UIColor DefaultBarBackgroundColor;

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);


            if (e.OldElement != null)
			{
				e.OldElement.PropertyChanged -= OnElementPropertyChanged;
			}
			if (e.NewElement != null)
			{
				e.NewElement.PropertyChanged += OnElementPropertyChanged;
			}

			DefaultTintColor = TabBar.TintColor;
			DefaultBarBackgroundColor = TabBar.BackgroundColor;
		}

		void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
				switch (e.PropertyName)
				{
					case nameof(PlatformTabbedPage.BarBackgroundColor):
					case nameof(PlatformTabbedPage.BarBackgroundApplyTo):
						SetBarBackgroundColor();
						SetTintedColor();
						break;
					case nameof(PlatformTabbedPage.SelectedColor):
						SetTintedColor();
						break;
					default:
						//base.OnElementProp⁄ertyChanged(sender, e);
						break;
				}
		}

		public override void ViewWillAppear(bool animated)
		{

			if (TabBar?.Items == null)
				return;
			SetTintedColor();
			SetBarBackgroundColor();

			if (FormsTabbedPage != null)
			{
				for (int i = 0; i < TabBar.Items.Length; i++)
				{
					var item = TabBar.Items[i];
					var icon = FormsTabbedPage.Children[i].Icon;

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

			base.ViewWillAppear(animated);
		}

		private void SetTintedColor()
		{
			if (FormsTabbedPage.SelectedColor != default(Color))
				TabBar.TintColor = FormsTabbedPage.SelectedColor.ToUIColor();
			else
			{
				TabBar.TintColor = DefaultTintColor;
			}
		}

		private void SetBarBackgroundColor()
		{
			if (FormsTabbedPage.BarBackgroundApplyTo.HasFlag(BarBackgroundApplyTo.iOS))
			{
				TabBar.BackgroundColor= FormsTabbedPage.BarBackgroundColor != default(Xamarin.Forms.Color)
					? FormsTabbedPage.BarBackgroundColor.ToUIColor()
					: DefaultBarBackgroundColor;
			}
			else
			{
				TabBar.BackgroundColor = DefaultBarBackgroundColor;
			}
		}
	}
}
