using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using Messier16.Forms.Controls.Droid;
using Messier16.Forms.Controls;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(PlatformTabbedPage), typeof(PlatformTabbedPageRenderer))]
namespace Messier16.Forms.Controls.Droid
{
    public class PlatformTabbedPageRenderer : TabbedPageRenderer
    {
        public static void Init()
        {
            var test = DateTime.UtcNow;
        }

        private PlatformTabbedPage FormsTabbedPage => Element as PlatformTabbedPage;
        Android.Graphics.Color _selectedColor = Android.Graphics.Color.Black;
        private static readonly Android.Graphics.Color DefaultUnselectedColor = Xamarin.Forms.Color.Gray.Darken().ToAndroid();
		private static Android.Graphics.Color BarBackgroundDefault;
        private Android.Graphics.Color _unselectedColor = DefaultUnselectedColor;

		ViewPager _viewPager;
		TabLayout _tabLayout;

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
			
            base.OnElementChanged(e);

            // Get tabs
            for (int i = 0; i < ChildCount; i++)
            {
                var v = GetChildAt(i);
                if (v is ViewPager)
                    _viewPager = (ViewPager)v;
                else if (v is TabLayout)
                    _tabLayout = (TabLayout)v;
            }


            if (e.OldElement != null)
            {
                _tabLayout.TabSelected -= TabLayout_TabSelected;
                _tabLayout.TabUnselected -= TabLayout_TabUnselected;
            }

            if (e.NewElement != null)
            {
				BarBackgroundDefault = (_tabLayout.Background as ColorDrawable)?.Color ?? Android.Graphics.Color.Blue;
                SetSelectedColor();
                SetBarBackgroundColor();
                _tabLayout.TabSelected += TabLayout_TabSelected;
                _tabLayout.TabUnselected += TabLayout_TabUnselected;

            	SetupTabColors();
				SelectTab(0);
            }

        }

		void SelectTab(int position)
		{
			if (_tabLayout.TabCount > position)
			{
				_tabLayout.GetTabAt(position).Icon?.SetColorFilter(_selectedColor, PorterDuff.Mode.SrcIn);
				_tabLayout.GetTabAt(position).Select();
			}
			else 
			{
				throw new IndexOutOfRangeException();
			}
		}


        void SetupTabColors()
        {
            _tabLayout.SetSelectedTabIndicatorColor(_selectedColor);
            _tabLayout.SetTabTextColors(_unselectedColor, _selectedColor);
            for (int i = 0; i < _tabLayout.TabCount; i++)
            {
                var tab = _tabLayout.GetTabAt(i);
                tab.Icon?.SetColorFilter(_unselectedColor, PorterDuff.Mode.SrcIn);
            }
        }

        private void TabLayout_TabUnselected(object sender, TabLayout.TabUnselectedEventArgs e)
        {
            var tab = e.Tab;
            tab.Icon?.SetColorFilter(_unselectedColor, PorterDuff.Mode.SrcIn);
        }

        private void TabLayout_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
        {
            var tab = e.Tab;
            _viewPager.CurrentItem = tab.Position;
            tab.Icon?.SetColorFilter(_selectedColor, PorterDuff.Mode.SrcIn);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            int lastPosition = _tabLayout.SelectedTabPosition;
            switch (e.PropertyName)
            {
                case nameof(PlatformTabbedPage.BarBackgroundColor):
                case nameof(PlatformTabbedPage.BarBackgroundApplyTo):
                    SetBarBackgroundColor();
                    SetupTabColors();
					SelectTab(lastPosition);
                    break;
                case nameof(PlatformTabbedPage.SelectedColor):
                    SetSelectedColor();
                    SetupTabColors();
					SelectTab(lastPosition);
                    break;
                default:
                    base.OnElementPropertyChanged(sender, e);
                    break;
            }
        }

        private void SetSelectedColor()
        {

            if (FormsTabbedPage.SelectedColor != default(Xamarin.Forms.Color))
                _selectedColor = FormsTabbedPage.SelectedColor.ToAndroid();
        }

        private void SetBarBackgroundColor()
        {
            if (FormsTabbedPage.BarBackgroundApplyTo.HasFlag(BarBackgroundApplyTo.Android))
            {
				_tabLayout.SetBackgroundColor(FormsTabbedPage.BarBackgroundColor.ToAndroid());
                _unselectedColor = FormsTabbedPage.BarBackgroundColor != default(Xamarin.Forms.Color)
                    ? FormsTabbedPage.BarBackgroundColor.Darken().ToAndroid()
                    : DefaultUnselectedColor;
            }
            else
            {
				_tabLayout.SetBackgroundColor(BarBackgroundDefault);
                _unselectedColor = DefaultUnselectedColor;
            }
        }
    }
}
