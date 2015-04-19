using System;
using CoreGraphics;
using CoreImage;
using Foundation;
using SpriteKit;
using UIKit;
using System.Data.Sql;
using Mono.Data.Sqlite;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.IO;
using SQLite;
using MessageUI;
using Twitter;
using Social;
using GameKit;
using Xamarin.Social;
using Xamarin.Social.Services;



namespace spriteTest101
{
	public class MainMenuScene:BasicScene
	{
		GameCenterManager1 gcm = new GameCenterManager1 ();


		UIImagePickerController imgPckr = new UIImagePickerController();
		MFMailComposeViewController mailController;


		UIActionSheet actionSheet;

		SKLabelNode newGame   = new SKLabelNode (){
			Text = "To Game",
			FontColor = UIColor.White,
			FontName = "GillSans-Bold",
			FontSize = 30,
			Color = UIColor.Yellow
			};

		SKLabelNode unlocked   = new SKLabelNode (){
			Text = "Ammo Galleria",
			FontColor = UIColor.White,
			FontName = "GillSans-Bold",
			FontSize = 30,
		};
		SKLabelNode snaps     = new SKLabelNode (){
			Text = "Select A H8er",
			FontColor = UIColor.White,
			FontName = "GillSans-Bold",
			FontSize = 30,
		};

		SKLabelNode highScoreButton     = new SKLabelNode (){
			Text = "Highest Scores",
			FontColor = UIColor.White,
			FontName = "GillSans-Bold",
			FontSize = 30,
		};

		SKLabelNode leaderBoardsButton     = new SKLabelNode (){
			Text = "Leaderboards",
			FontColor = UIColor.White,
			FontName = "GillSans-Bold",
			FontSize = 30,
		};

		SKLabelNode rate      	   = new SKLabelNode (){
			Text = "Rate",
			FontColor = UIColor.White,
			FontName = "GillSans-Bold",
			FontSize = 30,
		};


		SKLabelNode share  = new SKLabelNode (){
			Text = "Share",
			FontColor = UIColor.White,
			FontName = "GillSans-Bold",
			FontSize = 30,
		};

