using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SelectStage : MonoBehaviour {

    [SerializeField, Header("プレイヤーの顔（デバッグ用）")]
    Sprite[] playersface;

    SelectPNControll selectPN;
    bool cursor = false;
    private string stage;
    private static readonly int STAGEMAX = 8;
    private AudioSource sound01;
    private AudioSource sound02;
    private AudioSource sound03;
    private PlayData loadData;
    private int StageNum = 1;
    // Use this for initialization
    void Start()
    {
        selectPN = GameObject.Find("Cursor").GetComponent<SelectPNControll>();        
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];
        sound03 = audioSources[2];        
        //playernum = num.PlayerNum;
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        if (cursor == true)
        {
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
                StageNum++;

                if (StageNum > STAGEMAX)
                {
                    StageNum = 1;
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
                StageNum--;

                if (StageNum < 1)
                {
                    StageNum = 4;
                }

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
                StageNum += 4;

                if (StageNum > STAGEMAX)
                {
                    StageNum -=8;
                }
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
                StageNum -= 4;

                if (StageNum < 1)
                {
                    StageNum += 8;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, XboxController.First))
            {
                if (StageNum == 8)
                {
                    NumCount();
                }
                NumCount();
            }
                myTransform.position = pos;  // 座標を設定
            if (Input.GetKeyDown(KeyCode.Backspace) || XCI.GetButtonDown(XboxButton.A, XboxController.First))
            {
                sound01.PlayOneShot(sound03.clip);
                cursor = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, XboxController.First))
            {
                loadData = new PlayData(selectPN.PlayerNum, null, stage);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, XboxController.First))
        { 
            cursor = true;
        }        
    }

    void NumCount()
    {        
        if (StageNum == 1)
        {
            stage = "stage1";
        }
        if (StageNum == 2)
        {
            stage = "stage2";
        }
            if (StageNum == 3)
        {
            stage = "stage3";
        }
        if (StageNum == 4)
        {
            stage = "stage4";
        }
        if (StageNum == 5)
        {
            stage = "stage5";
        }
        if (StageNum == 6)
        {
            stage = "stage6";
        }

        if (StageNum == 7)
        {
            stage = "stage7";
        }

        if (StageNum == 8)
        {
            StageNum =Random.Range(1, 8);
        }

        Debug.Log(stage);
        sound01.PlayOneShot(sound02.clip);        
        StartCoroutine("coRoutine");     
    }
    IEnumerator coRoutine()
    {
        yield return new WaitForSeconds(1); // num秒待機
        SceneManagerController.LoadScene();
    }
}
