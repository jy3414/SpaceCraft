using UnityEngine;
using System.Collections;

public class StarScore : Resource {

	private const int score = 5;

	public StarScore() {
		existTime = 60f;	
	}

	void OnCollisionEnter(Collision collision) {

		var hit = collision.gameObject;

		ShipController sc = hit.GetComponent<ShipController> ();
		if (sc != null) {
			sc.SetAcceleration (sc.AccelerateForce * 2);
		}

		if (hit.tag == "Player" && hit.GetComponent<IMAI>() == null) {
			PhotonNetwork.player.AddScore (score);
		} else if (hit.GetComponent<IMAI>() != null)
        {
            hit.GetComponent<IMAI>().score += 5;
        }

		NetworkBullet networkBullet = hit.GetComponent<NetworkBullet> ();
		if (networkBullet != null) {
			PhotonView bulletpv = networkBullet.GetComponent<PhotonView> ();

			if (bulletpv != null) {
				if (bulletpv.isMine && bulletpv.GetComponent<IMAIBULLET>().ai == false) {
					PhotonNetwork.player.AddScore (score * 2);
				}
			}

		}

		DestroyRegion (gameObject);

	}
		
}
