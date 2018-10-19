using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour {

    [SerializeField, Header("使うBGM")]
    private AudioClip[] bgm;

    private AudioSource audioPlay;

    private int soundNum = -1;

	// Use this for initialization
	void Start () {
        audioPlay = GetComponent<AudioSource>();
        audioPlay.volume = .3f;
	}
	
	// Update is called once per frame
	void Update () {
        // ランダムでBGMを再生する
        if (soundNum == -1)
        {
            soundNum = Random.Range(0, 4);
            audioPlay.clip = bgm[soundNum];
            audioPlay.Play();
        }
	}
}
