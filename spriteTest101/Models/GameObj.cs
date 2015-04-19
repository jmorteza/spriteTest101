using System;
using System.Collections.Generic;
using CoreGraphics;
using CoreImage;
using Foundation;
using SpriteKit;
using UIKit;

namespace spriteTest101
{




	public static class GameObj
	{


		public static int finalScore;
		public static int levelSelected;
		public static UIImage objSelected;
		public static SKSpriteNode CurrentGame;
		//public static SKSpriteNode[] nodeArr;
		public static int[] CurrentLevelArr;
		public static int LineNum;
		public static int ToBeatScore;


		public static UIImage finalImage;
		public static UIImage mImage1;
		public static UIImage mImage2;
		public static UIImage mImage3;
		public static int selectedHater;



		static GameObj ()
		{

			finalScore = 0;


		}



	
	}
}

