using System;
using CoreGraphics;
using AVKit;
using AVFoundation;
using CoreAudioKit;

using AudioUnit;
using AudioToolbox;
using CoreImage;
using Foundation;
using SpriteKit;
using UIKit;


namespace spriteTest101
{
	public class LevelsScene:BasicScene
	{

		SKLabelNode playButton = new SKLabelNode();
//		SKLabelNode buttonTopLeft      = new SKLabelNode ();
		SKLabelNode backButton = new SKLabelNode();

//		SKLabelNode easyLabel = new SKLabelNode ();
//		SKLabelNode mediumLabel = new SKLabelNode ();
//		SKLabelNode hardLabel = new SKLabelNode ();
//

		SKSpriteNode prevClicked = new SKSpriteNode ();
//		SKSpriteNode currentClicked = new SKSpriteNode();

		SKSpriteNode rowColour = new SKSpriteNode ();  
		
		SKLabelNode HLabelLevel  = new SKLabelNode();
		SKLabelNode HLabelScore  = new SKLabelNode();
		SKLabelNode HLabelStatus = new SKLabelNode();

		SKLabelNode _menuButton = new SKLabelNode(){
			Text = "Menu",
			FontName = "GillSans-Bold",
			FontSize = 26,
			FontColor = UIColor.White,

		};


		public LevelsScene(CGSize size):base(size)
		{
			SetupScene ();
			GameObj.levelSelected = 1;

			var topPoint = Size.Height / 1.5f ;

			createLevelItems (1,topPoint  , EasyLevelObj.level1);
			createLevelItems (2,topPoint - 60 , EasyLevelObj.level2);
			createLevelItems (3,topPoint - 120  , EasyLevelObj.level3);
			createLevelItems (4,topPoint - 180f  , EasyLevelObj.level4);
			createLevelItems (5,topPoint - 240f  , EasyLevelObj.level5);

		}

		public void textScaling(SKLabelNode scaled){

			var up   = SKAction.ScaleBy (1.2f, 0.5f);
			//var down = SKAction.ScaleBy (0.8f, 0.5f);
			var seq = SKAction.Sequence (new SKAction[]{up,up.ReversedAction});//({up,down}); 
			var fort = SKAction.RepeatActionForever (seq);

			scaled.RunAction (fort);

		}

