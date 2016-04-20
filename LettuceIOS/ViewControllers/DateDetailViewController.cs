
using System;

using Foundation;
using UIKit;
using Lettuce.Core;
using SDWebImage;
using CoreGraphics;


namespace Lettuce.IOS
{
	public partial class DateDetailViewController : UIViewController
	{
		private MatchingDate theDate;

		public DateDetailViewController () : base ("DateDetailViewController", null)
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
			
			// Perform any additional setup after loading the view, typically from a nib.
			TopConstraint.Constant = HomeScreenViewController.LayoutGuideSize;
		}

		public void SetCurrentDate(MatchingDate theDate) 
		{
			this.theDate = theDate;


		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.DateTitleLabel.Text = theDate.title;
			if (theDate.selfie != null)
				SelfieView.SetImage (new Uri (theDate.selfie));
			else
				SelfieHeight.Constant = 0;
			this.DateDescription.Text = theDate.description;
			string paymentStr = "DatePayment_" + theDate.paymentStyle.ToString() + "_String";

			LettuceServer.Instance.GetUserInfo (theDate.proposerid, (theUser) => {
				if (theUser != null) {
					InvokeOnMainThread(() => {
						this.UserAgeLabel.Text = AgeRaceGenderStr(theUser);
						this.UserDescription.Text = theUser.description;
						this.UserLocLabel.Text = CityStateStr(theUser);
						this.UserProfileImage.SetImage(new NSUrl(LettuceServer.Instance.GetUserProfileImage(theUser.facebookid)));
						this.UserNameLabel.Text = theUser.fullname;
						string nicknameStr = theUser.nickname;
						if (string.IsNullOrEmpty(nicknameStr))
							nicknameStr = theUser.fullname;
						this.NicknameLabel.Text = string.Format("ProposedBy_String".Localize(), nicknameStr);
						this.DatePayLabel.Text = String.Format(paymentStr.Localize(), theUser.firstname);
					});
				}

			});

			ActivityList.RegisterNibForCellReuse (UINib.FromName (ActivitySummaryCell.Key, NSBundle.MainBundle), ActivitySummaryCell.Key);
			ActivityList.Source = new DateActivityDataSource (theDate);
			ActivityList.RowHeight = 164;
			ActivityList.ReloadData ();

			UIBarButtonItem pinBtn = null, applyBtn = null;


			pinBtn = new UIBarButtonItem ("pin", UIBarButtonItemStyle.Bordered, (s, e) => {
				theDate.pinned = !theDate.pinned;
				if (theDate.pinned)
					pinBtn.Title = "unpin";
				else
					pinBtn.Title = "pin";
			});

			pinBtn.Width = 100;

			applyBtn = new UIBarButtonItem ("apply", UIBarButtonItemStyle.Plain, (s, e) => {


			});
			applyBtn.Width = 100;


			this.NavigationItem.SetRightBarButtonItems (new UIBarButtonItem[]{ pinBtn, applyBtn }, true);
			if (theDate.pinned)
				pinBtn.Title = "unpin";

		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
			ActivityListHeight.Constant = ActivityList.ContentSize.Height;
			CGSize theSize = ScrollingView.ContentSize;
			var theBottom = UserDescription.ConvertRectToView (UserDescription.Bounds, null).Bottom;
			theSize.Height = theBottom;
			ScrollingView.ContentSize = theSize;
		}

		public string AgeRaceGenderStr (UserRecord theUser) {
			DateTime today = DateTime.Now;
			int yearsOld = today.Year - theUser.dob.Year;

			string raceStr = LettuceServer.Instance.EthnicityName (theUser.ethnicity).Localize();
			string genderStr = LettuceServer.Instance.GenderName (theUser.gender).Localize();

			return String.Format ("AgeRaceGender_String".Localize(), yearsOld, raceStr, genderStr);
		}

		public string CityStateStr (UserRecord theUser) {
			return String.Format ("CityState_String".Localize(), theUser.city, theUser.state);
		}
	}
}

