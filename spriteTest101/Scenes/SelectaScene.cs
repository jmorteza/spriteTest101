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


namespace spriteTest101
{
	public class SelectaScene:BasicScene
	{
		UIActionSheet actionSheet;


		SKSpriteNode _img1 = new SKSpriteNode(UIColor.Black,new CGSize(100f,100f));
		SKSpriteNode _img2 = new SKSpriteNode(UIColor.Black,new CGSize(100f,100f));
		SKSpriteNode _img3 = new SKSpriteNode(UIColor.Black,new CGSize(100f,100f));

		UIImage _image1;
		UIImage _image2;
		UIImage _image3;

		SKSpriteNode _camNodeFrame = new SKSpriteNode(UIColor.White,new CGSize(290f,180f));

		SKSpriteNode _camNode = new SKSpriteNode(SKTexture.FromImageNamed("camIcon1"));//UIColor.DarkGray,new CGSize(300f,300f));
		int currentSelected;


		SKSpriteNode _pushPin = new SKSpriteNode (SKTexture.FromImageNamed ("pushpin"));
		CGPoint _pinPosition = new CGPoint ();

		SKLabelNode _menuButton = new SKLabelNode (){
			Text = "Menu",
			FontName = "GillSans-Bold",
			FontSize = 26,
			FontColor = UIColor.White,
		};

		UIImagePickerController imgPckr = new UIImagePickerController();


		public SelectaScene(CGSize size):base(size)
		{


			BackgroundColor = UIColor.White;

			_camNode.Size = new CGSize (300, 300);

			_img1.Position = new CGPoint (52, Size.Height / 1.3f);
			_img2.Position = new CGPoint (Size.Width / 2, Size.Height / 1.3f);
			_img3.Position = new CGPoint (Size.Width - 52, Size.Height / 1.3f);
			_camNode.Position = new CGPoint (Size.Width/2, Size.Height / 2.7f);

			_camNodeFrame.Position = new CGPoint (_camNode.Position.X, _camNode.Position.Y - 14);

			nodeScaling (_camNode);
			nodeScaling (_camNodeFrame);

			AddChild (_camNodeFrame);
			AddChild (_camNode);
			AddChild (_img1);
			AddChild (_img2);
			AddChild (_img3);

			_menuButton.Position = new CGPoint (Size.Width/6, Size.Height - 55);

			AddChild (_menuButton);

			currentSelected = 1;
			movePin (_img1.Position);
			_pushPin.SetScale (0.9f);
			_pushPin.ZRotation = -0.5f;



			AddChild (_pushPin);

			loadStartUpHater ();




//			if (GameObj.mImage1 != null) {
//				currentSelected = 1;
//				var im = GameObj.mImage1;//UIImage.FromFile ("chicken1");
//				loadHater (im);
//			}




		}
	


		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			foreach (var touch in touches) {
				CGPoint location = (touch as UITouch).LocationInNode (this);
				if (_camNode.Frame.Contains (location)) {
					//PresentScene(new MainMenuScene(Size));
				


					actionSheet = new UIActionSheet ("Which Hater?");
					//actionSheet.AddButton ("Delete");
					actionSheet.AddButton ("Cancel");
					actionSheet.AddButton ("Camera");
					actionSheet.AddButton ("Photo Album");

					//actionSheet.DestructiveButtonIndex = 0; // red
					actionSheet.CancelButtonIndex = 0;  // black

					actionSheet.Clicked += delegate(object a, UIButtonEventArgs b) {
						Console.WriteLine ("Button " + b.ButtonIndex.ToString () + " clicked");
						if (b.ButtonIndex == 1) {
							//camera
							//imgPckr = null;
							imgPckr = new UIImagePickerController ();
							//imgPckr.Delegate = Self;
							imgPckr.AllowsEditing = true;
							imgPckr.SourceType = UIImagePickerControllerSourceType.Camera;
							//imgPckr.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.Camera);
							imgPckr.FinishedPickingMedia += Handle_FinishedPickingMedia;
							imgPckr.Canceled += Handle_Canceled;

							Scene.View.Window.RootViewController.PresentModalViewController (imgPckr, true);

						} else if (b.ButtonIndex == 2) {
							//album
							//imgPckr = null;
							imgPckr = new UIImagePickerController ();
							//imgPckr.Delegate = Self;
							imgPckr.AllowsEditing = true;
							imgPckr.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
							//imgPckr.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.Camera);
							imgPckr.FinishedPickingMedia += Handle_FinishedPickingMedia;
							imgPckr.Canceled += Handle_Canceled;

							Scene.View.Window.RootViewController.PresentModalViewController (imgPckr, true);
						}
					};

					actionSheet.ShowInView (View);


				}

				if (_img1.Frame.Contains (location)) {
					currentSelected = 1;
					GameObj.finalImage = _image1;
					movePin (_img1.Position);

				}
				if (_img2.Frame.Contains (location)) {
					currentSelected = 2;
					GameObj.finalImage = _image2;
					movePin (_img2.Position);


				}
				if (_img3.Frame.Contains (location)) {
					currentSelected = 3;
					GameObj.finalImage = _image3;
					movePin (_img3.Position);


				}

				if (_menuButton.Frame.Contains (location)) {
					DataStorage ();
					PresentScene (new MainMenuScene (Size));
				}


			}
		}



