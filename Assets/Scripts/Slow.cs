using UnityEngine;
using System.Collections;

public class Slow : Resource {

	private float slowTime = 5f;


	public Slow() {
		existTime = 120f;	
	}

	// Update is called once per frame
	void Update () {
/*		if (existTime > 0) {
			existTime -= Time.deltaTime;
			if (existTime <= 0) {
				DestroyRegion (gameObject);
			}
		}
*/
	}

	void OnTriggerStay(Collider other)
    {
        var hit = other.gameObject;
        ShipController sc = hit.GetComponent<ShipController>();
        if (sc != null)
        {
            PhotonView pv = sc.GetComponent<PhotonView>();
            if (pv != null)
            {
                pv.RPC("SlowEffect", PhotonTargets.All, slowTime);
            }
            else
            {
                Debug.Log("Error in getting photonview");
            }
        }
    }

}
