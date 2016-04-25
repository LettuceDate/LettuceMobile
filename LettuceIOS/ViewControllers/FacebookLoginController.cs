using System;
using UIKit;
using Foundation;
using Facebook.LoginKit;
using Facebook.CoreKit;
using CoreGraphics;
using System.Collections.Generic;

namespace Lettuce.IOS
{
	public partial class FacebookLoginController : UIViewController
	{
		// facebook login stuff
		List<string> readPermissions = new List<string> { "public_profile", "user_birthday", "user_location" };
		LoginButton loginButton;
		ProfilePictureView pictureView;
		UILabel nameLabel;


		public FacebookLoginController () : base ("FacebookLoginController", null)
		{
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			LoginView.Layer.CornerRadius = 16;
			LoginView.Layer.ShadowRadius = 5;
			LoginView.Layer.ShadowOffset = new CGSize (5, 5);
			LoginView.Layer.ShadowOpacity = .5f;
			LoginView.Layer.ShadowColor = new CGColor (0f, .5f);


			Profile.Notifications.ObserveDidChange ((sender, e) => {

				if (e.NewProfile == null)
					return;

				LoginView.Hidden = true;
				nameLabel.Text = e.NewProfile.Name;
			});

			CGRect viewBounds = UIScreen.MainScreen.Bounds; 
			viewBounds.Width -= 64;


			// Set the Read and Publish permissions you want to get
			nfloat leftEdge = (viewBounds.Width - 220) /2;
			loginButton = new LoginButton (new CGRect (leftEdge, 60, 220, 46)) {
				LoginBehavior = LoginBehavior.Native,
				ReadPermissions = readPermissions.ToArray ()
			};

			// Handle actions once the user is logged in
			loginButton.Completed += (sender, e) => {
				if (e.Error != null) {
					// Handle if there was an error
				}

				if (e.Result.IsCancelled) {
					// Handle if the user cancelled the login request
				}

				// Handle your successful login
				FinalizeLogin();
			};

			// Handle actions once the user is logged out
			loginButton.LoggedOut += (sender, e) => {
				// Handle your logout
				nameLabel.Text = "";
			};

			// The user image profile is set automatically once is logged in
			pictureView = new ProfilePictureView (new CGRect (leftEdge, 140, 220, 220));

			// Create the label that will hold user's facebook name
			nameLabel = new UILabel (new CGRect (20, 360, viewBounds.Width - 40, 21)) {
				TextAlignment = UITextAlignment.Center,
				BackgroundColor = UIColor.Clear
			};

			// If you have been logged into the app before, ask for the your profile name
			if (AccessToken.CurrentAccessToken != null) {
				var request = new GraphRequest ("/me?fields=name", null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
				request.Start ((connection, result, error) => {
					// Handle if something went wrong with the request
					if (error != null) {
						new UIAlertView ("Error...", error.Description, null, "Ok", null).Show ();
						return;
					}

					// Get your profile name
					var userInfo = result as NSDictionary;
					nameLabel.Text = userInfo ["name"].ToString ();
					var controller = AppDelegate.ProfileController;
					this.NavigationController.PresentViewController(controller, true, null);

				});
			}

			// Add views to main view
			LoginView.AddSubview (loginButton);
			LoginView.AddSubview (pictureView);
			LoginView.AddSubview (nameLabel);

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		private void FinalizeLogin()
		{
			DismissViewController (true, null);
		}
	}
}


