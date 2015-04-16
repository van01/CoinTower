using UnityEngine;
using System.Collections;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GPGSManager : MonoBehaviour {
	
	private static GPGSManager sInstance;

	private int nTotalPlayCount;
	private int nTotalCoin;
	private int nCoinCount;

	/*public static GPGSManager GetInstance
	{
		get
		{
			if(sInstance == null) sInstance = this;
			
			return sInstance;
		}
	}*/
	
	void Start()
	{
		init();
	}
	
	void init()
	{
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate((bool success) =>
		                              {
			if (success) Debug.Log("Sign in Success");
			else Debug.Log("Sign in Fail");
		});
	}
	
	public void PostScore(int nScore)
	{
		Social.ReportScore(nScore, IDs.LB, (bool success) => {
			if (success) Debug.Log("Post Success");
			else Debug.Log("Post Fail");
		});
	}
	
	public void ProgessiveAchievement(AchievementType type)
	{
		nCoinCount = PlayerPrefs.GetInt("Coin Count");

		if (type == AchievementType.PlayCount)
		{
			((PlayGamesPlatform) Social.Active).IncrementAchievement(
				IDs.TP1,1,(bool success) => {
				//handle success or failure
			});
			((PlayGamesPlatform) Social.Active).IncrementAchievement(
				IDs.TP2,1,(bool success) => {
				//handle success or failure
			});
			((PlayGamesPlatform) Social.Active).IncrementAchievement(
				IDs.TP3,1,(bool success) => {
				//handle success or failure
			});
			((PlayGamesPlatform) Social.Active).IncrementAchievement(
				IDs.TP4,1,(bool success) => {
				//handle success or failure
			});
			((PlayGamesPlatform) Social.Active).IncrementAchievement(
				IDs.TP5,1,(bool success) => {
				//handle success or failure
			});
		}
		
		if(type == AchievementType.TotalCoin)
		{
			((PlayGamesPlatform) Social.Active).IncrementAchievement(
				IDs.TC1,nCoinCount,(bool success) => {
				//handle success or failure
			});
			((PlayGamesPlatform) Social.Active).IncrementAchievement(
				IDs.TC2,nCoinCount,(bool success) => {
				//handle success or failure
			});
			((PlayGamesPlatform) Social.Active).IncrementAchievement(
				IDs.TC3,nCoinCount,(bool success) => {
				//handle success or failure
			});
			((PlayGamesPlatform) Social.Active).IncrementAchievement(
				IDs.TC4,nCoinCount,(bool success) => {
				//handle success or failure
			});
			((PlayGamesPlatform) Social.Active).IncrementAchievement(
				IDs.TC5,nCoinCount,(bool success) => {
				//handle success or failure
			});
		}

		if(type == AchievementType.BestScore)
		{
			if (nCoinCount >= 10){
				Social.ReportProgress(IDs.BS1, 100.0f, (bool success) => {
					//handle success or failure
				});

				if (nCoinCount >= 30){
					Social.ReportProgress(IDs.BS2, 100.0f, (bool success) => {
						//handle success or failure
					});

					if(nCoinCount >= 50){
						Social.ReportProgress(IDs.BS3, 100.0f, (bool success) => {
							//handle success or failure
						});

						if(nCoinCount >= 80){
							Social.ReportProgress(IDs.BS4, 100.0f, (bool success) => {
								//handle success or failure
							});

							if(nCoinCount >= 100){
								Social.ReportProgress(IDs.BS5, 100.0f, (bool success) => {
									//handle success or failure
								});
							}
						}
					}
				}
			}
		}
	}
	
	public void postAchievement()
	{
		nTotalPlayCount = PlayerPrefs.GetInt("Total Play Count");
		nTotalCoin = PlayerPrefs.GetInt("Total Build Coin");

		if (nTotalPlayCount >= 100){
			Social.ReportProgress(IDs.TP4, 0.0f, (bool success) => {
				//handle success or failure
			});
			if (nTotalPlayCount >= 500){
				Social.ReportProgress(IDs.TP5, 0.0f, (bool success) => {
					//handle success or failure
				});
			}
		}
		
	}

	public void ShowLeaderboard(){
		Social.ShowLeaderboardUI();
	}
	
	public void ShowAchievements(){
		Social.ShowAchievementsUI();
	}
}

// 업적 , 리더보드 ID Class
public class IDs
{
	public const string LB = "CgkIvY2w3dwREAIQBg"; // 리더보드 ID

	public const string TP1 = "CgkIvY2w3dwREAIQBw"; // Times play (2)
	public const string TP2 = "CgkIvY2w3dwREAIQCA";	// 10
	public const string TP3 = "CgkIvY2w3dwREAIQCQ";	// 100
	public const string TP4 = "CgkIvY2w3dwREAIQCg";	// 500
	public const string TP5 = "CgkIvY2w3dwREAIQCw"; // 1000

	public const string TC1 = "CgkIvY2w3dwREAIQDA";	// Total Coins (20)
	public const string TC2 = "CgkIvY2w3dwREAIQDQ";	// 100
	public const string TC3 = "CgkIvY2w3dwREAIQDg";	// 1000
	public const string TC4 = "CgkIvY2w3dwREAIQDw";	// 5000
	public const string TC5 = "CgkIvY2w3dwREAIQEA";	// 10000

	public const string BS1 = "CgkIvY2w3dwREAIQEQ";	// Best Score (10)
	public const string BS2 = "CgkIvY2w3dwREAIQEg";	// 30
	public const string BS3 = "CgkIvY2w3dwREAIQEw";	// 50
	public const string BS4 = "CgkIvY2w3dwREAIQFA";	// 80
	public const string BS5 = "CgkIvY2w3dwREAIQFQ";	// 100
}

public enum AchievementType
{
	PlayCount,
	TotalCoin,
	BestScore
}

public enum AchievementState
{
	LOCK,
	UNLOCK
}