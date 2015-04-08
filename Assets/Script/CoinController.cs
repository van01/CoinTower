using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {
	public enum CoinState{
		IDLE,
		MOVING,
		FIXED,
		DISABLE,
		STANDBY,
		FALL,
	}
	public CoinState state = CoinState.IDLE;

	public int CoinFaceNo;

	private GameController tmpGameController;
	private CoinConstructor tmpCoinConstructor;
	private CoinAni tmpCoinAni;

	private int windRandInt = 0;

	private SoundCoinEffect tmpSoundCoinEffect;

	private bool bFixed = false;
	private bool bIdle = false;
	private bool bFall = false;

	void Start () {
		tmpGameController = GameObject.Find("GameController").GetComponent<GameController>();
		tmpCoinConstructor = GameObject.Find("HitZone").GetComponent<CoinConstructor>();
		tmpSoundCoinEffect = GetComponent<SoundCoinEffect>();
		tmpCoinAni = GetComponent<CoinAni>();
	}

	void Update () {
		switch(state){
		case CoinState.IDLE:
			StateIdle();
			break;
		case CoinState.MOVING:
			StateMoving();
			break;
		case CoinState.FIXED:
			StateFixed();
			break;	
		case CoinState.DISABLE:
			StateDisable();
			break;
		}
	}

	void OnCollisionEnter(Collision c){
		string layerName = LayerMask.LayerToName(c.gameObject.layer);

		if (state == CoinState.MOVING){
			if (layerName == "Coin"){
				StateCoercion("FIXED");
			}
		}
	}

	public void StateCoercion(string SC){
		if (SC == "IDLE"){
			state = CoinState.IDLE;
		}
		if (SC == "MOVING"){	
			state = CoinState.MOVING;
		}
		if (SC == "FIXED"){
			state = CoinState.FIXED;
		}
		if (SC == "DISABLE"){
			state = CoinState.DISABLE;
		}
	}

	void StateIdle(){
		if (bIdle == false){
			tmpCoinConstructor.gameObject.SendMessage("WorkOn");
			tmpCoinConstructor.gameObject.SendMessage("WindRandom");
			bIdle = true;
		}
	}

	void StateMoving(){
		tmpCoinConstructor.gameObject.SendMessage("WorkOff");
		windRandInt = tmpCoinConstructor.randWind;
		float nPosY = this.gameObject.transform.position.y;
		float cPosX = this.gameObject.transform.position.x + Time.deltaTime * windRandInt;
		this.gameObject.transform.position = new Vector3 (cPosX,nPosY,0);

		if (tmpSoundCoinEffect.effectSoundNo == 0){
			tmpSoundCoinEffect.CoinEffectSoundChange(1);
			tmpSoundCoinEffect.effectSoundNo = 1;
		}
	}

	void StateFixed(){
		if (bFixed == false){
			tmpGameController.gameObject.SendMessage("CoinCounterMounterCall");
			tmpGameController.gameObject.SendMessage("CoinRigidDel");
			tmpCoinConstructor.gameObject.SendMessage("RandWindOn");
			tmpCoinAni.gameObject.SendMessage("CoinAniEnd");
			state = CoinState.IDLE;
			bFixed = true;
		}

		if (tmpSoundCoinEffect.effectSoundNo == 1){
			tmpSoundCoinEffect.CoinEffectSoundChange(2);
			tmpSoundCoinEffect.effectSoundNo = 2;
		}
	}

	void StateDisable(){
		Rigidbody tmp = GetComponent<Rigidbody>();
		Destroy(tmp);
		this.GetComponent<Renderer>().enabled = false;
		state = CoinState.IDLE;
	}
}
