
using System;

using Foundation;
using UIKit;

namespace Lettuce.IOS
{
	public partial class InterestedPersonCell : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("InterestedPersonCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("InterestedPersonCell");

		public InterestedPersonCell (IntPtr handle) : base (handle)
		{
		}

		public static InterestedPersonCell Create ()
		{
			return (InterestedPersonCell)Nib.Instantiate (null, null) [0];
		}
	}
}

