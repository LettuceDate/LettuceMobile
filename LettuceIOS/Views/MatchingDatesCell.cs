
using System;

using Foundation;
using UIKit;

namespace Lettuce.IOS
{
	public partial class MatchingDatesCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("MatchingDatesCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("MatchingDatesCell");

		public MatchingDatesCell (IntPtr handle) : base (handle)
		{
		}

		public static MatchingDatesCell Create ()
		{
			return (MatchingDatesCell)Nib.Instantiate (null, null) [0];
		}
	}
}

