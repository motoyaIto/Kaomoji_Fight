using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class CursorController : MonoBehaviour {

    private AudioSource audio;          //オーディオ
    [SerializeField]
    AudioClip Move_clip;//移動音
    [SerializeField]
    AudioClip Click_clip;//クリック音

    [SerializeField]
    GameObject TManager;     //タイトルマネージャ
    TitleManager TManager_cs;//タイトルマネージャのCS

    private bool LeftStickflag = false;//スティックが入力されていない(false)された(true)

    private Vector3[] target_pos;   //ターゲットのy座標
    private int target_number = 0;  //ターゲットの番号
	void Start () {
        audio = this.GetComponent<AudioSource>();
        //初期座標を入力
        this.transform.position = target_pos[target_number];

        TManager_cs = TManager.GetComponent<TitleManager>();
    }


    void Update()
    {
        // Controllerの左スティックのAxisを取得            
        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, XboxController.First), XCI.GetAxis(XboxAxis.LeftStickY, XboxController.First));

        //LeftStickの入力がない時
        if (LeftStickflag == false)
        {
            //下を押したときの処理
            if (Input.GetKeyDown(KeyCode.DownArrow) || XCI.GetDPadDown(XboxDPad.Down, XboxController.First) || (input.y < -0.9f && LeftStickflag == false))
            {
                //次のターゲット番号に変更
                target_number++;

                Move();
            }

            //上を押したときの処理
            if (Input.GetKeyDown(KeyCode.UpArrow) || XCI.GetDPadDown(XboxDPad.Up, XboxController.First) || (input.y > 0.9f && LeftStickflag == false))
            {
                //次のターゲット番号に変更
                target_number--;

                Move();
            }

            //プレイ人数を決定
            if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, XboxController.First))
            {
                //クリック音
                audio.PlayOneShot(Click_clip);

                //TManager_cs.PlayerNam_Data = target_number + 1;
            }
        }
        else
        {
            //スッティックが中心近くまで戻っていたら
            if (input.y < 0.1f && input.y > -0.1f)
            {
                LeftStickflag = false;
            }
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        //移動音
        audio.PlayOneShot(Move_clip);

        //ターゲットが配列をオーバーしたら
        if (target_number >= target_pos.Length)
        {
            target_number = 0;
        }
        if (target_number < 0)
        {
            target_number = target_pos.Length - 1;
        }

        //ターゲットのy座標に変更
        this.transform.position = target_pos[target_number];

        LeftStickflag = true;
    }

    /// <summary>
    /// ターゲットの座標を取得する
    /// </summary>
    public Vector3[] Target_pos_data
    {
        set
        {
            target_pos = value;
        }
    }
}
