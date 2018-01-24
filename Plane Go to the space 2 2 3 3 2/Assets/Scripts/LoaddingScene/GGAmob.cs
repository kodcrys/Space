using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class GGAmob : MonoBehaviour {

	public static GGAmob Intance { get; private set; }

	private BannerView bannerView;
	private InterstitialAd interstitial;

	private InterstitialAd interstitial1;
	private NativeExpressAdView nativeExpressAdView;
	private RewardBasedVideoAd rewardBasedVideo;
	private float deltaTime = 0.0f;
	private static string outputMessage = string.Empty;

	public static string OutputMessage
	{
		set { outputMessage = value; }
	}

	void Awake(){
		Intance = this;
	}

	public void Start()
	{
		isAllowReward = false;
		// Get singleton reward based video ad reference.
		rewardBasedVideo = RewardBasedVideoAd.Instance;

		// RewardBasedVideoAd is a singleton, so handlers should only be registered once.
		this.rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
		this.rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
		this.rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
		this.rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
		this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
		this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
		this.rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;
	}

	public void Update()
	{
		// Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
		// value.
		this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
	}

	// Returns an ad request with custom ad targeting.
	private AdRequest CreateAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
			.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
			.AddKeyword("game")
			.SetGender(Gender.Male)
			.SetBirthday(new DateTime(1985, 1, 1))
			.TagForChildDirectedTreatment(false)
			.AddExtra("color_bg", "9B30FF")
			.Build();
	}

	public void RequestBanner()
	{
		// These ad units are configured to always serve test ads.
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/6300978111";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-3940256099942544/2934735716";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create a 320x50 banner at the top of the screen.
		this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

		// Register for ad events.
		/*this.bannerView.OnAdLoaded += this.HandleAdLoaded;
		this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
		this.bannerView.OnAdOpening += this.HandleAdOpened;
		this.bannerView.OnAdClosed += this.HandleAdClosed;
		this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;*/

		// Load a banner ad.
		this.bannerView.LoadAd(this.CreateAdRequest());
	}

	public void RequestInterstitial()
	{
		// These ad units are configured to always serve test ads.
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/1033173712";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-3940256099942544/4411468910";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create an interstitial.
		this.interstitial = new InterstitialAd(adUnitId);

		// Register for ad events.
//		this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
		this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
		this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
		this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
//		this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

		// Load an interstitial ad.
		this.interstitial.LoadAd(this.CreateAdRequest());
	}


	public void RequestNativeExpressAdView()
	{
		// These ad units are configured to always serve test ads.
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/1072772517";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-3940256099942544/2562852117";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create a 320x150 native express ad at the top of the screen.
		this.nativeExpressAdView = new NativeExpressAdView(
		adUnitId,
		new AdSize(320, 150),
		AdPosition.Top);

		// Register for ad events.
		/*this.nativeExpressAdView.OnAdLoaded += this.HandleNativeExpressAdLoaded;
		this.nativeExpressAdView.OnAdFailedToLoad += this.HandleNativeExpresseAdFailedToLoad;
		this.nativeExpressAdView.OnAdOpening += this.HandleNativeExpressAdOpened;
		this.nativeExpressAdView.OnAdClosed += this.HandleNativeExpressAdClosed;
		this.nativeExpressAdView.OnAdLeavingApplication += this.HandleNativeExpressAdLeftApplication;*/

		// Load a native express ad.
		this.nativeExpressAdView.LoadAd(this.CreateAdRequest());
	}

	public void RequestRewardBasedVideo()
	{
		#if UNITY_EDITOR
		string adUnitId = "unused";
		#elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-3940256099942544/5224354917";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-3940256099942544/1712485313";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		rewardBasedVideo = RewardBasedVideoAd.Instance;

		//rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;
		//rewardBasedVideo.OnAdLoaded += this.HandleRewardBasedVideoLoaded;
		rewardBasedVideo.OnAdFailedToLoad += this.HandleRewardBasedVideoFailedToLoad;
		rewardBasedVideo.OnAdStarted += this.HandleRewardBasedVideoStarted;
		rewardBasedVideo.OnAdOpening += this.HandleRewardBasedVideoOpened;
		rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
		rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;
		//rewardBasedVideo.OnAdLeavingApplication += this.HandleRewardBasedVideoLeftApplication;

		AdRequest request = new AdRequest.Builder().Build();
		rewardBasedVideo.LoadAd(request, adUnitId);
	}

	public void ShowBanner(){
		this.bannerView.Show ();
	}

	public void HideBanner(){
		this.bannerView.Hide ();
	}

	public void ShowInterstitial()
	{
		if (this.interstitial.IsLoaded())
			this.interstitial.Show();
		else
			MonoBehaviour.print("Interstitial is not ready yet");
	}

	public void ShowRewardBasedVideo()
	{
		if (this.rewardBasedVideo.IsLoaded())
			this.rewardBasedVideo.Show();
		else
			MonoBehaviour.print("Reward based video ad is not ready yet");
	}


	#region Banner callback handlers

	private void HandleAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	private void HandleAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	private void HandleAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
	}

	private void HandleAdLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeftApplication event received");
	}

	#endregion

	#region Interstitial callback handlers

	private void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLoaded event received");
	}

	private void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		Time.timeScale = 1;
		MonoBehaviour.print(
		"HandleInterstitialFailedToLoad event received with message: " + args.Message);
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			RequestInterstitial ();
		}
	}

	private void HandleInterstitialOpened(object sender, EventArgs args)
	{
		Time.timeScale = 0;
		MonoBehaviour.print("HandleInterstitialOpened event received");
	}

	private void HandleInterstitialClosed(object sender, EventArgs args)
	{
		Time.timeScale = 1;
		StaticOption.isAdsSaw = true;
		MonoBehaviour.print("HandleInterstitialClosed event received");
	}

	private void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLeftApplication event received");
	}

	#endregion

	#region Native express ad callback handlers

	private void HandleNativeExpressAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleNativeExpressAdAdLoaded event received");
	}

	private void HandleNativeExpresseAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print(
		"HandleNativeExpressAdFailedToReceiveAd event received with message: " + args.Message);
	}

	private void HandleNativeExpressAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleNativeExpressAdAdOpened event received");
	}

	private void HandleNativeExpressAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleNativeExpressAdAdClosed event received");
	}

	private void HandleNativeExpressAdLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleNativeExpressAdAdLeftApplication event received");
	}

	#endregion

	#region RewardBasedVideo callback handlers

	private void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
	}

	private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		Time.timeScale = 1f;
		MonoBehaviour.print(
		"HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			RequestRewardBasedVideo ();
		}
	}

	private void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		Time.timeScale = 0f;
		StaticOption.isVideoReward = true;
		if(SoundManager.Intance.BGSound.isPlaying)
			SoundManager.Intance.MuteSoundBG ();
		if (SoundManager.Intance.GamePlaySound.isPlaying)
			SoundManager.Intance.StopSoundGamePlay ();
		if (SoundManager.Intance.SpaceshipSound.isPlaying)
			SoundManager.Intance.StopSpaceship ();
		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
	}

	private void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
	}
	
	public bool isRunx2Money;
	private void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{

		MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
		if (isAllowReward) {
			rewardedCoin = true;
			isAllowReward = false;
		}

		if (isRunx2Money == false) {
			//SoundManager.Intance.PlaySoundBG ();
			SoundManager.Intance.PlaySoundGamePlay ();
			SoundManager.Intance.PlaySpaceship ();
		} else {
			SoundManager.Intance.MuteSoundBG ();
			SoundManager.Intance.StopSoundGamePlay ();
			SoundManager.Intance.StopSpaceship ();
		}

		//isRunx2Money = false;

		Time.timeScale = 1f;
		StaticOption.isVideoReward = false;

		RequestRewardBasedVideo ();
	}

	private void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		string type = args.Type;
		double amount = args.Amount;
		isAllowReward = true;
		MonoBehaviour.print(
		"HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " + type);
	}
	
	bool isAllowReward;
	public bool rewardedCoin;

	private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
	}

		#endregion
}
