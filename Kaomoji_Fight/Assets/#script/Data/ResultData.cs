using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultData
{
    public int[] Ranking;      //ランキング
    public float Time;         //時間

    public float MAXDamage;                //与えた合計が一番大きかった数値
    public string MAXDamage_playerName;    //与えた合計が一番大きかった数値のプレイヤー名

    public string[] PlayersName;   //名前
    public Sprite[] PlayersFace;   //顔


    public static ResultData Instance
    {
        get;
        private set;
    }
    public ResultData(int[] ranking = null, float time = 0.00f, float MAXdamage = 0, string MAXdamage_playerName = null, string[] playersName = null, Sprite[] playersFace = null)
    {
        Instance = this;

        Ranking = ranking;
        Time = time;

        MAXDamage = MAXdamage;
        MAXDamage_playerName = MAXdamage_playerName;

        PlayersName = playersName;
        PlayersFace = playersFace;
    }
}
