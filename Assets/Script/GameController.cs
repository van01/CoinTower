using UnityEngine;
using System.Collections;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;


public class GameController : MonoBehaviour {
	
	public GameObject LobbyPopup;
	public GameObject EndPopup;

	public GameObject playing;
	public GameObject ready;

	public GameObject HitZone;
	public int coinTypeGenerater;
	public GameObject baseCoin;

	public int coinCounter;
	public int randWindValue;

	public bool playingHudActive = false;

	public enum GameState{
		LOBBY,
		READY,
		PLAYING,
		GAMEEND,
		RETRY,
	}
	public GameState state;

	public GameObject coinViewer;
	private bool bCoinViewerSend = false;

	private BoxCollider bCHitZone;

	private bool bEndScoreSend = false;
	private int bestScore;

	private bool bPlayingHudRedraw = false;

	private GameObject[] tmpParentCoin = new GameObject[]{};

	private CameraController tmpCamera;
	private CoinConstructor tmpCoinConstructor;
	private BGController tmpBGController;
	private SoundManager tmpSoundManager;
	private HUDController tmpHudController;

	private bool bLobbyMenuOn = false;
	private bool bReadyOn = false;
	private bool bPlayingOn = false;
	private bool bEndGameOn = false;

	// Use this for initialization
	void Start () {
		state = GameState.LOBBY;

		tmpCamera = GameObject.Find("Main Camera").GetComponent<CameraController>();
		tmpHudController = GameObject.Find("HUD").GetComponent<HUDController>();

		tmpCoinConstructor = HitZone.GetComponent<CoinConstructor>();
		bCHitZone = HitZone.GetComponent<BoxCollider>();

		tmpBGController = this.GetComponent<BGController>();
		tmpSoundManager = GetComponent<SoundManager>();

		//구글 플레이 초기화
		PlayGamesPlatform.Activate();
		PlayGamesPlatform.DebugLogEnabled = true;
		//구글 플레이 로그인
		Social.localUser.Authenticate((bool success)=>{
		});

	}
	
	// Update is called once per frame
	void Update () {
		switch(state){
		case GameState.LOBBY:
			LobbyMenu();
			break;

		case GameState.READY:
			ReadyGame();
			break;

		case GameState.PLAYING:
			PlayingGame();
		break;

		case GameState.GAMEEND:
			EndGame();
		break;

		case GameState.RETRY:
			Retry();
		break;
		}
	}

	void OnTriggerEnter(Collider c)	{
		if (c.transform.tag == "Coin"){
			state = GameState.GAMEEND;
		}
	}

	void LobbyMenu(){
		if (bLobbyMenuOn == false){
			bCHitZone.enabled = false;
			LobbyPopup.SetActive(true);
			if (tmpSoundManager.bgmNo == 0){
				tmpSoundManager.BGMChange(1);
				tmpSoundManager.bgmNo = 1;
			}
			bLobbyMenuOn = true;
			Debug.Log(state);
		}
	}

	void ReadyGame(){
		if (bReadyOn == false){
			state = GameState.READY;

			bCHitZone.enabled = true;
			LobbyPopup.SetActive(false);

			ready.SetActive(true);
			baseCoin.SetActive(true);
			playing.SetActive(true);
			playingHudActive = true;
			bCHitZone.enabled = true;

			if (tmpSoundManager.bgmNo == 1){
				tmpSoundManager.BGMChange(2);
				tmpSoundManager.bgmNo = 2;
			}
			PlayingHudRedraw();

			bLobbyMenuOn = false;
			bReadyOn = true;

			Debug.Log(state);
		}

		if (HitZone.GetComponent<CoinConstructor>().coinCount >= 2){
			state = GameState.PLAYING;
		}

	}

	void PlayingGame(){
		if (bPlayingOn == false){
			ready.SetActive(false);

			bReadyOn = false;
			bPlayingOn = true;

			Debug.Log(state);
		}

		PlayingHudRedraw();
	}

