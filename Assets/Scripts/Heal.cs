using UnityEngine;
using System.Collections;

public class Heal : Resource
{

	private int healAmount = 1000;

	public Heal() {
		existTime = 120f;	
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
                pv.RPC("RestoreHealth", PhotonTargets.All, healAmount);
				DestroyRegion (gameObject);
            }
            else
            {
                Debug.Log("Error in getting photonview");
            }
        }
    }
		
}