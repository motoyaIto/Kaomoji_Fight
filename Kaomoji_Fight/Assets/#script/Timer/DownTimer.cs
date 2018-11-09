using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DownTimer : MonoBehaviour {

    [SerializeField]
    private float TimeLimit = 180f;

    private float nowTime = 0f;         //今のプレイ時間

    private bool DownTimer_State = false;  //カウントダウンを開始する(ture)しない(false)

    private PlaySceneManager PSM;

	// Use this for initialization
	void Start () {
        Write_DownTimerText();
        PSM = GameObject.Find("PlaySceneManager").GetComponent<PlaySceneManager>();
    }
	
	// Update is called once per frame
	void Update () {

        if (DownTimer_State == true)
        {
            nowTime += Time.deltaTime;      //スタートしてからの秒数を格納

            Write_DownTimerText();
        }
    }

    /// <summary>
    /// ダウンタイマーのテキストに書く
    /// </summary>
    private void Write_DownTimerText()
    {
        int remainingTime = (int)(TimeLimit - nowTime);//残り秒

        //残り時間が無くなったらリザルトシーンに遷移する
        if (remainingTime <= 0)
        {
            DownTimer_State = false;
            //PSM.Result();
        }

        //分秒に変換
        int Minute = remainingTime / 60;
        int Seconds = remainingTime - 60 * Minute;

        //テキストを取得
        TextMeshProUGUI TM_timer = this.GetComponent<TextMeshProUGUI>();

        //テキストに書き込み
        TM_timer.text = Minute + ":";
        if (Seconds < 10) { TM_timer.text += "0" + Seconds; } else { TM_timer.text += Seconds; }
    }

    public bool DownTimer_State_data
    {
        set
        {
            DownTimer_State = value;
        }
        get
        {
            return DownTimer_State;
        }
    }
}
