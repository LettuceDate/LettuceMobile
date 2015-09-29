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
	[Register ("MatchingDatesCell")]
	partial class MatchingDatesCell
	{
		[Outlet]
		UIKit.UILabel DateTimeLabel { get; set; }

		[Outlet]
		UIKit.UILabel DateTitleLabel { get; set; }

		[Outlet]
		UIKit.UIButton PinBtn { get; set; }

		[Outlet]
		UIKit.UIImageView SelfieView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SelfieView != null) {
				SelfieView.Dispose ();
				SelfieView = null;
			}

			if (PinBtn != null) {
				PinBtn.Dispose ();
				PinBtn = null;
			}

			if (DateTimeLabel != null) {
				DateTimeLabel.Dispose ();
				DateTimeLabel = null;
			}

			if (DateTitleLabel != null) {
				DateTitleLabel.Dispose ();
				DateTitleLabel = null;
			}
		}
	}
}
