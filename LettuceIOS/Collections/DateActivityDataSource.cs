using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using Lettuce.Core;

namespace Lettuce.IOS
{
	public class DateActivityDataSource: UITableViewSource
	{
		public BaseDate currentDate {get; set;}


		public DateActivityDataSource (BaseDate dateSource)
		{
			currentDate = dateSource;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			nint numDates = 0;

			if (currentDate != null) {
				if (currentDate.activities != null)
					numDates = currentDate.activities.Count;
				else
					numDates = 0;
			}

			return numDates;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			ActivitySummaryCell cell = tableView.DequeueReusableCell(ActivitySummaryCell.Key, indexPath) as ActivitySummaryCell;

			cell.ConformToRecord(currentDate.activities[indexPath.Row]);

			return cell;
		}

		public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			switch (editingStyle) {
			case UITableViewCellEditingStyle.Delete:
				// to do - remove the item
				currentDate.activities.RemoveAt (indexPath.Row);
				tableView.DeleteRows (new NSIndexPath[]{ indexPath }, UITableViewRowAnimation.Fade);
				break;

			case UITableViewCellEditingStyle.None:
				break;
			}

		}

		public override string TitleForDeleteConfirmation (UITableView tableView, NSIndexPath indexPath)
		{
			return "Remove this activity?";
		}
		public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete;
		}

		public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
		{
			var item = currentDate.activities[sourceIndexPath.Row];
			var deleteAt = sourceIndexPath.Row;
			var insertAt = destinationIndexPath.Row;

			// are we inserting 
			if (destinationIndexPath.Row < sourceIndexPath.Row) {
				// add one to where we delete, because we're increasing the index by inserting
				deleteAt += 1;
			} else {
				// add one to where we insert, because we haven't deleted the original yet
				insertAt += 1;
			}
			currentDate.activities.Insert (insertAt, item);
			currentDate.activities.RemoveAt (deleteAt);
		}

	}
}