		public void movePin(CGPoint framePos){
		
			var tempX = framePos.X + 34;
			var tempY = framePos.Y + 36;

			_pushPin.Position = new CGPoint (tempX, tempY);
		
		}


		void Handle_Canceled (object sender, EventArgs e) {
			imgPckr.DismissModalViewController(true);
		}


		public void nodeScaling(SKSpriteNode scaled){

			var up   = SKAction.ScaleBy (0.8f, 0.9f);
			//var down = SKAction.ScaleBy (0.8f, 0.5f);
			var seq = SKAction.Sequence (new SKAction[]{up,up.ReversedAction});//({up,down}); 
			var fort = SKAction.RepeatActionForever (seq);

			scaled.RunAction (fort);

		}



		protected void Handle_FinishedPickingMedia (object sender, UIImagePickerMediaPickedEventArgs e)
		{
			// determine what was selected, video or image
			//bool isImage = false;

			UIImage cc = e.Info [UIImagePickerController.EditedImage] as UIImage;
			imgPckr.DismissModalViewController(true);

			loadHater (cc);
//			imgPckr = null;

			//GameObj.finalImage = cc;


			//Scene.View.Window.WillRemoveSubview (imgPckr.View);
			//imgPckr.RemoveFromParentViewController ();


			//imgPckr.View.RemoveFromSuperview ();
			//Scene.View.PresentScene (new MainMenuScene  (Size));
		}

		public async void loadHater(UIImage hater){

//			if (GameObj.finalImage != null) {
//				await System.Threading.Tasks.Task.Delay (2000);
				//RemoveAllChildren ();
				
			if(currentSelected == 1) {
				GameObj.finalImage = hater;
				_image1 = hater;
				_img1.RemoveFromParent ();
				_img1 = new SKSpriteNode (SKTexture.FromImage(hater));//Frage (xy.hater));//(UIColor.DarkGray,new CGSize(200f,200f)); 
				_img1.Size =  new CGSize(100f,100f);
				_img1.Position = new CGPoint (52, Size.Height / 1.3f);

				_pushPin.RemoveFromParent ();
				movePin(_img1.Position);
//				_img2.Position = new CGPoint (Size.Width / 2, Size.Height / 1.3f);

				AddChild(_img1);
				AddChild (_pushPin);



			}else if(currentSelected == 2){
				_img2.RemoveFromParent ();
				_img2 = new SKSpriteNode (SKTexture.FromImage(hater));//Frage (xy.hater));//(UIColor.DarkGray,new CGSize(200f,200f)); 
				_img2.Size =  new CGSize(100f,100f);
				_img2.Position = new CGPoint (Size.Width / 2, Size.Height / 1.3f);


				AddChild(_img2);
				_image2 = hater;
				GameObj.finalImage = hater;

				_pushPin.RemoveFromParent ();
				movePin(_img2.Position);
				AddChild (_pushPin);


			}else if(currentSelected == 3){
				_img3.RemoveFromParent ();
				_img3 = new SKSpriteNode (SKTexture.FromImage(hater));//Frage (xy.hater));//(UIColor.DarkGray,new CGSize(200f,200f)); 
				_img3.Size =  new CGSize(100f,100f);

				_img3.Position = new CGPoint (Size.Width - 52, Size.Height / 1.3f);


				AddChild(_img3);
				_image3 = hater;
				GameObj.finalImage = hater;


				_pushPin.RemoveFromParent ();
				movePin(_img3.Position);
				AddChild (_pushPin);

			}




		}





