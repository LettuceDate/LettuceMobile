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
            if (DateTimeLabel != null) {
                DateTimeLabel.Dispose ();
                DateTimeLabel = null;
            }

            if (DateTitleLabel != null) {
                DateTitleLabel.Dispose ();
                DateTitleLabel = null;
            }

            if (PinBtn != null) {
                PinBtn.Dispose ();
                PinBtn = null;
            }

            if (SelfieView != null) {
                SelfieView.Dispose ();
                SelfieView = null;
            }
        }
    }
}