using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SelectCharControll : MonoBehaviour {
    [SerializeField, Header("コントローラー番号")]
    private XboxController ControlerNamber = XboxController.First;//何番目のコントローラーを適用するか
    [SerializeField]
    private GameObject SSManager;
    Contoroller2d controller;   // コントローラー
    private PlaySceneManager PSM;
    bool cursor = false;
    bool move = false;
    private string face;
    private AudioSource sound01;
    private AudioSource sound02;
    private AudioSource sound03;
    private int FaceNum = 1;
    // Use this for initialization
    void Start () {
        controller = GetComponent<Contoroller2d>();
        PSM = GameObject.Find("PlaySceneManager").transform.GetComponent<PlaySceneManager>();
    }
	
	// Update is called once per frame
	void Update () {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        // Controllerの左スティックのAxisを取得            
        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, ControlerNamber), XCI.GetAxis(XboxAxis.LeftStickY, ControlerNamber));

        if (input.y == 0 && input.x == 0)
        {
            //スティックが倒れてなければtrue
            move = true;
        }

        if (cursor == true)
        {
            if (move == true)
            {
                //下を押したときの処理
                if (Input.GetKeyDown(KeyCode.DownArrow) || XCI.GetDPadDown(XboxDPad.Down, ControlerNamber) || input.y < -0.9f)
                {
                    if (sound01.isPlaying == false)
                    {
                        sound01.PlayOneShot(sound01.clip);
                        FaceNum++;
                    }
                    //一番下で押したとき上に戻る 
                    if (pos.y <= -3.3f)
                    {
                        pos.y = 2.2f;
                        FaceNum -= 4;
                    }
                    //下に移動
                    else
                    {
                        pos.y -= 1.9f;
                    }
                    move = false;
                }
                //上を押したときの処理
                if (Input.GetKeyDown(KeyCode.UpArrow) || XCI.GetDPadDown(XboxDPad.Up, ControlerNamber) || input.y > 0.9f)
                {

                    if (sound01.isPlaying == false)
                    {
                        sound01.PlayOneShot(sound01.clip);
                        FaceNum--;
                    }
                    //一番上で押したとき下に移動
                    if (pos.y >= 2.0f)
                    {
                        pos.y = -3.5f;
                        FaceNum += 4;
                    }
                    //上に移動
                    else
                    {
                        pos.y += 1.9f;
                    }
                    move = false;
                }
                //右を押したときの処理
                if (Input.GetKeyDown(KeyCode.RightArrow) || XCI.GetDPadDown(XboxDPad.Right, ControlerNamber) || input.x > 0.9f)
                {

                    if (sound01.isPlaying == false)
                    {
                        sound01.PlayOneShot(sound01.clip);
                        FaceNum += 4;
                    }
                    //右側で押したとき左へ移動
                    if (pos.x >= 20)
                    {
                        pos.x = 12;
                        FaceNum -= 8;
                    }
                    //右に移動
                    else
                    {
                        pos.x = 20;
                    }
                    move = false;
                }
                //左を押したときの処理
                if (Input.GetKeyDown(KeyCode.LeftArrow) || XCI.GetDPadDown(XboxDPad.Left, ControlerNamber) || input.x < -0.9f)
                {
                    if (sound01.isPlaying == false)
                    {
                        sound01.PlayOneShot(sound01.clip);
                        FaceNum -= 4;
                    }
                    //左で押したとき右に移動
                    if (pos.x <= 12)
                    {
                        pos.x = 20;
                        FaceNum += 8;
                    }
                    //左に移動
                    else
                    {
                        pos.x = 12;
                    }
                    move = false;
                }
            }
            //決定処理
            if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, ControlerNamber))
            {                
                FaceCount();
                StartCoroutine("coRoutine");
            }
            myTransform.position = pos;  // 座標を設定

            //人数セレクトに戻る処理
            if (Input.GetKeyDown(KeyCode.Backspace) || XCI.GetButtonDown(XboxButton.A, ControlerNamber))
            {
                sound01.PlayOneShot(sound03.clip);
                cursor = false;
            }
        }
        //ステージセレクトになった時カーソルを動かせるように
        if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, ControlerNamber))
        {
            cursor = true;
        }
    }

    void FaceCount()
    {
        if (FaceNum == 1)
        {
            face = "Face1";
        }
        if (FaceNum == 2)
        {
            face = "Face2";
        }
        if (FaceNum == 3)
        {
            face = "Face3";
        }
        if (FaceNum == 4)
        {
            face = "Face4";
        }
        if (FaceNum == 5)
        {
            face = "Face5";
        }
        if (FaceNum == 6)
        {
            face = "Face6";
        }

        if (FaceNum == 7)
        {
            face = "Face7";
        }       

        SelectSceneManager SSManager_script = SSManager.GetComponent<SelectSceneManager>();
       // SSManager_script.Playersface_Data = face;
        sound01.PlayOneShot(sound02.clip);
    }

    private void OnDisable()
    {
        PSM.death_player[CNConvert(ControlerNamber)] = false;
    }
    // Controllerの番号をint型で取得
    private int CNConvert(XboxController controlerNum)
    {
        switch (controlerNum)
        {
            case XboxController.First:
                return 0;
            case XboxController.Second:
                return 1;
            case XboxController.Third:
                return 2;
            case XboxController.Fourth:
                return 3;
            default:
                break;
        }
        return 4;
    }
}
