using System;
using iAd;
using SpriteKit;
using UIKit;
using CoreGraphics;
using CoreImage;
using Foundation;
using SpriteKit;
using UIKit;
using GoogleAdMobAds;
using GameKit;

namespace spriteTest101
{
	public class iAdDelegate : ADBannerViewDelegate {}




	public partial class ViewController : UIViewController 
	{

		public ADBannerView iAdBanner;
		public GADBannerView adView;
		const string AdmobID = "ca-app-pub-7503039537462304/9917348139";
		bool viewOnScreen = false;

//
//		GameCenterManager gameCenterManager;
//		GKLeaderboard currentLeaderBoard;
//

		public ViewController (IntPtr handle) : base (handle)  {}
		//public ViewController ()  {}


		//public class ViewController : UIViewController {}


//		public override void LoadView ()
//		{
//			View = new SKView ();
//
//			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0)) {
//				this.EdgesForExtendedLayout = UIRectEdge.None;
//			}
//		}



		public override bool PrefersStatusBarHidden ()
		{
			return true;
		}

		public override void ViewDidLoad ()
		{


			base.ViewDidLoad ();












//			NavigationController.SetNavigationBarHidden (true, true);


//			UIApplication.SharedApplication.SetStatusBarHidden (true, true);

//			var vv = new SKView ();
//			var nv = new UIView(
//			vv.Add (View);

			createBannerAds ();

			// Configure the view.
			//var skView = new SKView ();

			var skView = (SKView)View;
			skView.ShowsFPS = true;
			skView.ShowsNodeCount = true;

//			// Create and configure the scene.
			var scene = new MainMenuScene(skView.Bounds.Size);//skView.Bounds.Size); //MyScene (skView.Bounds.Size); //
			scene.ScaleMode = SKSceneScaleMode.AspectFill;

//			// Present the scene.
			skView.PresentScene (scene);
//





//			View = new SKView ();

			//(View as SKView).PresentScene (new MainMenuScene (View.Bounds.Size));
		}


		public void createBannerAds () {

			CGRect xxy = new CGRect(0,View.Frame.Height-50,View.Frame.Width,100f);
			iAdBanner = new ADBannerView
				(xxy);

			iAdBanner.Hidden = false;
			iAdBanner.AdLoaded += HandleAdLoaded;
			iAdBanner.FailedToReceiveAd += HandleFailedToReceiveAd;
			View.AddSubview (iAdBanner);

			adView = new GADBannerView(
				new CGRect(0, View.Frame.Height-GADAdSizeCons.Banner.Size.Height,
					View.Frame.Width,GADAdSizeCons.Banner.Size.Height)) {		
				AdUnitID = AdmobID,
				RootViewController = this,

			};

			adView.AdReceived += gadAdHandle;

			GADRequest request = GADRequest.Request  ; //= GADRequest ;
			request.TestDevices = new string[1]  {  "2cf5064e1aa0d8a637761a3665b96475"  };
		
			adView.LoadRequest (GADRequest.Request);
			View.AddSubview (adView);
		}

		void gadAdHandle (object sender, EventArgs e)
		{
			Console.WriteLine ("RECEIVED GOOGLE AD");
			//adView = (GADBannerView)sender;
			//this.View.AddSubview ((GADBannerView)sender);
			View.AddSubview(adView);

		}

		void HandleAdLoaded (object sender, EventArgs e)
		{
			if (iAdBanner == null)
				return;
			iAdBanner.Hidden = false;
			adView.Hidden = true;
		}

		void HandleFailedToReceiveAd (object sender, AdErrorEventArgs e)
		{
			if (iAdBanner == null)
				return;
			iAdBanner.Hidden = true;
			adView.Hidden = false;
		}





		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

//
//
//		public override void ViewWillAppear (bool animated)
//		{
//		//	(View as SKView).PresentScene (new MainMenuScene (View.Bounds.Size));
//		}

		public override bool ShouldAutorotate ()
		{
			return true;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.Portrait;
		}






	}
}



///////////////////////////////////////////
/// 
/// 
/// //			_ad.Hidden = true;
//			Resize ();
/// 
/// 			//Resize ();




//			adView.DidReceiveAd += (sender, args) => {
//				if (!viewOnScreen) View.AddSubview (adView);
//				viewOnScreen = true;
//			};


//(size: new CGSize(View.Frame.Width,GADAdSizeCons.Banner.Size.Height), origin: new CGPoint (0, View.Frame.Height-50)) {
//		GADRequest request2 = (GADRequest) request;
//request = new GADRequest();


//iAdBanner.Delegate = this;



//CGSize bannerSize = ADBannerView.SizeIdentifierPortrait;
