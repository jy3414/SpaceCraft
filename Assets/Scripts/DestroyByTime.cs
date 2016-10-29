using UnityEngine;
using System.Collections;
using Photon;

public class DestroyByTime : Photon.PunBehaviour {

	private double spawnTime;
	public double existTime;

	// Use this for initialization
	void Start () {
		spawnTime = PhotonNetwork.time;
	}

	// Update is called once per frame
	void Update () {
		if (existTime == 0) {
			existTime = 1.5f;
		}
		if (PhotonNetwork.time - spawnTime >= existTime) {
			PhotonView pv = GetComponent<PhotonView>();
			if (pv.instantiationId == 0) {
				Destroy(gameObject);
			} else {
				if (pv.isMine) {
					PhotonNetwork.Destroy(gameObject);
				}
			}
		}
	}
}