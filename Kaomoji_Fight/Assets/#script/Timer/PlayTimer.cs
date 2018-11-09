using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PlayTimer : MonoBehaviour {

    [SerializeField]
    private float TimeLimit = 180f;
    private static float s_TimeLimit; //１プレイの制限時間

    private float nowTime = 0f;     //今のプレイ時間

    private bool Timer_on = false;  //タイマーを

	// Use this for initialization
	void Start () {
        s_TimeLimit = TimeLimit;
	}
	
	// Update is called once per frame
	void Update () {
        s_TimeLimit = 500;
        nowTime += Time.deltaTime;      //スタートしてからの秒数を格納

        int remainingTime = (int)(s_TimeLimit - nowTime);//残り時間

        int Minute = remainingTime / 60;
        int Seconds = remainingTime - 60 * Minute;

        TextMeshProUGUI TM_timer = this.GetComponent<TextMeshProUGUI>();

        TM_timer.text = Minute + ":";
        if (Seconds < 10) { TM_timer.text += "0" + Seconds; } else { TM_timer.text += Seconds; }
        Debug.Log("m" + Minute + "S" +Seconds);

    }
}
