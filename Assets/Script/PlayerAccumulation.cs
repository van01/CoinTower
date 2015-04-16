using UnityEngine;
using System.Collections;

public class PlayerAccumulation : MonoBehaviour {

	private int totalPlayCount;
	private int totalBuildCoin;

	// Use this for initialization
	void Start () {
		totalPlayCount = PlayerPrefs.GetInt("Total Play Count");
		totalBuildCoin = PlayerPrefs.GetInt("Total Build Coin");
	}
	
	// Update is called once per frame
	public void PlayerUpdate(int nCoinCount){
		totalPlayCount += 1;
		PlayerPrefs.SetInt("Total Play Count", totalPlayCount);

		totalBuildCoin = totalBuildCoin + nCoinCount;
		PlayerPrefs.SetInt("Total Build Coin", totalBuildCoin);
	}
}
