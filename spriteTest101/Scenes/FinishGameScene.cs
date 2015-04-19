using System;
using CoreGraphics;
using CoreImage;
using Foundation;
using SpriteKit;
using UIKit;
using SQLite;
using System.IO;

using System.Data.Sql;
using Mono.Data.Sqlite;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using GameKit;

namespace spriteTest101
{
	public class FinishGameScene:BasicScene
	{
		//SKNode buttonBottomLeft   = new SKNode ();
		//SKNode buttonTopRight     = new SKNode ();
		//SKNode buttonBottomRight  = new SKNode ();
		SKLabelNode SCORECARD      = new SKLabelNode ();

		SKSpriteNode FINALIMG = new SKSpriteNode();
		SKLabelNode backButton = new SKLabelNode();

		SKLabelNode restartButton = new SKLabelNode () {
			FontName = "GillSans-Bold",
			FontColor = UIColor.White,
			FontSize = 40,
			Text = "Restart",
		};

	
		SKLabelNode nextLevelButton  = new SKLabelNode() {
			FontName = "GillSans-Bold",
			FontColor = UIColor.White,
			FontSize = 40,
			Text = "Next Level",
		};



		public FinishGameScene(CGSize size):base(size)
		{

			FinishingDataMethod ();



			//var prevScore = GameObj.CurrentLevelArr [(   (int)(ll) - 1   )];

			setupBasicScreen ();

			if (GameObj.finalScore < GameObj.ToBeatScore-1 || GameObj.finalScore == 0) {
				Console.WriteLine ("FAILEDDDD");
				setupFailedScreen ();
				//continue;
				//continue;
			}else {

				setupCompletedScreen ();
				checkHighest (GameObj.finalScore, GameObj.CurrentGame.Name);

			}



		}
	

		public async void checkHighest(int sc, string level){

			string catString = "com.commtech.inapppurchTest.StageScores";  //com.commtech.inapppurchTest.

			GKLocalPlayer xx = GKLocalPlayer.LocalPlayer;

			GKLeaderboard Leaderboard = new GKLeaderboard (new [] { xx.PlayerID }) {
				Identifier = catString
			};

			var ss = Leaderboard.LocalPlayerScore;
			//var scores = await Leaderboard.LoadScoresAsync ();
			var oldScore = 0;//ss.Value;
			//var oldScore = current.Value;

//			if (ss.Value != null) {
//			
//				oldScore = (int)ss.Value;
//			}
//			var Category = Convert.ToInt64(  level.Substring (1, 1));
//
//			Console.WriteLine ("XXXXXXXXXXXXXXXXXXMXMXMXMXMMXMXMXXXXXXMXMXMXXXXXXXX");
//

//			if (oldScore == null) {
				var newScore = new GKScore (catString) {
					Value = sc

				};
				newScore.ReportScore (new Action<NSError> ((error) => {
				if (error == null) {
					Console.WriteLine ("YYYYYYYYYYYYYYYYYYYYYYYYYYBBBYYYBYBYBYBYYyy");

					//new UIAlertView ("Score reported", "Score Reported successfully", null, "OK", null).Show ();
				} else {
					Console.WriteLine ("XXXXXXXXXXXXXXXXXXMXMXMXMXMMXMXMXXXXXXMXMXMXXXXXXXX");

					//new UIAlertView ("Score Reported Failed", "Score Reported Failed", null, "OK", null).Show ();
				}
				NSThread.SleepFor (1);
				//controller.updateHighScore ();
			}));

//
//			Console.WriteLine ("score::"  + newScore.Value);
//				await GKScore.ReportScoresAsync(new [] {  newScore });
////
//			}
//			else if (sc > oldScore) {
//			//	gcm.reportScore (sc, car);
//				var newScore = new GKScore (catString) {
//					Value = sc
//
//				};
//
//				//await GKScore.ReportScoresAsync(new [] {  newScore });
//			
//			}


			//GameCenterManager1 gcm = new GameCenterManager1 ();


		
		}


