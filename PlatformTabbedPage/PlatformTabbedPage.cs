using Xamarin.Forms;

namespace Messier16.Forms.Controls
{
	public class PlatformTabbedPage : TabbedPage
    {
        public static readonly BindableProperty HighlightedColorProperty =
            BindableProperty.Create(nameof(HighlightedColor), typeof(Color), typeof(PlatformTabbedPage), default(Color));

        public Color HighlightedColor
        {
            get { return (Color)GetValue(HighlightedColorProperty); }
            set { SetValue(HighlightedColorProperty, value); }
        }

        //public static readonly BindableProperty BarBackgroundColorProperty =
        //    BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(PlatformTabbedPage), default(Color));

        //public Color BackgroundColor
        //{
        //    get { return (Color)GetValue(HighlightedColorProperty); }
        //    set { SetValue(HighlightedColorProperty, value); }
        //}
    }
}
