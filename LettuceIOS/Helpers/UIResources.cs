using System;

using UIKit;

using CoreGraphics;
using Foundation;

using ObjCRuntime;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Lettuce.IOS
{
	public class LettuceColor
	{
		private static UIColor _pinkStandard = UIColor.FromRGB(216,27,97);
		private static UIColor _purpleStandard = UIColor.FromRGB(134,59,150);
		private static UIColor _blueStandard = UIColor.FromRGB(57,73,171);
		private static UIColor _greenStandard = UIColor.FromRGB(67,160,71);
		private static UIColor _yellowStandard = UIColor.FromRGB(251,176,59);
		private static UIColor _orangeStandard = UIColor.FromRGB(255,87,34);

		private static UIColor _pinkLight = UIColor.FromRGB(210,152,176);
		private static UIColor _purpleLight = UIColor.FromRGB(184,163,255);
		private static UIColor _blueLight = UIColor.FromRGB(167,170,200);
		private static UIColor _greenLight = UIColor.FromRGB(182,202,172);
		private static UIColor _yellowLight = UIColor.FromRGB(234,230,173);
		private static UIColor _orangeLight = UIColor.FromRGB(223,191,163);

		private static UIColor _blackStandard = UIColor.FromRGB(0,0,0);
		private static UIColor _whiteStandard = UIColor.FromRGB(255,255,255);

		private static UIColor _girlPink = UIColor.FromRGB(230, 0, 76);
		private static UIColor _boyBlue = UIColor.FromRGB(37, 175, 177);



		public LettuceColor ()
		{
		}

		public static UIColor GirlPink { get { return _girlPink; } }
		public static UIColor BoyBlue { get { return _boyBlue; } }

		public static UIColor Pink {get { return _pinkStandard; }}
		public static UIColor Purple {get { return _purpleStandard; }}
		public static UIColor Blue {get { return _blueStandard; }}
		public static UIColor Green {get { return _greenStandard; }}
		public static UIColor Yellow {get { return _yellowStandard; }}
		public static UIColor Orange {get { return _orangeStandard; }}

		public static UIColor LightPink {get { return _pinkLight; }}
		public static UIColor LightPurple {get { return _purpleLight; }}
		public static UIColor LightBlue {get { return _blueLight; }}
		public static UIColor LightGreen {get { return _greenLight; }}
		public static UIColor LightYellow {get { return _yellowLight; }}
		public static UIColor LightOrange {get { return _orangeLight; }}

		public static UIColor Black {get { return _blackStandard; }}
		public static UIColor White {get { return _whiteStandard; }}
	}

	public static class LocalizationExtensions
	{
		public static string Localize(this string key)
		{
			return NSBundle.MainBundle.LocalizedString(key, null);
		}
	}

	public static class NSStringExtensions
	{
		[DllImport(Constants.ObjectiveCLibrary, EntryPoint="objc_msgSend")]
		private extern static IntPtr IntPtr_objc_msgSend_IntPtr (IntPtr receiver, IntPtr selector, IntPtr arg1);

		public static NSString LocalizedStringWithFormat (this NSString @string)
		{
			var nsHandle = IntPtr_objc_msgSend_IntPtr (Class.GetHandle (typeof(NSString)), Selector.GetHandle ("localizedStringWithFormat:"), @string.Handle);
			var nsstring = Runtime.GetNSObject<NSString> (nsHandle);
			return nsstring;
		}
	}
}

