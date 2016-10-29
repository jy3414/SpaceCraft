using UnityEngine;
using System.Collections;

public class BackGroundMusic : MonoBehaviour {

    AudioSource fxSound;
    public AudioClip bgm;

    // Use this for initialization
    void Start () {

        fxSound = GetComponent<AudioSource>();
        fxSound.Play();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