	void EndGame(){
		if (bEndGameOn == false){
			if (bEndScoreSend == false){
				tmpHudController.gameObject.SendMessage("LastScore", coinCounter);
				bestScore = tmpHudController.GetComponent<HUDController>().nHighScore;
				tmpHudController.gameObject.SendMessage("BestScoreSend", bestScore);

				tmpCoinConstructor.gameObject.SendMessage("DeleteCoinViewer");

				bEndScoreSend = true;
			}

			bCoinViewerSend = false;
			bPlayingHudRedraw = false;

			EndPopup.SetActive(true);
			playing.SetActive(false);

			bCHitZone.enabled = false;

			if (tmpSoundManager.bgmNo == 2){
				tmpSoundManager.BGMChange(3);
				tmpSoundManager.bgmNo = 1;
			}
			ready.SetActive(false);

			bPlayingOn = false;
			bEndGameOn = true;

			Debug.Log(state);
		}
	}

	public void Retry(){
		bEndScoreSend = false;

		state = GameState.RETRY;

		tmpCamera.gameObject.SendMessage("BaseCameraTransform");
		tmpCamera.gameObject.SendMessage("ResetCoinCounterCamera");
		tmpCoinConstructor.gameObject.SendMessage("ResetCoinCounter");
		tmpCoinConstructor.gameObject.SendMessage("ResetRandWind");
		tmpBGController.gameObject.SendMessage("ResetBGScroll");
		tmpCoinConstructor.gameObject.SendMessage("ResetPrevRandCoinNo");

		EndPopup.SetActive(false);
		playing.SetActive(true);
		bCHitZone.enabled = true;

		tmpParentCoin = GameObject.FindGameObjectsWithTag("Coin");
		for(int i=0;i<tmpParentCoin.Length;i++)
		{
			Destroy(tmpParentCoin[i]);
		}

		state = GameState.PLAYING;

		if (tmpSoundManager.bgmNo == 1){
			tmpSoundManager.BGMChange(2);
			tmpSoundManager.bgmNo = 2;
		}

		bEndGameOn = false;
	}

	public void Home(){
		bEndScoreSend = false;

		baseCoin.SetActive(false);

		tmpCamera.gameObject.SendMessage("BaseCameraTransform");
		tmpCamera.gameObject.SendMessage("ResetCoinCounterCamera");
		tmpCoinConstructor.gameObject.SendMessage("ResetCoinCounter");
		tmpCoinConstructor.gameObject.SendMessage("ResetRandWind");
		tmpBGController.gameObject.SendMessage("ResetBGScroll");
		tmpCoinConstructor.gameObject.SendMessage("ResetPrevRandCoinNo");
		
		EndPopup.SetActive(false);
		
		tmpParentCoin = GameObject.FindGameObjectsWithTag("Coin");
		for(int i=0;i<tmpParentCoin.Length;i++)
		{
			Destroy(tmpParentCoin[i]);
		}
		
		state = GameState.LOBBY;
		
		if (tmpSoundManager.bgmNo == 1){
			tmpSoundManager.BGMChange(1);
			tmpSoundManager.bgmNo = 1;
		}
		bEndGameOn = false;
	}

	public void CoinCounterMounterCall(){
		tmpCoinConstructor.gameObject.SendMessage("CoinCountMounter");
	}

	void PlayingHudRedraw(){
		coinCounter = HitZone.GetComponent<CoinConstructor>().coinCount;
		randWindValue = HitZone.GetComponent<CoinConstructor>().randWind;

		if (bPlayingHudRedraw == false){
			HitZone.GetComponent<CoinConstructor>().randCoinNo = 1;
			bPlayingHudRedraw = true;
		}
		if (bCoinViewerSend == false){
			coinViewer.gameObject.SendMessage("CoinViewerChange", 1);
			bCoinViewerSend = true;
		}
	}

	public void StateCoercion(string SC){
		if (SC == "LOBBY"){
			state = GameState.LOBBY;
		}
		if (SC == "PLAYING"){	
			state = GameState.PLAYING;
		}
		if (SC == "READY"){
			state = GameState.READY;
		}
		if (SC == "RETRY"){
			state = GameState.RETRY;
		}
		if (SC == "GAMEEND"){
			state = GameState.GAMEEND;
		}
	}

	public void ShowLeaderboard(){
		Social.ShowLeaderboardUI();
	}

	public void ShowAchievements(){
		Social.ShowAchievementsUI();
	}
}
