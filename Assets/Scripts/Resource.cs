using UnityEngine;
using System.Collections;

public class Resource : MonoBehaviour {
	
	protected float existTime;

	// Update is called once per frame
	void Update () {
		if (existTime > 0) {
			existTime -= Time.deltaTime;
			if (existTime <= 0) {
				DestroyRegion (gameObject);
			}
		}
	}

	protected void DestroyRegion(GameObject obj) {
		if (GetComponent<PhotonView> ().instantiationId == 0) {
			Destroy (obj);
		} else {
			if (GetComponent<PhotonView> ().isMine) {
				PhotonNetwork.Destroy (obj);
			}
		}
	}
}
