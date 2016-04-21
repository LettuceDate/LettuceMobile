
using System;

using Foundation;
using UIKit;
using SharpMobileCode.ModalPicker;
using Lettuce.Core;
using System.Collections.Generic;
using CoreGraphics;

namespace Lettuce.IOS
{
	public partial class ProposeDatesViewController : UIViewController
	{
		private ModalPickerViewController modalPicker = null;
		public BaseDate newDate;
		private bool datePicked = false;
		private NSObject keyWatcher;

		public delegate void DateCreatedHandler(Lettuce.Core.BaseDate newDate);
		public event DateCreatedHandler DateCreated;

		public ProposeDatesViewController () : base ("ProposeDatesViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			newDate = new ProposedDate ();
			// Perform any additional setup after loading the view, typically from a nib.
			modalPicker = new ModalPickerViewController(ModalPickerType.Date, "2 hrs to 10 days", this) {
				HeaderBackgroundColor = UIColor.Red,
				HeaderTextColor = UIColor.White,
				TransitioningDelegate = new ModalPickerTransitionDelegate(),
				ModalPresentationStyle = UIModalPresentationStyle.Custom
			};

			modalPicker.DatePicker.Mode = UIDatePickerMode.DateAndTime;
			DateTime minDate = DateTime.UtcNow.AddHours (2);
			DateTime maxDate = DateTime.UtcNow.AddDays (10);

			modalPicker.DatePicker.MinimumDate = minDate.DateTimeToNSDate();
			modalPicker.DatePicker.MaximumDate = maxDate.DateTimeToNSDate();
			modalPicker.DatePicker.MinuteInterval = 15;
			modalPicker.OnModalPickerDismissed += HandleDatePicked;


			DateStartBtn.TouchUpInside += (object sender, EventArgs e) => 
			{
				ShowDateController();


			};

			CancelDateBtn.TouchUpInside += (object sender, EventArgs e) => {
				DismissViewController(true, null);
			};

			CreateDateBtn.TouchUpInside += (object sender, EventArgs e) => {
				LettuceServer.Instance.CreateDate(newDate, (theDate) =>
					{
						newDate = theDate;
						InvokeOnMainThread(() => {
						DismissViewController(true, null);
						if (DateCreated != null)
							DateCreated(theDate);
						});
					});

			};

			AddActivityBtn.TouchUpInside += (sender, e) => {
				if ((newDate.activities == null) || (newDate.activities.Count == 0))
					AddNewActivityToDate ();
				else
					RemoveAllActivitiesFromDate ();
			};
				

			ActivityTableView.RegisterNibForCellReuse (UINib.FromName (ActivitySummaryCell.Key, NSBundle.MainBundle), ActivitySummaryCell.Key);
			ActivityTableView.Source = new DateActivityDataSource (this.newDate);
			ActivityTableView.RowHeight = 164;

			keyWatcher = NSNotificationCenter.DefaultCenter.AddObserver (UITextView.TextDidChangeNotification, (notification) => {
				newDate.description = DescriptionText.Text;
				newDate.title = HeadlineText.Text;
				UpdateCreateButton();
			});
		}

		private void AddNewActivityToDate()
		{
			NewActivityController newActivityController = new NewActivityController ();
			if (newActivityController != null) {
				newActivityController.ActivityCreated += (Activity newActivity) => {
					// add the new activity
					if (newDate.activities == null)
						newDate.activities = new List<Activity>();
					newDate.activities.Add(newActivity);
					ActivityTableView.ReloadData();
					RedoLayoutSizes();

				};
				PresentModalViewController (newActivityController, true);
			}
		}

		private void RemoveAllActivitiesFromDate()
		{
			if (newDate.activities != null)
				newDate.activities.Clear();
			ActivityTableView.ReloadData();
			RedoLayoutSizes();
		}

		private void RedoLayoutSizes()
		{
			ActivityTableView.LayoutIfNeeded();
			var contentSize = new CGSize (Scroller.Bounds.Width, 400 + ActivityTableView.ContentSize.Height);
			Scroller.ContentSize = contentSize;
			if ((newDate.activities == null) || (newDate.activities.Count == 0))
				AddActivityBtn.SetTitle ("Add", UIControlState.Normal);
			else
				AddActivityBtn.SetTitle ("Remove", UIControlState.Normal);
		}

		public override void ViewWillUnload ()
		{
			base.ViewWillUnload ();
			if (keyWatcher != null)
				NSNotificationCenter.DefaultCenter.RemoveObserver (keyWatcher);
		}



		async void ShowDateController()
		{

			await PresentViewControllerAsync(modalPicker, true);
		}

		private void HandleDatePicked (object ss, EventArgs ee)
		{
			datePicked = true;
			newDate.starttime = modalPicker.DatePicker.Date.NSDateToDateTime ();
			UpdateDateLabel ();
			UpdateCreateButton ();
		}

		private void UpdateCreateButton()
		{
			InvokeOnMainThread (() => {
				if (datePicked && 
					(!String.IsNullOrEmpty(newDate.description)) &&
					(!String.IsNullOrEmpty(newDate.title)) &&
					(newDate.activities != null) &&
					(newDate.activities.Count > 0))
					CreateDateBtn.Enabled = true;
				else
					CreateDateBtn.Enabled = false;
			});

		}

		private void UpdateDateLabel()
		{
			InvokeOnMainThread (() => {
				var dateFormatter = new NSDateFormatter () {
					DateFormat = "EEE MMM dd, h:mm aaa"
				};

				DateTime theTime = newDate.starttime;

				NSDate tempDate = theTime.DateTimeToNSDate ();
				DateStartBtn.SetTitle(dateFormatter.ToString (tempDate), UIControlState.Normal);
			});

		}
	}
}

