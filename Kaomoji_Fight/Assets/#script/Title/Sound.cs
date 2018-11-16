using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

    private AudioSource sound01;
    // Use this for initialization
    void Start () {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];

        StartCoroutine("SoundCoroutine");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator SoundCoroutine()
    {
        yield return new WaitForSeconds(3.8f);
        sound01.PlayOneShot(sound01.clip);
    }
}
