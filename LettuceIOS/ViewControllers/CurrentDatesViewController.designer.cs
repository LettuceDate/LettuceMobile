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
    [Register ("CurrentDatesViewController")]
    partial class CurrentDatesViewController
    {
        [Outlet]
        UIKit.UITableView CurrentDatesTableView { get; set; }


        [Outlet]
        UIKit.UIButton FilterBtn { get; set; }


        [Outlet]
        UIKit.UILabel HeaderLabel { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint TopConstraint { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CurrentDatesTableView != null) {
                CurrentDatesTableView.Dispose ();
                CurrentDatesTableView = null;
            }

            if (TopConstraint != null) {
                TopConstraint.Dispose ();
                TopConstraint = null;
            }
        }
    }
}