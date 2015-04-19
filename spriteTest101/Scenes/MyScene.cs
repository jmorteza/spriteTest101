using System;
using CoreGraphics;
using CoreImage;
using Foundation;
using SpriteKit;
using UIKit;
using AudioToolbox;
//
//ANIMATIONS
//
//
////time to beat label
//scene beginning animation
//
//time last 5 seconds animation
//
//fine touch throwing animation
//
//
//
//  
namespace spriteTest101
{

	public class MyScene : BasicScene, ISKPhysicsContactDelegate
	{
		//public GameObj gOBJ = new GameObj();
		CGPoint FirstTouch;
		CGPoint LastTouch;

		int lastTimeUpdate = 0 ;
		int Timer;
		int countDown;
		int LevelTime;

		int scoreToBeat;
		int SpriteCount;
		int CurrentScore;
		int startTime1;

		SKSpriteNode PhotoNode    = new SKSpriteNode ();
		SKSpriteNode _spriteFrame = new SKSpriteNode();

		SKSpriteNode PhotoFrameNode = new SKSpriteNode (SKTexture.FromImageNamed ("darttarget"));//(UIColor.DarkGray,new CGSize(200f,200f));
		SKLabelNode aButton = new SKLabelNode();
		SKLabelNode bButton = new SKLabelNode();
		SKLabelNode TimeLabel = new SKLabelNode();

		UIImage hater1;
		//ImageObj xy = new ImageObj();

		bool isImage;
		UIImagePickerController imgPckr = new UIImagePickerController();

