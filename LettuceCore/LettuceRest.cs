using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Text;
using RestSharp;
using System.Runtime.Serialization;
using ServiceStack.Text;
using System.Threading.Tasks;

#if ANDROID
using Xamarin.Facebook;
#elif IOS
using Facebook;
#endif

namespace Lettuce.Core
{
	public delegate void string_callback(String theResult);    
	public delegate void int_callback(int theResult);    
	public delegate void Venue_callback(Venue theResult);
	public delegate void null_callback();
	public delegate void BaseDate_callback(BaseDate theDate);
	public delegate void BaseDateList_callback(List<BaseDate> dateList);
	public delegate void MatchingDateList_callback(List<MatchingDate> dateList);
	public delegate void UserRecord_callback(UserRecord theRec);
	public delegate void NotificationList_callback(List<BaseNotification> notifyList);
	public delegate void CommittedDateList_callback(List<CommittedDate> theResult);




    public class LettuceServer
    {
        private RestClient apiClient;
		private static LettuceServer _singleton = null;
		private static string localHostStr = "http://localhost:8080/api/v1";
		private static string networkHostStr = "http://192.168.0.4:8080/api/v1";
		private static string productionHostStr = "http://lettuce-1045.appspot.com/api/v1";
		private string apiPath =   networkHostStr;
        private string _uploadURL;
        private string _catchURL;
        private string _userImageURL;
		private UserRecord _currentUser;
		private Dictionary<int, ActivityType>	_activityTypes = new Dictionary<int, ActivityType>();
		private Dictionary<string, Venue>	_venueList = new Dictionary<string, Venue>();
		private Dictionary<int, string>	_genderNames = new Dictionary<int, string>();
		private Dictionary<int, string>	_ethnicityNames = new Dictionary<int, string>();
		    
		public LettuceServer()
        {
            System.Console.WriteLine("Using Production Server");
            apiClient = new RestClient(apiPath);
            apiClient.CookieContainer = new CookieContainer();
			Initialize ();
        }


		private void Initialize()
		{

			InitActivityNames (() =>
				{
					InitGenderNames (() =>
						{
							InitEthnicityNames (() =>
								{
									Console.WriteLine("Static Tables Initialized");
								});
						});
				});
		}

		public static LettuceServer Instance
        {
            get
            {
                if (_singleton == null)
					_singleton = new LettuceServer();
                return _singleton;
            }
        }

		public ActivityType ActivityType(int activityTypeId)
		{
			if (_activityTypes.ContainsKey (activityTypeId))
				return _activityTypes [activityTypeId];
			else
				return null;

		}

		public Dictionary<int, ActivityType> ActivityTypes {
			get { return _activityTypes; }
		}

		public Venue Venue(string venueId)
		{
			if (_venueList.ContainsKey (venueId))
				return _venueList [venueId];
			else
				return null;

		}

		public string EthnicityName(int whichEthnicity)
		{
			return _ethnicityNames [whichEthnicity] + "_Ethnicity";
		}

		public string GenderName(int whichGender)
		{
			return _genderNames [whichGender] + "_Gender";
		}

		public async Task<Venue> LoadVenue(string venueId)
		{
			Yelp.YelpAPI yelp = new Yelp.YelpAPI ();
			Yelp.Business result = null;

			try
			{
				string resultStr = await yelp.GetBusinessAsync (venueId);
				result = resultStr.FromJson<Yelp.Business> ();
			}
			catch (Exception exp)
			{
				Console.WriteLine("error fetching yelp business: " + exp.Message);
			}

			if (result == null) {
				// create placeholder
				result = Yelp.Business.CreateSample ();
			}

			// return venue from result
			var newVenue = new Venue (result);
			_venueList [newVenue.id] = newVenue;
			return newVenue;
		}

		public void GetUserInfo(long userId, UserRecord_callback callback)
		{
			string fullURL = "user/profile";

			RestRequest request = new RestRequest(fullURL, Method.GET);
			request.AddParameter ("id", userId);

			apiClient.ExecuteAsync<UserRecord>(request, (response) =>
				{
					UserRecord newUser = response.Data;
					if (newUser!= null)
					{
						callback(newUser);
					}
					else
						callback(null);
				});
		}


