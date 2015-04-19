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
	public class UnlockScene:BasicScene
	{
		SKSpriteNode _obj1 = new SKSpriteNode(UIColor.Blue,new CGSize(200f,200f));
		SKSpriteNode _obj2 = new SKSpriteNode(UIColor.Green,new CGSize(200f,200f));
		SKSpriteNode _obj3 = new SKSpriteNode(UIColor.Yellow,new CGSize(200f,200f));
		SKSpriteNode _obj4 = new SKSpriteNode(UIColor.Red,new CGSize(200f,200f));

		SKLabelNode _menuButton = new SKLabelNode (){
			Text = "Menu",
			FontName = "GillSans-Bold",
			FontSize = 26,
			FontColor = UIColor.White,
		};

		public UnlockScene(CGSize size):base(size)
		{
			
			//dbShit ();
			BackgroundColor = UIColor.White;


			_menuButton.Position = new CGPoint (Size.Width/6, Size.Height - 66);
			AddChild (_menuButton);

			_obj1.Size = new CGSize (Size.Width, 105);
			_obj2.Size = new CGSize (Size.Width, 105);
			_obj3.Size = new CGSize (Size.Width, 105);
			_obj4.Size = new CGSize (Size.Width, 105);



			_obj1.Position = new CGPoint (Size.Width/2, Size.Height / 1.33f);
			_obj2.Position = new CGPoint (Size.Width/2, _obj1.Position.Y - 110);
			_obj3.Position = new CGPoint (Size.Width/2, _obj2.Position.Y - 110);
			_obj4.Position = new CGPoint (Size.Width/2, _obj3.Position.Y - 110);




			AddChild (_obj1);
			AddChild (_obj2);
			AddChild (_obj3);
			AddChild (_obj4);



		}
	


		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			foreach (var touch in touches) {
				CGPoint location = (touch as UITouch).LocationInNode (this);
				if (_menuButton.Frame.Contains (location)) {
					PresentScene (new MainMenuScene (Size));
				}

			}
		}












		/// <summary>
		/// ///////////////DATA BASE TRIAL
		/// </summary>

		public void demo() {
			var connection = GetConnection ();
			using (var cmd = connection.CreateCommand ()) {
				connection.Open ();
				cmd.CommandText = "SELECT * FROM People";
				using (var reader = cmd.ExecuteReader ()) {
					while (reader.Read ()) {
						Console.Error.Write ("(Row ");
						Write (reader, 0);
						for (int i = 1; i < reader.FieldCount; ++i) {
							Console.Error.Write(" ");
							Write (reader, i);
						}
						Console.Error.WriteLine(")");
					}
				}
				connection.Close ();
			}

		}        

		public static SqliteConnection GetConnection()
		{
			var documents = Environment.GetFolderPath (
				Environment.SpecialFolder.Personal);
			string db = Path.Combine (documents, "mydb.db3");
			bool exists = File.Exists (db);
			if (!exists)
				SqliteConnection.CreateFile (db);
			var conn = new SqliteConnection("Data Source=" + db);
			if (!exists) {
				var commands = new[] {
					"CREATE TABLE People (PersonID INTEGER NOT NULL, FirstName ntext, LastName ntext)",
					"INSERT INTO People (PersonID, FirstName, LastName) VALUES (1, 'First', 'Last')",
					"INSERT INTO People (PersonID, FirstName, LastName) VALUES (2, 'Morteza', 'Jalali')",
					"INSERT INTO People (PersonID, FirstName, LastName) VALUES (3, 'Andyyy', 'Howaaa')",
				};
				conn.Open ();
				foreach (var cmd in commands) {
					using (var c = conn.CreateCommand()) {
						c.CommandText = cmd;
						c.CommandType = CommandType.Text;
						c.ExecuteNonQuery ();
					}
				}
				conn.Close ();
			}
			return conn;
		}

		public static void Write(SqliteDataReader reader, int index)
		{
			Console.Error.Write("({0} '{1}')", 
				reader.GetName(index), 
				reader [index]);
		}





	}
}













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