		public static Byte[] convertImageToByteArr (UIImage haterPic){
		
			Byte[] theBytes;
			using (NSData imageData = haterPic.AsPNG ()) {
			
				Byte[] myByteArray = new Byte[imageData.Length];
				System.Runtime.InteropServices.Marshal.Copy 
						(imageData.Bytes, myByteArray, 0, Convert.ToInt32 (imageData.Length));


				theBytes = myByteArray;
			}
			//haterPic.Dispose ();

			return theBytes;
		
		}

		public static UIImage convertByteArrToImg(Byte[] byyteme){
		
			var imgData = NSData.FromArray (byyteme);
			UIImage theIMG = UIImage.LoadFromData (imgData);
			return theIMG;

			//NSData *imageData = [NSData dataWithBytes:bytesData length:length];
			//UIImage *image = [UIImage imageWithData:imageData];


		
		}




		public void loadStartUpHater(){

			var dbpath = Path.Combine (
				Environment.GetFolderPath (Environment.SpecialFolder.Personal),
				"DatabasePath.db3");

			var db = new SQLiteConnection (dbpath);
			var table = db.Table<DataModel> ();
			foreach (var s in table) {

				System.Diagnostics.Debug.WriteLine ("11  "+s.Image1+"  22  "+s.Image2+"  33  "+s.Image3);

				if (s.Image3 != null) {
					currentSelected = 3;
					loadHater (convertByteArrToImg (s.Image3));
				}

				if (s.Image2 != null) {

					currentSelected = 2;
					loadHater (convertByteArrToImg (s.Image2));
				}

				if (s.Image1 != null) {
					currentSelected = 1;
					loadHater (convertByteArrToImg (s.Image1));
				}

				//currentSelected = 1;
				//				var im = GameObj.mImage1;//UIImage.FromFile ("chicken1");
				//				loadHater (im);
				//EasyLevelObj.level1 = stringToIntArr (s.Level1);

			
			
			}


		}



