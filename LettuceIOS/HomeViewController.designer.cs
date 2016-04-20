// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Lettuce.IOS
{
	[Register ("HomeViewController")]
	partial class HomeViewController
	{
		[Outlet]
		UIKit.UIView LightBox { get; set; }

		[Outlet]
		UIKit.UIView LoginView { get; set; }

		[Outlet]
		UIKit.UIButton ProposeDateBtn { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (LightBox != null) {
				LightBox.Dispose ();
				LightBox = null;
			}
			if (LoginView != null) {
				LoginView.Dispose ();
				LoginView = null;
			}
			if (ProposeDateBtn != null) {
				ProposeDateBtn.Dispose ();
				ProposeDateBtn = null;
			}
		}
	}
}
