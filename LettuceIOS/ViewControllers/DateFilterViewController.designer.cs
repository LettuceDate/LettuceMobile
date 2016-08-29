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
    [Register ("DateFilterViewController")]
    partial class DateFilterViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ApplyBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CancelBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl DateTypeSelector { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl GenderSelector { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ApplyBtn != null) {
                ApplyBtn.Dispose ();
                ApplyBtn = null;
            }

            if (CancelBtn != null) {
                CancelBtn.Dispose ();
                CancelBtn = null;
            }

            if (DateTypeSelector != null) {
                DateTypeSelector.Dispose ();
                DateTypeSelector = null;
            }

            if (GenderSelector != null) {
                GenderSelector.Dispose ();
                GenderSelector = null;
            }
        }
    }
}