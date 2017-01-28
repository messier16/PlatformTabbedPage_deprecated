using System;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using Messier16.Forms.Controls.Droid;
using Messier16.Forms.Controls;

[assembly: ExportRenderer(typeof(PlatformTabbedPage), typeof(PlatformTabbedPageRenderer))]
namespace Messier16.Forms.Controls.Droid
{
	public class PlatformTabbedPageRenderer : TabbedPageRenderer
	{
		public static void Init()
		{
			var test = DateTime.UtcNow;
		}

		Android.Graphics.Color SelectedColor = Android.Graphics.Color.Black;
		Android.Graphics.Color UnselectedColor = Android.Graphics.Color.LightGray;// App.PrimaryColor.ToAndroid();

		protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
		{
			base.OnElementChanged(e);

			var formsTabbedPage = Element as PlatformTabbedPage;

			if (formsTabbedPage.HighlightedColor != default(Xamarin.Forms.Color))
				SelectedColor = formsTabbedPage.HighlightedColor.ToAndroid();
		}



		protected override void OnWindowVisibilityChanged(Android.Views.ViewStates visibility)
		{
			base.OnWindowVisibilityChanged(visibility);
			SetupTabColors();
		}

		ViewPager viewPager;
		TabLayout tabLayout;
		void SetupTabColors()
		{
			for (int i = 0; i < ChildCount; i++)
			{
				var v = GetChildAt(i);
				if (v is ViewPager)
					viewPager = (ViewPager)v;
				else if (v is TabLayout)
					tabLayout = (TabLayout)v;
			}
			if (viewPager != null && tabLayout != null)
			{
				tabLayout.SetTabTextColors(UnselectedColor, SelectedColor);
				tabLayout.TabSelected += TabLayout_TabSelected;
				tabLayout.TabUnselected += TabLayout_TabUnselected;
				for (int i = 0; i < tabLayout.TabCount; i++)
				{
					var tab = tabLayout.GetTabAt(i);
					tab.Icon.SetColorFilter(UnselectedColor, PorterDuff.Mode.SrcIn);
				}

				// Select first tab
				if (tabLayout.TabCount > 0)
				{
					tabLayout.GetTabAt(0).Icon.SetColorFilter(SelectedColor, PorterDuff.Mode.SrcIn);
					tabLayout.GetTabAt(0).Select();
				}
			}


		}

		private void TabLayout_TabUnselected(object sender, TabLayout.TabUnselectedEventArgs e)
		{

			var tab = e.Tab;
			tab.Icon.SetColorFilter(UnselectedColor, PorterDuff.Mode.SrcIn);
		}

		private void TabLayout_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
		{
			var tab = e.Tab;
			viewPager.CurrentItem = tab.Position;
			tab.Icon.SetColorFilter(SelectedColor, PorterDuff.Mode.SrcIn);
		}
	}
}
