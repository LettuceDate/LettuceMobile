
using System;

using Foundation;
using UIKit;
using Lettuce.Core;
using SDWebImage;

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

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			this.DateTitleLabel.Text = theDate.title;
			if (theDate.selfie != null)
				SelfieView.SetImage (new Uri (theDate.selfie));
			else
				SelfieHeight.Constant = 0;
			this.DateDescription.Text = theDate.description;
			this.DatePayLabel.Text = theDate.paymentStyle.ToString ();

		}
	}
}