		public void SetupScene(){
		
			BackgroundColor = UIColor.White;

			playButton.Text = "Play!";
			playButton.FontColor = UIColor.White;
			playButton.FontName = "GillSans-Bold";
			playButton.FontSize = 30;
			playButton.Position = new CGPoint (Size.Width / 2, Size.Height / 8f);
			textScaling (playButton);
			AddChild (playButton);

			rowColour = SKSpriteNode.FromColor (UIColor.Orange, 
				new CGSize (Size.Width, 50));
			rowColour.Hidden = true;

			HLabelLevel.FontName = "GillSans-Bold";
			HLabelLevel.FontSize = 25;
			HLabelLevel.FontColor = UIColor.White;

			HLabelScore.FontName = "GillSans-Bold";
			HLabelScore.FontSize = 25;
			HLabelScore.FontColor = UIColor.White;



			_menuButton.Position = new CGPoint (Size.Width/6, Size.Height - 55);


			HLabelLevel.Position = new CGPoint (Size.Width / 2, Size.Height / 1.25f);
			HLabelScore.Position = new CGPoint (Size.Width / 2, Size.Height / 1.36f);

			AddChild (rowColour);
			AddChild (HLabelLevel);
			AddChild (HLabelScore);
			AddChild (_menuButton);

		
		}


		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			foreach (var touch in touches) {
				CGPoint location = (touch as UITouch).LocationInNode (this);
				GameObj.ToBeatScore = -13;

				if (_menuButton.Frame.Contains (location)) {
					PresentScene(new MainMenuScene(Size));
				}
					
				SKNode no = GetNodeAtPoint (location);


				if (no.Name != null) {

					playButtonPressedSound ();
					Console.WriteLine ("\n\nNODE NAME::::    " + no.Name);	
					if (prevClicked.Position == no.Position) {
						//prevClicked.Color = UIColor.Yellow;
						continue;
					}

					if (prevClicked.Name != null) {
						prevClicked.Color = UIColor.Red;
					}


					var spN = (SKSpriteNode) getNodeWhole (no.Position, 0);
					var rtt = (SKLabelNode)getNodeWhole (no.Position, 1);

					var xx = spN; 
					//currentClicked = spN;
					prevClicked = spN;
				
					rowColour.Position = new CGPoint (Size.Width/2, spN.Position.Y);
					rowColour.Hidden = true;



					var lN = Convert.ToInt32 (spN.Name.Substring (1, 1));
					loadLevelSettings(lN);


					int ss =  Convert.ToInt32( spN.Name.Substring (2, 1));

					if (ss != 1) {
						if (GameObj.CurrentLevelArr [ss - 2] == 0) {
							Console.WriteLine ("SS     :::::" + ss);

							Console.WriteLine ("PREVIOUS GAME SCORE:::::" + GameObj.CurrentLevelArr [0]);

							spN.Color = UIColor.Purple;
							playButton.Hidden = true;
							continue;
						}
					}

					if (rtt.Text == "0") {

						if      (ss == 1) { 
							spN.Color = UIColor.Green;
						}
						else if (ss == 2) {
							if (GameObj.CurrentLevelArr [0] != 0) {
								spN.Color = UIColor.Green;

							}
						}
						else if (ss == 3) {
							if (GameObj.CurrentLevelArr [1] != 0) {
								spN.Color = UIColor.Green;

							}
						}
						else if (ss == 4) {
							if (GameObj.CurrentLevelArr [2] != 0) {
								spN.Color = UIColor.Green;

							}
						}
						else if (ss == 5) {
							if (GameObj.CurrentLevelArr [3] != 0) {
								spN.Color = UIColor.Green;

							}
						}
					} 

					GameObj.CurrentGame = spN;


					if (xx.Name.Substring (1, 1) == "1") {
						updateHeader (false, -10);
					}
					else if (xx.Name.Substring (1, 1) == "2") {
						int level1tot = 0;

						bool emptCheck = false;
						foreach (int yy in EasyLevelObj.level1) {

							if (yy == 0) {
								emptCheck = true;
							}
							level1tot = level1tot + yy;

						}

						if (level1tot < 150 && emptCheck) {
							
							updateHeader (emptCheck, level1tot);	
							xx.Color = UIColor.Purple;
							Console.WriteLine ("COMPLETE PREVIOUS LEVEL");

						} else {
							updateHeader (false, -10);
						}

					}else if(xx.Name.Substring (1, 1) == "3"){

						int level2tot = 0;
						bool emptCheck = false;
						foreach (int yy in EasyLevelObj.level2) {

							if (yy == 0) { emptCheck = true; }
							level2tot = level2tot + yy;
						}

						if (level2tot < 100 && emptCheck) {
							updateHeader (emptCheck, level2tot);
							xx.Color = UIColor.Purple;
							Console.WriteLine ("COMPLETE PREVIOUS LEVEL");

						} else {
							updateHeader (false, -10);
						}

					}else if(xx.Name.Substring (1, 1) == "4"){

						int level3tot = 0;
						bool emptCheck = false;
						foreach (int yy in EasyLevelObj.level3) {

							if (yy == 0) {
								emptCheck = true;
							}
							level3tot = level3tot + yy;

						}

						if (level3tot < 100 && emptCheck) {
							updateHeader (emptCheck,level3tot);
							xx.Color = UIColor.Purple;
							Console.WriteLine ("COMPLETE PREVIOUS LEVEL");

						}else {
							updateHeader (false, -10);
						}

					}else if(xx.Name.Substring (1, 1) == "5"){

						int level4tot = 0;
						bool emptCheck = false;
						foreach (int yy in EasyLevelObj.level4) {

							if (yy == 0) {
								emptCheck = true;
							}
							level4tot = level4tot + yy;

						}

						if (level4tot < 100 && emptCheck) {
							
							xx.Color = UIColor.Purple;
							Console.WriteLine ("COMPLETE PREVIOUS LEVEL");

						}else {
							updateHeader (false, -10);
						}
					}
				}

				if (playButton.Frame.Contains (location) && !playButton.Hidden) {
					PresentScene (new MyScene (Size));
				}

			}
		}


		public void playButtonPressedSound(){

			//var actionSound = SKAction.PlaySoundFileNamed ("Buttonpressed1.wav", true); 

			this.RunAction(SKAction.PlaySoundFileNamed ("./MyOutput.caf", false));
		
		}


		public void loadLevelSettings(int llnm){
		
			if (llnm == 1) {
				GameObj.CurrentLevelArr = EasyLevelObj.level1;
				GameObj.LineNum = 1;

			} 
			else if (llnm == 2) {
				GameObj.CurrentLevelArr = EasyLevelObj.level2;
				GameObj.LineNum = 2;
			}
			else if (llnm == 3) {
				GameObj.CurrentLevelArr = EasyLevelObj.level3;
				GameObj.LineNum = 3;
			}
			else if (llnm == 4) {
				GameObj.CurrentLevelArr = EasyLevelObj.level4;
				GameObj.LineNum = 4;
			}
			else if (llnm == 5) {
				GameObj.CurrentLevelArr = EasyLevelObj.level5;
				GameObj.LineNum = 5;
			}
		
		
		}



