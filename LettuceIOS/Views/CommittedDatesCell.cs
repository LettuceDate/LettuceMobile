
using System;

using Foundation;
using UIKit;
using Lettuce.Core;


namespace Lettuce.IOS
{
	public partial class CommittedDatesCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("CommittedDatesCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("CommittedDatesCell");
		private BaseDate linkedDate;


		public CommittedDatesCell (IntPtr handle) : base (handle)
		{
		}

		public static CommittedDatesCell Create ()
		{
			return (CommittedDatesCell)Nib.Instantiate (null, null) [0];
		}

		public void ConformToEmpty()
		{
			linkedDate = null;
			DateIcon.Hidden = true;
			DateTitle.Text = "NoBookedDatesCell_String".Localize();
		}

		public void ConformToRecord(BaseDate theDate)
		{
			DateIcon.Hidden = false;
			linkedDate = theDate;
			DateTitle.Text = theDate.title;
		}
	}
}

