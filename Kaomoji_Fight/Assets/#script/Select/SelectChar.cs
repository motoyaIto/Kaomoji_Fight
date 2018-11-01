using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SelectChar : MonoBehaviour {

    [SerializeField, Header("コントローラー番号")]
    private XboxController ControlerNamber = XboxController.First;//何番目のコントローラーを適用するか

    [SerializeField]
    private GameObject SSManager;

    [SerializeField]
    private Camera camera;      //カメラ

    Contoroller2d controller;   // コントローラー
    private PlaySceneManager PSM;
    bool cursor = false;
    bool move = false;
    public string face;
    private AudioSource sound01;
    private AudioSource sound02;
    private AudioSource sound03;
    private int FaceNum = 1;
    // Use this for initialization
    void Start () {
        controller = GetComponent<Contoroller2d>();
        //PSM = GameObject.Find("PlaySceneManager").transform.GetComponent<PlaySceneManager>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];
        sound03 = audioSources[2];
    }
	
	// Update is called once per frame
	void Update ()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        // Controllerの左スティックのAxisを取得            
        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, ControlerNamber), XCI.GetAxis(XboxAxis.LeftStickY, ControlerNamber));

        Debug.Log(pos);

        if (camera.transform.position.x >= -17.9&& camera.transform.position.x <= -17.7)
        {
            cursor = true;
        }

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
                    if (pos.y <= -3)
                    {
                        pos.y = 2.4f;
                        FaceNum -= 4;
                    }
                    //下に移動
                    else
                    {
                        pos.y -= 1.86f;
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
                    if (pos.y >= 2.3)
                    {
                        pos.y = -3.2f;
                        FaceNum += 4;
                    }
                    //上に移動
                    else
                    {
                        pos.y += 1.86f;
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
                    if (pos.x >= -15)
                    {
                        pos.x = -23.7f;
                        FaceNum -= 8;
                    }
                    //右に移動
                    else
                    {
                        pos.x += 4.2f;
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
                    if (pos.x <= -23.3)
                    {
                        pos.x = -11.1f;
                        FaceNum += 12;
                    }
                    //左に移動
                    else
                    {
                        pos.x -= 4.2f;
                    }
                    move = false;
                }
            }
            //決定処理
            if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, ControlerNamber))
            {                
                FaceCount();                
                cursor = false;
            }
            myTransform.position = pos;  // 座標を設定
                       
        }       
    }

    void FaceCount()
    {
        switch (FaceNum)
        {
            case 1:
                face = "face1";
                break;
            case 2:
                face = "face2";
                break;
            case 3:
                face = "face3";
                break;
            case 4:
                face = "face4";
                break;
            case 5:
                face = "face5";
                break;
            case 6:
                face = "face6";
                break;
            case 7:
                face = "face7";
                break;
            case 8:
                face = "face8";
                break;
            case 9:
                face = "face9";
                break;
            case 10:
                face = "face10";
                break;
            case 11:
                face = "face11";
                break;
            case 12:
                face = "face12";
                break;
            case 13:
                face = "face13";
                break;
            case 14:
                face = "face14";
                break;
            case 15:
                face = "face15";
                break;
            case 16:
                face = "face16";
                break;
        }
        sound01.PlayOneShot(sound02.clip);
    }

    //private void OnDisable()
    //{
    //    PSM.death_player[CNConvert(ControlerNamber)] = false;
    //}
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
