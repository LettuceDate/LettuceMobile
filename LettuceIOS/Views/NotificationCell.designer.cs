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
	[Register ("NotificationCell")]
	partial class NotificationCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView NotifImage { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel NotifLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (NotifImage != null) {
				NotifImage.Dispose ();
				NotifImage = null;
			}
			if (NotifLabel != null) {
				NotifLabel.Dispose ();
				NotifLabel = null;
			}
		}
	}
}
