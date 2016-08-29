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
    [Register ("FacebookLoginController")]
    partial class FacebookLoginController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView LoginView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LoginView != null) {
                LoginView.Dispose ();
                LoginView = null;
            }
        }
    }
}