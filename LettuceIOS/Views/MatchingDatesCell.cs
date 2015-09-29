
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
		private BaseDate linkedDate;

		public MatchingDatesCell (IntPtr handle) : base (handle)
		{
		}

		public static MatchingDatesCell Create ()
		{
			return (MatchingDatesCell)Nib.Instantiate (null, null) [0];
		}

		public void ConformToEmpty()
		{
			PinBtn.Hidden = true;
			SelfieView.Hidden = true;
			DateTimeLabel.Hidden = true;
			DateTitleLabel.Text = "No matching dates found.";
		}

		public void ConformToRecord(BaseDate theDate)
		{
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

		}
	}
}

