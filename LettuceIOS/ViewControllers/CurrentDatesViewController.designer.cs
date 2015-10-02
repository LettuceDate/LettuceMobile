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
	[Register ("CurrentDatesViewController")]
	partial class CurrentDatesViewController
	{
		[Outlet]
		UIKit.UITableView CurrentDatesTableView { get; set; }

		[Outlet]
		UIKit.UIButton FilterBtn { get; set; }

		[Outlet]
		UIKit.UILabel HeaderLabel { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint TopConstraint { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TopConstraint != null) {
				TopConstraint.Dispose ();
				TopConstraint = null;
			}

			if (CurrentDatesTableView != null) {
				CurrentDatesTableView.Dispose ();
				CurrentDatesTableView = null;
			}

			if (FilterBtn != null) {
				FilterBtn.Dispose ();
				FilterBtn = null;
			}

			if (HeaderLabel != null) {
				HeaderLabel.Dispose ();
				HeaderLabel = null;
			}
		}
	}
}
