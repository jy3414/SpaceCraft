using UnityEngine;
using System.Collections;

public class FireAttack : MonoBehaviour
{
    private const int damage = 400;

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        Health health = hit.GetComponent<Health>();

        if (health != null)
        {
            PhotonView pv = health.GetComponent<PhotonView>();
            if (pv != null && GetComponent<NetworkBullet>().GetComponent<PhotonView>().isMine)
            {
                pv.RPC("TakeDamage", PhotonTargets.All, 400);
                //Add score when bullet hits target
                if (GetComponent<IMAIBULLET>().ai == false)
                {
                    PhotonNetwork.player.AddScore(20);
                }
            }
            else
            {
                Debug.Log("Error in getting photonview");
            }
        }

        if (GetComponent<PhotonView>().instantiationId == 0)
        {
            if (health != null)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (health != null)
            {
                if (GetComponent<PhotonView>().isMine)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }

    }
}
