using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

public class StageselectController : CursorController {

    //[SerializeField]
    //private GameObject stageimage;

    protected override void Start()
    {
        base.Start();

        DifferenceCursor = this.transform.GetChild(0).transform.GetComponent<RectTransform>().sizeDelta;

        //初期座標を入力
        this.transform.position = FirstTarget.transform.position + new Vector3(Difference_x * NowNumberColumn - DifferenceCursor.x, -(Difference_y * NowNumberLine));

    }
    protected override bool SelectMyMode()
    {
        if (TManager_cs.Mode_Data != TitleManager.SELECTMODE.STAGESELECT || TManager_cs.ControllerLock_Data == true)
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

        //Image image = stageimage.transform.GetChild(0).transform.GetComponent<Image>();
        //image.sprite = Resources.Load<Sprite>("textures/StagePicture/stage" + (NumberLine * NowNumberColumn) + (NowNumberLine + 1));
        //image.color = new Color(image.color.r, image.color.g, image.color.b, 122);
    }

    protected override void Decide()
    {
        int number = ((NumberLine * NowNumberColumn)) + (NowNumberLine + 1);

        if (number == 8)
        {
            number = UnityEngine.Random.Range(1, 7);
        }


        TManager_cs.Stage_name_Data = "stage" + number.ToString();

        TManager_cs.ChangePage(TitleManager.SELECTMODE.CHARACTERSELECT);
    }
}
