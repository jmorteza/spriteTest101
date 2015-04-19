using System;
using CoreGraphics;
using CoreGraphics;
using SpriteKit;
using UIKit;

namespace spriteTest101 {

	public class BasicScene : SKScene {

		protected UIColor SelectedColor { get; private set; }
		protected UIColor UnselectedColor { get; private set; }
		protected UIColor ButtonColor { get; private set; }
		protected UIColor InfoColor { get; private set; }

		protected float FrameMidX { get; private set; }
		protected float FrameMidY { get; private set; }

		protected SKSpriteNode bgr{ get; private set; }

		SKTransition transition;

		public BasicScene (CGSize size) : base (size)
		{
			ScaleMode = SKSceneScaleMode.AspectFill;

			//BackgroundColor = UIColor.FromRGBA (0.15f, 0.15f, 0.3f, 1f);


			bgr = new SKSpriteNode(SKTexture.FromImageNamed("brickwall1"));
			bgr.Size = Size;
			bgr.Position = new CGPoint (Size.Width / 2, Size.Height / 2);

			AddChild (bgr);


			UnselectedColor = UIColor.FromRGBA (0f, 0.5f, 0.5f, 1f);
			SelectedColor = UIColor.FromRGBA (0.5f, 1f, 0.99f, 1f);

			ButtonColor = UIColor.FromRGBA (1f, 1f, 0f, 1f);
			InfoColor = UIColor.FromRGBA (1f, 1f, 1f, 1f);

			FrameMidX = (float)Frame.GetMidX ();
			FrameMidY = (float)Frame.GetMidY ();

			transition = SKTransition.MoveInWithDirection (SKTransitionDirection.Right, 0.3);
		}

		public void PresentScene (BasicScene scene)
		{
			View.PresentScene (scene, transition);
		}
	}
}
