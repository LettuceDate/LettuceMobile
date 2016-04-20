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
	[Register ("SideMenuController")]
	partial class SideMenuController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton AboutBtn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton HomeBtn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView MenuImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton ProfileBtn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton SettingsBtn { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (AboutBtn != null) {
				AboutBtn.Dispose ();
				AboutBtn = null;
			}
			if (HomeBtn != null) {
				HomeBtn.Dispose ();
				HomeBtn = null;
			}
			if (MenuImage != null) {
				MenuImage.Dispose ();
				MenuImage = null;
			}
			if (ProfileBtn != null) {
				ProfileBtn.Dispose ();
				ProfileBtn = null;
			}
			if (SettingsBtn != null) {
				SettingsBtn.Dispose ();
				SettingsBtn = null;
			}
		}
	}
}
