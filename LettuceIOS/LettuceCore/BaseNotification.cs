using System;

namespace Lettuce.Core
{
	public class BaseNotification
	{
		public long id;
		public DateTime	date;
		public string	detail;
		public int		type;
		public bool		read;

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

