using Foundation;
using UIKit;
using Flurry.Analytics;
using HockeyApp;
using System;
using System.Threading.Tasks;
using Facebook.CoreKit;
using System.Collections.Generic;


namespace Lettuce.IOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// Various app keys - Facebook, Flurry, HockeyApp
		private string FlurryKey = "5YW4HP9W2P8YMQWRGQB2";
		private string HockeyID = "13567c35d7940036ee8035d18ecd04a3";
		private string FacebookAppID = "822825007835530";
		private string FacebookAppName = "Lettuce";
		public UINavigationController NavigationController {get; set;}
		public static ProfileViewController ProfileController { get; set; }

		// class-level declarations

		public override UIWindow Window {
			get;
			set;
		}

		public RootViewController RootViewController { get { return Window.RootViewController as RootViewController; } }


		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			// Code to start the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			// Flurry
			FlurryAgent.StartSession(FlurryKey);

			// HockeyApp
			//We MUST wrap our setup in this block to wire up
			// Mono's SIGSEGV and SIGBUS signals
			HockeyApp.Setup.EnableCustomCrashReporting (() => {

				//Get the shared instance
				var manager = BITHockeyManager.SharedHockeyManager;

				//Configure it to use our APP_ID
				manager.Configure (HockeyID);

				//Start the manager
				manager.StartManager ();

				//Authenticate (there are other authentication options)
				manager.Authenticator.AuthenticateInstallation ();

				//Rethrow any unhandled .NET exceptions as native iOS 
				// exceptions so the stack traces appear nicely in HockeyApp
				AppDomain.CurrentDomain.UnhandledException += (sender, e) => 
					Setup.ThrowExceptionAsNative(e.ExceptionObject);

				TaskScheduler.UnobservedTaskException += (sender, e) => 
					Setup.ThrowExceptionAsNative(e.Exception);
			});

			// Facebooke

			Profile.EnableUpdatesOnAccessTokenChange (true);
			Settings.AppID = FacebookAppID;
			Settings.DisplayName = FacebookAppName;


			//create the initial view controller
			ConfigNavMenu ();

			// This method verifies if you have been logged into the app before, and keep you logged in after you reopen or kill your app.
			return ApplicationDelegate.SharedInstance.FinishedLaunching (application, launchOptions);
		}

		private void ConfigNavMenu()
		{
			Window = new UIWindow(UIScreen.MainScreen.Bounds);
			Window.BackgroundColor = LettuceColor.BoyBlue;

			// If you have defined a root view controller, set it here:
			Window.RootViewController = new RootViewController();

			// make the window visible
			Window.MakeKeyAndVisible();


		}

		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			// We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
			return ApplicationDelegate.SharedInstance.OpenUrl (application, url, sourceApplication, annotation);
		}

		public override void OnResignActivation (UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground (UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground (UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated (UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate (UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}
	}
}


