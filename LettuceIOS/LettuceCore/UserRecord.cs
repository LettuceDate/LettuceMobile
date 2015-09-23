
using System;

namespace Lettuce.Core
{
	public class UserRecord
	{
		public long id { get; set;}
		public string nickname { get; set;}
		public string firstname { get; set;}
		public string lastname { get; set;}
		public DateTime dob { get; set;}
		public string facebookid { get; set;}
		public int ethnicity { get; set;}
		public int gender { get; set;}

		public UserRecord ()
		{
		}
	}
}