		public MyScene (CGSize size) : base (size)
		{
			// Setup your scene here
			BackgroundColor = UIColor.White;

			var cc = GameObj.CurrentGame;
			var nn = cc.Name;
			Console.WriteLine ("::::::" + nn);
			if 		(GameObj.levelSelected == 1) {
				
				if(nn.Substring(2,1) == "1"){
					scoreToBeat = 0;
				}
				else if(nn.Substring(2,1) == "2"){
					scoreToBeat = GameObj.CurrentLevelArr [0];
				}
				else if(nn.Substring(2,1) == "3"){
					scoreToBeat = GameObj.CurrentLevelArr [1];
				}
				else if(nn.Substring(2,1) == "4"){
					scoreToBeat = GameObj.CurrentLevelArr [2];
				}
				else if(nn.Substring(2,1) == "5"){
					scoreToBeat = GameObj.CurrentLevelArr [3];
				}

				if      (nn.Substring (1, 1) == "1") {
					LevelTime = 30+1;
				} 
				else if (nn.Substring (1, 1) == "2") {
					LevelTime = 25 + 1;
				}
				else if (nn.Substring (1, 1) == "3") {
					LevelTime = 20 + 1;
				}
				else if (nn.Substring (1, 1) == "4") {
					LevelTime = 15 + 1;
				}
				else if (nn.Substring (1, 1) == "5") {
					LevelTime = 10 + 1;
				}
			} 
			else if (GameObj.levelSelected == 2) {
				LevelTime = 20+1;

				if(nn.Substring(2,1) == "1"){
					scoreToBeat = 0;
				}
				else if(nn.Substring(2,1) == "2"){
					scoreToBeat = GameObj.CurrentLevelArr [0];
				}
				else if(nn.Substring(2,1) == "3"){
					scoreToBeat = GameObj.CurrentLevelArr [1];
				}
				else if(nn.Substring(2,1) == "4"){
					scoreToBeat = GameObj.CurrentLevelArr [2];
				}
				else if(nn.Substring(2,1) == "5"){
					scoreToBeat = GameObj.CurrentLevelArr [3];
				}

				
			} 
			else if (GameObj.levelSelected == 3) {
				LevelTime = 10+1;

				if(nn.Substring(2,1) == "1"){
					scoreToBeat = 0;
				}
				else if(nn.Substring(2,1) == "2"){
					scoreToBeat = GameObj.CurrentLevelArr [0];
				}
				else if(nn.Substring(2,1) == "3"){
					scoreToBeat = GameObj.CurrentLevelArr [1];
				}
				else if(nn.Substring(2,1) == "4"){
					scoreToBeat = GameObj.CurrentLevelArr [2];
				}
				else if(nn.Substring(2,1) == "5"){
					scoreToBeat = GameObj.CurrentLevelArr [3];
				}
			}

			Console.WriteLine ("SCORE TO BEAT::::" + scoreToBeat);
			Console.WriteLine ("SCORE TO BEATVAR::::" + GameObj.ToBeatScore);

			if (GameObj.ToBeatScore == -13) {
				GameObj.ToBeatScore = scoreToBeat;
			} else {
				scoreToBeat = GameObj.ToBeatScore;
			}

			PhotoFrameNode.Size     = new CGSize  (300, 300);
			PhotoFrameNode.Position = new CGPoint (Size.Width/2 ,Size.Height - (Size.Height / 2.77f) );
	
			AddChild(PhotoFrameNode);

			loadHater ();



			var xx = aButton.Frame;
			aButton.Text = "Menu";
			aButton.FontName = "GillSans-Bold";
			aButton.FontSize = 26;
			aButton.FontColor = UIColor.White;

			aButton.Position = new CGPoint (size.Width/6, 66);

			var yy = aButton.Frame;
		

			bButton.Text = "Picture";
			bButton.FontSize = 26;
			bButton.FontName = "GillSans-Bold";
			bButton.FontColor = UIColor.White;

			//bButton.Position = new CGPoint (size.Width-(size.Width/6), 66);//(size.Height / 12));



			TimeLabel.Text = ""+(LevelTime-1);
			TimeLabel.FontSize = 26;
			TimeLabel.FontName = "GillSans-Bold";
			TimeLabel.FontColor = UIColor.White;
			          TimeLabel.Position = new CGPoint (size.Width-50, size.Height - 55);//(size.Height / 12));

			AddChild (TimeLabel);
			AddChild (aButton);
			//AddChild (bButton);


			//////////////////////////////////////
			var scoreCardLabel = new SKLabelNode () {

				Text = "To Win: " + (GameObj.ToBeatScore+1),
				FontName = "GillSans-Bold",
				FontColor = UIColor.White,
				Position = new CGPoint (75, size.Height - 50),
				FontSize = 26,

			};

			AddChild (scoreCardLabel);

		}


		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			// Called when a touch begins
			foreach (var touch in touches) {
				var location = ((UITouch)touch).LocationInNode (this);
				FirstTouch = location;
				System.Diagnostics.Debug.WriteLine ("FIRST TOUCH:  "+FirstTouch);
			
				if (aButton.ContainsPoint(location))	{
					System.Diagnostics.Debug.WriteLine ("::::::::::BUTTON 1:::::::::::::"+FirstTouch);

					//var mm1 = new MainMenuScene(Size);
					PresentScene(new MainMenuScene(Size));
				}

				if(bButton.ContainsPoint(location)){

//					imgPckr =  new UIImagePickerController ();
//					imgPckr.Delegate = imgPckr;
//					imgPckr.AllowsEditing = true;
//					imgPckr.SourceType = UIImagePickerControllerSourceType.Camera;
//					imgPckr.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.Camera);
//					imgPckr.FinishedPickingMedia += Handle_FinishedPickingMedia;
//					Scene.View.Window.RootViewController.PresentModalViewController (imgPckr, true);

				}

			}
		}


		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);
			foreach (var touch in touches) {
				var location = ((UITouch)touch).LocationInNode (this);

				LastTouch = location;
				System.Diagnostics.Debug.WriteLine ("LAST  TOUCH:  "+LastTouch);


				if (Timer != 0) {
					AnalyzeTouch ();
				}
			}

		}

		public async void AnalyzeTouch(){

			var touchDiff  = LastTouch.Y - FirstTouch.Y;
			var touchDiffX = LastTouch.X - FirstTouch.X;

			if (touchDiff > 66){				



				var sprite = new SKSpriteNode ("dart_blue") {
										Position = new CGPoint(
														FirstTouch.X,-5f )
									};


				//dart_blue
//				var sprite1 = new SKSpriteNode ("knife1") {
//					Position = new CGPoint(
//						FirstTouch.X,-5f )
//				};



				sprite.SetScale (1.5f);
				AddChild (sprite);

				var finaldest = new CGPoint ();
				var action = SKAction.MoveTo (finaldest, 0.3);

				if (touchDiff < 150) {
					finaldest = new CGPoint (LastTouch.X + (touchDiffX * 0.5f) ,LastTouch.Y + (touchDiff * 0.3f));
					action = SKAction.MoveTo(finaldest,0.3);//FollowPath (np, 3.0);

				} else {
					finaldest = new CGPoint (LastTouch.X + (touchDiffX * 0.5f) ,
											 LastTouch.Y + (touchDiff  * 0.6f));

					action = SKAction.MoveTo(finaldest,0.2);
				}
					
				nfloat rotatingAngle = touchDiffX / 333;

				Console.WriteLine ("touchDiff:::" + touchDiffX);
				Console.WriteLine ("toRadians:::" + rotatingAngle);

				//to radians
				var RotAction = SKAction.RotateToAngle (-rotatingAngle, 0);

				//to small size
				var scaleAction = SKAction.ScaleTo(0.8f,0.1555);

				sprite.RunAction (action);
				sprite.RunAction (RotAction);
				sprite.RunAction (scaleAction);

				SpriteCount++;

				//COLLISION
				if (PhotoNode.ContainsPoint (finaldest)) {



					CurrentScore++;
					System.Diagnostics.Debug.WriteLine ("POINTS:    " + CurrentScore);
					System.Diagnostics.Debug.WriteLine ("collideddddddd::::::::::::::::::::::");
				
					await System.Threading.Tasks.Task.Delay (200);
					await System.Threading.Tasks.Task.Delay (200);

					var displayScoreNode = new SKLabelNode ();

					displayScoreNode.Text = "" + CurrentScore;     //.ToString ();
					displayScoreNode.Position = new CGPoint (Size.Width / 2, (Size.Height / 2) - 200);  
					displayScoreNode.FontSize = 80;
					displayScoreNode.FontColor = UIColor.Red;
					displayScoreNode.FontName = "GillSans-Bold";
					displayScoreNode.SetScale (0.1f);

					var moveUpAction = SKAction.MoveTo ((new CGPoint (Size.Width / 2, Size.Height / 2)), 0.5);
					var scaleUpAction = SKAction.ScaleBy (9f, 0.5);
					AddChild (displayScoreNode);

					displayScoreNode.RunAction (moveUpAction);
					displayScoreNode.RunAction (scaleUpAction);

					await System.Threading.Tasks.Task.Delay (1000);
					displayScoreNode.RemoveFromParent ();

				} else {
					await System.Threading.Tasks.Task.Delay (300);
					SystemSound.Vibrate.PlaySystemSound ();//.PlayAlertSound();
					SystemSound.Vibrate.Close ();
				}

				//REMOVE SLOW DART
				if (touchDiff < 100) {
					await System.Threading.Tasks.Task.Delay (200);
					sprite.RemoveFromParent ();
					SpriteCount--;
				}

				if(touchDiff > 120 && ( !PhotoFrameNode.ContainsPoint (finaldest) )) {
					await System.Threading.Tasks.Task.Delay (300);
					SystemSound.Vibrate.PlaySystemSound ();//.PlayAlertSound();
					SystemSound.Vibrate.Close ();
					await System.Threading.Tasks.Task.Delay (200);
				}
			}
		}


		protected void Handle_FinishedPickingMedia (object sender, UIImagePickerMediaPickedEventArgs e)
		{
			// determine what was selected, video or image
			//bool isImage = false;

			UIImage cc = e.Info [UIImagePickerController.EditedImage] as UIImage;

			GameObj.finalImage = cc;
			imgPckr.DismissModalViewController(true);
			Scene.View.PresentScene (new MyScene (Size));
		}

		public async void loadHater(){

			if (GameObj.finalImage != null) {
				await System.Threading.Tasks.Task.Delay (2000);
				//RemoveAllChildren ();



				_spriteFrame = new SKSpriteNode(SKTexture.FromImageNamed("framecopycopy"));
				_spriteFrame.Position = new CGPoint (Scene.Size.Width / 2, 
							 Scene.Size.Height - (Scene.Size.Height / 3.77f) - 11);
				_spriteFrame.SetScale (0.6f);
				AddChild (_spriteFrame);



				PhotoNode = new SKSpriteNode (SKTexture.FromImage(GameObj.finalImage));//Frage (xy.hater));//(UIColor.DarkGray,new CGSize(200f,200f)); 
				PhotoNode.Size = new CGSize (120f, 120f);
				PhotoNode.Position = new CGPoint (Scene.Size.Width / 2, Scene.Size.Height - (Scene.Size.Height / 3.77f));
				AddChild (PhotoNode);

			}
		}

		public async void moveHater(){
		
			nfloat minY = Scene.Size.Height / 2;
			nfloat maxY = Scene.Size.Height;
			nfloat maxX = Scene.Size.Width;



			Random random = new Random ();
			int randomNumberX = random.Next (100, 		(int)maxX - 100);
			int randomNumberY = random.Next ((int)minY, (int)maxY - 100);

			//PhotoNode.Size 	   = new CGSize (125, 150);


			_spriteFrame.Position = new CGPoint (randomNumberX, randomNumberY - 11);
			PhotoNode.Position = new CGPoint (randomNumberX, randomNumberY);

		}




		public async void loadStarter(){

			var scoreLoad = new SKLabelNode (){

				Text = "" + (GameObj.ToBeatScore + 1) + " To Win",   //.ToString ();
				Position = new CGPoint (Size.Width / 2, (Size.Height / 2) - 200),
				FontSize = 66,
				FontColor = UIColor.White,
				FontName = "GillSans-Bold",

			};
			scoreLoad.SetScale (0.1f);

			var moveUpAction = SKAction.MoveTo ((new CGPoint (Size.Width / 2, Size.Height / 2)), 1.0);
			var scaleUpAction = SKAction.ScaleBy (9f, 1.0);
			AddChild (scoreLoad);

			scoreLoad.RunAction (moveUpAction);
			scoreLoad.RunAction (scaleUpAction);

			await System.Threading.Tasks.Task.Delay (2000);
			scoreLoad.RemoveFromParent();

			for (int i = 0; i < 3; i++) {

				var startGameNode = new SKLabelNode (){
					Text = "" + (3-i),
					Position = new CGPoint (Size.Width / 2, (Size.Height / 2) - 200),
					FontSize = 80,
					FontColor = UIColor.Red,
					FontName = "GillSans-Bold",

				};
				startGameNode.SetScale (0.1f);

				var moveUpAction1 = SKAction.MoveTo ((new CGPoint (Size.Width / 2, Size.Height / 2)), 1.0);
				var scaleUpAction1 = SKAction.ScaleBy (9f, 1.0);
				AddChild (startGameNode);

				startGameNode.RunAction (moveUpAction1);
				startGameNode.RunAction (scaleUpAction1);

				await System.Threading.Tasks.Task.Delay (2000);
				startGameNode.RemoveFromParent();

			}
		
		}



		public async void finishingTimeAnimation(){
			var scale1 = SKAction.ScaleBy (6.0f, 0.5);
			TimeLabel.RunAction (scale1);

			var scale2 = scale1.ReversedAction;
			TimeLabel.RunAction (scale2);
		}

		public override async void Update (double currentTime)
		{
			var theTime = (int)currentTime;

			if (Timer == 0) {

				if(startTime1 == 0){
					startTime1++;
					loadStarter();
				}
				await System.Threading.Tasks.Task.Delay (8400);
			}

			//if (Timer % 5 == 0) {  moveHater ();  }

			if (theTime  >  lastTimeUpdate) {

				Timer++;
				countDown = LevelTime - Timer;
				TimeLabel.Text = ""+countDown;

				if (countDown % 3 == 0) {
					moveHater ();
				}

				if (countDown < 6) {
					finishingTimeAnimation ();
				}

				if (countDown < 1) {
					GameObj.finalScore = CurrentScore;
					PresentScene (new FinishGameScene (Size));
				}
				lastTimeUpdate = theTime;
			}
			// Run before each frame is rendered
			base.Update (currentTime);
		}
	
	
	}
}







