using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class PlayData
{
    private static readonly int PLAYERMAX = 4;//プレイヤーの最大人数
    
    public int playerNum = 1;//プレイヤーの合計人数
    public string StageText = System.IO.File.ReadAllText("Assets/Resources/Texts/test.txt", Encoding.GetEncoding("Shift_JIS"));//ステージデータ
    public PlayerData[] PlayersData = null;


    public static PlayData Instance
    {
        get;
        private set;
    }

    public PlayData(string SelectStageText = null, PlayerData[] SelectPlayersData = null)
    {
        Instance = this;

        //プレイヤーの顔のデータを元にプレイヤーの数を確定する
        if (SelectPlayersData != null)
        {
            playerNum = SelectPlayersData.Length;
        }

        //ステージデータを取得する
        if (SelectStageText != null)
        {
            StageText = System.IO.File.ReadAllText("Assets/Resources/Texts/" + SelectStageText + ".txt", Encoding.GetEncoding("Shift_JIS"));
        }

        //キャラクターデータを取得
        PlayersData = SelectPlayersData;
    }
}
