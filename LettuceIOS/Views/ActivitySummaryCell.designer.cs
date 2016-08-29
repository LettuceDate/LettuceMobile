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
            if (ActivityDescription != null) {
                ActivityDescription.Dispose ();
                ActivityDescription = null;
            }

            if (ActivityDurationLabel != null) {
                ActivityDurationLabel.Dispose ();
                ActivityDurationLabel = null;
            }

            if (ActivityTypeImage != null) {
                ActivityTypeImage.Dispose ();
                ActivityTypeImage = null;
            }

            if (ActivityTypeLabel != null) {
                ActivityTypeLabel.Dispose ();
                ActivityTypeLabel = null;
            }

            if (VenueImage != null) {
                VenueImage.Dispose ();
                VenueImage = null;
            }

            if (VenueNameLabel != null) {
                VenueNameLabel.Dispose ();
                VenueNameLabel = null;
            }
        }
    }
}