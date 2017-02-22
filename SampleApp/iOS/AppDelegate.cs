using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Messier16.Forms.Controls.iOS;
using UIKit;

namespace TabbedPage.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			PlatformTabbedPageRenderer.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}
