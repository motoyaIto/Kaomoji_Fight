using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class PlayernumController : CursorController {

    protected override void Start()
    {
        base.Start();

        DifferenceCursor = this.transform.GetChild(0).transform.GetComponent<RectTransform>().sizeDelta;

        //初期座標を入力
        this.transform.position = FirstTarget.transform.position + new Vector3(Difference_x * NowNumberColumn - DifferenceCursor.x, -(Difference_y * NowNumberLine));
    }
    protected override bool SelectMyMode()
    {
        if (TManager_cs.Mode_Data != TitleManager.SELECTMODE.PLAYERNUM || TManager_cs.ControllerLock_Data == true)
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
    }

    protected override void Decide()
    {
        TManager_cs.PlayerNum_Data = (NowNumberColumn + 1) * (NowNumberLine + 2);

        TManager_cs.ChangePage(TitleManager.SELECTMODE.STAGESELECT);
    }
}
