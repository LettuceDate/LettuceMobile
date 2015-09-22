using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using Lettuce.Core;

namespace Lettuce.IOS
{
	public class CurrentDatesTableSource : UITableViewDataSource
	{
		public List<CommittedDate>	CommittedDateList { get; set;}


		public CurrentDatesTableSource ()
		{
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			nint numDates = 1;

			if ((CommittedDateList == null) || (CommittedDateList.Count == 0))
				numDates = 1;
			else
				numDates = CommittedDateList.Count;

			return numDates;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			CommittedDatesCell cell = tableView.DequeueReusableCell(CommittedDatesCell.Key, indexPath) as CommittedDatesCell;

			if ((CommittedDateList == null) || (CommittedDateList.Count == 0))
				cell.ConformToEmpty ();
			else
				cell.ConformToRecord(CommittedDateList[indexPath.Row]);

			return cell;
		}
	}
}