		public void updateHeader(bool bb, int totlvl){
		
			if (bb) {
				HLabelLevel.Text = "Level " + GameObj.LineNum + " LOCKED! ";
				HLabelScore.FontSize = 15;
				HLabelScore.Text = "Complete All Stages From Previous Level";
//				HLabelLevel.Position = new CGPoint (Size.Width / 4, Size.Height / 1.25f);
//				HLabelScore.Position = new CGPoint (Size.Width / 4, Size.Height / 1.36f);


				playButton.Hidden = true;
			} else if (!bb && totlvl < 100 && totlvl > 0) {

				HLabelLevel.Text = "Level " + GameObj.LineNum + " LOCKED! ";
				HLabelScore.FontSize = 15;

				HLabelScore.Text = "You're Score is Too Low";
//
//				HLabelLevel.Position = new CGPoint (Size.Width / 2, Size.Height / 1.25f);
//				HLabelScore.Position = new CGPoint (Size.Width / 5, Size.Height / 1.36f);
//
//
				playButton.Hidden = true;

			} else if (!bb && totlvl == -10) {
			
				HLabelLevel.Text = "Level " + GameObj.LineNum;
				HLabelScore.FontSize = 25;

				var ff = Convert.ToInt32 (GameObj.CurrentGame.Name.Substring (2, 1));  //currentClicked.Name.Substring (2, 1));
				int tot = 0;

				foreach (int xc in GameObj.CurrentLevelArr) {
					tot = tot + xc;
				}

				HLabelScore.Text = "Score " + tot;
				playButton.Hidden = false;

			}

		}


		public void addItemBackground(CGPoint itemPos){

			var bottleCap = new SKSpriteNode(SKTexture.FromImageNamed("capitem1"));
			bottleCap.SetScale (0.4f);
			bottleCap.Position = itemPos;
			AddChild (bottleCap);

		}



		public SKNode getNodeWhole (CGPoint point,int u){
		
			var nodesArr = GetNodesAtPoint (point);
			var finalN = new SKNode ();
			var nL = new SKNode();
			var nS = new SKNode();

			foreach (SKNode t in nodesArr) {
			
			
				if (t.Name == "aNode") {
					nL = t;
				}
				else if(t.Name != null && t.Name != "aNode") {
					nS = t;
				}			
			
			}


			if (u == 0) {
				finalN = nS;
			
			} 
			else if (u == 1) {
				finalN = nL;
			}

//			Console.WriteLine ("GOTTEN NODES COUNT::--::" + nodesArr.Length);
//


			return finalN;
		}