		public void setupBasicScreen(){
		
			BackgroundColor = UIColor.White;

			textScaling (nextLevelButton);
			textScaling (restartButton);

			restartButton.Position = new CGPoint (Size.Width / 2, 200);
			nextLevelButton.Position = new CGPoint (Size.Width / 2, 200);


			backButton.Text = "Menu";
			backButton.FontColor = UIColor.White;
			backButton.FontName = "GillSans-Bold";
			backButton.FontSize = 30;
			backButton.Position = new CGPoint (Size.Width/6, Size.Height - 55);


			AddChild (backButton);


			SCORECARD.Text = "POINTS: "+GameObj.finalScore;
			SCORECARD.FontColor = UIColor.White;
			SCORECARD.FontName = "GillSans-Bold";
			SCORECARD.FontSize = 30;
			SCORECARD.Position = new CGPoint (Size.Width / 2, Size.Height - 200);
			AddChild (SCORECARD);

		
		}

		public void setupFailedScreen(){
		
			nextLevelButton.RemoveFromParent ();

			var fLabel = new SKLabelNode {
				FontName = "GillSans-Bold",
				FontColor = UIColor.White,
				FontSize = 40,
				Position = new CGPoint (Size.Width / 2, Size.Height - 150),
				Text = "You Lose!",
			};

			var tLabel = new SKLabelNode {
				FontName = "GillSans-Bold",
				FontSize = 25,
				FontColor = UIColor.White,
				Position = new CGPoint (Size.Width / 2, Size.Height / 2),
				Text = "Time to beat: "+GameObj.ToBeatScore,
			};

			AddChild (restartButton);

			AddChild (fLabel);
			AddChild (tLabel);

		
		}



		public void textScaling(SKLabelNode scaled){

			var up   = SKAction.ScaleBy (1.2f, 0.5f);
			//var down = SKAction.ScaleBy (0.8f, 0.5f);
			var seq = SKAction.Sequence (new SKAction[]{up,up.ReversedAction});//({up,down}); 
			var fort = SKAction.RepeatActionForever (seq);

			scaled.RunAction (fort);

		}


		public void setupCompletedScreen(){

			restartButton.RemoveFromParent ();
			AddChild (nextLevelButton);


			var wLabel = new SKLabelNode {
				FontName = "GillSans-Bold",
				FontColor = UIColor.White,
				FontSize = 40,
				Position = new CGPoint (Size.Width / 2, Size.Height - 150),
				Text = "You Win!",
			};


			AddChild (wLabel);


			var ll = GameObj.CurrentGame.Name.Substring (2, 1);
			Console.WriteLine ("level num   "+GameObj.levelSelected);
			Console.WriteLine ("line numm   "+GameObj.LineNum);
			Console.WriteLine ("column      "+ll);
			Console.WriteLine ("finascore   "+GameObj.finalScore);

			if 	(GameObj.levelSelected == 1) {

				if 		(GameObj.LineNum == 1) {
					if (ll == "1") {
						EasyLevelObj.level1 [0] = GameObj.finalScore;
					} 
					else if (ll == "2") {
						EasyLevelObj.level1 [1] = GameObj.finalScore;
					}
					else if (ll == "3") {
						EasyLevelObj.level1 [2] = GameObj.finalScore;
					}
					else if (ll == "4") {
						EasyLevelObj.level1 [3] = GameObj.finalScore;
					}
					else if (ll == "5") {
						EasyLevelObj.level1 [4] = GameObj.finalScore;
					}


				}
				else if (GameObj.LineNum == 2) {

					if (ll == "1") {
						EasyLevelObj.level2 [0] = GameObj.finalScore;
					} 
					else if (ll == "2") {
						EasyLevelObj.level2 [1] = GameObj.finalScore;
					}
					else if (ll == "3") {
						EasyLevelObj.level2 [2] = GameObj.finalScore;
					}
					else if (ll == "4") {
						EasyLevelObj.level2 [3] = GameObj.finalScore;
					}
					else if (ll == "5") {
						EasyLevelObj.level2 [4] = GameObj.finalScore;
					}
				}
				else if (GameObj.LineNum == 3) {


					if (ll == "1") {
						EasyLevelObj.level3 [0] = GameObj.finalScore;
					} 
					else if (ll == "2") {
						EasyLevelObj.level3 [1] = GameObj.finalScore;
					}
					else if (ll == "3") {
						EasyLevelObj.level3 [2] = GameObj.finalScore;
					}
					else if (ll == "4") {
						EasyLevelObj.level3 [3] = GameObj.finalScore;
					}
					else if (ll == "5") {
						EasyLevelObj.level3 [4] = GameObj.finalScore;
					}

				}
				else if (GameObj.LineNum == 4) {
					if (ll == "1") {
						EasyLevelObj.level4 [0] = GameObj.finalScore;
					} 
					else if (ll == "2") {
						EasyLevelObj.level4 [1] = GameObj.finalScore;
					}
					else if (ll == "3") {
						EasyLevelObj.level4 [2] = GameObj.finalScore;
					}
					else if (ll == "4") {
						EasyLevelObj.level4 [3] = GameObj.finalScore;
					}
					else if (ll == "5") {
						EasyLevelObj.level4 [4] = GameObj.finalScore;
					}

				}
				else if (GameObj.LineNum == 5) {

					if (ll == "1") {
						EasyLevelObj.level5 [0] = GameObj.finalScore;
					} 
					else if (ll == "2") {
						EasyLevelObj.level5 [1] = GameObj.finalScore;
					}
					else if (ll == "3") {
						EasyLevelObj.level5 [2] = GameObj.finalScore;
					}
					else if (ll == "4") {
						EasyLevelObj.level5 [3] = GameObj.finalScore;
					}
					else if (ll == "5") {
						EasyLevelObj.level5 [4] = GameObj.finalScore;
					}
				}

				//demo ();
				FinishingDataMethod ();

			} 

		}


