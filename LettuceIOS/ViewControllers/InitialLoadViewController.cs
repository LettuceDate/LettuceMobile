using System;
using UIKit;
using Foundation;
using Facebook.LoginKit;
using Facebook.CoreKit;
using CoreGraphics;
using System.Collections.Generic;
using Lettuce.Core;

namespace Lettuce.IOS
{
	public partial class InitialLoadViewController : UIViewController
	{
		public InitialLoadViewController () : base ("InitialLoadViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			// check facebook connection
			this.NavigationController.NavigationBarHidden = true;

			if (AccessToken.CurrentAccessToken == null) {
				StatusLabel.Text = "TryingFacebook_string".Localize ();
				InitFacebookLogin ();
			} else {
				FinalizeLogin ();
			}
		}


		private void InitFacebookLogin()
		{
			FacebookLoginController fbLogin = new FacebookLoginController ();
			fbLogin.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;

			PresentViewController (fbLogin, true, () => {
				FinalizeLogin();
			});
		}

		private void FinalizeLogin()
		{
			if (AccessToken.CurrentAccessToken != null) {
				InvokeOnMainThread (() => {
					StatusLabel.Text = "TryingServer_string".Localize ();
				});
				var request = new GraphRequest ("/me?fields=name,id,birthday,first_name,gender,last_name,interested_in,location", null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
				request.Start ((connection, result, error) => {
					// Handle if something went wrong with the request
					if (error != null) {
						new UIAlertView ("Error...", error.Description, null, "Ok", null).Show ();
						return;
					}

					// Get your profile name
					var userInfo = result as NSDictionary;

					LettuceServer.Instance.FacebookLogin (userInfo ["id"].ToString (), AccessToken.CurrentAccessToken.TokenString, (theUser) => {
						InvokeOnMainThread (() => {
							NavController.PopToRootViewController (true);
						});
					});
				});
			} else {
				InvokeOnMainThread (() => {
					StatusLabel.Text = "FacebookConnectFailed_string".Localize();
				});
			}
		}

		protected NavController NavController { 
			get {
				return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController;
			} 
		}
	}
}