		public MainMenuScene(CGSize size):base(size)
		{
			//var viewController1 = new UIViewController();





			GKLocalPlayer.LocalPlayer.AuthenticateHandler = (ui, err) => {


				if (ui != null){
					
					View.Window.RootViewController.PresentModalViewController (ui, true);
				}
					else {
					// Check if you are authenticated:
					var authenticated = GKLocalPlayer.LocalPlayer.Authenticated;
					gcm.reportScore(122,"com.commtech.inapppurchTest.StageScores");


				}
				Console.WriteLine ("Authentication result: {0}",err);
			};
		 
	
	
	


//				if (GKLocalPlayer.LocalPlayer.Authenticated)
//				{
//					var authenticated = GKLocalPlayer.LocalPlayer.Authenticated;
//					Console.WriteLine ("::::::authenticated local player: ");
//
//					//					navigationController.PresentViewController (viewController1, true, null);
//					//					return;
//				}
//				else {
////					viewController.Finished += (object sender, EventArgs e) => {
////						viewController.DismissViewController (true, null);
////					};
//
//					//viewController.
//
//					View.Window.RootViewController.PresentViewController(viewController,true,null);
//					//Window.RootViewController.PresentViewController(viewController,true,null);
//					//Console.WriteLine ("Error while trying to authenticate local player: " + error.Description);
//
//						
//				}
//				//				else {
//				//					Console.WriteLine ("::::::Error while trying to authenticate local player: " + error.Description);
//				//				}






//			GKLocalPlayer.LocalPlayer.AuthenticateHandler = (viewController, error) => {
//				if (error != null) {
//					Console.WriteLine ("Error while trying to authenticate local player: " + error.Description);
//					return;
//				}
//				if (GKLocalPlayer.LocalPlayer.Authenticated || (viewController == null))
//				{
//
////					viewController1.Finished += (object sender, EventArgs e) => {
////						viewController1.DismissViewController (true, null);
////					};
//
//					View.Window.RootViewController.PresentViewController (viewController1, true, null);
//					return;
//				}
//
//			};







//
//			EasyLevelObj.level1 = new int[] { 5, 6, 0, 0, 0 };
//			EasyLevelObj.level2 = new int[] { 4, 3, 0, 0, 0 };
//			EasyLevelObj.level3 = new int[] { 1, 2, 0, 0, 0 };
//			EasyLevelObj.level4 = new int[] { 9, 8, 0, 0, 0 };
//			EasyLevelObj.level5 = new int[] { 8, 7, 0, 0, 0 };
//
			dataMethod();
			//demo();
			//dbShit ();
			BackgroundColor = UIColor.White;

			snaps.Position = new CGPoint (size.Width / 2, size.Height / 1.24f);

			newGame.Position = new CGPoint (size.Width / 2, size.Height / 1.62f);
			//unlocked.Position = new CGPoint (size.Width / 2, size.Height / 1.8f);
			//highScoreButton.Position = new CGPoint (size.Width / 2, size.Height / 2.2f);
			leaderBoardsButton.Position = new CGPoint (size.Width / 2, size.Height / 2.379f);


			rate.Position = new CGPoint (size.Width / 3.5f, size.Height / 5f);
			share.Position = new CGPoint (size.Width / 1.4f, size.Height / 5f);

			if (GameObj.finalImage == null) {
				textScaling (snaps);
			} else {
			
				textScaling (newGame);
			}

			AddChild (newGame);
			AddChild (snaps);
			AddChild (leaderBoardsButton);
			//AddChild (unlocked);
			//AddChild (highScoreButton);
			AddChild (rate);
			AddChild (share);
		}
	


		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			foreach (var touch in touches) {
				CGPoint location = (touch as UITouch).LocationInNode (this);
				if (newGame.Frame.Contains (location)) {
					PresentScene(new LevelsScene(Size));
				}
				if(leaderBoardsButton.Frame.Contains(location)){
					GKGameCenterViewController controller = new GKGameCenterViewController ();
					controller.Finished += (object sender, EventArgs e) => {
						controller.DismissViewController (true, null);
					};
					controller.LeaderboardCategory = "com.commtech.inapppurchTest.StageScores";
					controller.LeaderboardTimeScope = GKLeaderboardTimeScope.AllTime;
					Scene.View.Window.RootViewController.PresentModalViewController (controller, true);


					//AppDelegate.
					//AppDelegate.Shared.ViewController.PresentViewController (controller, true, null);

				}

//				if(unlocked.Frame.Contains(location)){
//
//					PresentScene(new UnlockScene(Size));
//
//				}
				if (snaps.Frame.Contains (location)) {
//					imgPckr =  new UIImagePickerController ();
//					imgPckr.Delegate = imgPckr;
//					imgPckr.AllowsEditing = true;
//					imgPckr.SourceType = UIImagePickerControllerSourceType.Camera;
//					imgPckr.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.Camera);
//					imgPckr.FinishedPickingMedia += Handle_FinishedPickingMedia;
//					Scene.View.Window.RootViewController.PresentModalViewController (imgPckr, true);
//
//

					PresentScene(new SelectaScene(Size));
				}

				if (highScoreButton.Frame.Contains (location)) {
				
					PresentScene (new ScoresScene (Size));
				
				}

				if (rate.Frame.Contains (location)) {
				
				}
				if (share.Frame.Contains (location)) {
					
					actionSheet = new UIActionSheet ("Share this App");
					//actionSheet.AddButton ("Delete");
					actionSheet.AddButton ("Cancel");
					actionSheet.AddButton ("Facebook");
					actionSheet.AddButton ("Twitter");
					actionSheet.AddButton ("Email");

					actionSheet.AddButton ("SMS");

					//actionSheet.DestructiveButtonIndex = 0; // red
					actionSheet.CancelButtonIndex = 0;  // black
					actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) {
						Console.WriteLine ("Button " + b.ButtonIndex.ToString () + " clicked");


						if(b.ButtonIndex == 1){
							//facebook
						}else if(b.ButtonIndex == 2){
							//twitter

							shareTwitter();



						}else if(b.ButtonIndex == 3){
							//email
							//UIApplication.SharedApplication.OpenUrl(new NSUrl("http://www.google.com/"));
						

							shareEmail();



						}else if(b.ButtonIndex == 4){
							//sms

							shareText();

//							var smsTo = NSUrl.FromString("sms:18015551234");
//
//							UIApplication.SharedApplication.OpenUrl(smsTo);
//							var imessageTo = NSUrl.FromString("sms:john@doe.com");
//							UIApplication.SharedApplication.OpenUrl(imessageTo);
//
//							var smsTo1 = NSUrl.FromString("sms:18015551234");
//							if (UIApplication.SharedApplication.CanOpenUrl(smsTo1)) {
//								UIApplication.SharedApplication.OpenUrl(smsTo1);
//							} else {
//								// warn the user, or hide the button...
//							}

						}

					};

					actionSheet.ShowInView (View);

				}

			}
		}



		public void addItemBackground(CGPoint itemPos){}

		public void shareFaceobook(){

			if (SLComposeViewController.IsAvailable (SLServiceKind.Facebook)) {

				var slComposer = SLComposeViewController.FromService (SLServiceType.Facebook);
				slComposer.SetInitialText ("Bust your stress with this new iOS Game!");

				slComposer.CompletionHandler += (result) => {
					InvokeOnMainThread (() => {
						slComposer.DismissViewController (true, null);
					});
				};


				View.Window.RootViewController.PresentModalViewController (slComposer, true);




			}
		}

		public void shareTwitter(){

			if (TWTweetComposeViewController.CanSendTweet) {
				// Add code below
				var tweet = new TWTweetComposeViewController();

				tweet.Title = "TWiiiit";
				tweet.SetInitialText ("Bust your stress with this new iOS Game!");
				//tweet.AddImage ();
				//								tweet.SetCompletionHandler((TWTweetComposeViewControllerResult r) =>{
				//									DismissModalViewControllerAnimated(true); // hides the tweet
				//									if (r == TWTweetComposeViewControllerResult.Cancelled) {
				//										// user cancelled the tweet
				//									} else {
				//										// user sent the tweet (they may have edited it first)
				//									}
				//								});
				//

				View.Window.RootViewController.PresentModalViewController (tweet, true);

				//PresentModalViewController(tweet, true);
				tweet.AddUrl (new NSUrl("http://xamarin.com/xyz"));





			} else {
				// Show a message: Twitter may not be configured in Settings



			}

		}

		public void shareText(){
			if (MFMessageComposeViewController.CanSendText) {
				MFMessageComposeViewController message =
					new MFMessageComposeViewController();

				message.Finished += (sender, e) => {
					message.DismissViewController (true, null);
				};

				message.Body = "Bust your stress with this new iOS Games!!";
				message.Recipients = new string[] {"",""};
				View.Window.RootViewController.PresentModalViewController (message, false);

				//this.PresentModalViewController(message, false);
			}



			//			var smsTo = NSUrl.FromString("sms:18015551234");
			//			UIApplication.SharedApplication.OpenUrl(smsTo);
			//			var imessageTo = NSUrl.FromString("sms:john@doe.com");
			//			UIApplication.SharedApplication.OpenUrl(imessageTo);
			//

		}

		public void shareEmail(){

			if (MFMailComposeViewController.CanSendMail) {

				mailController = new MFMailComposeViewController ();
				mailController.SetToRecipients (new string[]{"",""});
				mailController.SetSubject ("StressBuster!");
				mailController.SetMessageBody ("Bust your stress with this new iOS Games!!", false);
				// do mail operations here


				mailController.Finished +=  (sender, e) =>  {
					//					Console.WriteLine (args.Result.ToString ());
					//					args.Controller.DismissViewController (true, null);
					mailController.DismissViewController(true,null);

					//PresentScene(mailController);
					//this//.PresentViewController (mailController, true, null);

				};

				Console.WriteLine("EMAIL CONTROLLER");
				View.Window.RootViewController.PresentModalViewController (mailController, true);

			}
		}

		public void rating(){
		}

		public void textScaling(SKLabelNode scaled){

			var up   = SKAction.ScaleBy (1.2f, 0.8f);
			//var down = SKAction.ScaleBy (0.8f, 0.5f);
			var seq = SKAction.Sequence (new SKAction[]{up,up.ReversedAction});//({up,down}); 
			var fort = SKAction.RepeatActionForever (seq);

			scaled.RunAction (fort);

		}







		/// <summary>
		/// ///////////////DATA BASE TRIAL
		/// </summary>

		public static void dataMethod(){

			var dbpath = Path.Combine (
				Environment.GetFolderPath (Environment.SpecialFolder.Personal),
				"DatabasePath.db3");

			var db = new SQLiteConnection (dbpath);
			//bool exists = File.Exists (db);

			//if (!exists) {
				db.CreateTable<DataModel> ();
			//}

			if (db.Table<DataModel> ().Count () == 0) {
			
				var newScore = new DataModel ();
				newScore.Level1 = intArrToString (EasyLevelObj.level1);
				newScore.Level2 = intArrToString (EasyLevelObj.level2);
				newScore.Level3 = intArrToString (EasyLevelObj.level3);
				newScore.Level4 = intArrToString (EasyLevelObj.level4);
				newScore.Level5 = intArrToString (EasyLevelObj.level5);
				db.Insert (newScore);

			} 
//			else {
//
//				db.DeleteAll<DataModel>();
//				var newScore = new DataModel ();
//				newScore.Level1 = intArrToString (EasyLevelObj.level1);
//				newScore.Level2 = intArrToString (EasyLevelObj.level2);
//				newScore.Level3 = intArrToString (EasyLevelObj.level3);
//				newScore.Level4 = intArrToString (EasyLevelObj.level4);
//				newScore.Level5 = intArrToString (EasyLevelObj.level5);
//				db.Insert (newScore);
//			
//			}

			Console.WriteLine("Reading data");
			var table = db.Table<DataModel> ();
			foreach (var s in table) {

				EasyLevelObj.level1 = stringToIntArr (s.Level1);
				EasyLevelObj.level2 = stringToIntArr (s.Level2);
				EasyLevelObj.level3 = stringToIntArr (s.Level3);
				EasyLevelObj.level4 = stringToIntArr (s.Level4);
				EasyLevelObj.level5 = stringToIntArr (s.Level5);




				Console.WriteLine (s.ID + " " + s.Level1);
				Console.WriteLine (s.ID + " " + s.Level2);
				Console.WriteLine (s.ID + " " + s.Level3);
				Console.WriteLine (s.ID + " " + s.Level4);
				Console.WriteLine (s.ID + " " + s.Level5);
			}

			
		
		}


		public static string intArrToString(int[] scoress)
		{
			string Arraystring = scoress[0].ToString();

			for(int i = 1; i < scoress.Length; i++) { 
				Arraystring += "," + scoress[i].ToString();
			}

			return Arraystring;

		}

		public static int[] stringToIntArr(string scoreString){
			//assign this via Reader
			string[] tokens = scoreString.Split(',');
			int[] myItems = Array.ConvertAll<string, int>(tokens, int.Parse);
			return myItems;
		}



		public static UIImage ToImage(byte[] data)
		{
			if (data==null) {
				return null;
			}
			UIImage image = null;
			try {

				image = new UIImage(NSData.FromArray(data));
				data = null;
			} catch (Exception ) {
				return null;
			}
			return image;
		}







	}
}












