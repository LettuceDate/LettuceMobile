// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

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
			if (ResultTitle != null) {
				ResultTitle.Dispose ();
				ResultTitle = null;
			}

			if (FilterBtn != null) {
				FilterBtn.Dispose ();
				FilterBtn = null;
			}

			if (ResultList != null) {
				ResultList.Dispose ();
				ResultList = null;
			}
		}
	}
}
