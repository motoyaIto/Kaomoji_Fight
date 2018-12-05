using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class CharacterselectController : CursorController
{
    private bool selectFace = false;//顔を選択した(true)していない(false)
    protected override void Start()
    {
        base.Start();

        //初期座標を入力
        //this.transform.position = new Vector3(FirstTarget.transform.position.x + Difference_x * NowNumberColumn, (FirstTarget.transform.position.y + Difference_y * NowNumberLine) + 0.35f);
        this.transform.position = new Vector3(FirstTarget.transform.position.x - 0.6f + Difference_x * NowNumberColumn, (FirstTarget.transform.position.y + Difference_y * NowNumberLine) + 0.35f);
        //Debug.Log(FirstTarget.transform.position.x - Difference_x);
    }

    protected override void Update()
    {
        if (this.SelectMyMode() == false|| selectFace == true) { return; }
        
        // Controllerの左スティックのAxisを取得            
        LeftStickInput = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, controllerNumber), XCI.GetAxis(XboxAxis.LeftStickY, controllerNumber));

        //LeftStickの入力がない時
        if (LeftStickflag == false)
        {
            this.PushButton();
        }
        else
        {
            //スッティックが中心近くまで戻っていたら
            if ((LeftStickInput.y < 0.1f && LeftStickInput.y > -0.1f) && (LeftStickInput.x < 0.1f && LeftStickInput.x > -0.1f))
            {
                LeftStickflag = false;
            }
        }

        //プレイ人数を決定
        if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, controllerNumber))
        {
            //クリック音
            audiosource.PlayOneShot(Click_clip);

            this.Decide();
        }
    }

    protected override bool SelectMyMode()
    {
        if (TManager_cs.Mode_Data != TitleManager.SELECTMODE.CHARACTERSELECT || TManager_cs.ControllerLock_Data == true)
        {
            return false;
        }

        return true;
    }

    protected override void PushButton()
    {
        //下を押したときの処理
        if (Push_DownButton() == true) { return; }

        //上を押したときの処理
        if (Push_UpButton() == true) { return; }

        //右を押したときの処理
        if (Push_RightBiutton() == true) { return; }

        //左を押したときの処理
        if (Push_LeftBiutton() == true) { return; }
    }

    protected override void Decide()
    {
        //選択した番号を取得
        int number = ((NumberLine * NowNumberColumn)) + (NowNumberLine + 1);

        //顔のテクスチャーを取得
        Sprite face = Resources.Load<Sprite>("textures/use/Player/Player" + number.ToString());
        //マネージャーに登録
        TManager_cs.SetPlayerFace(CNConvert(controllerNumber), face);

        //カーソルを薄くする
        Material cursor_mate = this.transform.GetComponent<Renderer>().material;

        cursor_mate.SetColor("_Color", new Color(cursor_mate.color.r, cursor_mate.color.g, cursor_mate.color.b, 0.5f));
        selectFace = true;  
    }

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
