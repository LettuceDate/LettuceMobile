using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using Lettuce.Core;

namespace Lettuce.IOS
{
	public class CurrentDatesTableSource : UITableViewDataSource
	{
		private List<BaseDate>	CommittedDateList { get; set;}
		private List<BaseDate>	TodayList { get; set;}

		private List<BaseDate>	TomorrowList { get; set;}

		private List<BaseDate>	RestList { get; set;}


		public CurrentDatesTableSource ()
		{
			TodayList = new List<BaseDate> ();
			TomorrowList = new List<BaseDate> ();
			RestList = new List<BaseDate> ();			
		}


		public void SetDateList(List<BaseDate> theList)
		{
			if (theList == null)
				theList = new List<BaseDate> ();
			
			CommittedDateList = theList;
			DateTime today = DateTime.UtcNow.Date;
			DateTime tomorrow = today.AddDays (1);
			TodayList.Clear ();
			TomorrowList.Clear ();
			RestList.Clear ();

			foreach (BaseDate curDate in theList) {
				DateTime curDateDate = curDate.starttime.Date;
				if (curDateDate.CompareTo (today) == 0)
					TodayList.Add (curDate);
				else if (curDateDate.CompareTo (tomorrow) == 0)
					TomorrowList.Add (curDate);
				else
					RestList.Add (curDate);

			}
		}
			
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			nint numDates = 1;
			List<BaseDate> sectionList;

			if (section == 0)
				sectionList = TodayList;
			else if (section == 1)
				sectionList = TomorrowList;
			else
				sectionList = RestList;

			if ((sectionList == null) || (sectionList.Count == 0))
				numDates = 1;
			else
				numDates = sectionList.Count;
			
			return numDates;
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			if (section == 0)
				return "ConfirmedDates_String".Localize();
			else if (section == 1)
				return "DatesWithApplications_String".Localize();
			else
				return "OpenDates_String".Localize();
		}
			
		public override nint NumberOfSections (UITableView tableView)
		{
			return 3;
		}



		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			CommittedDatesCell cell = tableView.DequeueReusableCell(CommittedDatesCell.Key, indexPath) as CommittedDatesCell;

			List<BaseDate> sectionList;

			if (indexPath.Section == 0)
				sectionList = TodayList;
			else if (indexPath.Section == 1)
				sectionList = TomorrowList;
			else
				sectionList = RestList;
			
			if ((sectionList == null) || (sectionList.Count == 0))
				cell.ConformToEmpty ();
			else
				cell.ConformToRecord(sectionList[indexPath.Row]);

			return cell;
		}
	}
}

