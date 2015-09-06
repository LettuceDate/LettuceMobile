using System;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Text;
using Android.Text.Style;
using Android.Provider;


using Android.Support.V7.Widget;
using Android.Support.V7.View;
using Android.Support.V7.AppCompat;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Graphics;
using Android.Media;

using Flurry.Analytics;


using Xamarin.Facebook;
using Xamarin.Facebook.AppEvents;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.Login.Widget;


[assembly:MetaData ("com.facebook.sdk.ApplicationId", Value ="@string/app_id")]
[assembly:MetaData ("com.facebook.sdk.ApplicationName", Value ="@string/app_name")]
namespace Lettuce.AndroidApp
{
	[Activity(Label = "Lettuce", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light", ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait )]
	public class MainActivity : Android.Support.V7.App.AppCompatActivity
	{
		private String[] mDrawerTitles = new string[] { "Home", "Browse", "Stats", "Profile"};
		private DrawerLayout mDrawerLayout;
		private ListView mDrawerList;
		private MyDrawerToggle mDrawerToggle;
		private bool refreshInProgress = false;
		private const string HOCKEYAPP_APPID = "cbfdd1d70cb71ce461a4e9532c52a18b";
		private const string FLURRY_APIKEY = "5N4WMCNN3W4PTV8GDMCX";

		ICallbackManager callbackManager;
		public LinearLayout loginView = null;
		ProfileTracker profileTracker;
		private ProgressDialog progressDlg;
		public static Typeface headlineFace;
		public static Typeface bodyFace;

		class MyDrawerToggle : Android.Support.V7.App.ActionBarDrawerToggle
		{
			private MainActivity baseActivity;

			public MyDrawerToggle(Activity activity, DrawerLayout drawerLayout, int openDrawerContentDescRes, int closeDrawerContentDescRes) :
			base(activity, drawerLayout, openDrawerContentDescRes, closeDrawerContentDescRes)
			{
				baseActivity = (MainActivity)activity;
			}
			public override void OnDrawerOpened(View drawerView)
			{
				base.OnDrawerOpened(drawerView);
				//baseActivity.Title = openString;


			}

			public override void OnDrawerClosed(View drawerView)
			{
				base.OnDrawerClosed(drawerView);
				//baseActivity.Title = closeString;
			}
		}

		class DrawerItemAdapter<T> : ArrayAdapter<T>
		{
			T[] _items;
			Activity _context;

			public DrawerItemAdapter(Context context, int textViewResourceId, T[] objects) :
			base(context, textViewResourceId, objects)
			{
				_items = objects;
				_context = (Activity)context;
			}

			public override View GetView(int position, View convertView, ViewGroup parent)
			{
				View mView = convertView;
				if (mView == null)
				{
					mView = _context.LayoutInflater.Inflate(Resource.Layout.DrawerListItem, parent, false);

				}

				TextView text = mView.FindViewById<TextView>(Resource.Id.ItemName);

				if (_items[position] != null)
				{
					text.Text = _items[position].ToString();
					text.SetTextColor(_context.Resources.GetColor(Resource.Color.Lettuce_dark_green));
					text.SetTypeface(MainActivity.bodyFace, TypefaceStyle.Bold);
				}

				return mView;
			}
		}

		protected override void OnCreate (Bundle bundle)
		{
			Window.SetFlags (WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);
			base.OnCreate (bundle);
			// Facebook SDK
			Flurry.Analytics.FlurryAgent.Init(this, FLURRY_APIKEY);
			FacebookSdk.SdkInitialize (this.ApplicationContext);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			loginView = FindViewById<LinearLayout>(Resource.Id.loginView);
			headlineFace = Typeface.CreateFromAsset(Assets, "fonts/RammettoOne-Regular.ttf");
			bodyFace = Typeface.CreateFromAsset(Assets, "fonts/SourceCodePro-Regular.ttf");

			// set up drawer
			mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			mDrawerList = FindViewById<ListView>(Resource.Id.left_drawer);
			// Set the adapter for the list view
			mDrawerList.Adapter = new DrawerItemAdapter<String>(this, Resource.Layout.DrawerListItem, mDrawerTitles);
			// Set the list's click listener
			mDrawerList.ItemClick += mDrawerList_ItemClick;

			mDrawerToggle = new MyDrawerToggle(this, mDrawerLayout, Resource.String.drawer_open, Resource.String.drawer_close);


			mDrawerLayout.SetDrawerListener(mDrawerToggle);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetHomeButtonEnabled(true);
			SupportActionBar.SetBackgroundDrawable(new Android.Graphics.Drawables.ColorDrawable( Resources.GetColor(Resource.Color.Lettuce_light_green)));

			loginView = FindViewById<LinearLayout>(Resource.Id.loginView);
			//CreateDirectoryForPictures();

			selectItem(0);


			// HockeyApp
			// Register the crash manager before Initializing the trace writer
			HockeyApp.CrashManager.Register (this, HOCKEYAPP_APPID); 

			//Register to with the Update Manager
			HockeyApp.UpdateManager.Register (this, HOCKEYAPP_APPID);

			// Initialize the Trace Writer
			HockeyApp.TraceWriter.Initialize ();

			// Wire up Unhandled Expcetion handler from Android
			AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) => 
			{
				// Use the trace writer to log exceptions so HockeyApp finds them
				HockeyApp.TraceWriter.WriteTrace(args.Exception);
				args.Handled = true;
			};

			// Wire up the .NET Unhandled Exception handler
			AppDomain.CurrentDomain.UnhandledException +=
				(sender, args) => HockeyApp.TraceWriter.WriteTrace(args.ExceptionObject);

			// Wire up the unobserved task exception handler
			TaskScheduler.UnobservedTaskException += 
				(sender, args) => HockeyApp.TraceWriter.WriteTrace(args.Exception);

			// Facebook SDK

			callbackManager = CallbackManagerFactory.Create ();

			var loginCallback = new FacebookCallback<LoginResult> {
				HandleSuccess = loginResult => {
					UpdateUI ();
				},
				HandleCancel = () => {
					ShowAlert (
						GetString (Resource.String.cancelled),
						GetString (Resource.String.permission_not_granted));


					UpdateUI ();                        
				},
				HandleError = loginError => {
					if (loginError is FacebookAuthorizationException) {
						ShowAlert (
							GetString (Resource.String.cancelled),
							GetString (Resource.String.permission_not_granted));
					}
					UpdateUI ();
				}
			};

			LoginManager.Instance.RegisterCallback (callbackManager, loginCallback);

			profileTracker = new CustomProfileTracker {
				HandleCurrentProfileChanged = (oldProfile, currentProfile) => {
					UpdateUI ();
				}
			};

			Android.Content.PM.PackageInfo siglist = this.PackageManager.GetPackageInfo("com.lettucedate.app", Android.Content.PM.PackageInfoFlags.Signatures);

			foreach (Android.Content.PM.Signature curSig in siglist.Signatures)
			{
				Java.Security.MessageDigest md = Java.Security.MessageDigest.GetInstance("SHA");
				md.Update(curSig.ToByteArray());
				string something = Base64.EncodeToString(md.Digest(), Base64Flags.Default);
				System.Console.WriteLine(something);
			}


			progressDlg = new ProgressDialog(this);
			progressDlg.SetProgressStyle(ProgressDialogStyle.Spinner);

			UpdateUI();

		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();

			profileTracker.StopTracking ();
		}


		protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
		{
			if (resultCode == Android.App.Result.Ok)
			{
				switch (requestCode) {

				// more cases go here...


				default:
					base.OnActivityResult (requestCode, resultCode, data);
					callbackManager.OnActivityResult (requestCode, (int)resultCode, data);
					break;
				}
			}
			else
				base.OnActivityResult(requestCode, resultCode, data);
		}


		private void UpdateUI ()
		{
			var enableButtons = AccessToken.CurrentAccessToken != null;


			var profile = Profile.CurrentProfile;
			ProfilePictureView pic = loginView.FindViewById<ProfilePictureView> (Resource.Id.profilePicture);

			if (enableButtons && profile != null) {
				pic.ProfileId = profile.Id;
				/*
				SupportActionBar.Show();
				actionPrompt.Visibility = ViewStates.Invisible;
				promptText.Visibility = ViewStates.Visible;
				PhotoTossRest.Instance.FacebookLogin(profile.Id, AccessToken.CurrentAccessToken.Token, (newUser) =>
					{
						if (newUser != null)
						{
							RunOnUiThread(() =>
								{
									loginView.Visibility = ViewStates.Gone;
									selectItem(0);
									homePage.Refresh();
								});

						}
					});
					*/

			} else {
				pic.ProfileId = null;
				/*
				SupportActionBar.Hide();
				loginView.Visibility = ViewStates.Visible;
				actionPrompt.Visibility = ViewStates.Visible;
				promptText.Visibility = ViewStates.Invisible;
				if (PhotoTossRest.Instance.CurrentUser != null)
					PhotoTossRest.Instance.Logout();
				*/
			}


		}


		void ShowAlert (string title, string msg, string buttonText = null)
		{
			new Android.Support.V7.App.AlertDialog.Builder (this)
				.SetTitle (title)
				.SetMessage (msg)
				.SetPositiveButton (buttonText, (s2, e2) => { })
				.Show ();
		}

		protected override void OnStart ()
		{
			base.OnStart ();
			FlurryAgent.OnStartSession(this, FLURRY_APIKEY);
		}

		protected override void OnStop ()
		{
			FlurryAgent.OnEndSession(this);
			base.OnStop ();
		}

		protected override void OnResume()
		{
			base.OnResume();
			AppEventsLogger.ActivateApp (this);
			UpdateFB ();
		}

		protected void UpdateFB()
		{
			if (AccessToken.CurrentAccessToken == null) {
				// better sign in
				//Intent	promptTask = new Intent (this, typeof(FirstRunActivity));
				//StartActivityForResult (promptTask, Utilities.SIGNIN_INTENT);

			} else {
				// already signed in
			}
		}

		protected override void OnPause()
		{
			base.OnPause();
			AppEventsLogger.DeactivateApp (this);
		}

		public override bool OnMenuOpened(int featureId, IMenu menu)
		{
			if (featureId == (int)WindowFeatures.ActionBar && menu != null)
			{
				try
				{
					var menuBuilder = JNIEnv.GetObjectClass(menu.Handle);
					var setOptionalIconsVisibleMethod = JNIEnv.GetMethodID(menuBuilder, "setOptionalIconsVisible",
						"(Z)V");
					JNIEnv.CallVoidMethod(menu.Handle, setOptionalIconsVisibleMethod, new[] { new JValue(true) });

				}
				catch (Exception e)
				{
					System.Console.WriteLine (e.Message);
				}
			}
			return base.OnMenuOpened(featureId, menu);
		}



		protected override void OnPostCreate(Bundle savedInstanceState)
		{
			base.OnPostCreate(savedInstanceState);
			mDrawerToggle.SyncState();
		}

		public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged(newConfig);
			mDrawerToggle.OnConfigurationChanged(newConfig);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (mDrawerToggle.OnOptionsItemSelected(item))
			{
				return true;
			}
			else
			{ 
				switch (item.ItemId)
				{
				/*
				case Resource.Id.PhotoButton:
					TakeAPicture();
					return true;
					break;
				case Resource.Id.CatchButton:
					CatchAPicture();
					return true;
					break;
				case Resource.Id.AboutBtn:
					break;
				case Resource.Id.SettingsBtn:
					break;
*/
				default:
					// show never get here.
					break;
				}
			}
			// Handle your other action bar items...

			return base.OnOptionsItemSelected(item);
		}

		void mDrawerList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			selectItem(e.Position);
		}