//var timeDiff = theTime - lastTimeUpdate;
//lastTimeUpdate = theTime;

//Console.WriteLine ("TIME::   " + theTime);
//TimeLabel.Text = ""+(LevelTime-1);
//await System.Threading.Tasks.Task.Delay (1000);






//	
//		public SKScene MainMenuScene(CGSize theSize){
//
//
//			var THESCENE = new SKScene ();
//			THESCENE.Size = theSize;
//
//
//
//			var buttonTopLeft      = new SKNode ();
//			var buttonBottomLeft   = new SKNode ();
//			var buttonTopRight     = new SKNode ();
//			var buttonBottomRight  = new SKNode ();
//
//			buttonTopLeft.Position = new CGPoint 
//			(Scene.Size.Width / 4, Scene.Size.Height - (Scene.Size.Height / 4));
//
//
//			buttonTopLeft.Scene.BackgroundColor = UIColor.DarkGray;
//
//
//
//
//
//
//			THESCENE.AddChild (buttonTopLeft);
//
//
//
//
//			return THESCENE;
//
//		}


//		void HandleDidBeginContact(object sender, EventArgs args) {
//			var contact = sender as SKPhysicsContact;
//		
//public void DidBeginContact (SKPhysicsContact contact)
//{
//
//	SKPhysicsBody body1;
//	SKPhysicsBody body2;
//
//
//	if (contact.BodyA.CategoryBitMask < contact.BodyB.CategoryBitMask) {
//
//		body1 = contact.BodyA;
//		body2 = contact.BodyB;
//	} 
//	else {
//
//		body1 = contact.BodyB;
//		body2 = contact.BodyA;
//
//	}
//
//	if ((body1.CategoryBitMask & DartCat) != 0 &&
//		(body2.CategoryBitMask & FrameCat) != 0) {
//
//		System.Diagnostics.Debug.WriteLine ("collideddddddd:  ");
//
//
//	}
//
//	//			if(contact.BodyA.IsOfCategory(DartCat) && contact.BodyB.IsOfCategory(FrameCat))
//	//			{
//	//
//	//
//	//
//	//			}
//
//}


