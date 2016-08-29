using System;

using Foundation;
using UIKit;
using Lettuce.Core;

namespace Lettuce.IOS
{
	public partial class DateSummaryCollectionViewCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString("DateSummaryCollectionViewCell");
		public static readonly UINib Nib = UINib.FromName("DateSummaryCollectionViewCell", NSBundle.MainBundle);
		private BaseDate linkedDate;

		static DateSummaryCollectionViewCell()
		{
			Nib = UINib.FromName("DateSummaryCollectionViewCell", NSBundle.MainBundle);
		}

		protected DateSummaryCollectionViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public void ConformToEmpty()
		{
			linkedDate = null;
			//DateIcon.Hidden = true;
			//DateTitle.Text = "NoBookedDatesCell_String".Localize();
		}

		public void ConformToRecord(BaseDate theDate)
		{
			//DateIcon.Hidden = false;
			linkedDate = theDate;
			//DateTitle.Text = theDate.title;
		}

	}
}