		private Android.Support.V4.App.Fragment oldPage = null;

		private void selectItem(int position)
		{
			Android.Support.V4.App.Fragment newPage = null;
			var fragmentManager = this.SupportFragmentManager;
			var ft = fragmentManager.BeginTransaction();
			bool firstTime = false;
			string pageName = "";

			/*
			switch (position)
			{
			case 0:
				if (homePage == null)
				{
					homePage = new HomeFragment();
					homePage.MainPage = this;
					firstTime = true;
				}
				newPage = homePage;
				pageName = "PhotoToss";
				break;
			case 1:
				if (browsePage == null)
				{
					browsePage = new BrowseFragment();
					browsePage.MainPage = this;
					firstTime = true;
				}
				newPage = browsePage;
				break;
			case 2:
				if (statsPage == null)
				{
					statsPage = new StatsFragment();
					statsPage.MainPage = this;
					firstTime = true;
				}
				newPage = statsPage;
				break;
			case 3:
				if (profilePage == null)
				{
					profilePage = new ProfileFragment();
					profilePage.MainPage = this;
					firstTime = true;
				}
				newPage = profilePage;
				break;
			}
			*/

			if (oldPage != newPage)
			{
				if (oldPage != null)
				{
					// to do - deactivate it
					ft.Hide(oldPage);

				}

				oldPage = newPage;

				if (newPage != null)
				{
					if (firstTime)
						ft.Add(Resource.Id.fragmentContainer, newPage);
					else
						ft.Show(newPage);
				}

				ft.Commit();

				// update selected item title, then close the drawer
				if (!String.IsNullOrEmpty(pageName))
					Title = pageName;
				else
					Title = mDrawerTitles[position];

				mDrawerList.SetItemChecked(position, true);
				mDrawerLayout.CloseDrawer(mDrawerList);
			}
		}

