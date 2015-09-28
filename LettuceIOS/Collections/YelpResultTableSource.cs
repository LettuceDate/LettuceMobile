using System;
using Lettuce.Core.Yelp;
using Foundation;
using UIKit;

namespace Lettuce.IOS
{
	public class YelpResultTableSource : UITableViewSource
	{
		public YelpResults resultSet { get; set;}
		public NewActivityController curActivity { get; set; }


		public YelpResultTableSource (NewActivityController theActivity)
		{
			curActivity = theActivity;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			curActivity.newActivity.venueid = resultSet.businesses [indexPath.Row].id;
			curActivity.ActivityChanged ();
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			nint numItems = 0;

			if (resultSet == null)
				numItems = 0;
			else
				numItems = resultSet.businesses.Count;

			return numItems;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			YelpResultCell cell = tableView.DequeueReusableCell(YelpResultCell.Key, indexPath) as YelpResultCell;

			cell.ConformToRecord(resultSet.businesses[indexPath.Row]);

			return cell;
		}
	}
}

