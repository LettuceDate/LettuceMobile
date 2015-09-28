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
	[Register ("ActivitySummaryCell")]
	partial class ActivitySummaryCell
	{
		[Outlet]
		UIKit.UILabel ActivityDescription { get; set; }

		[Outlet]
		UIKit.UILabel ActivityDurationLabel { get; set; }

		[Outlet]
		UIKit.UIImageView ActivityTypeImage { get; set; }

		[Outlet]
		UIKit.UILabel ActivityTypeLabel { get; set; }

		[Outlet]
		UIKit.UIImageView VenueImage { get; set; }

		[Outlet]
		UIKit.UILabel VenueNameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ActivityTypeImage != null) {
				ActivityTypeImage.Dispose ();
				ActivityTypeImage = null;
			}

			if (VenueNameLabel != null) {
				VenueNameLabel.Dispose ();
				VenueNameLabel = null;
			}

			if (ActivityTypeLabel != null) {
				ActivityTypeLabel.Dispose ();
				ActivityTypeLabel = null;
			}

			if (ActivityDurationLabel != null) {
				ActivityDurationLabel.Dispose ();
				ActivityDurationLabel = null;
			}

			if (ActivityDescription != null) {
				ActivityDescription.Dispose ();
				ActivityDescription = null;
			}

			if (VenueImage != null) {
				VenueImage.Dispose ();
				VenueImage = null;
			}
		}
	}
}
