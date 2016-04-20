using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using Lettuce.Core;


namespace Lettuce.IOS
{
	public class NotificationsTableSource : UITableViewSource
	{
		public List<BaseNotification>	NotificationList { get; set;}



		public NotificationsTableSource ()
		{
			NotificationList = new List<BaseNotification> ();
		}


		public void SetNotificationList(List<BaseNotification> theList)
		{
			if (theList == null)
				theList = new List<BaseNotification> ();

			NotificationList.Clear ();

			foreach (BaseNotification curNotif in theList) {
				NotificationList.Add (curNotif);
			}
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			nint numNotifs = 1;


			if ((NotificationList == null) || (NotificationList.Count == 0))
				numNotifs = 1;
			else
				numNotifs = NotificationList.Count;

			return numNotifs;
		}

		public override string TitleForHeader (UITableView tableView, nint section)
		{
			return "Notification_Header".Localize();
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}



		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			NotificationCell cell = tableView.DequeueReusableCell(NotificationCell.Key, indexPath) as NotificationCell;


			if ((NotificationList == null) || (NotificationList.Count == 0))
				cell.ConformToEmpty ();
			else
				cell.ConformToRecord(NotificationList[indexPath.Row], this);

			return cell;
		}
	}
}

