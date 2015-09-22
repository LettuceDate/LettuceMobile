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
