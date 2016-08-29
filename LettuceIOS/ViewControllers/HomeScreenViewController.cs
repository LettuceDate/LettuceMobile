using System;
using Foundation;
using UIKit;
using Lettuce.Core;
using CoreGraphics;

namespace Lettuce.IOS
{
	public partial class HomeScreenViewController : UITabBarController
	{
		UIViewController myDatesView, matchingDatesView, notificationsView;
		public static nfloat LayoutGuideSize = 0;

		public HomeScreenViewController () : base (null, null)
		{
			myDatesView = new ODHomeVC();
			myDatesView.TabBarItem = new UITabBarItem ("CurrentDates_Tab".Localize(), UIImage.FromBundle ("CurrentDatesIcon"), 0);


			matchingDatesView = new MatchingDatesViewController();
			matchingDatesView.TabBarItem = new UITabBarItem ("MatchingDates_Tab".Localize(), UIImage.FromBundle ("MatchingDatesIcon"), 1);

			notificationsView = new NotificationsViewController();
			notificationsView.TabBarItem = new UITabBarItem ("Notifications_Tab".Localize(), UIImage.FromBundle ("AppliedDatesIcon"), 2);


			var tabs = new UIViewController[] {
				myDatesView, matchingDatesView, notificationsView
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

			newBtn.Clicked += (object sender, EventArgs e) => 
			{
				ProposeDatesViewController proposeController = new ProposeDatesViewController ();
				if (proposeController != null) {

					this.NavigationController.PresentModalViewController (proposeController, true);
				}
			};


		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			NavigationController.NavigationBarHidden = false;
			LettuceServer.Instance.GetNotificationCountForUser ((theCount) => {
				InvokeOnMainThread(() => {
				if (theCount > 0)
					notificationsView.TabBarItem.BadgeValue = theCount.ToString();
				else
					notificationsView.TabBarItem.BadgeValue = null;
				});

			});

			PrepareChannelBar();
		}

		private void PrepareChannelBar()
		{
			string headerImage =  "";//"header";
			if (String.IsNullOrEmpty(headerImage))
			{
				Title = "OpenDate";
				this.NavigationController.NavigationBar.SetBackgroundImage(null, UIBarMetrics.Default);
			}
			else {
				Title = "";
				UIImage theImage = UIImage.FromBundle(headerImage); //UIImageHelper.ImageFromUrl(headerImage);
				if (theImage != null)
				{
					CGRect theRect = this.NavigationController.NavigationBar.Frame;
					theRect.Width *= UIScreen.MainScreen.Scale;
					theRect.Height *= UIScreen.MainScreen.Scale;
					nfloat aspectRatio = theImage.Size.Width / theImage.Size.Height;
					nfloat newWidth = theRect.Width;
					nfloat newHeight = newWidth / aspectRatio;
					nfloat offset = (newHeight - theRect.Height) / 2;
					if (newHeight < theRect.Height)
					{
						newHeight = theRect.Height;
						newWidth = newHeight * aspectRatio;
						offset = 0;
					}

					UIGraphics.BeginImageContext(theRect.Size);
					theImage.Draw(new CGRect(0, -offset, newWidth, newHeight));
					UIImage newImage = UIGraphics.GetImageFromCurrentImageContext();
					UIGraphics.EndImageContext();
					this.NavigationController.NavigationBar.SetBackgroundImage(newImage, UIBarMetrics.Default);
					theImage.Dispose();
				}
			}
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


