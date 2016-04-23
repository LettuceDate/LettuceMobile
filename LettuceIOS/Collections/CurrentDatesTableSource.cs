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
		private List<BaseDate>	DatesWithApplicantsList { get; set;}
		private List<BaseDate>	UsersDateList { get; set;}


		public CurrentDatesTableSource ()
		{
	
		}


		public void SetBookedDateList(List<BaseDate> theList)
		{
			if (theList == null)
				theList = new List<BaseDate> ();
			
			CommittedDateList = theList;

		}

		public void SetUsersDateList(List<BaseDate> theList)
		{
			if (theList != null) {
				UsersDateList = new List<BaseDate> ();
				DatesWithApplicantsList = new List<BaseDate> ();

				foreach (BaseDate curDate in theList) {
					if (curDate.hasApplication)
						DatesWithApplicantsList.Add (curDate);
					else
						UsersDateList.Add (curDate);
				}
			}

		}
			
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			nint numDates = 1;
			List<BaseDate> sectionList;

			if (section == 0)
				sectionList = CommittedDateList;
			else if (section == 1)
				sectionList = DatesWithApplicantsList;
			else
				sectionList = UsersDateList;

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
				sectionList = CommittedDateList;
			else if (indexPath.Section == 1)
				sectionList = DatesWithApplicantsList;
			else
				sectionList = UsersDateList;
			
			if ((sectionList == null) || (sectionList.Count == 0))
				cell.ConformToEmpty ();
			else
				cell.ConformToRecord(sectionList[indexPath.Row]);

			return cell;
		}
	}
}

