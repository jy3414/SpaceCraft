﻿using UnityEngine;
using System.Collections;

public class InvisiblePower : Resource
{

    void OnTriggerEnter(Collider other)
    {

        var hit = other.gameObject;

        if (hit.tag == "Player")
        {
            ShipController sc = hit.GetComponent<ShipController>();
            PhotonView pv = sc.GetComponent<PhotonView>();
            if (pv != null)
            {
                pv.RPC("setBullet", PhotonTargets.All, "Invisible Bullet");
            }
			DestroyRegion (gameObject);
        }

    }
}