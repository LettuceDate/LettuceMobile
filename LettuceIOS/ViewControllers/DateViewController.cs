
using System;

using Foundation;
using UIKit;
using Lettuce.Core;


namespace Lettuce.IOS
{
	public partial class DateViewController : UITabBarController
	{
		private CurrentDatesViewController tab1;
		private MatchingDatesViewController tab2;
		private InterestedDatesViewController tab3;
		private PostedDatesViewController tab4;
		private nint targetPage;
		public static DateViewController Instance { get; set;}

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

			tab3 = new InterestedDatesViewController();
			tab3.TabBarItem = new UITabBarItem ("InterestedPeople_Tab".Localize(), UIImage.FromBundle ("AppliedDatesIcon"), 2);

			tab4 = new PostedDatesViewController();
			tab4.TabBarItem = new UITabBarItem ("PostedDates_Tab".Localize(), UIImage.FromBundle ("PostedDatesIcon"), 2);

			var tabs = new UIViewController[] {
				tab1, tab2, tab3, tab4
			};

			ViewControllers = tabs;
			Instance = this;
		}

		public void EditDate(MatchingDate theDate)
		{
			DateDetailViewController dateViewer = new DateDetailViewController ();
			if (dateViewer != null) {
				this.NavigationController.PushViewController (dateViewer, true);
				dateViewer.SetCurrentDate (theDate);
			}
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