		public void createLevelItems(int lev, nfloat h,int[] levelscores) {


			var sp1 = new SKSpriteNode ();
			//sp1.Name = "score";

			var tp1 = new SKLabelNode (){
				Text = ""+levelscores[0],
				FontColor = UIColor.White,
				FontName = "GillSans-Bold",
				FontSize = 15,
				Color = UIColor.White,
				VerticalAlignmentMode = SKLabelVerticalAlignmentMode.Center,
				Name = "aNode",


			};

			var sp2 = new SKSpriteNode ();
			var tp2 = new SKLabelNode (){
				Text = ""+levelscores[1],
				FontColor = UIColor.White,
				FontName = "GillSans-Bold",
				FontSize = 15,
				Color = UIColor.White,
				VerticalAlignmentMode = SKLabelVerticalAlignmentMode.Center
					,
				Name = "aNode"
					
			};

			var sp3 = new SKSpriteNode ();
			var tp3 = new SKLabelNode (){
				Text = ""+levelscores[2],
				FontColor = UIColor.White,
				FontName = "GillSans-Bold",
				FontSize = 15,
				Color = UIColor.White,
				VerticalAlignmentMode = SKLabelVerticalAlignmentMode.Center,
				Name = "aNode"
					
			};

			var sp4 = new SKSpriteNode ();
			var tp4 = new SKLabelNode () {
				Text = "" + levelscores [3],
				FontColor = UIColor.White,
				FontName = "GillSans-Bold",
				FontSize = 15,
				Color = UIColor.White,
				VerticalAlignmentMode = SKLabelVerticalAlignmentMode.Center,
				Name = "aNode"
					
			};

			var sp5 = new SKSpriteNode ();
			var tp5 = new SKLabelNode (){
				Text = ""+levelscores[4],
				FontColor = UIColor.White,
				FontName = "GillSans-Bold",
				FontSize = 15,
				Color = UIColor.White,
				VerticalAlignmentMode = SKLabelVerticalAlignmentMode.Center,
				Name = "aNode"
					
			};

			//CHECK FOR
			//level selected 1 2 3  = easymodel/mediummodel/difficultmodel
			//height for row number
			//column number


			if (GameObj.levelSelected == 1) {
				if 		(lev == 1) {
					
					sp1.Name = "111";
					sp2.Name = "112";
					sp3.Name = "113";
					sp4.Name = "114";
					sp5.Name = "115";

				}
				else if (lev == 2) {


					sp1.Name = "121";
					sp2.Name = "122";
					sp3.Name = "123";
					sp4.Name = "124";
					sp5.Name = "125";
				}
				else if (lev == 3) {

					sp1.Name = "131";
					sp2.Name = "132";
					sp3.Name = "133";
					sp4.Name = "134";
					sp5.Name = "135";

				}
				else if (lev == 4) {


					sp1.Name = "141";
					sp2.Name = "142";
					sp3.Name = "143";
					sp4.Name = "144";
					sp5.Name = "145";

				}
				else if (lev == 5) {

					sp1.Name = "151";
					sp2.Name = "152";
					sp3.Name = "153";
					sp4.Name = "154";
					sp5.Name = "155";

				}


			} 
			else if (GameObj.levelSelected == 2) {


				if 		(lev == 1) {
					sp1.Name = "211";
					sp2.Name = "212";
					sp3.Name = "213";
					sp4.Name = "214";
					sp5.Name = "215";
				}
				else if (lev == 2) {

					sp1.Name = "221";
					sp2.Name = "222";
					sp3.Name = "223";
					sp4.Name = "224";
					sp5.Name = "225";
				}
				else if (lev == 3) {

					sp1.Name = "231";
					sp2.Name = "232";
					sp3.Name = "233";
					sp4.Name = "234";
					sp5.Name = "235";

				}
				else if (lev == 4) {

					sp1.Name = "241";
					sp2.Name = "242";
					sp3.Name = "243";
					sp4.Name = "244";
					sp5.Name = "245";

				}
				else if (lev == 5) {

					sp1.Name = "251";
					sp2.Name = "252";
					sp3.Name = "253";
					sp4.Name = "254";
					sp5.Name = "255";

				}



			} 
			else if (GameObj.levelSelected == 3) {
				
				if 		(lev == 1) {


					sp1.Name = "311";
					sp2.Name = "312";
					sp3.Name = "313";
					sp4.Name = "314";
					sp5.Name = "315";
				}
				else if (lev == 2) {

					sp1.Name = "321";
					sp2.Name = "322";
					sp3.Name = "323";
					sp4.Name = "324";
					sp5.Name = "325";
				}
				else if (lev == 3) {

					sp1.Name = "331";
					sp2.Name = "332";
					sp3.Name = "333";
					sp4.Name = "334";
					sp5.Name = "335";

				}
				else if (lev == 4) {
					sp1.Name = "341";
					sp2.Name = "342";
					sp3.Name = "343";
					sp4.Name = "344";
					sp5.Name = "345";

				}
				else if (lev == 5) {

					sp1.Name = "351";
					sp2.Name = "352";
					sp3.Name = "353";
					sp4.Name = "354";
					sp5.Name = "355";

				}

			}





			sp1.Color = UIColor.Red;
			sp1.Size = new CGSize (27,27);
			sp1.Alpha = 0.3f;



			sp2.Color = UIColor.Red;
			sp2.Size = new CGSize (27,27);
			sp2.Alpha = 0.3f;


			sp3.Color = UIColor.Red;
			sp3.Size = new CGSize (27,27);
			sp3.Alpha = 0.3f;


			sp4.Color = UIColor.Red;
			sp4.Size = new CGSize (27,27);
			sp4.Alpha = 0.3f;


			sp5.Color = UIColor.Red;
			sp5.Size = new CGSize (27,27);
			sp5.Alpha = 0.3f;




			sp3.Position = new CGPoint (Size.Width / 2, h);

			sp1.Position = new CGPoint (33, h);//Size.Height - (Size.Height / 4));
			sp5.Position = new CGPoint (Size.Width - 33, h);


			sp2.Position = new CGPoint ((sp1.Position.X + sp3.Position.X)/2, h);
			sp4.Position = new CGPoint ((sp3.Position.X + sp5.Position.X)/2, h);


			tp1.Position = sp1.Position;
			tp2.Position = sp2.Position;
			tp3.Position = sp3.Position;
			tp4.Position = sp4.Position;
			tp5.Position = sp5.Position;


			addItemBackground (sp1.Position);
			addItemBackground (sp2.Position);
			addItemBackground (sp3.Position);
			addItemBackground (sp4.Position);
			addItemBackground (sp5.Position);




			var t1 = tp1.Frame;
			t1.Height = 33;//(new CGRect(tp1.Position,33,33,33);
			t1.Width = 33;


			var t2 = tp2.Frame;
			t2.Height = 33;//(new CGRect(tp1.Position,33,33,33);
			t2.Width = 33;

			var t3 = tp3.Frame;
			t3.Height = 33;//(new CGRect(tp1.Position,33,33,33);
			t3.Width = 33;


			var t4 = tp4.Frame;
			t4.Height = 33;//(new CGRect(tp1.Position,33,33,33);
			t4.Width = 33;

			var t5 = tp5.Frame;
			t5.Height = 33;//(new CGRect(tp1.Position,33,33,33);
			t5.Width = 33;




			AddChild (sp1);
			AddChild (sp2);
			AddChild (sp3);
			AddChild (sp4);
			AddChild (sp5);


			AddChild (tp1);
			AddChild (tp2);
			AddChild (tp3);
			AddChild (tp4);
			AddChild (tp5);

//			if (tp1.Text == "0") { }else{ AddChild (tp1); }
//			if (tp2.Text == "0") { }else{ AddChild (tp2); }
//			if (tp3.Text == "0") { }else{ AddChild (tp3); }
//			if (tp4.Text == "0") { }else{ AddChild (tp4); }
//			if (tp5.Text == "0") { }else{ AddChild (tp5); }



		}



	
	}
}








