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
	[Register ("YelpResultCell")]
	partial class YelpResultCell
	{
		[Outlet]
		UIKit.UIImageView VenueImage { get; set; }

		[Outlet]
		UIKit.UILabel VenueName { get; set; }

		[Outlet]
		UIKit.UIImageView VenueRatingImage { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (VenueImage != null) {
				VenueImage.Dispose ();
				VenueImage = null;
			}

			if (VenueRatingImage != null) {
				VenueRatingImage.Dispose ();
				VenueRatingImage = null;
			}

			if (VenueName != null) {
				VenueName.Dispose ();
				VenueName = null;
			}
		}
	}
}
