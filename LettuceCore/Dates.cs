using System;
using System.Collections.Generic;

namespace Lettuce.Core
{

	public class BaseDate
	{
		public long id { get; set; }
		public long proposerid { get; set; }
		public string title { get; set; }
		public string startTimeStr { get; set; }
		public string description { get; set; }
		public bool active {get; set;}
		public string selfie { get; set; }
		public int paymentStyle { get; set; }
		public List<Activity> activities { get; set; }

		public BaseDate()
		{
			
		}


		public DateTime starttime {
			get {
				if (String.IsNullOrEmpty (startTimeStr))
					return DateTime.UtcNow;
				else {
					DateTime newDate = DateTime.ParseExact (startTimeStr, "yyyy-MM-ddTHH:mm:ssK", System.Globalization.CultureInfo.InvariantCulture);
					return newDate;
				}
			}

			set {
				string dateStr = value.ToString ("yyyy-MM-ddTHH:mm:ssK", System.Globalization.CultureInfo.InvariantCulture);
				startTimeStr = dateStr;
			}
		}

	}
		


	public class ProposedDate : BaseDate
	{
		public ProposedDate ()
		{
		}
	}


	public class MatchingDate : BaseDate
	{
		public bool applied { get; set; }
		public bool pinned { get; set; }
		public int status { get; set; }

		public MatchingDate ()
		{
			applied = false;
			pinned = false;
		}
	}


	public class CommittedDate : BaseDate
	{
		public CommittedDate ()
		{
		}

		public static CommittedDate GenerateTest()
		{
			CommittedDate newDate = new CommittedDate();
			newDate.title = "This is a date title";

			return newDate;
		}

		public static List<CommittedDate> GenerateTestList(int numItems)
		{
			List<CommittedDate> dateList = new List<CommittedDate> ();

			for (int i = 0; i < numItems; i++) {
				CommittedDate newDate = GenerateTest ();
				newDate.title = string.Format ("This is date {0}", i + 1);
				dateList.Add (newDate);
			}

			return dateList;
		}
	}

	public class AppliedDate : BaseDate
	{
		public AppliedDate ()
		{
		}
	}
}