///////////////////////////////
/// 
/// TOUCHES DID BEGAN
/// 
/// //					Console.WriteLine ("PREV POS::: " + prevClicked.Position);
//					Console.WriteLine ("NO POSSS::: " + no.Position);

/// //var ree = (SKLabelNode)rr [1];


//th.Name = rr[1].Name;

//						if (ree.Text != "0") {
//							th.Color = UIColor.Yellow;
//						}
//th.Color = UIColor.Yellow;


//if (tx.Text == "0") {}
//prevClicked =th;//xx;

//continue;


//if (GetNodesAtPoint (no.Position).Length == 1) { continue; }



// th SPRITE
// rtt LABEL
//					if (no.Name != "aNode") {
//						th = (SKSpriteNode)rt [1];
//						rtt = (SKLabelNode)rt [0];
//					} else {
//						th = (SKSpriteNode)rt [0];
//						rtt = rt [1];
//					}

//					//th.Name = rr[1].Name;
//					if (rtt.Text != "0") {
//						th.Color = UIColor.Yellow;
//					}
//xx.Color = UIColor.Yellow;
//if(no.Position != prevClicked.Position){
//	prevClicked.Name = "oo";

//xx;
//}



//GameObj.CurrentGame = th;

//var sr = GetChildNode ("rowSelected");
//RemoveChildren (new SKNode[]{sr});
//var noode =	hardLabel.Frame.Contains (location)//(SKLabelNode) GetNodeAtPoint (xx.Position);





//
//					int uu =  Convert.ToInt32 (xx.Name.Substring (1, 1));

//					if (ss == 1) {
//						if         (uu == 1){
//						} else if  (uu == 2){
//						} else if  (uu == 3){
//						} else if  (uu == 4){
//						} else if  (uu == 5){
//						}
//					} else if (ss == 2) {
//						if         (uu == 1){
//							if (EasyLevelObj.level1 [ss - 1] == 0) { continue; }
//						} else if  (uu == 2){
//							if (EasyLevelObj.level2 [ss - 1]== 0) { continue; }
//						} else if  (uu == 3){
//							if (EasyLevelObj.level3 [ss - 1]== 0) { continue; }
//						} else if  (uu == 4){
//							if (EasyLevelObj.level4 [ss - 1]== 0) { continue; }
//						} else if  (uu == 5){
//							if (EasyLevelObj.level5 [ss - 1]== 0) { continue; }
//						}
//
//					} else if (ss == 3) {
//						if         (uu == 1){
//							if (EasyLevelObj.level1 [ss - 1]== 0) { continue; }
//						} else if  (uu == 2){
//							if (EasyLevelObj.level2 [ss - 1]== 0) { continue; }
//						} else if  (uu == 3){
//							if (EasyLevelObj.level3 [ss - 1]== 0) { continue; }
//						} else if  (uu == 4){
//							if (EasyLevelObj.level4 [ss - 1]== 0) { continue; }
//						} else if  (uu == 5){
//							if (EasyLevelObj.level5 [ss - 1]== 0) { continue; }
//						}
//
//					} else if (ss == 4) {
//						if         (uu == 1){
//							if (EasyLevelObj.level1 [ss - 1]== 0) { continue; }
//						} else if  (uu == 2){
//							if (EasyLevelObj.level2 [ss - 1]== 0) { continue; }
//						} else if  (uu == 3){
//							if (EasyLevelObj.level3 [ss - 1]== 0) { continue; }
//						} else if  (uu == 4){
//							if (EasyLevelObj.level4 [ss - 1]== 0) { continue; }
//						} else if  (uu == 5){
//							if (EasyLevelObj.level5 [ss - 1]== 0) { continue; }
//						}
//
//					} else if (ss == 5) {
//						if         (uu == 1){
//							if (EasyLevelObj.level1 [ss - 1]== 0) { continue; }
//						} else if  (uu == 2){
//							if (EasyLevelObj.level2 [ss - 1]== 0) { continue; }
//						} else if  (uu == 3){
//							if (EasyLevelObj.level3 [ss - 1]== 0) { continue; }
//						} else if  (uu == 4){
//							if (EasyLevelObj.level4 [ss - 1]== 0) { continue; }
//						} else if  (uu == 5){
//							if (EasyLevelObj.level5 [ss - 1]) { continue; }
//						}
//					}
//
//


