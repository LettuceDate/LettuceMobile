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
	}
}

