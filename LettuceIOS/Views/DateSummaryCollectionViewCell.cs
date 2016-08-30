using System;

using Foundation;
using UIKit;
using Lettuce.Core;
using SDWebImage;

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

		public override void MovedToSuperview()
		{
			base.MovedToSuperview();
			CommIcon.Layer.CornerRadius = 30;
			PersonImage.Layer.CornerRadius = 30;
			NoticeImg.Layer.CornerRadius = 22;
			CountView.Layer.CornerRadius = 12;

			this.Layer.MasksToBounds = false;
			this.Layer.ShadowOffset = new CoreGraphics.CGSize(8, 0);
			this.Layer.ShadowOpacity = .75f;
			this.Layer.ShadowRadius = 16;
			this.Layer.ShadowColor = new CoreGraphics.CGColor(0.5f, 0.5f, 0.5f, 1);
			this.Layer.ShadowPath = UIBezierPath.FromRect(this.Layer.Bounds).CGPath;

		}

		public void ConformToEmpty()
		{
			linkedDate = null;
			//DateIcon.Hidden = true;
			//DateTitle.Text = "NoBookedDatesCell_String".Localize();
		}

		// celltypes
		// 1 - booked date
		// 2 - date with applicant
		// 3 - open date
		// 4 - proposed date
		// 5 - date in the past
		public void ConformToRecord(BaseDate theDate, int cellType)
		{
			linkedDate = theDate;
			AgeLabel.Text = theDate.proposerAge.ToString();
			CommIcon.Image = UIImage.FromBundle("mailbadge");


			if (cellType == 1)
			{
				CommIcon.Hidden = false;

				if (theDate.messageCount > 0)
				{
					CountView.Hidden = false;
					CountLabel.Hidden = false;
					CountLabel.Text = theDate.messageCount.ToString();
				}
				else {
					CountLabel.Hidden = true;
					CountView.Hidden = true;
				}
			}
			else {
				CountView.Hidden = true;
				CountLabel.Hidden = true;
				CommIcon.Hidden = true;
			}

			DateTimeLabel.Text = theDate.shortTimeStr;

			string paymentStr = "";

			switch (theDate.paymentStyle)
			{
				case 0:
					paymentStr = "DatePayment_Summary_0_str".Localize();
					break;
				case 1:
					paymentStr = "DatePayment_Summary_1_str".Localize();
					break;
				case 2:
					paymentStr = "DatePayment_Summary_2_str".Localize();
					break;
			}
			DateTreatLabel.Text = paymentStr;

			string dateTypeStr = "";
			Activity activity = theDate.activities[0];

			switch (activity.type)
			{
				case 1:
					dateTypeStr = "DateType_1_str".Localize();
					break;
				case 2:
					dateTypeStr = "DateType_2_str".Localize();
					break;
				case 3:
					dateTypeStr = "DateType_3_str".Localize();
					break;
				case 4:
					dateTypeStr = "DateType_4_str".Localize();
					break;
			}
			DateTypeLabel.Text = dateTypeStr;

			NameLabel.Text = theDate.proposerName;

			NoticeImg.Image = UIImage.FromBundle("newbadge");
			PersonImage.SetImage(new NSUrl(theDate.selfie));

			string relativeDateStr = "";
			if (theDate.starttime.Date == DateTime.Today)
				relativeDateStr = "Date_Today_Str".Localize();
			else if (theDate.starttime.Date == DateTime.Today.AddDays(1))
				relativeDateStr = "Date_Tomorrow_Str".Localize();
			else
				relativeDateStr = theDate.starttime.DayOfWeek.ToString();   // TO DO - localize.

			RelativeDayLabel.Text = relativeDateStr;

			LettuceServer.Instance.LoadVenue(activity.venueid).ContinueWith((theTask) =>
			{
				Venue theVenue = theTask.Result;
				InvokeOnMainThread(() =>
				{

					VenueAddressLabel.Text = theVenue.address;
					VenueNameLabel.Text = theVenue.title;
					VenueImage.SetImage(new NSUrl(theVenue.image));
				});
			});
		}

	}
}