//
//							var comString = "1" + GameObj.LineNum.ToString() + "1";
//							Console.WriteLine ("COMSTRING::   "+comString);
//
//							var newObj = new SKSpriteNode ();
//							newObj = (SKSpriteNode) GetChildNode (comString);
//							newObj.Color = UIColor.Purple;
//
//							updateHeader (emptCheck,level4tot);
//							xx = newObj;





//.Scene.BackgroundColor = UIColor.Yellow;

//}


//				foreach (SKNode ex in Scene.Children) {
//
//					if (ex.Name != null) { 
//						string nn = ex.Name;
//						if (GetChildNode (nn).Frame.Contains (location)) {
//					
//					
//							Console.WriteLine ("NODE NAME::::    " + nn);	
//					
//						}
//					}
//
//
//
//				}
//



//				if (easyLabel.Frame.Contains (location)) {
//
//					RemoveAllChildren ();
//					SetupScene ();
//					GameObj.levelSelected = 1;
//					easyLabel.FontColor = UIColor.Blue;
//					createLevelItems (1,Size.Height / 1.6f  , EasyLevelObj.level1);
//					createLevelItems (2,Size.Height / 1.85f , EasyLevelObj.level2);
//					createLevelItems (3,Size.Height / 2.2f  , EasyLevelObj.level3);
//					createLevelItems (4,Size.Height / 2.7f  , EasyLevelObj.level4);
//					createLevelItems (5,Size.Height / 3.5f  , EasyLevelObj.level5);
//
//
//
//
//				}
//				if (mediumLabel.Frame.Contains (location)) {
//
//
//					RemoveAllChildren ();
//					SetupScene ();
//					GameObj.levelSelected = 2;
//					easyLabel.FontColor = UIColor.Black;
//
//					mediumLabel.FontColor = UIColor.Blue;
//					createLevelItems (1,Size.Height / 1.6f  , MediumLevelObj.level1);
//					createLevelItems (2,Size.Height / 1.85f , MediumLevelObj.level2);
//					createLevelItems (3,Size.Height / 2.2f  , MediumLevelObj.level3);
//					createLevelItems (4,Size.Height / 2.7f  , MediumLevelObj.level4);
//					createLevelItems (5,Size.Height / 3.5f  , MediumLevelObj.level5);
//
//
//
//
//
//				}
//				if (hardLabel.Frame.Contains (location)) {
//
//					RemoveAllChildren ();
//					SetupScene ();
//					GameObj.levelSelected = 3;
//					easyLabel.FontColor = UIColor.Black;
//
//
//					hardLabel.FontColor = UIColor.Blue;
//					createLevelItems (1,Size.Height / 1.6f  , HardLevelObj.level1);
//					createLevelItems (2,Size.Height / 1.85f , HardLevelObj.level2);
//					createLevelItems (3,Size.Height / 2.2f  , HardLevelObj.level3);
//					createLevelItems (4,Size.Height / 2.7f  , HardLevelObj.level4);
//					createLevelItems (5,Size.Height / 3.5f  , HardLevelObj.level5);
//
//				}












//				sp1.Name = "00";
//				sp2.Name = "001";
//				sp3.Name = "002";
//				sp4.Name = "003";
//				sp5.Name = "004";

//				tp1.Name = "00";
//				tp2.Name = "001";
//				tp3.Name = "002";
//				tp4.Name = "003";
//				tp5.Name = "004";
//			

