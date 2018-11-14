using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultData
{
    PlayerData[] PlaersData;    //プレイヤーデータ
    private float time;          //時間

    public static ResultData Instance
    {
        get;
        private set;
    }
    public ResultData(PlayerData[] BattlePlayers)
    {
        Instance = this;

        PlaersData = BattlePlayers;

        
    }

    public float PlayingTime
    {
        set
        {
            time = value;
        }
        get
        {
            return time;
        }
    }

}