//		public const uint DartCat  = 0x1 << 0;
//		public const uint FrameCat = 0x1 << 1;
//
//
//static const UInt32 dartCategory ;

//uint32_t projectileCategory     =  0x1 << 0;
//static const uint32_t monsterCategory        =  0x1 << 1;
//			this.PhysicsWorld.Gravity = new CGVector (0,0);//vv;
//
////				new CGVector() //CGVectorMake(0,0);
//			PhysicsWorld.ContactDelegate = this;

//PhotoFrameNode.PhysicsBody = SKPhysicsBody.CreateRectangularBody(PhotoFrameNode.Size);

//
//
//			PhotoFrameNode.PhysicsBody.CategoryBitMask = 1;   //SetCategoryBitMask (CONTACT_BITS.Wall);
//			PhotoFrameNode.PhysicsBody.CollisionBitMask = 0;
//			PhotoFrameNode.PhysicsBody.ContactTestBitMask = PhotoFrameNode.PhysicsBody.CollisionBitMask;
//



//BackgroundColor = new UIColor (0.15f, 0.15f, 0.3f, 1.0f);

//			var myLabel = new SKLabelNode ("Chalkduster") {
//				Text = "Hello, World!",
//				FontSize = 30,
//				Position = new CGPoint (Frame.Width / 2, Frame.Height / 2)
//			};
//
//			AddChild (myLabel);
//
//this.PhysicsWorld.DidBeginContact += this.HandleDidBeginContact;


