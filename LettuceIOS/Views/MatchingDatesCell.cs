
using System;

using Foundation;
using UIKit;
using Lettuce.Core;
using SDWebImage;

namespace Lettuce.IOS
{
	public partial class MatchingDatesCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("MatchingDatesCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("MatchingDatesCell");
		private MatchingDate linkedDate;
		private MatchingDatesTableSource parentSource;

		public MatchingDatesCell (IntPtr handle) : base (handle)
		{
		}

		public static MatchingDatesCell Create ()
		{
			MatchingDatesCell theCell = (MatchingDatesCell)Nib.Instantiate (null, null) [0];

			return theCell;
		}

		private void HandleClick(object sender, EventArgs e)
		{
			if (linkedDate != null) {
				if (linkedDate.pinned)
					linkedDate.pinned = false;
				else
					linkedDate.pinned = true;
				parentSource.UpdateCellStatus (this, linkedDate);
			}
		}

		public void ConformToEmpty(int section)
		{
			linkedDate = null;
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
		}

		public void ConformToRecord(MatchingDate theDate, MatchingDatesTableSource theSource)
		{
			parentSource = theSource;
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
		}
	}
}