//		public void demo() {
//			var connection = GetConnection ();
//			using (var cmd = connection.CreateCommand ()) {
//				connection.Open ();
//				cmd.CommandText = "SELECT * FROM LevelsDB";
//				using (var reader = cmd.ExecuteReader ()) {
//					while (reader.Read ()) {
//						Console.Error.Write ("(Row ");
//						Write (reader, 0);
//
//
//						for (int i = 1; i < reader.FieldCount - 1; ++i) {
//							Console.Error.Write (" ");
//							Write (reader, i);
//
//
//							Console.Error.Write ("EXXXXXX{{   " + i);
//
//							var ex = reader.GetString (i);
//
//							var lvlArr = stringToIntArr (ex);
//
//							if (reader.GetString (1).Substring (0, 1) != "0" &&
//								EasyLevelObj.level1 [0] == 0) {
//								if (i == 1) {
//									EasyLevelObj.level1 = lvlArr;
//								} else if (i == 2) {
//									EasyLevelObj.level2 = lvlArr;
//								} else if (i == 3) {
//									EasyLevelObj.level3 = lvlArr;
//								} else if (i == 4) {
//									EasyLevelObj.level4 = lvlArr;
//								} else if (i == 5) {
//									EasyLevelObj.level5 = lvlArr;
//								}
//							} 
//
//
//							Console.Error.Write ("++stringggg++" + ex + "::::::::::::::");
//
//						}
//
//
//
//						//						var bb = reader.GetValue (6);
//						//						Console.WriteLine ("byte 6 type::::  "+bb.GetType());
//						//
//
//
//						//						UIImage i1 = UIImage.LoadFromData (NSData.FromArray (reader.GetValue (6)));
//						//						var ii = ToImage(reader.GetValue(6) as byte[]);
//						//
//						//
//						//						var spppp = new SKSpriteNode (SKTexture.FromImage (ii)
//						//							);
//						//						spppp.Position = new CGPoint (Size.Width / 2, Size.Height / 2);
//						//						AddChild (spppp);
//						//
//						//Console.WriteLine ("byte 6 type::::  "+ToImage(reader.GetValue(6) as byte[]));
//
//						//UIImage.LoadFromData(NSData.FromStream(
//						//																	reader.GetStream(6)));
//
//						//(NSData.FromArray(reader.GetValue(6)));   //GetValue (6) ;
//
//
//
//
//						//Console.WriteLine ("reader 6 type::::  "+reader.GetFieldType(6));
//
//
//
//
//
//
//
//						Console.Error.WriteLine(")");
//					}
//				}
//				connection.Close ();
//			}
//
//		}    
//
//		public static SqliteConnection GetConnection()
//		{
//			int[] l1 = EasyLevelObj.level1;
//			int[] l2 = EasyLevelObj.level2;
//				//new int[] { 3,5,6,3,5 };
//
//
////
////
////			string Arraystring = l1[0].ToString();
////
////			for(int i = 1; i < l1.Length; i++) { 
////				Arraystring += "," + l1[i].ToString();
////			}
////
////
//
//			var documents = Environment.GetFolderPath (
//				Environment.SpecialFolder.Personal);
//			string db = Path.Combine (documents, "mydb.db3");
//			bool exists = File.Exists (db);
//			if (!exists)
//				SqliteConnection.CreateFile (db);
//			var conn = new SqliteConnection("Data Source=" + db);
//			if (!exists) {
//
//				var crCommand = "CREATE TABLE LevelsDB (PersonID INTEGER NOT NULL, Level1 ntext, Level2 ntext, " +
//				                "Level3 ntext, Level4 ntext, Level5 ntext, Image1 blob)";
//					//"Image1 Blob, Image2 Blob, Image3 Blob, SelectedHater INTEGER)";
//
//				//var imageDB = "";
//				//var commands = //new[] {
////					"INSERT INTO LevelsDB (PersonID, FirstName, LastName, Level2) VALUES (2, 'Morteza', 'Jalali','"+ Arraystring+"')",
////					"INSERT INTO LevelsDB (PersonID, FirstName, LastName) VALUES (3, 'Andyyy', 'Howaaa')",
//				//};
//
//				string[] commands = new string[2];
//				//if (EasyLevelObj.level1[0] == 0) {
//				
//				var im = UIImage.FromFile ("chicken1");
//				commands [0] = crCommand;
//				commands [1] = "INSERT INTO LevelsDB (PersonID, Level1, Level2, Level3, Level4, Level5, Image1) VALUES (1, '" + intArrToString (EasyLevelObj.level1) + "', '" + intArrToString (EasyLevelObj.level2) + "','" + intArrToString (EasyLevelObj.level3) + "','" + intArrToString (EasyLevelObj.level4) + "','" + intArrToString (EasyLevelObj.level5) + "','"
//
//					+ im + "')";
//
//
//				//} 
//
//
//				conn.Open ();
//				foreach (var cmd in commands) {
//					using (var c = conn.CreateCommand()) {
//						c.CommandText = cmd;
//						c.CommandType = CommandType.Text;
//  						var ee = c.ExecuteNonQuery ();
//					}
//				}
//				conn.Close ();
//			}
//
//
//
//			if (exists) {
//
//				if (EasyLevelObj.level1 [0] != 0) {
//					string[] commands1 = new string[2];
//
//					commands1 [0] = "DELETE FROM LevelsDB WHERE PersonID = 1";
//					commands1 [1] = "INSERT INTO LevelsDB (PersonID, Level1, Level2, Level3, Level4, Level5) VALUES (1, '" + intArrToString (EasyLevelObj.level1) + "', '" + intArrToString (EasyLevelObj.level2) + "','" + intArrToString (EasyLevelObj.level3) + "','" + intArrToString (EasyLevelObj.level4) + "','" + intArrToString (EasyLevelObj.level5) + "')";
//
//
//					conn.Open ();
//					foreach (var cmd in commands1) {
//						using (var c = conn.CreateCommand ()) {
//							c.CommandText = cmd;
//							c.CommandType = CommandType.Text;
//							c.ExecuteNonQuery ();
//						}
//					}
//					conn.Close ();
//				}
//			
//			}
//
//
//			return conn;
//		}
//
//		public static void Write(SqliteDataReader reader, int index)
//		{
//			Console.Error.Write("({0} '{1}')", 
//				reader.GetName(index), 
//				reader [index]);
//		}




