//				sprite.PhysicsBody.CategoryBitMask = DartCat;   //SetCategoryBitMask (CONTACT_BITS.Wall);
//				sprite.PhysicsBody.ContactTestBitMask = FrameCat;
//				sprite.PhysicsBody.CollisionBitMask = sprite.PhysicsBody.CollisionBitMask;//1;
//

//				sprite.PhysicsBody.CategoryBitMask = 0;   //SetCategoryBitMask (CONTACT_BITS.Wall);
//				sprite.PhysicsBody.CollisionBitMask = 1;
//				sprite.PhysicsBody.ContactTestBitMask = sprite.PhysicsBody.CollisionBitMask;
//
//
//

//				sprite.PhysicsBody.UsesPreciseCollisionDetection = true;
//				sprite.PhysicsBody.Dynamic = true;
//


//var comb = SKAction.Sequence ([smallScaleAction]);
//var smallScaleAction = SKAction.ScaleBy (0.2f, 0.1);

//var smallScaleAction = SKAction.ScaleBy (0.2f, 0.1);
//var comb = SKAction.Sequence ([smallScaleAction]);



//AddChild (PhotoNode);


//imgPckr.NavigationController.DismissModalViewController(true);


//			switch(e.Info[UIImagePickerController.MediaType].ToString()) {
//			case "public.image":
//				Console.WriteLine("Image selected");
//				isImage = true;
//				break;
//			case "public.video":
//				Console.WriteLine("Video selected");
//				break;
//			}
//
//			// get common info (shared between images and video)
//			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
//			if (referenceURL != null)
//				Console.WriteLine("Url:"+referenceURL.ToString ());
//
//			// if it was an image, get the other image info
//			if(isImage) {
//				// get the original image
//				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
//				if(originalImage != null) {
//					// do something with the image
//					Console.WriteLine ("got the original image");
//					hater1 = originalImage; // display
//
//				}
//			} else { // if it's a video
//				// get video url
//				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
//				if(mediaURL != null) {
//					Console.WriteLine(mediaURL.ToString());
//				}
//			}
//			// dismiss the picker
//			imgPckr.NavigationController.DismissModalViewController(true);
//hater1 = cc; // display

