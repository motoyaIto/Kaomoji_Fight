using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SelectStage : MonoBehaviour {

    [SerializeField, Header("プレイヤーの顔（デバッグ用）")]
    Sprite[] playersface;

    [SerializeField, Header("コントローラー番号")]
    private XboxController ControlerNamber = XboxController.First;//何番目のコントローラーを適用するか

    SelectPNControll selectPN;
    bool cursor = false;
    bool move = true;
    private string stage;
    private static readonly int STAGEMAX = 8;
    private AudioSource sound01;
    private AudioSource sound02;
    private AudioSource sound03;
    private PlayData loadData;
    private int StageNum = 1;
    private bool SelectStop = false;


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
        StartCoroutine("Select");      
        StartCoroutine("coRoutine");     
    }
    IEnumerator coRoutine()
    {
        yield return new WaitForSeconds(1); // num秒待機
        SceneManagerController.LoadScene();
    }

    IEnumerator Select()
    {        
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        // Controllerの左スティックのAxisを取得            
        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, ControlerNamber), XCI.GetAxis(XboxAxis.LeftStickY, ControlerNamber));

        if (input.y < -0.9f || input.y > 0.9f|| input.x < -0.9f || input.x > 0.9f)
        {
            move = false;
        }

        if (cursor == true)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || XCI.GetDPadDown(XboxDPad.Down, ControlerNamber) || input.y < -0.9f)
            {
                if (move == true)
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
                }
                if (move == false)
                {
                    yield return new WaitForSeconds(0.1f); // num秒待機
                    sound01.PlayOneShot(sound01.clip);
                    if (pos.y <= -3.3f)
                    {
                        pos.y = 2.2f;
                    }
                    else
                    {
                        pos.y -= 1.9f;
                    }
                }
                StageNum++;

                if (StageNum > STAGEMAX)
                {
                    StageNum = 1;
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || XCI.GetDPadDown(XboxDPad.Up, ControlerNamber) || input.y > 0.9f)
            {
                if (move == true)
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
                }
                if (move == false)
                {
                    yield return new WaitForSeconds(0.1f); // num秒待機
                    sound01.PlayOneShot(sound01.clip);
                    if (pos.y >= 2.0f)
                    {
                        pos.y = -3.5f;
                    }
                    else
                    {
                        pos.y += 1.9f;
                    }
                }
                StageNum--;

                if (StageNum < 1)
                {
                    StageNum = 4;
                }

            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || XCI.GetDPadDown(XboxDPad.Right, ControlerNamber) || input.x > 0.9f)
            {
                if (move == true)
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
                }
                if (move == false)
                {
                    yield return new WaitForSeconds(0.1f); // num秒待機
                    sound01.PlayOneShot(sound01.clip);
                    if (pos.x >= 2.4f)
                    {
                        pos.x = -5.5f;
                    }
                    else
                    {
                        pos.x = 2.4f;
                    }
                }
                StageNum += 4;

                if (StageNum > STAGEMAX)
                {
                     StageNum -= 8;
                }
            }            
            if (Input.GetKeyDown(KeyCode.LeftArrow) || XCI.GetDPadDown(XboxDPad.Left, ControlerNamber) || input.x < -0.9f)
            {
                if (move == true)
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
                }
                if (move == false)
                {
                    yield return new WaitForSeconds(0.1f); // num秒待機
                    sound01.PlayOneShot(sound01.clip);
                    if (pos.x <= -5.5f)
                    {
                        pos.x = 2.4f;
                    }
                    else
                    {
                        pos.x = -5.5f;
                    }
                }
                StageNum -= 4;

                if (StageNum < 1)
                {
                    StageNum += 8;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, ControlerNamber))
            {
                if (StageNum == 8)
                {
                    NumCount();
                }
                NumCount();
            }
            myTransform.position = pos;  // 座標を設定
            if (Input.GetKeyDown(KeyCode.Backspace) || XCI.GetButtonDown(XboxButton.A, ControlerNamber))
            {
                sound01.PlayOneShot(sound03.clip);
                cursor = false;
            }
            if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, ControlerNamber))
            {
                loadData = new PlayData(selectPN.PlayerNum, null, stage);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, ControlerNamber))
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
            StageNum = Random.Range(1, 8);
        }

        Debug.Log(stage);
        sound01.PlayOneShot(sound02.clip);
    }
}
