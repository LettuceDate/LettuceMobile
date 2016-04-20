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
	[Register ("NotificationsViewController")]
	partial class NotificationsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel NotifyHeader { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView ResultList { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NSLayoutConstraint TopConstraint { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (NotifyHeader != null) {
				NotifyHeader.Dispose ();
				NotifyHeader = null;
			}
			if (ResultList != null) {
				ResultList.Dispose ();
				ResultList = null;
			}
			if (TopConstraint != null) {
				TopConstraint.Dispose ();
				TopConstraint = null;
			}
		}
	}
}