//			 if (h == 1) {
//			
//				sp1.Name = "01";
//				sp2.Name = "02";
//				sp3.Name = "03";
//				sp4.Name = "04";
//				sp5.Name = "05";
//
//
//				tp1.Name = "00";
//				tp2.Name = "001";
//				tp2.Name = "002";
//				tp2.Name = "003";
//				tp2.Name = "004";
//
//
//			}
//			else if (h == 2) {
//			
//				sp1.Name = "1";
//				sp2.Name = "2";
//				sp3.Name = "3";
//				sp4.Name = "4";
//				sp5.Name = "5";
//
//				tp1.Name = "00";
//				tp2.Name = "001";
//				tp2.Name = "002";
//				tp2.Name = "003";
//				tp2.Name = "004";
//
//
//			}
//			else if (h == 3) {
//			
//				sp1.Name = "6";
//				sp2.Name = "7";
//				sp3.Name = "8";
//				sp4.Name = "9";
//				sp5.Name = "11";
//
//
//				tp1.Name = "00";
//				tp2.Name = "001";
//				tp2.Name = "002";
//				tp2.Name = "003";
//				tp2.Name = "004";
//
//			}
//			else if (h == 4) {
//			
//				sp1.Name = "12";
//				sp2.Name = "13";
//				sp2.Name = "14";
//				sp2.Name = "15";
//				sp2.Name = "16";
//
//
//				tp1.Name = "00";
//				tp2.Name = "001";
//				tp2.Name = "002";
//				tp2.Name = "003";
//				tp2.Name = "004";
//
//			}
//








//			buttonTopLeft.Text = "New Game";
//			buttonTopLeft.FontColor = UIColor.White;
//			buttonTopLeft.FontName = "GillSans-Bold";
//			buttonTopLeft.FontSize = 30;
//			buttonTopLeft.Position = new CGPoint (size.Width / 2, size.Height / 1.2f);
//			buttonTopLeft.Frame.Width = size.Width / 5f;
//			buttonTopLeft.Frame.Height = size.Height / 8f;
//
//			var bfr = buttonTopLeft.Frame; 
//
//			bfr.Width = size.Width / 5f;
//			bfr.Height = size.Height / 8f;


//			int[] xx = new int[]{1, 2, 3, 4, 5};
//
//
//			easyLabel.Color = UIColor.Blue;
//			createLevelItems (Size.Height / 1.6f  , xx);
//			createLevelItems (Size.Height / 1.85f , xx);
//			createLevelItems (Size.Height / 2.2f  , xx);
//			createLevelItems (Size.Height / 2.7f  , xx);
//			createLevelItems (Size.Height / 3.5f  , xx);
//

//new CGRect(new CGSize(size.Width / 5, size.Height  /5));
//(Scene.Size.Width / 4, Scene.Size.Height - (Scene.Size.Height / 4));


//buttonTopLeft.Scene.BackgroundColor = UIColor.DarkGray;

//this.AddChild (buttonTopLeft);
//AddChild (buttonTopLeft);


//AddChild (easyLabel);
//AddChild (mediumLabel);
//AddChild (hardLabel);

////////////////////////////
//			GameObj.levelSelected = 1;
//			easyLabel.FontColor = UIColor.Blue;
//			createLevelItems (Size.Height / 1.6f  , EasyLevelObj.level1);
//			createLevelItems (Size.Height / 1.85f , EasyLevelObj.level2);
//			createLevelItems (Size.Height / 2.2f  , EasyLevelObj.level3);
//			createLevelItems (Size.Height / 2.7f  , EasyLevelObj.level4);
//			createLevelItems (Size.Height / 3.5f  , EasyLevelObj.level5);
//
//		
//		SKNode buttonBottomLeft   = new SKNode ();
//		SKNode buttonTopRight     = new SKNode ();
//		SKNode buttonBottomRight  = new SKNode ();
//
//


//			easyLabel.Text = "Easy";
//			mediumLabel.Text = "Medium";
//			hardLabel.Text = "Hard";
//
//			easyLabel.FontColor = UIColor.Black;
//			easyLabel.FontName = "GillSans-Bold";
//			easyLabel.FontSize = 25;
//
//			mediumLabel.FontColor = UIColor.Black;
//			mediumLabel.FontName = "GillSans-Bold";
//			mediumLabel.FontSize = 25;
//
//			hardLabel.FontColor = UIColor.Black;
//			hardLabel.FontName = "GillSans-Bold";
//			hardLabel.FontSize = 25;

//easyLabel.Position = new CGPoint (Size.Width / 5.5f, Size.Height / 1.2f);
//mediumLabel.Position = new CGPoint (Size.Width / 2, Size.Height / 1.2f);
//hardLabel.Position = new CGPoint (Size.Width / 1.2f, Size.Height / 1.2f);