		private void InitActivityNames(null_callback callback)
		{
			string fullURL = "admin/activitynames";

			RestRequest request = new RestRequest(fullURL, Method.GET);

			apiClient.ExecuteAsync<List<ActivityType>>(request, (response) =>
				{
					List<ActivityType> map = response.Data;

					if (map != null) {
						foreach (ActivityType curType in map)
						{
							_activityTypes[curType.id] = curType;
						}
					}
					else {
						_activityTypes.Clear();
						_activityTypes.Add(1, Lettuce.Core.ActivityType.CreateSample(1, "coffee"));
						_activityTypes.Add(2, Lettuce.Core.ActivityType.CreateSample(2, "lunch"));
						_activityTypes.Add(3, Lettuce.Core.ActivityType.CreateSample(3, "dinner"));
					}

					if (callback != null)
						callback();
				});

		}

		private void InitGenderNames(null_callback callback)
		{
			string fullURL = "admin/gendernames";

			RestRequest request = new RestRequest(fullURL, Method.GET);

			apiClient.ExecuteAsync<List<GenderType>>(request, (response) =>
				{
					List<GenderType> map = response.Data;

					if (map != null) {
						foreach (GenderType curType in map)
						{
							_genderNames[curType.id] = curType.typename;
						}
					}
					else {
						_genderNames.Clear();
					}

					if (callback != null)
						callback();
				});

		}

		private void InitEthnicityNames(null_callback callback)
		{
			string fullURL = "admin/ethnicitynames";

			RestRequest request = new RestRequest(fullURL, Method.GET);

			apiClient.ExecuteAsync<List<EthnicityType>>(request, (response) =>
				{
					List<EthnicityType> map = response.Data;

					if (map != null) {
						foreach (EthnicityType curType in map)
						{
							_ethnicityNames[curType.id] = curType.typename;
						}
					}
					else {
						_ethnicityNames.Clear();
					}

					if (callback != null)
						callback();
				});

		}

		// GetNotificationsForUser
		public void GetNotificationsForUser(NotificationList_callback callback)
		{
			string fullURL = "notifications";

			RestRequest request = new RestRequest(fullURL, Method.GET);

			apiClient.ExecuteAsync<List<BaseNotification>>(request, (response) =>
				{
					List<BaseNotification> newNotifications = response.Data;
					if (newNotifications!= null)
					{
						callback(newNotifications);
					}
					else {
						newNotifications = new List<BaseNotification>();
						newNotifications.Add(BaseNotification.CreateSample());
						newNotifications.Add(BaseNotification.CreateSample());
						newNotifications.Add(BaseNotification.CreateSample());
						callback(newNotifications);
					}
				});
		}

		public void GetNotificationCountForUser(int_callback callback)
		{
			string fullURL = "notifications";

			RestRequest request = new RestRequest(fullURL, Method.GET);

			request.AddParameter ("count", true);
			apiClient.ExecuteAsync<int>(request, (response) =>
				{
					if (response.ResponseStatus == ResponseStatus.Completed)
					{
						int newCount = response.Data;
						callback(newCount);
					}
				});
		}

		public void GetMatchingDatesForUser(MatchingDateList_callback callback)
		{
			string fullURL = "date";

			RestRequest request = new RestRequest(fullURL, Method.GET);
			request.AddParameter ("matches", true);

			apiClient.ExecuteAsync<List<MatchingDate>>(request, (response) =>
				{
					List<MatchingDate> newDates = response.Data;
					if (newDates!= null)
					{
						callback(newDates);
					}
					else
						callback(null);
				});
		}

		public void CountMatchingDatesForUser(int_callback callback)
		{
			string fullURL = "date";

			RestRequest request = new RestRequest(fullURL, Method.GET);
			request.AddParameter ("matches", true);
			request.AddParameter ("count", true);
			apiClient.ExecuteAsync<int>(request, (response) =>
				{
					int newCount = response.Data;
					callback(newCount);
				});
		}

		public void GetBookedDatesForUser(BaseDateList_callback callback)
		{
			string fullURL = "date";

			RestRequest request = new RestRequest(fullURL, Method.GET);
			request.AddParameter ("booked", true);

			apiClient.ExecuteAsync<List<BaseDate>>(request, (response) =>
				{
					List<BaseDate> newDate = response.Data;
					if (newDate != null)
					{
						callback(newDate);
					}
					else
						callback(null);
				});
		}

		public void CountBookedDatesForUser(int_callback callback)
		{
			string fullURL = "date";

			RestRequest request = new RestRequest(fullURL, Method.GET);
			request.AddParameter ("booked", true);
			request.AddParameter ("count", true);
			apiClient.ExecuteAsync<int>(request, (response) =>
				{
					int newCount = response.Data;
					callback(newCount);
				});
		}

