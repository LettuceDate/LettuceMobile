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
	[Register ("MatchingDatesViewController")]
	partial class MatchingDatesViewController
	{
		[Outlet]
		UIKit.UIButton FilterBtn { get; set; }

		[Outlet]
		UIKit.UITableView ResultList { get; set; }

		[Outlet]
		UIKit.UILabel ResultTitle { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (FilterBtn != null) {
				FilterBtn.Dispose ();
				FilterBtn = null;
			}
			if (ResultList != null) {
				ResultList.Dispose ();
				ResultList = null;
			}
			if (ResultTitle != null) {
				ResultTitle.Dispose ();
				ResultTitle = null;
			}
		}
	}
}
