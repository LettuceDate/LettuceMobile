using System;
using System.Collections.Generic;

namespace Lettuce.Core
{

	public class BaseDate
	{
		public string Title { get; set; }

		public BaseDate()
		{

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
		public MatchingDate ()
		{
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
			newDate.Title = "This is a date title";

			return newDate;
		}

		public static List<CommittedDate> GenerateTestList(int numItems)
		{
			List<CommittedDate> dateList = new List<CommittedDate> ();

			for (int i = 0; i < numItems; i++) {
				CommittedDate newDate = GenerateTest ();
				newDate.Title = string.Format ("This is date {0}", i + 1);
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