		public void GetUsersOwnDates(BaseDateList_callback callback)
		{
			string fullURL = "date";

			RestRequest request = new RestRequest(fullURL, Method.GET);

			apiClient.ExecuteAsync<List<BaseDate>>(request, (response) =>
				{
					List<BaseDate> newDate = response.Data;
					if (newDate != null)
					{
						callback(newDate);
					}
					else
						callback(null);
				});
		}

		public void CountUsersOwnDates(int_callback callback)
		{
			string fullURL = "date";

			RestRequest request = new RestRequest(fullURL, Method.GET);
			request.AddParameter ("count", true);
			apiClient.ExecuteAsync<int>(request, (response) =>
				{
					int newCount = response.Data;
					callback(newCount);
				});
		}


		public void GetInterestedUsers(BaseDateList_callback callback)
		{
			string fullURL = "date";

			RestRequest request = new RestRequest(fullURL, Method.GET);

			apiClient.ExecuteAsync<List<BaseDate>>(request, (response) =>
				{
					List<BaseDate> newDate = response.Data;
					if (newDate != null)
					{
						callback(newDate);
					}
					else
						callback(null);
				});
		}

		public void CountInterestedUsers(int_callback callback)
		{
			string fullURL = "date";

			RestRequest request = new RestRequest(fullURL, Method.GET);
			request.AddParameter ("count", true);
			apiClient.ExecuteAsync<int>(request, (response) =>
				{
					int newCount = response.Data;
					callback(newCount);
				});
		}



		public void CreateDate(BaseDate theDate, BaseDate_callback callback)
		{
			string fullURL = "date";

			RestRequest request = new RestRequest(fullURL, Method.POST);
			request.AddParameter("date", theDate.ToJson());

			apiClient.ExecuteAsync<BaseDate>(request, (response) =>
				{
					BaseDate newDate = response.Data;
					if (newDate != null)
					{
						callback(newDate);
					}
					else
						callback(null);
				});
		}



		public void FacebookLogin(string userId, string token, UserRecord_callback callback)
		{
			string fullURL = "user/facebooklogin";

			RestRequest request = new RestRequest(fullURL, Method.POST);
			//request.AddHeader("Content-Type", "application/json; charset=utf-8");
			//request.RequestFormat = DataFormat.Json;
			request.AddParameter("id", userId);
			request.AddParameter("token", token);


			apiClient.ExecuteAsync<UserRecord>(request, (response) =>
				{
					UserRecord newUser = response.Data;
					if (newUser != null)
					{
						_currentUser = newUser;
						callback(newUser);
					}
					else
						callback(null);
				});
		}


		public void GetCurrentDates(CommittedDateList_callback callback)
		{
			if (callback != null)
				callback(CommittedDate.GenerateTestList(new Random().Next(5,20)));

		}


        public UserRecord CurrentUser
        {
            get { return _currentUser; }
        }

        public string GetUserProfileImage(string username)
        {
            if (!String.IsNullOrEmpty(username))
             return "https://graph.facebook.com/" + username + "/picture?type=square";
            else
                return "https://s3-us-west-2.amazonaws.com/app.goheard.com/images/unknown-user.png";
        }

        

        public void Logout()
        {
            string fullURL = "user/logout";

            RestRequest request = new RestRequest(fullURL, Method.POST);

            apiClient.Execute(request);

            _currentUser = null;
        }



        
  

        public void GetUploadURL(string_callback callback)
        {
            string fullURL = "image/upload";

            RestRequest request = new RestRequest(fullURL, Method.GET);

            apiClient.ExecuteAsync(request, (response) =>
            {
                _uploadURL = response.Content;
                callback(_uploadURL);
            });

        }




        public void UploadImage(Stream photoStream, double longitude, double latitude, string_callback callback)
        {
            RestClient onetimeClient = new RestClient(_uploadURL);
            onetimeClient.CookieContainer = apiClient.CookieContainer;

            var request = new RestRequest("", Method.POST);
            request.AddHeader("Accept", "*/*");
            //request.AlwaysMultipartFormData = true;
            request.AddParameter("long", longitude);
            request.AddParameter("lat", latitude);
            request.AddFile("file", ReadToEnd(photoStream), "file", "image/jpeg");

            onetimeClient.ExecuteAsync(request, (response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
					string newRec = response.Content;
                    callback(newRec);
                }
                else
                {
                    //error ocured during upload
                    callback(null);
                }
            });
        }

       




        public byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = stream.Position;
            stream.Position = 0;

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                stream.Position = originalPosition;
            }
        }

    }
}