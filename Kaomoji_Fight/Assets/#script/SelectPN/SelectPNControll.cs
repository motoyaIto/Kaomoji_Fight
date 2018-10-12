using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SelectPNControll : MonoBehaviour {

    private static readonly int PLAYERMAX = 4;
    private PlayData loadData;
    private int PlayerNum = 1;
    private AudioSource sound01;
    private AudioSource sound02;
    float TimeCount = 1;
    // Use this for initialization
    void Start () {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        if (Input.GetKeyDown(KeyCode.DownArrow) || XCI.GetDPadDown(XboxDPad.Down, XboxController.First))
        {
            sound01.PlayOneShot(sound01.clip);
            if (pos.y <= -3.3f)
            {
                pos.y = 2.2f;
            }
            else
            {
                pos.y -= 1.9f;
            }

            //プレイヤーの合計人数
            PlayerNum++;

            if (PlayerNum > PLAYERMAX)
            {
                PlayerNum = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || XCI.GetDPadDown(XboxDPad.Up, XboxController.First))
        {
            sound01.PlayOneShot(sound01.clip);
            if (pos.y >= 2.0f)
            {
                pos.y = -3.5f;
            }
            else
            {
                pos.y += 1.9f;
            }

            //プレイヤーの合計人数
            PlayerNum--;

            if (PlayerNum < 1)
            {
                PlayerNum = PLAYERMAX;
            }
        }
        myTransform.position = pos;  // 座標を設定

        //プレイ人数を決定
        if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, XboxController.First))
        { 
            sound01.PlayOneShot(sound02.clip);
            loadData = new PlayData(PlayerNum);
            StartCoroutine("coRoutine");
        }
    }

    IEnumerator coRoutine() 
    {
        yield return new WaitForSeconds(1); // num秒待機
        SceneManagerController.ChangeCene();
    }
}
