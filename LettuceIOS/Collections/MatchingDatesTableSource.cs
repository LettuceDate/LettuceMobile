using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using Lettuce.Core;
namespace Lettuce.IOS
{
	public class MatchingDatesTableSource : UITableViewSource
	{
		public List<MatchingDate>	Dates { get; set;}
		private List<MatchingDate>	PinnedDates { get; set;}
		private List<MatchingDate>	AppliedDates { get; set;}
		private List<MatchingDate>	OtherDates { get; set;}
		private UITableView myTable;


		public MatchingDatesTableSource ()
		{
			PinnedDates = new List<MatchingDate> ();
			AppliedDates = new List<MatchingDate> ();
			OtherDates = new List<MatchingDate> ();			
		}

		public void SetDateList(List<MatchingDate> theList, UITableView theTable)
		{
			Dates = theList;
			myTable = theTable;
			PinnedDates.Clear ();
			AppliedDates.Clear ();
			OtherDates.Clear ();

			foreach (MatchingDate curDate in theList) {
				if (curDate.applied)
					AppliedDates.Add (curDate);
				else if (curDate.pinned)
					PinnedDates.Add (curDate);
				else
					OtherDates.Add (curDate);
			}
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			int section = indexPath.Section;
			MatchingDate theDate = null;
			List<MatchingDate> sectionList;

			if (section == 0)
				sectionList = AppliedDates;
			else if (section == 1)
				sectionList = PinnedDates;
			else
				sectionList = OtherDates;

			if ((sectionList == null) || (sectionList.Count == 0))
				theDate = null;
			else
				theDate = sectionList [indexPath.Row];

			if (theDate != null) {
				DateViewController.Instance.EditDate (theDate);

			}
			
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			nfloat theHeight = 64;
			int section = indexPath.Section;

			List<MatchingDate> sectionList;

			if (section == 0)
				sectionList = AppliedDates;
			else if (section == 1)
				sectionList = PinnedDates;
			else
				sectionList = OtherDates;

			if ((sectionList == null) || (sectionList.Count == 0))
				theHeight = 96;
			else
				theHeight = 64;


			return theHeight;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			nint numDates = 1;
			List<MatchingDate> sectionList;

			if (section == 0)
				sectionList = AppliedDates;
			else if (section == 1)
				sectionList = PinnedDates;
			else
				sectionList = OtherDates;

			if ((sectionList == null) || (sectionList.Count == 0))
				numDates = 1;
			else
				numDates = sectionList.Count;

			return numDates;
		}

		public void UpdateCellStatus(MatchingDatesCell theCell, MatchingDate theDate)
		{
			if (theDate.pinned) {
				OtherDates.Remove (theDate);
				PinnedDates.Add (theDate);
			} else {
				// unpinned
				PinnedDates.Remove (theDate);
				OtherDates.Add (theDate);
			}
			myTable.ReloadData ();
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 3;
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			if (section == 0)
				return "MatchingDateListApplied_String".Localize();
			else if (section == 1)
				return "MatchingDateListPinned_String".Localize();
			else
				return "MatchingDateListOther_String".Localize();
		}


		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			MatchingDatesCell cell = tableView.DequeueReusableCell(MatchingDatesCell.Key, indexPath) as MatchingDatesCell;

			List<MatchingDate> sectionList;

			if (indexPath.Section == 0)
				sectionList = AppliedDates;
			else if (indexPath.Section == 1)
				sectionList = PinnedDates;
			else
				sectionList = OtherDates;

			if ((sectionList == null) || (sectionList.Count == 0))
				cell.ConformToEmpty (indexPath.Section);
			else
				cell.ConformToRecord(sectionList[indexPath.Row], this);
			

			return cell;
		}
	}
}
