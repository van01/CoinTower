using UnityEngine;
using System.Collections;

public class UICoinViewer : MonoBehaviour {
	public GameObject coin;
	public GameObject coin_01;
	public GameObject coin_02;
	public GameObject coin_03;
	public GameObject coin_04;
	public GameObject coin_05;



	public void CoinViewerChange(int i){
		float coinSize = 0.0f;

		if (i == 1){
			coin = coin_01;
			coinSize = 0.3f;
		}
		if (i == 2){
			coin = coin_02;
			coinSize = 0.33f;
		}
		if (i == 3){
			coin = coin_03;
			coinSize = 0.36f;
		}
		if (i == 4){
			coin = coin_04;
			coinSize = 0.39f;
		}
		if (i == 5){
			coin = coin_05;
			coinSize = 0.42f;
		}

		GameObject Child = Instantiate(coin,this.transform.position,Quaternion.Euler(0,0,0)) as GameObject;
		Rigidbody tmpRigidbody = Child.GetComponent<Rigidbody>();
		Destroy(tmpRigidbody);

		Component tmpCoinController = Child.GetComponent<CoinController>();
		tmpCoinController.gameObject.SendMessage("StateCoercion","STANDBY");
		//Destroy(tmpCoinController);

		Child.transform.parent = this.transform;
		Child.transform.localScale = Child.transform.localScale * coinSize;

		Child.GetComponent<CoinAni>();

		//int rFaceAniNum = Random.Range(0,4);	//
		int rFaceAniNum = 0;	// 
		Child.gameObject.SendMessage("ChangeAni", rFaceAniNum);	//
	}

	public void CoinViewerDelete(int i){
		GameObject tmpParentCoin = transform.FindChild("Coin_0" + i.ToString() + "(Clone)").gameObject;
		Destroy(tmpParentCoin);
	}
}
