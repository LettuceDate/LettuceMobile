using System;

using Foundation;
using UIKit;
using Lettuce.Core;

namespace Lettuce.IOS
{
	public partial class NotificationsViewController : UIViewController
	{
		private NotificationsTableSource dataSource;
		private bool refreshNeeded = false;

		public NotificationsViewController () : base ("NotificationsViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
		
			this.ResultList.RegisterNibForCellReuse (UINib.FromName (NotificationCell.Key, NSBundle.MainBundle), NotificationCell.Key);
			dataSource = new NotificationsTableSource ();
			ResultList.Source = dataSource;
			ResultList.RowHeight = 64;
			LettuceServer.Instance.GetNotificationsForUser((notifyList) => {
				InvokeOnMainThread(() => {
					if (notifyList != null) {
						dataSource.SetNotificationList(notifyList);
						ResultList.ReloadData();
						this.NotifyHeader.Text = String.Format("NotificationsHeader_String".Localize(), notifyList.Count);
						refreshNeeded = false;
					} else {
						this.NotifyHeader.Text = String.Format("NotificationsHeader_String".Localize(), 0);
					}
				});
			});
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			TopConstraint.Constant = 64;//HomeViewController.LayoutGuideSize;
			UpdateViewConstraints ();

			if (refreshNeeded) {
				dataSource.SetNotificationList(dataSource.NotificationList);
				ResultList.ReloadData();
				refreshNeeded = false;
			}
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			refreshNeeded = true;
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


