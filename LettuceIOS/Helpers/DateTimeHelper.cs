﻿using System;
using Foundation;

namespace Lettuce.IOS
{
	public static class DateTimeHelper
	{
		public static DateTime NSDateToDateTime(this NSDate date)
		{
			// NSDate has a wider range than DateTime, so clip
			// the converted date to DateTime.Min|MaxValue.
			double secs = date.SecondsSinceReferenceDate;
			if (secs < -63113904000)
				return DateTime.MinValue;
			if (secs > 252423993599)
				return DateTime.MaxValue;
			return (DateTime) date;
		}

		public static NSDate DateTimeToNSDate(this DateTime date)
		{
			if (date.Kind == DateTimeKind.Unspecified)
				date = DateTime.SpecifyKind (date, DateTimeKind.Utc);
			return (NSDate) date;
		}
	}
}

