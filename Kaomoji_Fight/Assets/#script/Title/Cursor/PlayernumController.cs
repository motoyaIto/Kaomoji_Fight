using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class PlayernumController : CursorController {

    protected override void Start()
    {
        base.Start();

        ////初期座標を入力
        //for (int i = 0; i < Target.Length; i++)
        //{
        //    target_pos[i] = new Vector3(this.transform.position.x, Target[i].transform.position.y, this.transform.position.z);
        //}

        //this.transform.position = target_pos[target_number];
    }

    protected override void Update()
    {
        if (TManager_cs.Mode_Data != TitleManager.SELECTMODE.PLAYERNUM || TManager_cs.ControllerLock_Data == true)
        {
            return;
        }

        // Controllerの左スティックのAxisを取得            
        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, XboxController.First), XCI.GetAxis(XboxAxis.LeftStickY, XboxController.First));

        //LeftStickの入力がない時
        if (LeftStickflag == false)
        {
            //下を押したときの処理
            if (Push_DownButton(input)) { return; }

            //上を押したときの処理
            if (Push_UpButton(input)) { return; }
        }
        else
        {
            //スッティックが中心近くまで戻っていたら
            if (input.y < 0.1f && input.y > -0.1f)
            {
                LeftStickflag = false;
            }
        }

        //プレイ人数を決定
        if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, XboxController.First))
        {
            //クリック音
            audiosource.PlayOneShot(Click_clip);

            //TManager_cs.PlayerNum_Data = target_number + 1;

            TManager_cs.ChangePage(TitleManager.SELECTMODE.STAGESELECT);
        }
    }


}
