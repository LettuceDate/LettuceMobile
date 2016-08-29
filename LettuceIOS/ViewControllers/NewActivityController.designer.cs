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
    [Register ("NewVenueController")]
    partial class NewActivityController
    {
        [Outlet]
        UIKit.UITextView ActivityDescription { get; set; }


        [Outlet]
        UIKit.UIButton CancelBtn { get; set; }


        [Outlet]
        UIKit.UIButton ChooseBtn { get; set; }


        [Outlet]
        UIKit.UIButton ChooseTypeBtn { get; set; }


        [Outlet]
        UIKit.UITableView ResultTable { get; set; }


        [Outlet]
        UIKit.UIButton SearchBtn { get; set; }


        [Outlet]
        UIKit.UITextField SearchField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ActivityDescription != null) {
                ActivityDescription.Dispose ();
                ActivityDescription = null;
            }

            if (CancelBtn != null) {
                CancelBtn.Dispose ();
                CancelBtn = null;
            }

            if (ChooseBtn != null) {
                ChooseBtn.Dispose ();
                ChooseBtn = null;
            }

            if (ChooseTypeBtn != null) {
                ChooseTypeBtn.Dispose ();
                ChooseTypeBtn = null;
            }

            if (ResultTable != null) {
                ResultTable.Dispose ();
                ResultTable = null;
            }

            if (SearchBtn != null) {
                SearchBtn.Dispose ();
                SearchBtn = null;
            }

            if (SearchField != null) {
                SearchField.Dispose ();
                SearchField = null;
            }
        }
    }
}