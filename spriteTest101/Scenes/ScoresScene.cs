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
	public class ScoresScene:BasicScene
	{


		SKLabelNode _hStage = new SKLabelNode (){
			FontName = "GillSans-Bold",
			FontSize = 26,
			FontColor = UIColor.White,
		};


		SKSpriteNode _scoreSBgr = new SKSpriteNode (UIColor.DarkGray, new CGSize (77f, 30f));
		SKSpriteNode _scoreLBgr = new SKSpriteNode(UIColor.DarkGray,new CGSize(77f,30f));

		SKLabelNode _hStageText = new SKLabelNode (){
			Text = "Stage Score",
			FontName = "GillSans-Bold",
			FontSize = 26,
			FontColor = UIColor.White,
		};

		SKLabelNode _hLevelText = new SKLabelNode (){
			Text = "Level Score",
			FontName = "GillSans-Bold",
			FontSize = 26,
			FontColor = UIColor.White,
		};



		SKLabelNode _hLevel = new SKLabelNode (){
			FontName = "GillSans-Bold",
			FontSize = 26,
			FontColor = UIColor.White,
		};
		SKLabelNode _menuButton = new SKLabelNode (){
			Text = "Menu",
			FontName = "GillSans-Bold",
			FontSize = 26,
			FontColor = UIColor.White,
		};


		public ScoresScene(CGSize size):base(size)
		{
			BackgroundColor = UIColor.White;

		

			_hStageText.Position = new CGPoint (100, Size.Height - 120);
			_hStage.Position = new CGPoint (250, Size.Height - 120);

			_hLevelText.Position = new CGPoint (100, Size.Height - 160);
			_hLevel.Position = new CGPoint (250, Size.Height - 160);

			_scoreSBgr.Position = addHeightToNode( _hStage.Position);
			_scoreLBgr.Position = addHeightToNode( _hLevel.Position);

			_menuButton.Position = new CGPoint (Size.Width/6, Size.Height - 66);


			_hLevel.Text = "" + loadHighestLevel ();
			_hStage.Text = "" + loadHighestStage ();

			AddChild (_menuButton);

			AddChild (_scoreLBgr);
			AddChild (_scoreSBgr);
			AddChild (_hStage);
			AddChild (_hStageText);
			AddChild (_hLevel);
			AddChild (_hLevelText);

			//var highestStage = loadHighestStage ();
			//var highestLevel = loadHighestLevel ();
//			Console.WriteLine ("HIGHEST STAGE " + highestStage);
//			Console.WriteLine ("HIGHEST LEVEL " + highestLevel);
//

		}
	


		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			foreach (var touch in touches) {
				CGPoint location = (touch as UITouch).LocationInNode (this);
				if (_menuButton.Frame.Contains (location)) {
					PresentScene(new MainMenuScene(Size));
				}

			}
		}

		public int loadHighestStage(){
			int hh = 0;

			foreach (int l1 in EasyLevelObj.level1) {
				if (l1 > hh) {
					hh = l1;
				}
			}
			foreach (int l2 in EasyLevelObj.level2) {
				if (l2 > hh) {
					hh = l2;
				}
			}
			foreach (int l3 in EasyLevelObj.level3) {
				if (l3 > hh) {
					hh = l3;
				}
			}
			foreach (int l4 in EasyLevelObj.level4) {
				if (l4 > hh) {
					hh = l4;
				}
			}
			foreach (int l5 in EasyLevelObj.level5) {
				if (l5 > hh) {
					hh = l5;
				}
			}

			return hh;
		}

		public int loadHighestLevel(){
		
			List<int> hl = new List<int>();

			int tot1 = 0;
			foreach (int l1 in EasyLevelObj.level1) {
				tot1 = tot1 + l1;
			}
			int tot2 = 0;
			foreach (int l2 in EasyLevelObj.level2) {
				tot2 = tot2 + l2;
			}
			int tot3 = 0;
			foreach (int l3 in EasyLevelObj.level3) {
				tot3 = tot3 + l3;
			}
			int tot4 = 0;
			foreach (int l4 in EasyLevelObj.level4) {
				tot4 = tot4 + l4;
			}
			int tot5 = 0;
			foreach (int l5 in EasyLevelObj.level5) {
				tot5 = tot5 + l5;
			}


			hl.Add (tot1);
			hl.Add (tot2);
			hl.Add (tot3);
			hl.Add (tot4);
			hl.Add (tot5);
			hl.Sort ();


			return hl.Last ();
		}

		public CGPoint addHeightToNode(CGPoint pos){
		
		
			var x = pos.X;
			var y = pos.Y;

			return new CGPoint (x, y + 10);
		
		
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





