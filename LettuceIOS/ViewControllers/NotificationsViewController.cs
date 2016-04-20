using System;
using Lettuce.Core;
using UIKit;

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
		
			this.ResultList.RegisterNibForCellReuse (UINib.FromName (MatchingDatesCell.Key, NSBundle.MainBundle), MatchingDatesCell.Key);
			dataSource = new MatchingDatesTableSource ();
			ResultList.Source = dataSource;
			ResultList.RowHeight = 64;
			LettuceServer.Instance.GetMatchingDatesForUser((dateList) => {
				if (dateList != null) {
					InvokeOnMainThread(() => {
						dataSource.SetNotificationList(dateList, ResultList);
						ResultList.ReloadData();
						this.ResultTitle.Text = String.Format("FoundMatchingDates_String".Localize(), dateList.Count);
						refreshNeeded = false;
					});
				}
			});
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


