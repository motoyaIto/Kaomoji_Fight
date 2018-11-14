using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour {

    private AudioSource audioPlay;

    // BGM
    private AudioClip bgm1;
    private AudioClip bgm2;
    private AudioClip bgm3;
    private AudioClip bgm4;
    private AudioClip bgm5;
    private AudioClip secret;

    private int soundNum = -1;

    private DownTimer timer;

    private void Awake()
    {
        timer = GameObject.Find("DownTimer").transform.GetComponent<DownTimer>();

        audioPlay = GetComponent<AudioSource>();
        audioPlay.volume = .3f;

        bgm1 = (AudioClip)Resources.Load("Sound/BGM/Play/Buttle1");
        bgm2 = (AudioClip)Resources.Load("Sound/BGM/Play/Buttle2");
        bgm3 = (AudioClip)Resources.Load("Sound/BGM/Play/Buttle3");
        bgm4 = (AudioClip)Resources.Load("Sound/BGM/Play/Buttle4");
        bgm5 = (AudioClip)Resources.Load("Sound/BGM/Play/Buttle5");
        secret = (AudioClip)Resources.Load("Sound/BGM/Play/Secret");
    }

    // Use this for initialization
    void Start () {
        // ランダムでBGMを再生する
        if (soundNum == -1)
        {
            soundNum = Random.Range(0, 51);
            switch (soundNum)
            {
                case 27:
                case 14:
                case 38:
                case 15:
                case 13:
                case 2:
                case 50:
                case 10:
                case 19:
                case 0:
                    audioPlay.clip = bgm1;
                    break;
                case 35:
                case 40:
                case 49:
                case 42:
                case 24:
                case 43:
                case 44:
                case 26:
                case 7:
                case 48:
                    audioPlay.clip = bgm2;
                    break;
                case 20:
                case 17:
                case 16:
                case 45:
                case 1:
                case 31:
                case 18:
                case 23:
                case 4:
                case 6:
                    audioPlay.clip = bgm3;
                    break;
                case 39:
                case 51:
                case 33:
                case 21:
                case 8:
                case 34:
                case 46:
                case 25:
                case 12:
                case 9:
                    audioPlay.clip = bgm4;
                    break;
                case 30:
                case 11:
                case 32:
                case 3:
                case 37:
                case 29:
                case 47:
                case 41:
                case 22:
                case 28:
                    audioPlay.clip = bgm5;
                    break;
                case 5:
                    audioPlay.clip = secret;
                    break;
            }
            audioPlay.Play();
        }

    }

    // Update is called once per frame
    void Update () {
        // 制限時間になったらBGMを止める
        if (!timer.DownTimer_State_data)
        {
            BgmStop();
        }
	}


    public void BgmStop()
    {
        audioPlay.Stop();
    }
}
