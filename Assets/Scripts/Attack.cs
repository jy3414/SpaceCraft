using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    private const int damage = 200;

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        Health health = hit.GetComponent<Health>();

        if (health != null)
        {
            PhotonView pv = health.GetComponent<PhotonView>();
            if (pv != null && GetComponent<NetworkBullet>().GetComponent<PhotonView>().isMine)
            {
                pv.RPC("TakeDamage", PhotonTargets.All, damage);
                //Add score when bullet hits target
                if (GetComponent<IMAIBULLET>().ai == false)
                {
                    PhotonNetwork.player.AddScore(10);
                }
            }
            else
            {
                Debug.Log("Error in getting photonview");
            }
        }

        if (GetComponent<PhotonView>().instantiationId == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (GetComponent<PhotonView>().isMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }

    }
}
