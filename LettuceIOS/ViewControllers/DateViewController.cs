
using System;

using Foundation;
using UIKit;

namespace Lettuce.IOS
{
	public partial class DateViewController : UITabBarController
	{
		private CurrentDatesViewController tab1;
		private MatchingDatesViewController tab2;
		private AppliedDatesViewController tab3;
		private InterestedDatesViewController tab4;
		private nint targetPage;

		public DateViewController () : base ("DateViewController", null)
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
			tab1 = new CurrentDatesViewController();
			tab1.TabBarItem = new UITabBarItem ("CurrentDates_Tab".Localize(), UIImage.FromBundle ("CurrentDatesIcon"), 0);


			tab2 = new MatchingDatesViewController();
			tab2.TabBarItem = new UITabBarItem ("MatchingDates_Tab".Localize(), UIImage.FromBundle ("MatchingDatesIcon"), 1);
			tab2.View.BackgroundColor = UIColor.Orange;

			tab3 = new AppliedDatesViewController();
			tab3.TabBarItem = new UITabBarItem ("InterestedPeople_Tab".Localize(), UIImage.FromBundle ("AppliedDatesIcon"), 2);
			tab3.View.BackgroundColor = UIColor.Red;

			tab4 = new InterestedDatesViewController();
			tab4.TabBarItem = new UITabBarItem ("PostedDates_Tab".Localize(), UIImage.FromBundle ("PostedDatesIcon"), 2);
			tab4.View.BackgroundColor = UIColor.Red;


			var tabs = new UIViewController[] {
				tab1, tab2, tab3, tab4
			};

			ViewControllers = tabs;
		}

		public override void ViewWillAppear (bool animated)
		{
			if (targetPage > 0) {
				this.SelectedIndex = targetPage-1;
			}
			targetPage = 0;
					
			base.ViewWillAppear (animated);
		}

		public void SetCurrentPage(nint whichTab)
		{
			targetPage = whichTab;
		}
	}
}

