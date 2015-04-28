using UnityEngine;
using System.Collections;

public class SoundButtonEffect : MonoBehaviour {
	public AudioClip ButtonEffectSound;
	public AudioClip CameraButtonEffectSound;

	// Use this for initialization
	public void ButtonEffect(){
		GetComponent<AudioSource>().clip = ButtonEffectSound;
		GetComponent<AudioSource>().Play();
	}
	public void CameraButtonEffect(){
		print ("CameraButtonEffect");
		GetComponent<AudioSource>().clip = CameraButtonEffectSound;
		GetComponent<AudioSource>().Play();
	}
}
