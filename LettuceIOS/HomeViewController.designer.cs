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
	[Register ("HomeViewController")]
	partial class HomeViewController
	{
		[Outlet]
		UIKit.UIButton ActiveDateBtn { get; set; }

		[Outlet]
		UIKit.UILabel ActiveDateString { get; set; }

		[Outlet]
		UIKit.UIButton BrowseDateBtn { get; set; }

		[Outlet]
		UIKit.UILabel DateMatchString { get; set; }

		[Outlet]
		UIKit.UIButton InterestedDateBtn { get; set; }

		[Outlet]
		UIKit.UILabel InterestedDateString { get; set; }

		[Outlet]
		UIKit.UIView LightBox { get; set; }

		[Outlet]
		UIKit.UIView LoginView { get; set; }

		[Outlet]
		UIKit.UIButton ProposeDateBtn { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LightBox != null) {
				LightBox.Dispose ();
				LightBox = null;
			}

			if (ActiveDateBtn != null) {
				ActiveDateBtn.Dispose ();
				ActiveDateBtn = null;
			}

			if (ActiveDateString != null) {
				ActiveDateString.Dispose ();
				ActiveDateString = null;
			}

			if (BrowseDateBtn != null) {
				BrowseDateBtn.Dispose ();
				BrowseDateBtn = null;
			}

			if (DateMatchString != null) {
				DateMatchString.Dispose ();
				DateMatchString = null;
			}

			if (InterestedDateBtn != null) {
				InterestedDateBtn.Dispose ();
				InterestedDateBtn = null;
			}

			if (InterestedDateString != null) {
				InterestedDateString.Dispose ();
				InterestedDateString = null;
			}

			if (LoginView != null) {
				LoginView.Dispose ();
				LoginView = null;
			}

			if (ProposeDateBtn != null) {
				ProposeDateBtn.Dispose ();
				ProposeDateBtn = null;
			}
		}
	}
}