		public void DataStorage(){

			var dbpath = Path.Combine (
				Environment.GetFolderPath (Environment.SpecialFolder.Personal),
				"DatabasePath.db3");

			var db = new SQLiteConnection (dbpath);
			if (db.Table<DataModel> ().Count () != 0) {
			
				db.DeleteAll<DataModel> ();
				var newScore = new DataModel ();
				newScore.Level1 = intArrToString (EasyLevelObj.level1);
				newScore.Level2 = intArrToString (EasyLevelObj.level2);
				newScore.Level3 = intArrToString (EasyLevelObj.level3);
				newScore.Level4 = intArrToString (EasyLevelObj.level4);
				newScore.Level5 = intArrToString (EasyLevelObj.level5);

				if (_image1 != null) {
					newScore.Image1 = convertImageToByteArr (_image1);
				}
				if (_image2 != null) {
					newScore.Image2 = convertImageToByteArr (_image2);
				}
				if (_image3 != null) {
					newScore.Image3 = convertImageToByteArr (_image3);
				}

				db.Insert (newScore);

				//newScore.Level5 = intArrToString (EasyLevelObj.level5);
				//db.Insert (newScore);
			
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


	}
}



//
//
//		public void demo() {
//			var connection = GetConnection ();
//			using (var cmd = connection.CreateCommand ()) {
//				connection.Open ();
//				cmd.CommandText = "SELECT * FROM LevelsDB";
//				using (var reader = cmd.ExecuteReader ()) {
//					while (reader.Read ()) {
//						Console.Error.Write ("(Row ");
//						Write (reader, 0);
//						for (int i = 1; i < reader.FieldCount; ++i) {
//							Console.Error.Write(" ");
//							Write (reader, i);
//							var ex = reader.GetString (i);
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
//
//
//							Console.Error.Write("++stringggg++"+ex+"::::::::::::::");
//
//						}
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
//			//new int[] { 3,5,6,3,5 };
//
//
//			//
//			//
//			//			string Arraystring = l1[0].ToString();
//			//
//			//			for(int i = 1; i < l1.Length; i++) { 
//			//				Arraystring += "," + l1[i].ToString();
//			//			}
//			//
//			//
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
//					"Level3 ntext, Level4 ntext, Level5 ntext)";
//
//				//var imageDB = "";
//				//var commands = //new[] {
//				//					"INSERT INTO LevelsDB (PersonID, FirstName, LastName, Level2) VALUES (2, 'Morteza', 'Jalali','"+ Arraystring+"')",
//				//					"INSERT INTO LevelsDB (PersonID, FirstName, LastName) VALUES (3, 'Andyyy', 'Howaaa')",
//				//};
//
//				string[] commands = new string[3];
//				//if (EasyLevelObj.level1[0] == 0) {
//
//
//				commands [0] = crCommand;
//				commands [1] = "INSERT INTO LevelsDB (PersonID, Level1, Level2, Level3, Level4, Level5) VALUES (1, '" + intArrToString (EasyLevelObj.level1) + "', '" + intArrToString (EasyLevelObj.level2) + "','" + intArrToString (EasyLevelObj.level3) + "','" + intArrToString (EasyLevelObj.level4) + "','" + intArrToString (EasyLevelObj.level5) + "')";
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
//						var ee = c.ExecuteNonQuery ();
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
//


//		public UIImage combineImages(UIImage theHaterPic, UIImage frameImage) {
//			UIImage imgg = null;
//			CGSize newImageSize = new CGSize ();
//
//			UIGraphics.BeginImageContextWithOptions (newImageSize, false, Scene.View.ContentScaleFactor);
//			UIGraphics.BeginImageContext (newImageSize);
//
//			imgg = UIGraphics.GetImageFromCurrentImageContext ();
//			UIGraphics.EndImageContext ();
//			return imgg;
//
//		}


//		- (UIImage*)imageByCombiningImage:(UIImage*)firstImage withImage:(UIImage*)secondImage {
//			UIImage *image = nil;
//
//			CGSize newImageSize = CGSizeMake(MAX(firstImage.size.width, secondImage.size.width), MAX(firstImage.size.height, secondImage.size.height));
//			if (UIGraphicsBeginImageContextWithOptions != NULL) {
//				UIGraphicsBeginImageContextWithOptions(newImageSize, NO, [[UIScreen mainScreen] scale]);
//			} else {
//				UIGraphicsBeginImageContext(newImageSize);
//			}
//			[firstImage drawAtPoint:CGPointMake(roundf((newImageSize.width-firstImage.size.width)/2),
//				roundf((newImageSize.height-firstImage.size.height)/2))];
//			[secondImage drawAtPoint:CGPointMake(roundf((newImageSize.width-secondImage.size.width)/2),
//				roundf((newImageSize.height-secondImage.size.height)/2))];
//			image = UIGraphicsGetImageFromCurrentImageContext();
//			UIGraphicsEndImageContext();
//
//			return image;
//		}

















//			EasyLevelObj.level1 = new int[] { 5, 6, 0, 0, 0 };
//			EasyLevelObj.level2 = new int[] { 4, 3, 0, 0, 0 };
//			EasyLevelObj.level3 = new int[] { 1, 2, 0, 0, 0 };
//			EasyLevelObj.level4 = new int[] { 9, 8, 0, 0, 0 };
//			EasyLevelObj.level5 = new int[] { 8, 7, 0, 0, 0 };
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





