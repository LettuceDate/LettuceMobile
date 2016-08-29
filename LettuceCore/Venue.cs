
using System;

namespace Lettuce.Core
{
	public class Venue
	{
		public string id { get; set;}
		public string title { get; set;}
		public string image { get; set;}
		public double rating { get; set;}
		public string ratingImage { get; set;}
		public string address { get; set; }


		public Venue ()
		{
		}

		public Venue (Yelp.Business theBusiness)
		{
			id = theBusiness.id;
			title = theBusiness.name;
			image = theBusiness.image_url;
			rating = theBusiness.rating;
			ratingImage = theBusiness.rating_img_url_large;
			address = theBusiness.location.address[0];

		}

	}

}

