using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBGM : MonoBehaviour {
    private new AudioSource audio;

    // BGM
    private AudioClip bgm;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = .3f;

        bgm = (AudioClip)Resources.Load("Sound/BGM/Result/result9");
        audio.clip = bgm;
    }

    // Use this for initialization
    void Start () {
        audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
