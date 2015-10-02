
using System;

using Foundation;
using UIKit;
using Lettuce.Core;

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
			TopConstraint.Constant = HomeViewController.LayoutGuideSize;
		}

		public void SetCurrentDate(MatchingDate theDate) 
		{
			this.theDate = theDate;
		}
	}
}

