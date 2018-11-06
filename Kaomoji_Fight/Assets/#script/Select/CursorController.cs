using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class CursorController : MonoBehaviour {

    private AudioSource audio;  //オーディオ
    [SerializeField]
    AudioClip Move_clip;        //移動音
    [SerializeField]
    AudioClip Click_clip;       //クリック音

    [SerializeField]
    GameObject TManager;     //タイトルマネージャ
    TitleManager TManager_cs;//タイトルマネージャのCS

    [SerializeField]
    GameObject[] Target;//ターゲット

    private bool LeftStickflag = false;//スティックが入力されていない(false)された(true)

    private Vector3[] target_pos;   //ターゲットのy座標
    private int target_number = 0;  //ターゲットの番号
	void Start () {
        audio = this.GetComponent<AudioSource>();

        target_pos = new Vector3[Target.Length];
        //初期座標を入力
        for(int i = 0; i < Target.Length; i++)
        {
            target_pos[i] = new Vector3(this.transform.position.x, Target[i].transform.position.y, this.transform.position.z);
        }

        this.transform.position = target_pos[target_number];

        TManager_cs = TManager.GetComponent<TitleManager>();
    }


    void Update()
    {
        if(TManager_cs.Mode_data != TitleManager.SELECTMODE.PLAYERNAM && TManager_cs.Mode_data != TitleManager.SELECTMODE.STAGESELECT)
        {
            return;
        }
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

                TManager_cs.PlayerNum_data = target_number + 1;

                TManager_cs.ChangePage(TitleManager.SELECTMODE.STAGESELECT);
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
}
