
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

			// Perform any additional setup after loading the view, typically from a nib.
			HeaderLabel.Text = "LookingForDates_String".Localize();

			FilterBtn.TouchUpInside += (object sender, EventArgs e) => 
			{

				// handle date filtering
			};

			CurrentDatesTableView.RegisterNibForCellReuse (UINib.FromName (CommittedDatesCell.Key, NSBundle.MainBundle), CommittedDatesCell.Key);
			dataSource = new CurrentDatesTableSource ();
			CurrentDatesTableView.DataSource = dataSource;

		}

		public override void ViewWillAppear (bool animated)
		{
			LettuceServer.Instance.GetCurrentDates ((dateList) => {
				dataSource.CommittedDateList = dateList;
				CurrentDatesTableView.ReloadData ();

			});
		}
	}
}

