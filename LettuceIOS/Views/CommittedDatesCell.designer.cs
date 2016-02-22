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
	[Register ("CommittedDatesCell")]
	partial class CommittedDatesCell
	{
		[Outlet]
		UIKit.UIImageView DateIcon { get; set; }

		[Outlet]
		UIKit.UILabel DateTitle { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (DateIcon != null) {
				DateIcon.Dispose ();
				DateIcon = null;
			}
			if (DateTitle != null) {
				DateTitle.Dispose ();
				DateTitle = null;
			}
		}
	}
}
