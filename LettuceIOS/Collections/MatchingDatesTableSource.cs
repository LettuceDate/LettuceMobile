using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using Lettuce.Core;
namespace Lettuce.IOS
{
	public class MatchingDatesTableSource : UITableViewDataSource
	{
		public List<BaseDate>	Dates { get; set;}


		public MatchingDatesTableSource ()
		{
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			nint numDates = 1;

			if ((Dates == null) || (Dates.Count == 0))
				numDates = 1;
			else
				numDates = Dates.Count;

			return numDates;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			MatchingDatesCell cell = tableView.DequeueReusableCell(MatchingDatesCell.Key, indexPath) as MatchingDatesCell;

			if ((Dates == null) || (Dates.Count == 0))
				cell.ConformToEmpty ();
			else
				cell.ConformToRecord(Dates[indexPath.Row]);

			return cell;
		}
	}
}
