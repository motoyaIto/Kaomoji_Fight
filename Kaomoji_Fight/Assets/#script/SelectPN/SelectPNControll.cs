using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class SelectPNControll : MonoBehaviour {

    public static int playerNum = 0;
    // Use this for initialization
    void Start () {
		
	}
	
    public static int getPlayerNum()
    {
        return playerNum;
    }

	// Update is called once per frame
	void Update () {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        if (Input.GetKeyDown(KeyCode.DownArrow) || XCI.GetButton(XboxButton.DPadDown, XboxController.First))
        {
            if (pos.y <= -3.3f)
            {
                pos.y = 2.2f;
            }
            else
            {
                pos.y -= 1.9f;    
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || XCI.GetButton(XboxButton.DPadUp, XboxController.First))
        {
            if (pos.y >= 2.0f)
            {
                pos.y = -3.5f;
            }
            else
            {
                pos.y += 1.9f;
            }
        }
        myTransform.position = pos;  // 座標を設定

        if (Input.GetKeyDown(KeyCode.Space)||XCI.GetButton(XboxButton.B, XboxController.First))
        {
            if (pos.y <= 3.0f && pos.y >= 1.5)
            {
                playerNum = 1;
            }

            if (pos.y <= 1.0f && pos.y >= 0.3)
            {
                playerNum = 2;
            }
            if (pos.y <= -1.0f && pos.y >= -2.3)
            {
                playerNum = 3;
            }

            if (pos.y <= -3.0f && pos.y >= -4.0)
            {
                playerNum = 4;
            }
            Debug.Log(playerNum);
        }
    }
}
