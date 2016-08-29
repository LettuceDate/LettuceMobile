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
    [Register ("DateDetailViewController")]
    partial class DateDetailViewController
    {
        [Outlet]
        UIKit.UITableView ActivityList { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint ActivityListHeight { get; set; }


        [Outlet]
        UIKit.UILabel DateDescription { get; set; }


        [Outlet]
        UIKit.UILabel DatePayLabel { get; set; }


        [Outlet]
        UIKit.UILabel DateTitleLabel { get; set; }


        [Outlet]
        UIKit.UILabel HeadlineLabel { get; set; }


        [Outlet]
        UIKit.UILabel NicknameLabel { get; set; }


        [Outlet]
        UIKit.UIScrollView ScrollingView { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint SelfieHeight { get; set; }


        [Outlet]
        UIKit.UIImageView SelfieView { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint TopConstraint { get; set; }


        [Outlet]
        UIKit.UILabel UserAgeLabel { get; set; }


        [Outlet]
        UIKit.UILabel UserDescription { get; set; }


        [Outlet]
        UIKit.UILabel UserLocLabel { get; set; }


        [Outlet]
        UIKit.UILabel UserNameLabel { get; set; }


        [Outlet]
        UIKit.UIImageView UserProfileImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ActivityList != null) {
                ActivityList.Dispose ();
                ActivityList = null;
            }

            if (ActivityListHeight != null) {
                ActivityListHeight.Dispose ();
                ActivityListHeight = null;
            }

            if (DateDescription != null) {
                DateDescription.Dispose ();
                DateDescription = null;
            }

            if (DatePayLabel != null) {
                DatePayLabel.Dispose ();
                DatePayLabel = null;
            }

            if (DateTitleLabel != null) {
                DateTitleLabel.Dispose ();
                DateTitleLabel = null;
            }

            if (NicknameLabel != null) {
                NicknameLabel.Dispose ();
                NicknameLabel = null;
            }

            if (ScrollingView != null) {
                ScrollingView.Dispose ();
                ScrollingView = null;
            }

            if (SelfieHeight != null) {
                SelfieHeight.Dispose ();
                SelfieHeight = null;
            }

            if (SelfieView != null) {
                SelfieView.Dispose ();
                SelfieView = null;
            }

            if (TopConstraint != null) {
                TopConstraint.Dispose ();
                TopConstraint = null;
            }

            if (UserAgeLabel != null) {
                UserAgeLabel.Dispose ();
                UserAgeLabel = null;
            }

            if (UserDescription != null) {
                UserDescription.Dispose ();
                UserDescription = null;
            }

            if (UserLocLabel != null) {
                UserLocLabel.Dispose ();
                UserLocLabel = null;
            }

            if (UserNameLabel != null) {
                UserNameLabel.Dispose ();
                UserNameLabel = null;
            }

            if (UserProfileImage != null) {
                UserProfileImage.Dispose ();
                UserProfileImage = null;
            }
        }
    }
}