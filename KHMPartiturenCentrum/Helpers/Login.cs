using static KHM.App;

namespace KHM.Helpers
{
	public class Login
	{
		#region Get User properties
		public static void FillUserProperties( int _userId )
		{
			ObservableCollection<UserModel> Users = DBCommands.GetUsers ( );

			foreach ( UserModel user in Users )
			{
				if ( user.UserId == _userId )
				{
					ScoreUsers.SelectedUserId = _userId;
					ScoreUsers.SelectedUserName = user.UserName;
					ScoreUsers.SelectedUserFullName = user.UserFullName;
					ScoreUsers.SelectedUserPassword = user.UserPassword;
					ScoreUsers.SelectedUserEmail = user.UserEmail;
					ScoreUsers.SelectedUserRoleId = user.UserRoleId;
					ScoreUsers.SelectedUserCoverSheetFolder = user.CoverSheetFolder;
					ScoreUsers.SelectedUserDownloadFolder = user.DownloadFolder;
				}
			}
		}
		#endregion

		#region Check E-mail login
		public static string CheckEmailLogin( string _login )
		{
			ObservableCollection<UserModel> _users = DBCommands.GetUsers();

			var _validUserName = "invaliduser";

			foreach ( UserModel usr in _users )
			{
				if ( usr.UserEmail == _login ) { _validUserName = usr.UserName; }
			}

			return _validUserName;
		}
		#endregion

		#region Check valid username
		public static bool CheckUserName( string userName, int userId )
		{
			// UserName or UserId should ot be 0 or empty
			if ( String.IsNullOrEmpty( userName ) ) { return false; }
			if ( userId == 0 ) { return false; }

			// Returns false if UserName does not already exist and true if it is already used (bool UserExists = DBCommands.CheckUserName( tbUserName.Text );)
			ObservableCollection<UserModel> _users = DBCommands.GetUsers();

			foreach ( var user in _users )
			{
				if ( user.UserName.ToLower() == userName.ToLower() )
				{
					// Still valid if the Username belongs to the selected User
					if ( user.UserId == userId )
					{
						return false;
					}
					else
					{
						return true;
					}
				}
			}
			return false;
		}

		#endregion

		#region Check valid user password
		public static int CheckUserPassword( string _login, string _password )
		{
			// UserName or UserId should ot be 0 or empty
			if ( String.IsNullOrEmpty( _login ) ) { return 0; }
			if ( String.IsNullOrEmpty( _password ) ) { return 0; }

			var _pwLoggedInUser = Helper.HashPepperPassword(_password, _login);

			ObservableCollection<UserModel> _users = DBCommands.GetUsers();

			foreach ( var user in _users )
			{
				//var _pwToCheck = Helper.HashPepperPassword(_password, user.UserName);
				if ( user.UserEmail.ToLower() == _login.ToLower() || user.UserName.ToLower() == _login.ToLower() )
				{
					if ( _pwLoggedInUser == user.UserPassword )
					{
						return user.UserId;
					}
					else
					{
						return 0;
					}
				}
			}
			return 0;
		}

		#endregion

		#region Check valid e-mail (Is it unique)
		public static bool CheckEMail( string _email, int _userId )
		{
			// UserName or UserId should not be 0 or empty
			if ( String.IsNullOrEmpty( _email ) ) { return false; }
			if ( _userId == 0 ) { return false; }

			ObservableCollection<UserModel> _users = DBCommands.GetUsers();

			foreach ( var user in _users )
			{
				if ( user.UserEmail.ToLower() == _email.ToLower() )
				{
					// Still valid if the Username belongs to the selected User
					if ( user.UserId == _userId )
					{
						return false;
					}
					else
					{
						return true;
					}
				}
			}
			return false;
		}
		#endregion

		#region Check if the e-mail address has the correct format
		public static bool IsValidEmail( string _email )
		{
			if ( string.IsNullOrWhiteSpace( _email ) )
				return false;

			// Normalize the domain
			_email = Regex.Replace( _email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds( 200 ) );

			// Examines the domain part of the _email and normalizes it.
			string DomainMapper( Match match )
			{
				// Use IdnMapping class to convert Unicode domain names.
				var idn = new IdnMapping();

				// Pull out and process domain name (throws ArgumentException on invalid)
				string domainName = idn.GetAscii(match.Groups[2].Value);

				return match.Groups [ 1 ].Value + domainName;
			}

			return Regex.IsMatch( _email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds( 250 ) );
		}
		#endregion
	}
}
