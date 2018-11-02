using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SelectPNControll : MonoBehaviour {

    [SerializeField]
    private GameObject TManager;//セレクトシーンマネージャー

    [SerializeField]
    private Camera camera;      //カメラ

    [SerializeField, Header("コントローラー番号")]
    private XboxController ControlerNamber = XboxController.First;//何番目のコントローラーを適用するか

    bool cursor = false;
    bool move = false;
    private static readonly int PLAYERMAX = 4;
    public int PlayerNum = 1;
    private AudioSource sound01;
    private AudioSource sound02;

   
    // Use this for initialization
    void Start ()
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

        // Controllerの左スティックのAxisを取得            
        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, ControlerNamber), XCI.GetAxis(XboxAxis.LeftStickY, ControlerNamber));
        
        if(camera.transform.position.x == 0)
        {
            cursor = true;
        }

        if (input.y == 0)
        {
            //スティックを倒してなかったらtrueに
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
                        //プレイヤーの合計人数
                        PlayerNum++;
                        //4の時に下を押したら1に戻る
                        if (PlayerNum > PLAYERMAX) 
                        {
                            PlayerNum = 1;
                        }
                    }
                    //4の時に下を押したら1に戻る
                    if (pos.y <= -3.3f)
                    {
                        pos.y = 2.2f;
                    }
                    //カーソル移動
                    else
                    {
                        pos.y -= 1.9f;
                    }

                    Debug.Log("+して" + PlayerNum);
                    move = false;
                }           
                //上を押したときの処理
                if (Input.GetKeyDown(KeyCode.UpArrow) || XCI.GetDPadDown(XboxDPad.Up, ControlerNamber) || input.y > 0.9f)
                {
                    if (sound01.isPlaying == false)
                    {
                        //プレイヤーの合計人数
                        PlayerNum--;
                        //1の時に上を押したら4に行く
                        if (PlayerNum < 1)
                        {
                            PlayerNum = PLAYERMAX;
                        }
                        sound01.PlayOneShot(sound01.clip);
                    }
                    //1の時に上上を押したら4に行く                
                    if (pos.y >= 2.0f)   
                    {
                        pos.y = -3.5f;
                    }
                    //カーソルを上に移動
                    else
                    {
                        pos.y += 1.9f;
                    }
                    Debug.Log("-して" + PlayerNum);
                    move = false;
                }
            }
            myTransform.position = pos;  // 座標を設定
            //プレイ人数を決定
            if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, ControlerNamber))
            {
                TitleManager TManager_cs = TManager.GetComponent<TitleManager>();

                //TManager_cs.PlayerNam_Data = PlayerNum;                
                sound01.PlayOneShot(sound02.clip);                
                camera.transform.position = new Vector3(-17.8f, 0, 0);  //人数セレクトに移動
                Debug.Log("最終値" + PlayerNum);
                cursor = false;
            }
        }
    }   
}
