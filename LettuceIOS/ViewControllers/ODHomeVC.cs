using System;
using Foundation;
using UIKit;
using Lettuce.Core;
using CoreGraphics;
using System.Collections.Generic;

namespace Lettuce.IOS
{
	public partial class ODHomeVC : UIViewController
	{
		private DateCollectionSource confirmedDateSource;
		private DateCollectionSource applicantDataSource;
		private DateCollectionSource openDataSource;

		public ODHomeVC() : base("ODHomeVC", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			// set up the collections
			ConfirmedDatesCollection.RegisterNibForCell(UINib.FromName(DateSummaryCollectionViewCell.Key, NSBundle.MainBundle), DateSummaryCollectionViewCell.Key);
			confirmedDateSource = new DateCollectionSource();
			ConfirmedDatesCollection.DataSource = confirmedDateSource;


			ApplicationCollection.RegisterNibForCell(UINib.FromName(DateSummaryCollectionViewCell.Key, NSBundle.MainBundle), DateSummaryCollectionViewCell.Key);
			applicantDataSource = new DateCollectionSource();
			ApplicationCollection.DataSource = applicantDataSource;

			OpenDatesCollection.RegisterNibForCell(UINib.FromName(DateSummaryCollectionViewCell.Key, NSBundle.MainBundle), DateSummaryCollectionViewCell.Key);
			openDataSource = new DateCollectionSource();
			OpenDatesCollection.DataSource = openDataSource;



		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			// load the various data
			PrepareAllForLoad();

			StartConfirmedDatesLoad();
			StartAppliedDatesLoad();
			StartOpenDatesLoad();
		}

		private void PrepareAllForLoad()
		{
			// Confirmation
			ConfirmedDatesLabel.Text = "Loading_Confirmed".Localize();
			ConfirmedDatesViewAll.Hidden = true;

			// Application
			ApplicantDatesLabel.Text = "Loading_Applied".Localize();
			ApplicantDatesViewAll.Hidden = true;

			// Open
			OpenDatesLabel.Text = "Loading_Open".Localize();
			OpenDatesViewAll.Hidden = true;
		}

		private void StartConfirmedDatesLoad()
		{
			LettuceServer.Instance.GetBookedDatesForUser((bookedList) =>
			{
				UpdateConfirmedDates(bookedList);
			});
		}

		private void StartAppliedDatesLoad()
		{
			LettuceServer.Instance.GetInterestedUsers((bookedList) =>
			{
				UpdateApplieddDates(bookedList);
			});
		}

		private void StartOpenDatesLoad()
		{
			LettuceServer.Instance.GetUsersOwnDates((bookedList) =>
			{
				UpdateOpenDates(bookedList);
			});
		}

		private void UpdateConfirmedDates(List<BaseDate> dateList)
		{
			InvokeOnMainThread(() =>
			{
				UpdateConfirmedDatesHeader(dateList);
				confirmedDateSource.SetData(dateList);
				ConfirmedDatesCollection.ReloadData();
			});
		}

		private void UpdateConfirmedDatesHeader(List<BaseDate> dateList)
		{
			int dateCount = 0;
			if (dateList != null)
				dateCount = dateList.Count;

			if (dateCount == 0)
				ConfirmedDatesLabel.Text = "No_Confirmed_Dates".Localize();
			else
				ConfirmedDatesLabel.Text = string.Format("Confirmed_Dates_Count".Localize(), dateCount);

			ConfirmedDatesViewAll.Hidden = false;
		}

		private void UpdateApplieddDates(List<BaseDate> dateList)
		{
			InvokeOnMainThread(() =>
			{
				UpdateApplieddDatesHeader(dateList);
				applicantDataSource.SetData(dateList);
				ApplicationCollection.ReloadData();
			});
		}

		private void UpdateApplieddDatesHeader(List<BaseDate> dateList)
		{
			int dateCount = 0;
			if (dateList != null)
				dateCount = dateList.Count;

			if (dateCount == 0)
				ApplicantDatesLabel.Text = "No_Applied_Dates".Localize();
			else
				ApplicantDatesLabel.Text = string.Format("Applied_Dates_Count".Localize(), dateCount);

			ApplicantDatesViewAll.Hidden = false;
		}

		private void UpdateOpenDates(List<BaseDate> dateList)
		{
			InvokeOnMainThread(() =>
			{
				UpdateOpenDatesHeader(dateList);
				openDataSource.SetData(dateList);
				OpenDatesCollection.ReloadData();
			});
		}

		private void UpdateOpenDatesHeader(List<BaseDate> dateList)
		{
			int dateCount = 0;
			if (dateList != null)
				dateCount = dateList.Count;

			if (dateCount == 0)
				OpenDatesLabel.Text = "No_Open_Dates".Localize();
			else
				OpenDatesLabel.Text = string.Format("Open_Dates_Count".Localize(), dateCount);

			OpenDatesViewAll.Hidden = false;
		}

	}
}


