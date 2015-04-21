using UnityEngine;
using System.Collections;

public class CoinActive : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		string layerName = LayerMask.LayerToName(c.gameObject.layer);
		if (layerName == "Coin"){
			if (c.gameObject.GetComponent<CoinController>().state.ToString() == "IDLE"){
				c.gameObject.SendMessage("StateCoercion", "DISABLE");
			}
		}
	}
}
