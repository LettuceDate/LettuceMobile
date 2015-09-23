
using System;

using Foundation;
using UIKit;

namespace Lettuce.IOS
{
	public partial class PostedDatesCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("PostedDatesCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("PostedDatesCell");

		public PostedDatesCell (IntPtr handle) : base (handle)
		{
		}

		public static PostedDatesCell Create ()
		{
			return (PostedDatesCell)Nib.Instantiate (null, null) [0];
		}
	}
}

