
using System;

using Foundation;
using UIKit;
using JVMenuPopover;
using Facebook.LoginKit;
using Facebook.CoreKit;
using CoreGraphics;
using System.Collections.Generic;
using Lettuce.Core;

namespace Lettuce.IOS
{
	public partial class HomeViewController : JVMenuViewController
	{
		List<string> readPermissions = new List<string> { "public_profile", "user_birthday" };
		LoginButton loginButton;
		ProfilePictureView pictureView;
		UILabel nameLabel;

		public HomeViewController () : base ()
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}



		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// If was send true to Profile.EnableUpdatesOnAccessTokenChange method
			// this notification will be called after the user is logged in and
			// after the AccessToken is gotten

			if (AccessToken.CurrentAccessToken == null) {
				InitFacebookLogin ();
			} else {
				FinalizeLogin ();
			}

			// set up events
			ActiveDateBtn.TouchUpInside += (object sender, EventArgs e) => 
			{
				ShowDateView(1);
			};

			BrowseDateBtn.TouchUpInside += (object sender, EventArgs e) => 
			{
				ShowDateView(2);
			};

			InterestedDateBtn.TouchUpInside += (object sender, EventArgs e) => 
			{
				ShowDateView(4);
			};

			ProposeDateBtn.TouchUpInside += (object sender, EventArgs e) => 
			{
				ProposeDatesViewController proposeController = new ProposeDatesViewController ();
				if (proposeController != null) {

					this.NavigationController.PresentModalViewController (proposeController, true);
				}
			};


		}

		private void FinalizeLogin()
		{
			if (AccessToken.CurrentAccessToken != null) {
				var request = new GraphRequest ("/me?fields=name,id,birthday,first_name,gender,last_name,interested_in", null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
				request.Start ((connection, result, error) => {
					// Handle if something went wrong with the request
					if (error != null) {
						new UIAlertView ("Error...", error.Description, null, "Ok", null).Show ();
						return;
					}

					// Get your profile name
					var userInfo = result as NSDictionary;

					LettuceServer.Instance.FacebookLogin (userInfo["id"].ToString(), AccessToken.CurrentAccessToken.TokenString, (theUser) => {
						InvokeOnMainThread(() => {
							LoginView.Hidden = true;
							LightBox.Hidden = true;
							});
						});
				});
			}


		}

		private void ShowDateView(int whichView)
		{
			DateViewController dateViewer = new DateViewController ();
			if (dateViewer != null) {

				this.NavigationController.PushViewController (dateViewer, true);
				dateViewer.SetCurrentPage (whichView);
			}
		}

		private void InitFacebookLogin()
		{
			LoginView.Hidden = false;
			Profile.Notifications.ObserveDidChange ((sender, e) => {

				if (e.NewProfile == null)
					return;
				
				LoginView.Hidden = true;
				LightBox.Hidden = true;
				nameLabel.Text = e.NewProfile.Name;
			});
			CGRect viewBounds = LoginView.Bounds;

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
	}
}

