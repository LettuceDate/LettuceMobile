using System;

using UIKit;

namespace Lettuce.IOS
{
	public partial class SideMenuController : UIViewController
	{
		public SideMenuController () : base ("SideMenuController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			HomeBtn.TouchUpInside += (sender, e) => {
				NavController.PopToRootViewController(false);
				SidebarController.CloseMenu();
			};

			ProfileBtn.TouchUpInside += (sender, e) => {
				NavController.PushViewController(new ProfileViewController(), false);
				SidebarController.CloseMenu();
			};

			SettingsBtn.TouchUpInside += (sender, e) => {
				NavController.PushViewController(new SettingsViewController(), false);
				SidebarController.CloseMenu();
			};

			AboutBtn.TouchUpInside += (sender, e) => {
				NavController.PushViewController(new AboutViewController(), false);
				SidebarController.CloseMenu();
			};

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		protected RootViewController RootController { 
			get {
				return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController;
			} 
		}

		protected SidebarNavigation.SidebarController SidebarController { 
			get {
				return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.SidebarController;
			} 
		}

		// provide access to the sidebar controller to all inheriting controllers
		protected NavController NavController { 
			get {
				return (UIApplication.SharedApplication.Delegate as AppDelegate).RootViewController.NavController;
			} 
		}
	}
}


