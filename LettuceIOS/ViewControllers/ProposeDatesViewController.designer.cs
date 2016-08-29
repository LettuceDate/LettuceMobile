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
    [Register ("ProposeDatesViewController")]
    partial class ProposeDatesViewController
    {
        [Outlet]
        UIKit.UITableView ActivityTableView { get; set; }


        [Outlet]
        UIKit.UIButton AddActivityBtn { get; set; }


        [Outlet]
        UIKit.UIButton CancelDateBtn { get; set; }


        [Outlet]
        UIKit.UIButton CreateDateBtn { get; set; }


        [Outlet]
        UIKit.UIButton DateStartBtn { get; set; }


        [Outlet]
        UIKit.UITextView DescriptionText { get; set; }


        [Outlet]
        UIKit.UITextField HeadlineText { get; set; }


        [Outlet]
        UIKit.UISegmentedControl PayTypeControl { get; set; }


        [Outlet]
        UIKit.UIScrollView Scroller { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ActivityTableView != null) {
                ActivityTableView.Dispose ();
                ActivityTableView = null;
            }

            if (AddActivityBtn != null) {
                AddActivityBtn.Dispose ();
                AddActivityBtn = null;
            }

            if (CancelDateBtn != null) {
                CancelDateBtn.Dispose ();
                CancelDateBtn = null;
            }

            if (CreateDateBtn != null) {
                CreateDateBtn.Dispose ();
                CreateDateBtn = null;
            }

            if (DateStartBtn != null) {
                DateStartBtn.Dispose ();
                DateStartBtn = null;
            }

            if (DescriptionText != null) {
                DescriptionText.Dispose ();
                DescriptionText = null;
            }

            if (HeadlineText != null) {
                HeadlineText.Dispose ();
                HeadlineText = null;
            }

            if (PayTypeControl != null) {
                PayTypeControl.Dispose ();
                PayTypeControl = null;
            }

            if (Scroller != null) {
                Scroller.Dispose ();
                Scroller = null;
            }
        }
    }
}