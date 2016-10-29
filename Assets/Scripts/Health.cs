using UnityEngine;
using UnityEngine.UI;
using Photon;
using System.Collections;

public class Health : Photon.MonoBehaviour
{

    public const int maxHealth = 2000;

    public int currentHealth = maxHealth;
	public Slider slider;
	public Image sliderBackground;

    [PunRPC]
    public void TakeDamage(int amount)
    {	
        currentHealth -= amount;
        PhotonView pv = GetComponent<PhotonView>();
        if (pv != null)
        {
            pv.RPC("OnChangeHealth", PhotonTargets.All);
        }
        else
        {
            Debug.Log("Error in getting photonview");
        }

        if (currentHealth <= 500 && pv.GetComponent<IMAI>() == null)
        {
            RandomMatchmaker rm = GameObject.FindObjectOfType<RandomMatchmaker>();
            rm.createRecoveryRegion = true;
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    [PunRPC]
    public void RestoreHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > 2000)
        {
            currentHealth = 2000;
        }
        PhotonView pv = GetComponent<PhotonView>();
        if (pv != null)
        {
            pv.RPC("OnChangeHealth", PhotonTargets.All);
        }
        else
        {
            Debug.Log("Error in getting photonview");
        }
    }

    [PunRPC]
    void OnChangeHealth()
    {
		slider.value = maxHealth - currentHealth;
		if (0.4 * maxHealth < currentHealth && currentHealth <= 0.7 * maxHealth) {
			sliderBackground.color = Color.yellow;
		} else if (currentHealth <= 0.4 * maxHealth) {
			sliderBackground.color = Color.red;
		} else if (currentHealth > 0.7 * maxHealth) {
			sliderBackground.color = Color.green;
		}
    }


    void Die()
    {
        PhotonView pv = GetComponent<PhotonView>();
        if (pv.instantiationId == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (pv.isMine)
            {
                if (gameObject.tag == "Player" && GetComponent<IMAI>() == null)
                {
                    RandomMatchmaker rm = GameObject.FindObjectOfType<RandomMatchmaker>();
                    rm.mainCamera.enabled = true;
                    GameObject.FindObjectOfType<RandomMatchmaker>().respawnTimer = 5f;

                    PhotonNetwork.player.SetScore(0);
                    
                }
                else
                {
                    RandomMatchmaker rm = GameObject.FindObjectOfType<RandomMatchmaker>();
                    if (GetComponent<IMAI>().num == 0)
                    {
                        GameObject.FindObjectOfType<RandomMatchmaker>().respawnTimerAI = 5f;
                    } else if (GetComponent<IMAI>().num == 1)
                    {
                        GameObject.FindObjectOfType<RandomMatchmaker>().respawnTimerAI1 = 5f;
                    } else if (GetComponent<IMAI>().num == 2)
                    {
                        GameObject.FindObjectOfType<RandomMatchmaker>().respawnTimerAI2 = 5f;
                    } else if (GetComponent<IMAI>().num == 3)
                    {
                        GameObject.FindObjectOfType<RandomMatchmaker>().respawnTimerAI3 = 5f;
                    } else
                    {
                        GameObject.FindObjectOfType<RandomMatchmaker>().respawnTimerAI4 = 5f;
                    }
                }

                PhotonNetwork.Destroy(gameObject);
                Vector3 explosionPosition = gameObject.transform.position;
                GameObject explosion = PhotonNetwork.Instantiate("ObjectExplosion",
                    explosionPosition, Quaternion.identity, 0);
            } 
        }

    }

}