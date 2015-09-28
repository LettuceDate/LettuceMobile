
using System;

using Foundation;
using UIKit;
using Lettuce.Core;
using Lettuce.Core.Yelp;
using SDWebImage;

namespace Lettuce.IOS
{
	public partial class YelpResultCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("YelpResultCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("YelpResultCell");
		private Business linkedBiz;

		public YelpResultCell (IntPtr handle) : base (handle)
		{
		}

		public static YelpResultCell Create ()
		{
			return (YelpResultCell)Nib.Instantiate (null, null) [0];
		}

		public void ConformToRecord(Business theBiz)
		{
			linkedBiz = theBiz;
			VenueName.Text = linkedBiz.name;
			if (linkedBiz.image_url != null)
				VenueImage.SetImage (new NSUrl (linkedBiz.image_url));
			else {
				// VenueImage.SetImage(placeholderImage);
			}
			VenueRatingImage.SetImage (new NSUrl (linkedBiz.rating_img_url_large));
		}
	}
}