		public static void FinishingDataMethod(){
			var dbpath = Path.Combine (
				Environment.GetFolderPath (Environment.SpecialFolder.Personal),
				"DatabasePath.db3");

			var db = new SQLiteConnection (dbpath);
			//bool exists = File.Exists (db);
			if (db.Table<DataModel> ().Count () != 0) {
				db.DeleteAll<DataModel> ();
				var newScore = new DataModel ();
				newScore.Level1 = intArrToString (EasyLevelObj.level1);
				newScore.Level2 = intArrToString (EasyLevelObj.level2);
				newScore.Level3 = intArrToString (EasyLevelObj.level3);
				newScore.Level4 = intArrToString (EasyLevelObj.level4);
				newScore.Level5 = intArrToString (EasyLevelObj.level5);
				db.Insert (newScore);
			}
//			var table = db.Table<DataModel> ();
//			foreach (var s in table) {
//			
//				Console.WriteLine (s.ID + " " + s.Level1);
//				Console.WriteLine (s.ID + " " + s.Level2);
//				Console.WriteLine (s.ID + " " + s.Level3);
//				Console.WriteLine (s.ID + " " + s.Level4);
//				Console.WriteLine (s.ID + " " + s.Level5);
//			
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




		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			foreach (var touch in touches) {
				CGPoint location = (touch as UITouch).LocationInNode (this);
				if (restartButton.Frame.Contains (location)) {

					PresentScene (new MyScene (Size));
					continue;
				}


				if (nextLevelButton.Frame.Contains (location)) {

					var tempN = new SKSpriteNode ();
					tempN.Name = GameObj.CurrentGame.Name.Substring (0, 2);
					Console.WriteLine ("eeee::::" + tempN.Name);
					var lastDigit    = GameObj.CurrentGame.Name.Substring (2, 1);

					if 		  (lastDigit == "1") {
						tempN.Name = tempN.Name + "2";

					} 
					else if (lastDigit == "2") {
						tempN.Name = tempN.Name + "3";


					} else if (lastDigit == "3") {
						tempN.Name = tempN.Name + "4";


					} else if (lastDigit == "4") {
						tempN.Name = tempN.Name + "5";

					} else if (lastDigit == "5") {

						PresentScene (new MainMenuScene (Size));
					}



					GameObj.CurrentGame = tempN;
					GameObj.ToBeatScore = GameObj.finalScore;


					PresentScene (new LevelsScene (Size));     //MyScene (Size));
					//continue;
				}

				if (backButton.Frame.Contains (location)) {


					PresentScene(new MainMenuScene(Size));


				}
			}
		}



	
	}
}























