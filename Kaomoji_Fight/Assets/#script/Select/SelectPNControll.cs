using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SelectPNControll : MonoBehaviour {

    [SerializeField, Header("コントローラー番号")]
    private XboxController ControlerNamber = XboxController.First;//何番目のコントローラーを適用するか

    public GameObject numPlayer;
    bool cursor = true;
    bool move = true;
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
        StartCoroutine("Select");
    }

    IEnumerator Select()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        // Controllerの左スティックのAxisを取得            
        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, ControlerNamber), XCI.GetAxis(XboxAxis.LeftStickY, ControlerNamber));

        if(input.y<-0.9f||input.y>0.9f)
        {
            move = false;
        }
        if (cursor == true)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || XCI.GetDPadDown(XboxDPad.Down, ControlerNamber) || input.y < -0.9f)
            {
                if(move==true)
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
                if(move==false)
                {
                    yield return new WaitForSeconds(0.1f); // num秒待機                    
                    if (pos.y <= -3.3f)
                    {
                        pos.y = 2.2f;
                    }
                    else
                    {
                        pos.y -= 1.9f;
                    }
                    sound01.PlayOneShot(sound01.clip);
                }
                //プレイヤーの合計人数
                PlayerNum++;

                if (PlayerNum > PLAYERMAX)
                {
                    PlayerNum = 1;
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
                    if (pos.y >= 2.0f)
                    {
                        pos.y = -3.5f;
                    }
                    else
                    {
                        pos.y += 1.9f;
                    }
                    sound01.PlayOneShot(sound01.clip);
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
            if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, ControlerNamber))
            {
                sound01.PlayOneShot(sound02.clip);
                cursor = false;
                numPlayer.transform.position += new Vector3(100, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Backspace) || XCI.GetButtonDown(XboxButton.A, ControlerNamber))
        {
            if (cursor == false)
            {
                cursor = true;
                numPlayer.transform.position -= new Vector3(100, 0, 0);
            }

        }
    }
}
