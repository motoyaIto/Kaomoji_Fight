using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersData
{
    private static readonly int PLAYERMAX = 4;//プレイヤーの最大人数

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="players">プレイヤーの人数</param>
    public PlayersData(int players = 1)
    {
        Instance = this;
        playerNum = players;

        //プレイヤーが最大値を超えて生成しようとしたときに抑える
        if (playerNum > PLAYERMAX)
        {
            playerNum = PLAYERMAX;
        }
    }

    public static PlayersData Instance
    {
        get;
        private set;
    }

    public int playerNum = 1;
    
}
