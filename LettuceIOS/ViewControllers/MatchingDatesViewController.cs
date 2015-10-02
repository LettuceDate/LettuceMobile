
using System;

using Foundation;
using UIKit;
using Lettuce.Core;


namespace Lettuce.IOS
{
	public partial class MatchingDatesViewController : UIViewController
	{
		private MatchingDatesTableSource dataSource;

		public MatchingDatesViewController () : base ("MatchingDatesViewController", null)
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
			
			// Perform any additional setup after loading the view, typically from a nib.
			this.ResultTitle.Text = "LookingForMatchingDates_String".Localize();

			this.FilterBtn.TouchUpInside += (object sender, EventArgs e) => 
			{

			};

			this.ResultList.RegisterNibForCellReuse (UINib.FromName (MatchingDatesCell.Key, NSBundle.MainBundle), MatchingDatesCell.Key);
			dataSource = new MatchingDatesTableSource ();
			ResultList.Source = dataSource;
			ResultList.RowHeight = 64;
			LettuceServer.Instance.GetMatchingDatesForUser((dateList) => {
				if (dateList != null) {
					InvokeOnMainThread(() => {
						dataSource.SetDateList(dateList, ResultList);
						ResultList.ReloadData();
						this.ResultTitle.Text = String.Format("FoundMatchingDates_String".Localize(), dateList.Count);
					});
				}
			});

			TopConstraint.Constant = HomeViewController.LayoutGuideSize;
		}
	}
}

