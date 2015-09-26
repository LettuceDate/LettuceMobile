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
	[Register ("ProposeDatesViewController")]
	partial class ProposeDatesViewController
	{
		[Outlet]
		UIKit.UITableView ActivityTableView { get; set; }

		[Outlet]
		UIKit.UIButton AddActivityBtn { get; set; }

		[Outlet]
		UIKit.UILabel DateLabel { get; set; }

		[Outlet]
		UIKit.UITextView DescriptionText { get; set; }

		[Outlet]
		UIKit.UITextField HeadlineText { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (DateLabel != null) {
				DateLabel.Dispose ();
				DateLabel = null;
			}

			if (HeadlineText != null) {
				HeadlineText.Dispose ();
				HeadlineText = null;
			}

			if (DescriptionText != null) {
				DescriptionText.Dispose ();
				DescriptionText = null;
			}

			if (AddActivityBtn != null) {
				AddActivityBtn.Dispose ();
				AddActivityBtn = null;
			}

			if (ActivityTableView != null) {
				ActivityTableView.Dispose ();
				ActivityTableView = null;
			}
		}
	}
}
