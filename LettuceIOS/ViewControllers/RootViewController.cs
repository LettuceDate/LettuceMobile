using System;
using UIKit;
using Foundation;
using Facebook.LoginKit;
using Facebook.CoreKit;
using CoreGraphics;
using System.Collections.Generic;
using Lettuce.Core;
using SidebarNavigation;

namespace Lettuce.IOS
{
	public partial class RootViewController : UIViewController
	{

		// the sidebar controller for the app
		public SidebarNavigation.SidebarController SidebarController { get; private set; }

		// the navigation controller
		public NavController NavController { get; private set; }

		// facebook login stuff
		List<string> readPermissions = new List<string> { "public_profile", "user_birthday", "user_location" };
		LoginButton loginButton;
		ProfilePictureView pictureView;
		UILabel nameLabel;
		UIView	LoginView = new UIView ();
		UIView LightBox = new UIView ();
		private HomeScreenViewController _homeVC = null;
		private ProfileViewController _profileVC = null;
		private SettingsViewController _settingsVC = null;
		private AboutViewController _aboutVC = null;


		public HomeScreenViewController HomeController {
			get {
				if (_homeVC == null)
					_homeVC = new HomeScreenViewController ();
				return _homeVC;
			}
		}

		public ProfileViewController ProfileController {
			get {
				if (_profileVC == null)
					_profileVC = new ProfileViewController ();
				return _profileVC;
			}
		}

		public SettingsViewController SettingsController {
			get {
				if (_settingsVC == null)
					_settingsVC = new SettingsViewController ();
				return _settingsVC;
			}
		}

		public AboutViewController AboutController {
			get {
				if (_aboutVC == null)
					_aboutVC = new AboutViewController ();
				return _aboutVC;
			}
		}

		public RootViewController() : base(null, null)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// create a slideout navigation controller with the top navigation controller and the menu view controller
			NavController = new NavController();
			NavController.PushViewController(HomeController, false);
			SidebarController = new SidebarNavigation.SidebarController(this, NavController, new SideMenuController());
			SidebarController.MenuWidth = 220;
			SidebarController.ReopenOnRotate = false;
			SidebarController.MenuLocation = SidebarNavigation.SidebarController.MenuLocations.Left;

			if (AccessToken.CurrentAccessToken == null) {
				InitFacebookLogin ();
			} else {
				FinalizeLogin ();
			}
		}

		private void InitFacebookLogin()
		{
			return;


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

		private void FinalizeLogin()
		{
			if (AccessToken.CurrentAccessToken != null) {
				var request = new GraphRequest ("/me?fields=name,id,birthday,first_name,gender,last_name,interested_in,location", null, AccessToken.CurrentAccessToken.TokenString, null, "GET");
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
	}
}