//					else if(xx.Name.StartsWith("2")){
//
//						if (xx.Name.Substring (1, 1) == "1") {
//							GameObj.CurrentLevelArr = MediumLevelObj.level1;
//							GameObj.LineNum = 1;
//							Console.WriteLine ("level1");
//						}
//						else if (xx.Name.Substring (1, 1) == "2") {
//							GameObj.CurrentLevelArr = MediumLevelObj.level2;
//							GameObj.LineNum = 2;
//							Console.WriteLine ("level2");
//						}
//						else if (xx.Name.Substring (1, 1) == "3") {
//							GameObj.CurrentLevelArr = MediumLevelObj.level3;
//							GameObj.LineNum = 3;
//							Console.WriteLine ("level3");
//						}
//						else if (xx.Name.Substring (1, 1) == "4") {
//							GameObj.CurrentLevelArr = MediumLevelObj.level4;
//							GameObj.LineNum = 4;
//							Console.WriteLine ("level4");
//						}
//						else if (xx.Name.Substring (1, 1) == "5") {
//							GameObj.CurrentLevelArr = MediumLevelObj.level5;
//							GameObj.LineNum = 5;
//							Console.WriteLine ("level5");
//						}
//
//					}
//					else if(xx.Name.StartsWith("3")){
//
//						if (xx.Name.Substring (1, 1) == "1") {
//							GameObj.CurrentLevelArr = HardLevelObj.level1;
//							GameObj.LineNum = 1;
//							Console.WriteLine ("level1");
//
//						}
//						else if (xx.Name.Substring (1, 1) == "2") {
//							GameObj.CurrentLevelArr = HardLevelObj.level2;
//							GameObj.LineNum = 2;
//							Console.WriteLine ("level2");
//
//						}
//						else if (xx.Name.Substring (1, 1) == "3") {
//							GameObj.CurrentLevelArr = HardLevelObj.level3;
//							GameObj.LineNum = 3;
//							Console.WriteLine ("level3");
//
//						}
//						else if (xx.Name.Substring (1, 1) == "4") {
//							GameObj.CurrentLevelArr = HardLevelObj.level4;
//							GameObj.LineNum = 4;
//							Console.WriteLine ("level4");
//
//						}
//						else if (xx.Name.Substring (1, 1) == "5") {
//							GameObj.CurrentLevelArr = HardLevelObj.level5;
//							GameObj.LineNum = 5;
//							Console.WriteLine ("level5");
//
//						}
//					}




//SKSpriteNode th = new SKSpriteNode ();

//					if (no.Name == "aNode") {
//						var rr = GetNodesAtPoint (no.Position);
//						Console.WriteLine ("COUNT::: " + rr.Length);
//						th = (SKSpriteNode)rr [0];
//					}
//
//					var rt = GetNodesAtPoint (no.Position);
//					var rtt = new SKLabelNode ();
//					th = (SKSpriteNode)rt [0];
//
//					if (rt [1].Name == "aNode") {
//						rtt = (SKLabelNode)rt [1];
//					}	
//					if (rt [0].Name == "aNode") {
//						rtt = (SKLabelNode)rt [0];
//					}
//
//
//					else {
//						continue;
//					}


//							int level2tot = 0;
//							bool emptCheck = false;
//							foreach (int yy in EasyLevelObj.level2) {
//								if (yy == 0) {emptCheck = true;}
//								level2tot = level2tot + yy;
//							}
//							if (level2tot < 100 && emptCheck) {
//								Console.WriteLine ("COMPLETE PREVIOUS LEVEL");
//							}




//					if (xx.Name.Substring (1, 1) != "1") {
//						Console.WriteLine ("NOT LEVEL1!!!!!!!!!!!!!!!!!");
//					}

//					if     (xx.Name.StartsWith("1")){
//						if (xx.Name.Substring (1, 1) == "1") {
//
//							GameObj.CurrentLevelArr = EasyLevelObj.level1;
//							GameObj.LineNum = 1;
//							Console.WriteLine ("level111");
//						
//						}
//						else if (xx.Name.Substring (1, 1) == "2") {
//
//							GameObj.CurrentLevelArr = EasyLevelObj.level2;
//							GameObj.LineNum = 2;
//							Console.WriteLine ("level2");
//
//						}
//						else if (xx.Name.Substring (1, 1) == "3") {
//
//
//							GameObj.CurrentLevelArr = EasyLevelObj.level3;
//							GameObj.LineNum = 3;
//							Console.WriteLine ("level3");
//
//						}
//						else if (xx.Name.Substring (1, 1) == "4") {
//
//							GameObj.CurrentLevelArr = EasyLevelObj.level4;
//							GameObj.LineNum = 4;
//							Console.WriteLine ("level4");
//
//						}
//						else if (xx.Name.Substring (1, 1) == "5") {
//
//							GameObj.CurrentLevelArr = EasyLevelObj.level5;
//							GameObj.LineNum = 5;
//							Console.WriteLine ("level5");
//
//						}
//
//
//
//					}


//string gy;