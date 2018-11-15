using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterselectController : CursorController
{
    protected override void Start()
    {
        base.Start();

        //初期座標を入力
        this.transform.position = new Vector3(FirstTarget.transform.position.x + Difference_x * NowNumberColumn, (FirstTarget.transform.position.y + Difference_y * NowNumberLine) + 0.35f);
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
        //throw new NotImplementedException();
    }

}
