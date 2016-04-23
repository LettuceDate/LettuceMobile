
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
			TopConstraint.Constant = HomeScreenViewController.LayoutGuideSize;
			UpdateViewConstraints ();
			LettuceServer.Instance.GetBookedDatesForUser ((bookedDateList) => {
				dataSource.SetBookedDateList(bookedDateList);

				InvokeOnMainThread(() => {
					CurrentDatesTableView.ReloadData ();
					LettuceServer.Instance.GetUsersOwnDates ((userDateList) => {
						dataSource.SetUsersDateList(userDateList);

						InvokeOnMainThread(() => {
							CurrentDatesTableView.ReloadData ();

						});

					});
				});

			});
		}
	}
}

