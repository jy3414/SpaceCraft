using UnityEngine;
using System.Collections;
using Photon;

public class Name : Photon.PunBehaviour
{
    public TextMesh textMesh;
    
    // Use this for initialization
    void Start()
    {
        textMesh.text = photonView.owner.name;
        if (photonView.isMine && photonView.GetComponent<IMAI>() == null) {
            textMesh.color = Color.green;
        } else {
            textMesh.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}