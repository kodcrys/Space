public class SaveState {

	// All money
	public long money = 1000000;

	// Coin
	public long coin = 0;

	// Fade of MenuScene
	public bool fadeInOut = false;

	// Coin Click
	public int levelCoinClick = 1;
	public long coinUpdateClick = 100;
	public long coinClick = 1;

	// Storage
	public int levelStorage = 1;
	public long coinUpdateStorage = 100;
	public long storage = 100;

	// Bonus Time
	public int levelBonusTime = 1;
	public long coinUpdateBonusTime = 100;
	public bool isBonusTime = false;
	public float bonusTime = 5f;

	// Time Coin
	public int levelTimeCoin = 1;
	public long coinUpdateTimeCoin = 500;
	public long timeCoin = 0;

	// Sound On/Off
	public bool statusSound = true;

	// Ads X2 Colddown
	public float timeColdDown = 300f;
	public float timeCountdown = 0f;
	public bool isSawAds = false;

	// Check Internet
	public bool haveInternet = false;	

	// Check Cutscene Skip
	public bool cutScene1Saw = false;
	public bool cutScene2Saw = false;

	// Check InFirstGame;
	public bool isFirstGameUpgradeScene = true;
	public bool isFirstGameBitCoin = true;

	// 
	public bool isPurchaseRemoveAds;

	//
	public int startingScreen = 0;

	// isX2
	public bool isX2pack1 = true, isX2pack5 = true, isX2pack10 = true, isX2pack15 = true, isX2pack30 = true, isX2pack50 = true, isX2pack100 = true;

	// check spaceship type evol
	public int indexEvolWater = 0, indexEvolFire = 0, indexEvolWood = 0, indexEvolEarth = 0, indexEvolMetal = 0;
}
