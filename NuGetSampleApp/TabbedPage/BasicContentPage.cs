using System;

using Xamarin.Forms;

namespace TabbedPage
{
	public class BasicContentPage : ContentPage
	{
		public BasicContentPage(string title)
		{
			Title = title;
			Content = new StackLayout
			{
				Children = {
					new Label { 
						Text = Title, 
						FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) 
					}
				}
			};
		}
	}
}

