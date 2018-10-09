using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SelectPNControll : MonoBehaviour {

    [SerializeField]
    private static readonly int PLAYERMAX = 4;
    private static int playerNum = 1;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        if (Input.GetKeyDown(KeyCode.DownArrow) || XCI.GetDPadDown(XboxDPad.Down, XboxController.First))
        {
            if (pos.y <= -3.3f)
            {
                pos.y = 2.2f;
            }
            else
            {
                pos.y -= 1.9f;
            }

            //プレイヤーの合計人数
            playerNum++;

            if (playerNum > PLAYERMAX)
            {
                playerNum = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || XCI.GetDPadDown(XboxDPad.Up, XboxController.First))
        {
            if (pos.y >= 2.0f)
            {
                pos.y = -3.5f;
            }
            else
            {
                pos.y += 1.9f;
            }

            //プレイヤーの合計人数
            playerNum--;

            if (playerNum < 1)
            {
                playerNum = PLAYERMAX;
            }
        }
        myTransform.position = pos;  // 座標を設定

        //プレイ人数を決定
        if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, XboxController.First))
        {
            SceneManagerController.ChangeCene();
        }
    }
}
