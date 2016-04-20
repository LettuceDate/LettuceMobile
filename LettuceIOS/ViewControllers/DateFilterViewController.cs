using System;

using UIKit;

namespace Lettuce.IOS
{
	public partial class DateFilterViewController : UIViewController
	{
		public DateFilterViewController () : base ("DateFilterViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			CancelBtn.TouchUpInside += (object sender, EventArgs e) => {
				DismissModalViewController(true);
			};

			ApplyBtn.TouchUpInside += (object sender, EventArgs e) => {
				DismissModalViewController(true);
			};
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}


