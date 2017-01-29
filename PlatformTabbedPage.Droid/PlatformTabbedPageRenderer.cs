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
        private Android.Graphics.Color _unselectedColor = DefaultUnselectedColor;// App.PrimaryColor.ToAndroid();

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
                SetSelectedColor();
                SetBarBackgroundColor();
                _tabLayout.TabSelected += TabLayout_TabSelected;
                _tabLayout.TabUnselected += TabLayout_TabUnselected;
            }

        }

        protected override void OnWindowVisibilityChanged(Android.Views.ViewStates visibility)
        {
            base.OnWindowVisibilityChanged(visibility);


            SetupTabColors();

            // Select first tab
            if (_tabLayout.TabCount > 0)
            {
                _tabLayout.GetTabAt(0)?.Icon.SetColorFilter(_selectedColor, PorterDuff.Mode.SrcIn);
                _tabLayout.GetTabAt(0)?.Select();
            }
        }

        ViewPager _viewPager;
        TabLayout _tabLayout;
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
                    _tabLayout.GetTabAt(lastPosition).Icon?.SetColorFilter(_selectedColor, PorterDuff.Mode.SrcIn);
                    _tabLayout.GetTabAt(lastPosition).Select();
                    base.OnElementPropertyChanged(sender, new PropertyChangedEventArgs(nameof(PlatformTabbedPage.BarBackgroundColor)));
                    break;
                case nameof(PlatformTabbedPage.SelectedColor):
                    SetSelectedColor();
                    SetupTabColors();
                    _tabLayout.GetTabAt(lastPosition).Icon?.SetColorFilter(_selectedColor, PorterDuff.Mode.SrcIn);
                    _tabLayout.GetTabAt(lastPosition).Select();
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
                _unselectedColor = FormsTabbedPage.BarBackgroundColor != default(Xamarin.Forms.Color)
                    ? FormsTabbedPage.BarBackgroundColor.Darken().ToAndroid()
                    : DefaultUnselectedColor;
            }
            else
            {
                _unselectedColor = DefaultUnselectedColor;
            }
        }
    }
}
