using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeData
{
    private static readonly int PLAYERMAX = 4;//プレイヤーの最大人数

    public int playerNum = 1;


    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="players">プレイヤーの人数</param>
    public PlayeData(int players = 1)
    {
        Instance = this;
        playerNum = players;

        //プレイヤーが最大値を超えて生成しようとしたときに抑える
        if (playerNum > PLAYERMAX)
        {
            playerNum = PLAYERMAX;
        }
    }

    public static PlayeData Instance
    {
        get;
        private set;
    }

   
    
}
