using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public int coinCounter = 1;
	public float baseCameraHeight = 9f;
	public float baseCameraDepth = -200f;
	public float changeCameraHeight;
	public float coinFixedPositionY;

	public float changeValue = 2.0f;

	private GameObject tmpObject;
	private GameObject tmpGameController;

	private BGController tmpBGScroll;

	private float prevCoinPositionY = 0f;
	private float prevCameraPositionY;

	void Start(){
		tmpObject = GameObject.Find("HitZone");
		tmpBGScroll = GameObject.Find("GameController").GetComponent<BGController>();
	}

	void LateUpdate(){
		if (coinCounter == 1){
			BaseCameraTransform();
		}

		if (coinCounter < tmpObject.GetComponent<CoinConstructor>().coinCount){
			coinCounter = tmpObject.GetComponent<CoinConstructor>().coinCount;
			//changeCameraHeight = (coinCounter - 1) * changeValue + baseCameraHeight;
			changeValue = coinFixedPositionY - prevCoinPositionY;
			if (changeValue >= 2)
				changeValue = 2;
			if (changeValue <=0)
				changeValue = 0;

			changeCameraHeight = changeValue + prevCameraPositionY + baseCameraHeight;

			prevCameraPositionY += changeValue;

			prevCoinPositionY = coinFixedPositionY;

			transform.position = new Vector3(0,changeCameraHeight,baseCameraDepth);

			tmpBGScroll.gameObject.SendMessage("BGScroll");


		}
	}

	public void BaseCameraTransform(){
			transform.position = new Vector3(0,baseCameraHeight,baseCameraDepth);
		}

	public void ResetCoinCounterCamera(){
		coinCounter = 1;
		coinFixedPositionY = 2;
		prevCameraPositionY = 0;
		changeValue = 2;
		changeCameraHeight = 0;
	}
}
