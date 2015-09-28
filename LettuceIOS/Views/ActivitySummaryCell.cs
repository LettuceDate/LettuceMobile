
using System;

using Foundation;
using UIKit;
using Lettuce.Core;
using SDWebImage;


namespace Lettuce.IOS
{
	public partial class ActivitySummaryCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("ActivitySummaryCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("ActivitySummaryCell");

		private Activity linkedActivity;

		public ActivitySummaryCell (IntPtr handle) : base (handle)
		{
		}

		public static ActivitySummaryCell Create ()
		{
			return (ActivitySummaryCell)Nib.Instantiate (null, null) [0];
		}

		public async void ConformToRecord(Activity theActivity)
		{
			linkedActivity = theActivity;
			var curType = LettuceServer.Instance.ActivityType (theActivity.type);

			if (!String.IsNullOrEmpty(curType.icon))
				this.ActivityTypeImage.SetImage (new NSUrl(curType.icon));
			this.ActivityTypeLabel.Text = curType.typename;
			this.ActivityDescription.Text = linkedActivity.description;
			this.ActivityDurationLabel.Text = String.Format("{0} minutes", linkedActivity.duration);
			// fetch the venue
			var curVenue = LettuceServer.Instance.Venue(theActivity.venueid);

			if (curVenue == null) {
				curVenue = await LettuceServer.Instance.LoadVenue(theActivity.venueid);
				InvokeOnMainThread (() => {
					this.VenueNameLabel.Text = curVenue.title;
					if (!String.IsNullOrEmpty(curVenue.image))
						this.VenueImage.SetImage (new NSUrl(curVenue.image));
				});
			} else {
				this.VenueNameLabel.Text = curVenue.title;
				if (!String.IsNullOrEmpty(curVenue.image))
					this.VenueImage.SetImage (new NSUrl(curVenue.image));
			}

		}
	}
}

