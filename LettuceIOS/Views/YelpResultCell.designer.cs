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

            if (VenueName != null) {
                VenueName.Dispose ();
                VenueName = null;
            }

            if (VenueRatingImage != null) {
                VenueRatingImage.Dispose ();
                VenueRatingImage = null;
            }
        }
    }
}