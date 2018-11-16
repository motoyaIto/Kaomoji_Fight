using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultData
{
    public float Time;         //時間

    public int MAXDamage;                //与えた合計が一番大きかった数値
    public string MAXDamage_playerName;    //与えた合計が一番大きかった数値のプレイヤー名

    public RankingData[] Ranking;     //ランキング
   


    public static ResultData Instance
    {
        get;
        private set;
    }
    public ResultData(float time = 0.00f, int MAXdamage = 0, string MAXdamage_playerName = null, RankingData[] ranking = null)
    {
        Instance = this;

        Time = time;

        MAXDamage = MAXdamage;
        MAXDamage_playerName = MAXdamage_playerName;

        Ranking = ranking;
    }
}
