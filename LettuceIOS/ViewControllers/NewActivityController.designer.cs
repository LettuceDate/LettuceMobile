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
	[Register ("NewVenueController")]
	partial class NewActivityController
	{
		[Outlet]
		UIKit.UITextView ActivityDescription { get; set; }

		[Outlet]
		UIKit.UIButton CancelBtn { get; set; }

		[Outlet]
		UIKit.UIButton ChooseBtn { get; set; }

		[Outlet]
		UIKit.UIButton ChooseTypeBtn { get; set; }

		[Outlet]
		UIKit.UITableView ResultTable { get; set; }

		[Outlet]
		UIKit.UIButton SearchBtn { get; set; }

		[Outlet]
		UIKit.UITextField SearchField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ActivityDescription != null) {
				ActivityDescription.Dispose ();
				ActivityDescription = null;
			}

			if (SearchField != null) {
				SearchField.Dispose ();
				SearchField = null;
			}

			if (SearchBtn != null) {
				SearchBtn.Dispose ();
				SearchBtn = null;
			}

			if (ResultTable != null) {
				ResultTable.Dispose ();
				ResultTable = null;
			}

			if (CancelBtn != null) {
				CancelBtn.Dispose ();
				CancelBtn = null;
			}

			if (ChooseBtn != null) {
				ChooseBtn.Dispose ();
				ChooseBtn = null;
			}

			if (ChooseTypeBtn != null) {
				ChooseTypeBtn.Dispose ();
				ChooseTypeBtn = null;
			}
		}
	}
}
