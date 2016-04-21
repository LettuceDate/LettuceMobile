using System;

using Foundation;
using UIKit;
using Lettuce.Core;

namespace Lettuce.IOS
{
	public partial class NotificationCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("NotificationCell");
		public static readonly UINib Nib;
		private BaseNotification linkedNotification;
		private NotificationsTableSource parentSource;

		static NotificationCell ()
		{
			Nib = UINib.FromName ("NotificationCell", NSBundle.MainBundle);
		}

		public NotificationCell (IntPtr handle) : base (handle)
		{
		}

		public static NotificationCell Create ()
		{
			NotificationCell theCell = (NotificationCell)Nib.Instantiate (null, null) [0];

			return theCell;
		}

		public void ConformToEmpty()
		{
			linkedNotification = null;
			NotifLabel.Text = "No Notifications.  Get more active!";
			/*
			PinBtn.Hidden = true;
			SelfieView.Hidden = true;
			DateTimeLabel.Hidden = true;
			if (section == 0)
				DateTitleLabel.Text = "NoAppliedDatesCell_String".Localize();
			else if (section == 1)
				DateTitleLabel.Text = "NoPinnedDatesCell_String".Localize();
			else
				DateTitleLabel.Text = "NoMatchingDatesCell_String".Localize();

			PinBtn.TouchUpInside -= HandleClick;
			*/
		}

		public void ConformToRecord(BaseNotification theNotif, NotificationsTableSource theSource)
		{
			parentSource = theSource;
			linkedNotification = theNotif;
			NotifLabel.Text = theNotif.detail;
			/*
			PinBtn.TouchUpInside -= HandleClick;
			linkedDate = theDate;
			PinBtn.Hidden = false;
			SelfieView.Hidden = false;
			DateTimeLabel.Hidden = false;
			DateTimeLabel.Text = linkedDate.starttime.ToString ("g");
			DateTitleLabel.Text = linkedDate.title;
			if (!String.IsNullOrEmpty (linkedDate.selfie))
				SelfieView.SetImage (new NSUrl (linkedDate.selfie));
			else
				SelfieView.Image = UIImage.FromBundle ("LaunchIcon");

			if (theDate.applied) {
				PinBtn.SetTitle ("interested", UIControlState.Normal);
			} else if (theDate.pinned) {
				PinBtn.SetTitle ("unpin", UIControlState.Normal);
			} else {
				PinBtn.SetTitle ("pin", UIControlState.Normal);
			}
			PinBtn.TouchUpInside += HandleClick;
			*/
		}
	}
}
