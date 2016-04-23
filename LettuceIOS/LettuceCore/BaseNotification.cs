using System;

namespace Lettuce.Core
{
	public class BaseNotification
	{
		public long id { get; set;}
		public DateTime	date { get; set;}
		public string	detail { get; set;}
		public int		type { get; set;}
		public bool		read { get; set;}

		public BaseNotification ()
		{
		}

		public static BaseNotification CreateSample() {
			BaseNotification newGuy = new BaseNotification();
			newGuy.date = DateTime.Now;
			newGuy.id = 1;
			newGuy.detail = "Hey you were notified of something";
			newGuy.type = 2;
			newGuy.read = false;

			return newGuy;
		}
	}
}

