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
			View.BackgroundColor = LettuceColor.BoyBlue;

			// create a slideout navigation controller with the top navigation controller and the menu view controller
			NavController = new NavController();
			NavController.NavigationBar.BackgroundColor = LettuceColor.BoyBlue;
			SidebarController = new SidebarNavigation.SidebarController(this, NavController, new SideMenuController());
			SidebarController.MenuWidth = 220;
			SidebarController.ReopenOnRotate = false;
			SidebarController.MenuLocation = SidebarNavigation.SidebarController.MenuLocations.Left;
			NavController.PushViewController (HomeController , false);
			NavController.PushViewController (new InitialLoadViewController (), false);

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);



		}
			




	}
}


