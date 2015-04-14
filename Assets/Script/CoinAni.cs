using UnityEngine;
using System.Collections;

public class CoinAni : MonoBehaviour {
	
	public const int IDLE00 = 0;
	public const int IDLE01 = 1;
	public const int IDLE02 = 2;
	public const int IDLE03 = 3;

	public GameObject Face;
	Animator anim;

	private int FaceNum;
	
	// Use this for initialization
	void Start () {
		anim = Face.gameObject.GetComponent<Animator>();
		FaceNum = gameObject.GetComponent<CoinController>().CoinFaceNo;
		ChangeFace(FaceNum);
	}

	void ChangeFace(int faceNum){
		if (faceNum > 0){
			Face.SetActive(true);
			anim.SetInteger("faceNumber", faceNum);
		}
	}
	
	public void ChangeAni(int aniNum){
		if ( FaceNum > 0){
			StartCoroutine(WaitIdleAni(aniNum));
		}
	}
	
	IEnumerator WaitIdleAni(int aniNum){
		float waitTime = 0f; 

		yield return new WaitForSeconds (waitTime);
		anim.SetInteger("aniNumber", aniNum);
	}
	public void CoinAniEnd(){
		if ( FaceNum > 0){
			anim.SetInteger("aniNumber", 2);
		}
	}
}