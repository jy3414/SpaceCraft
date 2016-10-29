using UnityEngine;
using System.Collections;

public class FirePower : Resource
{

	void Update() {
		
	}

	void OnTriggerEnter(Collider other)
	{
		var hit = other.gameObject;

		if (hit.tag == "Player")
		{
			ShipController sc = hit.GetComponent<ShipController>();
			PhotonView pv = sc.GetComponent<PhotonView>();
			if (pv != null)
			{
				pv.RPC("setBullet", PhotonTargets.All, "Fire Bullet");
			}
			DestroyRegion (gameObject);
		}
	}
}