//
//			var bfr = newGame.Frame; 
//
//			bfr.Width = size.Width / 5f;
//			bfr.Height = size.Height / 8f;
//
//
//


//new CGRect(new CGSize(size.Width / 5, size.Height  /5));
//(Scene.Size.Width / 4, Scene.Size.Height - (Scene.Size.Height / 4));


//buttonTopLeft.Scene.BackgroundColor = UIColor.DarkGray;

//AddChild (buttonTopLeft);







//
//		public void dbShit(){
//
//			var db = new SQLiteConnection (dbPath);
//
//
//		}

//
//
////			var documents = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
////			var pathToDatabase =  .Combine(documents, "db_adonet.db");
////			SqliteConnection.CreateFile(pathToDatabase);
////
////
//
//		}

//		public SQLite.SQLiteConnection GetConnection(){
//			var sqliteFilename = "TodoSQLite.db3";
//			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
//			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
//			var path = Path.Combine(libraryPath, sqliteFilename);
//
//			// This is where we copy in the prepopulated database
//			Console.WriteLine (path);
//			if (!File.Exists (path)) {
//				File.Copy (sqliteFilename, path);
//			}
//
//			var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
//			var conn = new SQLite.Net.SQLiteConnection(plat, path);
//
//			// Return the database connection 
//			return conn;
//		
//		
//		}

//		#region ISQLite implementation
//		public class SQLite.Net.SQLiteConnection GetConnection ()
//		{
//			var sqliteFilename = "TodoSQLite.db3";
//			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
//			string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
//			var path = Path.Combine(libraryPath, sqliteFilename);
//
//			// This is where we copy in the prepopulated database
//			Console.WriteLine (path);
//			if (!File.Exists (path)) {
//				File.Copy (sqliteFilename, path);
//			}
//
//			var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
//			var conn = new SQLite.Net.SQLiteConnection(plat, path);
//
//			// Return the database connection 
//			return conn;
//		}
//		#endregion





