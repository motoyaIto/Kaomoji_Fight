using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultData
{
    PlayerData[] PlaersData;    //プレイヤーデータ


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

}
