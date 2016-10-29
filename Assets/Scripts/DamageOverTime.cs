using UnityEngine;
using System.Collections;

public class DamageOverTime : Resource {

	private int damage = 3;

	public DamageOverTime() {
		existTime = 60f;	
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
        Health health = hit.GetComponent<Health>();
        if (health != null)
        {
            PhotonView pv = health.GetComponent<PhotonView>();
            if (pv != null)
            {
                pv.RPC("TakeDamage", PhotonTargets.All, damage);
            }
            else
            {
                Debug.Log("Error in getting photonview");
            }
        }
    }
		
}
