using UnityEngine;
using System.Collections;

public class NetworkBullet : NetworkCharacter
{

    private int countDown = 10;

    // Update is called once per frame
    void Update() {
        if (countDown > 0)
        {
            lastUpdateTime = 1f;
            countDown--;
        } else
        {
            lastUpdateTime = 0.1f;
        }

		SmoothMove ();

    }

}