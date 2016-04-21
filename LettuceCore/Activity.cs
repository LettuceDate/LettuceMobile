
using System;

namespace Lettuce.Core
{
	public class Activity
	{
		public long id { get; set;}
		public string description { get; set;}
		public int type { get; set;}
		public string venueid { get; set;}
		public int duration { get; set;}


		public Activity ()
		{
		}
	}

	public class ActivityType
	{
		public int id {get; set;}
		public string typename {get; set;}
		public string icon { get; set;}

		public static ActivityType CreateSample(int theId, string typeName) {
			ActivityType newGuy = new ActivityType();
			newGuy.id = theId;
			newGuy.typename = typeName;

			return newGuy;
		}
	}
}

