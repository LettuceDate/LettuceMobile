
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
		private CommittedDate linkedDate;


		public CommittedDatesCell (IntPtr handle) : base (handle)
		{
		}

		public static CommittedDatesCell Create ()
		{
			return (CommittedDatesCell)Nib.Instantiate (null, null) [0];
		}

		public void ConformToEmpty()
		{
			DateTitle.Text = "No dates at the moment";
		}

		public void ConformToRecord(CommittedDate theDate)
		{
			linkedDate = theDate;
			DateTitle.Text = theDate.title;
		}
	}
}