		protected override void OnTitleChanged(Java.Lang.ICharSequence title, Android.Graphics.Color color)
		{
			//base.OnTitleChanged (title, color);
			this.SupportActionBar.Title = title.ToString();

			SpannableString s = new SpannableString(title);
			s.SetSpan(new TypefaceSpan(this, "RammettoOne-Regular.ttf"), 0, s.Length(), SpanTypes.ExclusiveExclusive);
			s.SetSpan(new ForegroundColorSpan(Resources.GetColor(Resource.Color.Lettuce_red)), 0, s.Length(), SpanTypes.ExclusiveExclusive);

			this.SupportActionBar.TitleFormatted = s;



		}

		public void StartRefresh(Action callback = null)
		{
			if (!refreshInProgress)
			{
				refreshInProgress = true;

				RunOnUiThread(() =>
					{
						/*
						if (homePage != null)
							homePage.Refresh();
						if (browsePage != null)
							browsePage.Refresh();
						if (statsPage != null)
							statsPage.Refresh();
						if (profilePage != null)
							profilePage.Refresh();
						*/
						if (callback != null)
							callback();
						refreshInProgress = false;
					});


			}
		}
	}

	class FacebookCallback<TResult> : Java.Lang.Object, IFacebookCallback where TResult : Java.Lang.Object
	{
		public Action HandleCancel { get; set; }
		public Action<FacebookException> HandleError { get; set; }
		public Action<TResult> HandleSuccess { get; set; }

		public void OnCancel ()
		{
			var c = HandleCancel;
			if (c != null)
				c ();
		}

		public void OnError (FacebookException error)
		{
			var c = HandleError;
			if (c != null)
				c (error);
		}

		public void OnSuccess (Java.Lang.Object result)
		{
			var c = HandleSuccess;
			if (c != null)
				c (result.JavaCast<TResult> ());
		}
	}

	class CustomProfileTracker : ProfileTracker
	{
		public delegate void CurrentProfileChangedDelegate (Profile oldProfile, Profile currentProfile);

		public CurrentProfileChangedDelegate HandleCurrentProfileChanged { get; set; }

		protected override void OnCurrentProfileChanged (Profile oldProfile, Profile currentProfile)
		{
			var p = HandleCurrentProfileChanged;
			if (p != null)
				p (oldProfile, currentProfile);
		}
	}
}


