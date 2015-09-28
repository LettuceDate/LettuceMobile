
using System;

using Foundation;
using UIKit;
using Lettuce.Core;
using Lettuce.Core.Yelp;
using ServiceStack.Text;

namespace Lettuce.IOS
{
	public partial class NewActivityController : UIViewController
	{
		public Activity newActivity { get; set;}
		private YelpResultTableSource dataSource;
		private NSObject keyWatcher;
		public delegate void ActivityCreatedHandler(Lettuce.Core.Activity newActivity);
		public event ActivityCreatedHandler ActivityCreated;


		public NewActivityController () : base ("NewActivityController", null)
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
			newActivity = new Lettuce.Core.Activity ();

			// Perform any additional setup after loading the view, typically from a nib.
			ChooseTypeBtn.TouchUpInside += (object sender, EventArgs e) => {
				// to do - show new touch'
				UIAlertController actionSheet = UIAlertController.Create ("Activity Type", "What do you feel like doing?", UIAlertControllerStyle.ActionSheet);
				foreach (int curTypeId in LettuceServer.Instance.ActivityTypes.Keys) {
					ActivityType curType = LettuceServer.Instance.ActivityType(curTypeId);
					actionSheet.AddAction (UIAlertAction.Create (curType.typename, UIAlertActionStyle.Default, (action) => SetActivityType (curType.id)));
				}
				actionSheet.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null));

				this.PresentViewController (actionSheet, true, null);
			};

			CancelBtn.TouchUpInside += (object sender, EventArgs e) => {
				DismissViewController (true, null);
			};

			ChooseBtn.TouchUpInside += (object sender, EventArgs e) => {
				DismissViewController (true, null);
				if (ActivityCreated != null)
					ActivityCreated(newActivity);
			};

			SearchBtn.TouchUpInside += (object sender, EventArgs e) => { DoYelpSearch(); };

			SearchField.ShouldReturn = (theField) => {
				theField.ResignFirstResponder();
				DoYelpSearch();
				return false;
			};

			keyWatcher = NSNotificationCenter.DefaultCenter.AddObserver (UITextView.TextDidChangeNotification, (notification) => {
				newActivity.description = ActivityDescription.Text;
				ActivityChanged();
			});

			ResultTable.RegisterNibForCellReuse (UINib.FromName (YelpResultCell.Key, NSBundle.MainBundle), YelpResultCell.Key);
			dataSource = new YelpResultTableSource (this);
			ResultTable.Source = dataSource;

			ActivityChanged ();
		}
			
		private void SetActivityType(int newType)
		{
			newActivity.type = newType;
			newActivity.duration = 90;
			ChooseTypeBtn.SetTitle (LettuceServer.Instance.ActivityType (newType).typename, UIControlState.Normal);
			ActivityChanged ();

		}

		public override void ViewWillUnload ()
		{
			base.ViewWillUnload ();
			if (keyWatcher != null)
				NSNotificationCenter.DefaultCenter.RemoveObserver (keyWatcher);
		}
		private void DoYelpSearch()
		{
			string searchStr = SearchField.Text;
			YelpAPI yelp = new YelpAPI ();

			string resultStr = yelp.Search (SearchField.Text, "Beverly Hills, CA");

			YelpResults resultSet = resultStr.FromJson<YelpResults> ();
			dataSource.resultSet = resultSet;
			ResultTable.ReloadData ();

		}

		public void ActivityChanged()
		{
			InvokeOnMainThread (() => {
				if ((!String.IsNullOrEmpty (newActivity.description)) &&
				   (newActivity.duration > 0) &&
				   (!String.IsNullOrEmpty (newActivity.venueid))) {
					ChooseBtn.Enabled = true;
				} else {
					ChooseBtn.Enabled = false;
				}
			});
		}
			
	}
}

