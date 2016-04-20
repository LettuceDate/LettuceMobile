
using System;

using Foundation;
using UIKit;
using Lettuce.Core;
using CoreGraphics;


namespace Lettuce.IOS
{
	public partial class CurrentDatesViewController : UIViewController
	{
		private CurrentDatesTableSource dataSource;

		public CurrentDatesViewController () : base ("CurrentDatesViewController", null)
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

			CurrentDatesTableView.RegisterNibForCellReuse (UINib.FromName (CommittedDatesCell.Key, NSBundle.MainBundle), CommittedDatesCell.Key);
			dataSource = new CurrentDatesTableSource ();
			CurrentDatesTableView.DataSource = dataSource;

		}

		public override void ViewWillAppear (bool animated)
		{
			TopConstraint.Constant = HomeViewController.LayoutGuideSize;
			UpdateViewConstraints ();
			LettuceServer.Instance.GetBookedDatesForUser ((dateList) => {
				dataSource.SetDateList(dateList);

				InvokeOnMainThread(() => {
					/*
				if ((dateList == null) || (dateList.Count == 0))
					HeaderLabel.Text = "NoBookedDates_String".Localize();
				else
					HeaderLabel.Text = String.Format("FoundBookedDates_String".Localize(), dateList.Count);
					*/
					CurrentDatesTableView.ReloadData ();
				});



			});
		}
	}
}