//BackgroundColor = UIColor.Blue;


//			buttonTopLeft.Frame.Width = size.Width / 5f;
//			buttonTopLeft.Frame.Height = size.Height / 8f;

//			var bfr = buttonTopLeft.Frame; 
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




//			else if (GameObj.levelSelected == 2) {
//
//
//				if (GameObj.LineNum == 1) {
//
//					if (ll == "1") {
//						MediumLevelObj.level1 [0] = GameObj.finalScore;
//					} else if (ll == "2") {
//						MediumLevelObj.level1 [1] = GameObj.finalScore;
//					} else if (ll == "3") {
//						MediumLevelObj.level1 [2] = GameObj.finalScore;
//					} else if (ll == "4") {
//						MediumLevelObj.level1 [3] = GameObj.finalScore;
//					} else if (ll == "5") {
//						MediumLevelObj.level1 [4] = GameObj.finalScore;
//					}
//
//				} 
//				else if (GameObj.LineNum == 2) {
//					if (ll == "1") {
//						MediumLevelObj.level2 [0] = GameObj.finalScore;
//					} else if (ll == "2") {
//						MediumLevelObj.level2 [1] = GameObj.finalScore;
//					} else if (ll == "3") {
//						MediumLevelObj.level2 [2] = GameObj.finalScore;
//					} else if (ll == "4") {
//						MediumLevelObj.level2 [3] = GameObj.finalScore;
//					} else if (ll == "5") {
//						MediumLevelObj.level2 [4] = GameObj.finalScore;
//					}
//				} else if (GameObj.LineNum == 3) { 
//					if (ll == "1") {
//						MediumLevelObj.level3 [0] = GameObj.finalScore;
//					} else if (ll == "2") {
//						MediumLevelObj.level3 [1] = GameObj.finalScore;
//					} else if (ll == "3") {
//						MediumLevelObj.level3 [2] = GameObj.finalScore;
//					} else if (ll == "4") {
//						MediumLevelObj.level3 [3] = GameObj.finalScore;
//					} else if (ll == "5") {
//						MediumLevelObj.level3 [4] = GameObj.finalScore;
//					}
//
//				} else if (GameObj.LineNum == 4) { 
//					if (ll == "1") {
//						MediumLevelObj.level4 [0] = GameObj.finalScore;
//					} else if (ll == "2") {
//						MediumLevelObj.level4 [1] = GameObj.finalScore;
//					} else if (ll == "3") {
//						MediumLevelObj.level4 [2] = GameObj.finalScore;
//					} else if (ll == "4") {
//						MediumLevelObj.level4 [3] = GameObj.finalScore;
//					} else if (ll == "5") {
//						MediumLevelObj.level4 [4] = GameObj.finalScore;
//					}
//				} else if (GameObj.LineNum == 5) { 
//					if (ll == "1") {
//						MediumLevelObj.level5 [0] = GameObj.finalScore;
//					} else if (ll == "2") {
//						MediumLevelObj.level5 [1] = GameObj.finalScore;
//					} else if (ll == "3") {
//						MediumLevelObj.level5 [2] = GameObj.finalScore;
//					} else if (ll == "4") {
//						MediumLevelObj.level5 [3] = GameObj.finalScore;
//					} else if (ll == "5") {
//						MediumLevelObj.level5 [4] = GameObj.finalScore;
//					}
//
//				}
//			}
//
//			else if (GameObj.levelSelected == 3) {
//
//				if (GameObj.LineNum == 1) { 
//					if (ll == "1") {
//						HardLevelObj.level1 [0] = GameObj.finalScore;
//					} else if (ll == "2") {
//						HardLevelObj.level1 [1] = GameObj.finalScore;
//					} else if (ll == "3") {
//						HardLevelObj.level1 [2] = GameObj.finalScore;
//					} else if (ll == "4") {
//						HardLevelObj.level1 [3] = GameObj.finalScore;
//					} else if (ll == "5") {
//						HardLevelObj.level1 [4] = GameObj.finalScore;
//					} 
//				}
//
//				else if (GameObj.LineNum == 2) {
//					if (ll == "1") {
//						HardLevelObj.level2 [0] = GameObj.finalScore;
//					} else if (ll == "2") {
//						HardLevelObj.level2 [1] = GameObj.finalScore;
//					} else if (ll == "3") {
//						HardLevelObj.level2 [2] = GameObj.finalScore;
//					} else if (ll == "4") {
//						HardLevelObj.level2 [3] = GameObj.finalScore;
//					} else if (ll == "5") {
//						HardLevelObj.level2 [4] = GameObj.finalScore;
//					} 
//				}	
//
//				else if (GameObj.LineNum == 3) {
//					if (ll == "1") {
//						HardLevelObj.level3 [0] = GameObj.finalScore;
//					} else if (ll == "2") {
//						HardLevelObj.level3 [1] = GameObj.finalScore;
//					} else if (ll == "3") {
//						HardLevelObj.level3 [2] = GameObj.finalScore;
//					} else if (ll == "4") {
//						HardLevelObj.level3 [3] = GameObj.finalScore;
//					} else if (ll == "5") {
//						HardLevelObj.level3 [4] = GameObj.finalScore;
//					} 
//				}	
//
//				else if (GameObj.LineNum == 4) { 
//					if (ll == "1") {
//						HardLevelObj.level4 [0] = GameObj.finalScore;
//					} else if (ll == "2") {
//						HardLevelObj.level4 [1] = GameObj.finalScore;
//					} else if (ll == "3") {
//						HardLevelObj.level4 [2] = GameObj.finalScore;
//					} else if (ll == "4") {
//						HardLevelObj.level4 [3] = GameObj.finalScore;
//					} else if (ll == "5") {
//						HardLevelObj.level4 [4] = GameObj.finalScore;
//					} 
//				}	
//
//				else if (GameObj.LineNum == 5) {
//					if (ll == "1") {
//						HardLevelObj.level5 [0] = GameObj.finalScore;
//					} else if (ll == "2") {
//						HardLevelObj.level5 [1] = GameObj.finalScore;
//					} else if (ll == "3") {
//						HardLevelObj.level5 [2] = GameObj.finalScore;
//					} else if (ll == "4") {
//						HardLevelObj.level5 [3] = GameObj.finalScore;
//					} else if (ll == "5") {
//						HardLevelObj.level5 [4] = GameObj.finalScore;
//					} 
//				}	
//
//












//}






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
////			int[] l1 = EasyLevelObj.level1;
////			int[] l2 = EasyLevelObj.level2;
////			//new int[] { 3,5,6,3,5 };
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
//				//var commands = //new[] {
//				//					"INSERT INTO LevelsDB (PersonID, FirstName, LastName, Level2) VALUES (2, 'Morteza', 'Jalali','"+ Arraystring+"')",
//				//					"INSERT INTO LevelsDB (PersonID, FirstName, LastName) VALUES (3, 'Andyyy', 'Howaaa')",
//				//};
//
//				string[] commands = new string[2];
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
//						c.ExecuteNonQuery ();
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
//					commands1 [1] = "INSERT INTO LevelsDB (PersonID, Level1, Level2, Level3, Level4, Level5) VALUES (1, '" 
//
//
//						+ intArrToString (EasyLevelObj.level1) + "', '" + intArrToString (EasyLevelObj.level2) + "','" + intArrToString (EasyLevelObj.level3) + "','" + intArrToString (EasyLevelObj.level4) + "','" + intArrToString (EasyLevelObj.level5) + "')";
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