//hater1 = cc;
//xy.hater = cc;

//			var abc= new SKSpriteNode(SKTexture.FromImage(cc));//(UIColor.DarkGray,new CGSize(200f,200f)); 
//			abc.Size = new CGSize(200f,200f);
//			abc.Position = new CGPoint (Scene.Size.Width/2 ,Scene.Size.Height - (Scene.Size.Height / 3.77f) );
//			AddChild (abc);
//

//Scene.View.PresentScene (new MyScene(Scene.Size));

//var PhotoNode = new SKSpriteNode (SKTexture.FromImage (xy.hater));//(UIColor.DarkGray,new CGSize(200f,200f)); 
//PhotoNode.Size = new CGSize (200f, 200f);
//PhotoNode.Position = new CGPoint (Scene.Size.Width / 2, Scene.Size.Height - (Scene.Size.Height / 3.77f));

//loadHater ();




//			if (touchDiff < 0) {
//			
////				if (xy.hater != null) {
////
////					var PhotoNode = new SKSpriteNode (SKTexture.FromImage (xy.hater));//(UIColor.DarkGray,new CGSize(200f,200f)); 
////					PhotoNode.Size = new CGSize (200f, 200f);
////					PhotoNode.Position = new CGPoint (Scene.Size.Width / 2, Scene.Size.Height - (Scene.Size.Height / 3.77f));
////					AddChild (PhotoNode);
////
////				}
//			
//			
//			}

//			if (SpriteCount == 10) {
//			
//				GameObj.finalScore = CurrentScore;
//				//gOBJ.finalScore = CurrentScore;
//				PresentScene (new FinishGameScene (Size));
//			
//			}




//LastTouch.X,LastTouch.Y + (touchDiff * 0.6f));
//SystemSound.Vibrate.PlaySystemSound ();//.PlayAlertSound();
//SystemSound.Vibrate.Close();
//var smallScaleAction = SKAction.ScaleBy (0.2f, 0.1);
//displayScoreNode.RunAction (smallScaleAction);
//var comb = SKAction.Sequence ([smallScaleAction]);









//		public async void waitaSec(){
//
//			 System.Threading.Tasks.Task.Delay (6000);
//
//
//		}



//var MM = new SKScene ();
//					MM = MainMenuScene (Size);
//PresentScene (new LevelsScene (Size));

//Scene.View.PresentScene (mm1);
//
//					Scene.RemoveAllChildren();
//					Scene.View.PresentScene (new MyScene(Scene.Size));

//imgPckr.Canceled += Handle_Canceled;


//var img = new ImageObj(Size);
//hater1 = img.hater;
//					var PhotoNode = new SKSpriteNode(SKTexture.FromImage(hater1));//(UIColor.DarkGray,new CGSize(200f,200f)); 
//					PhotoNode.Size = new CGSize(200f,200f);
//					PhotoNode.Position = new CGPoint (Scene.Size.Width/2 ,Scene.Size.Height - (Scene.Size.Height / 3.77f) );
//					AddChild (PhotoNode);


//SKSpriteNode *HPhoto = [SKSpriteNode 
//spriteNodeWithTexture: 
//[SKTexture textureWithImageNamed:@"framecopycopy"] size:CGSizeMake(145.00,168.00)];

//xx.Size = new CGSize (100, 50);
//aButton.Size = new CGSize (100, 50);
//bButton.Color = UIColor.Yellow;
//aButton.Color = UIColor.Yellow;
//xx.Size = new CGSize (100, 50);
//aButton.Size = new CGSize (100, 50);

//			UIImage ff = new UIImage ("Spaceship");
//			var spss = new SKSpriteNode(SKTexture.FromImage(ff));



