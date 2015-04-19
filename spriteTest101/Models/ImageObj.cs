using System;
using CoreGraphics;
using CoreImage;
using Foundation;
using SpriteKit;
using UIKit;


namespace spriteTest101
{
	public class ImageObj//:UIImagePickerControllerDelegate
	{
		public UIImage hater;
		//UIImagePickerController picker= new UIImagePickerController();


		public ImageObj ()
		{

			//hater = xx;
			//picker 

			//Scene.View.Window.RootViewController.PresentViewController(picker,true,null);



		}



//		protected void Handle_FinishedPickingMedia (object sender, UIImagePickerMediaPickedEventArgs e)
//		{
//			// determine what was selected, video or image
//			bool isImage = false;
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
//					hater = originalImage; // display
//
//					//PhotoFrameNode.RemoveFromParent ();
//					//new SKSpriteNode ("Spaceship")
////					var PhotoNode = new SKSpriteNode(SKTexture.FromImage(hater));//(UIColor.DarkGray,new CGSize(200f,200f)); 
////					PhotoNode.Size = new CGSize(200f,200f);
////					PhotoNode.Position = new CGPoint (Scene.Size.Width/2 ,Scene.Size.Height - (Scene.Size.Height / 3.77f) );
////					AddChild (PhotoNode);
//
//				}
//			} else { // if it's a video
//				// get video url
//				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
//				if(mediaURL != null) {
//					Console.WriteLine(mediaURL.ToString());
//				}
//			}
//
//			picker.View.Window.RootViewController.DismissModalViewController (true);
//			// dismiss the picker
//			//picker.View.Window.RootViewController
//			//picker.View.Window.RootViewController.DismissModalViewController (true);
//			//picke.DismissModalViewControllerAnimated (true);
//		}
//
	}
}

