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
	}
}
