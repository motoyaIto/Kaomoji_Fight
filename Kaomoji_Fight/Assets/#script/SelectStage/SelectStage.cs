using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SelectStage : MonoBehaviour {

    private string stage;
    private AudioSource sound01;
    private AudioSource sound02;
    float TimeCount = 1;
    // Use this for initialization
    void Start()
    {
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
            myTransform.position = pos;  // 座標を設定

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
            myTransform.position = pos;  // 座標を設定
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || XCI.GetDPadDown(XboxDPad.Right, XboxController.First))
        {
            sound01.PlayOneShot(sound01.clip);
            if (pos.x >= 2.4f)
            {
                pos.x = -5.5f;
            }
            else
            {
                pos.x = 2.4f;
            }
            myTransform.position = pos;  // 座標を設定
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || XCI.GetDPadDown(XboxDPad.Left, XboxController.First))
        {
            sound01.PlayOneShot(sound01.clip);
            if (pos.x <= -5.5f)
            {
                pos.x = 2.4f;
            }
            else
            {
                pos.x = -5.5f;
            }
            myTransform.position = pos;  // 座標を設定
        }
        //プレイ人数を決定
        if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, XboxController.First))
        {
            if(pos.x<=-5.4 && pos.x >= -5.6 && pos.y>=2.1 && pos.y <= 2.3)
            {
                stage = "stage1";
            }
            if (pos.x <= -5.4 && pos.x >= -5.6 && pos.y >= 0.2 && pos.y <= 0.4)
            {
                stage = "stage2";
            }
            if (pos.x <= -5.4 && pos.x >= -5.6 && pos.y <= -1.5 && pos.y >= -1.7)
            {
                stage = "stage3";
            }
            if (pos.x <= -5.4 && pos.x >= -5.6 && pos.y <= -3.4 && pos.y >= -3.6)
            {
                stage = "stage4";
            }
            if (pos.x >= 2.3 && pos.x <= 2.5 && pos.y >= 2.1 && pos.y <= 2.3)
            {
                stage = "stage5";
            }
            if (pos.x >= 2.3 && pos.x <= 2.5 && pos.y >= 0.2 && pos.y <= 0.4)
            {
                stage = "stage6";
            }
            if (pos.x >= 2.3 && pos.x <= 2.5 && pos.y <= -1.5 && pos.y >= -1.7)
            {
                stage = "stage7";
            }
            if (pos.x >= 2.3 && pos.x <= 2.5 && pos.y <= -3.4 && pos.y >= -3.6)
            {
                stage = "stage8";
            }
            Debug.Log(stage);
            sound01.PlayOneShot(sound02.clip);
            //StartCoroutine("coRoutine");
        }
    }

    IEnumerator coRoutine()
    {
        yield return new WaitForSeconds(1); // num秒待機
        SceneManagerController.ChangeScene();
    }
}
