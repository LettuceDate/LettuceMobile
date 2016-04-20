
using System;

using Foundation;
using UIKit;

namespace Lettuce.IOS
{
	public partial class AboutViewController : UIViewController
	{
		public AboutViewController () : base ()
		{
			this.Title = "About OpenDate";
			UIBarButtonItem menuBtn = new UIBarButtonItem ("Menu", UIBarButtonItemStyle.Plain, null);
			UIBarButtonItem newBtn = new UIBarButtonItem (UIBarButtonSystemItem.Add);
			this.NavigationItem.SetLeftBarButtonItem (menuBtn, false);
			this.NavigationItem.SetRightBarButtonItem (newBtn, true);

			menuBtn.Clicked += (object sender, EventArgs e) => 
			{
				SidebarController.ToggleMenu();
			};
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
			
			// Perform any additional setup after loading the view, typically from a nib.
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

