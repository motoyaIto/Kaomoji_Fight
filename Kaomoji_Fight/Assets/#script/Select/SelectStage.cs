using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SelectStage : MonoBehaviour {

    [SerializeField, Header("プレイヤーの顔（デバッグ用）")]
    Sprite[] playersface;

    [SerializeField, Header("コントローラー番号")]
    private XboxController ControlerNamber = XboxController.First;//何番目のコントローラーを適用するか

    [SerializeField]
    private GameObject SSManager;

    SelectPNControll selectPN;
    bool cursor = false;
    bool move = false;
    private string stage;
    private static readonly int STAGEMAX = 8;
    private AudioSource sound01;
    private AudioSource sound02;
    private AudioSource sound03;
    private PlayData loadData;
    private int StageNum = 1;
    private bool SelectStop = false;

    [SerializeField]
    private int Random_min = 1;//ランダムの最小値
    [SerializeField]
    private int Random_max = 2;//ランダムの最大値

    // Use this for initialization
    void Start()
    {
        selectPN = GameObject.Find("Cursor").GetComponent<SelectPNControll>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];
        sound03 = audioSources[2];        
    }

    // Update is called once per frame
    void Update()
    {
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
                        StageNum++;
                    }
                    //一番下で押したとき上に戻る 
                    if (pos.y <= -3.3f)
                    {
                        pos.y = 2.2f;
                        StageNum -= 4;
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
                        StageNum--;
                    }
                    //一番上で押したとき下に移動
                    if (pos.y >= 2.0f)
                    {
                        pos.y = -3.5f;
                        StageNum += 4;
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
                        StageNum += 4;
                    }
                    //右側で押したとき左へ移動
                    if (pos.x >= 20)
                    {
                        pos.x = 12;
                        StageNum -= 8;
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
                        StageNum -= 4;
                    }
                    //左で押したとき右に移動
                    if (pos.x <= 12)
                    {
                        pos.x = 20;
                        StageNum += 8;
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
                if (StageNum == 8)
                {
                    NumCount();
                }
                NumCount();
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
    IEnumerator coRoutine()
    {
        yield return new WaitForSeconds(1); // num秒待機
        SceneManagerController.LoadScene();
    }

    //StageNumに応じてステージ名を与える
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
        //8の時だけそれ以外からランダムで値を取る
        if (StageNum == 8)
        {
            StageNum = Random.Range(Random_min, Random_max);
        }

        SelectSceneManager SSManager_script = SSManager.GetComponent<SelectSceneManager>();
        SSManager_script.Stage_name_Data = stage;
        sound01.PlayOneShot(sound02.clip);
    }
}
