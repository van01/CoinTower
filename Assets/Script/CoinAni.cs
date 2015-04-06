using UnityEngine;
using System.Collections;

public class CoinAni : MonoBehaviour {
	
	public const int IDLE00 = 0;
	public const int IDLE01 = 1;
	public const int IDLE02 = 2;
	public const int IDLE03 = 3;
	
	public GameObject Face;
	Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = Face.gameObject.GetComponent<Animator>();

		int rFaceNum = Random.Range(1,3);
		ChangeFace(rFaceNum);
	}

	void ChangeFace(int faceNum){
		anim.SetInteger("faceNumber", faceNum);
	}
	
	public void ChangeAni(int aniNum){
		StartCoroutine(WaitIdleAni(aniNum));
	}
	
	IEnumerator WaitIdleAni(int aniNum){
		float waitTime = 10.0f; 
		if (aniNum <= 3){
			if (aniNum > 0)
			{
				yield return new WaitForSeconds (waitTime);
				print("ifAniNum >= 0 :::::::::::" + aniNum);
				anim.SetInteger("aniNumber", aniNum);
				ChangeAni(0);
			}
			else {
				yield return new WaitForSeconds (waitTime);
				print ("ifAniNum == 0 !!!!!!!!!!" + aniNum);
				anim.SetInteger("aniNumber", aniNum);
				
				int randAniNum = Random.Range(1,5);
				aniNum = randAniNum;
				
				ChangeAni(aniNum);	
			}
		}
	}
}