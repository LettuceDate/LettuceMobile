using System;

using UIKit;

namespace Lettuce.IOS
{
	public partial class HomeScreenViewController : UITabBarController
	{
		UIViewController myDatesView, openDatesView, notificationsView;

		public HomeScreenViewController () : base (null, null)
		{
			myDatesView = new CurrentDatesViewController();
			myDatesView.TabBarItem = new UITabBarItem ("CurrentDates_Tab".Localize(), UIImage.FromBundle ("CurrentDatesIcon"), 0);


			openDatesView = new MatchingDatesViewController();
			openDatesView.TabBarItem = new UITabBarItem ("MatchingDates_Tab".Localize(), UIImage.FromBundle ("MatchingDatesIcon"), 1);

			notificationsView = new NotificationsViewController();
			notificationsView.TabBarItem = new UITabBarItem ("Notifications_Tab".Localize(), UIImage.FromBundle ("AppliedDatesIcon"), 2);


			var tabs = new UIViewController[] {
				myDatesView, openDatesView, notificationsView
			};

			ViewControllers = tabs;
			this.Title = "OpenDate";
			UIBarButtonItem menuBtn = new UIBarButtonItem ("Menu", UIBarButtonItemStyle.Plain, null);
			UIBarButtonItem newBtn = new UIBarButtonItem (UIBarButtonSystemItem.Add);
			this.NavigationItem.SetLeftBarButtonItem (menuBtn, false);
			this.NavigationItem.SetRightBarButtonItem (newBtn, true);

			menuBtn.Clicked += (object sender, EventArgs e) => 
				{
					SidebarController.ToggleMenu();
				};


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


