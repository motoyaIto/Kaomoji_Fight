using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class PlayData
{
    private static readonly int PLAYERMAX = 4;//プレイヤーの最大人数

    //public SelectPNControll selectPNControll;
    
    public int playerNum = 1;//プレイヤーの合計人数
    public string StageText = System.IO.File.ReadAllText("Assets/Resources/Texts/test.txt", Encoding.GetEncoding("Shift_JIS"));//ステージデータ

    public string[] PlayersName = null; //プレイヤーの名前
    public Sprite[] PlayersFace = null;//プレイヤーの顔文字
  

   /// <summary>
   ///  コンストラクタ
   /// </summary>
   /// <param name="SelectStageText">ステージの名前</param>
   /// <param name="SelectPlayersName">プレイヤーの名前</param>
   /// <param name="SelectPlayersFace">プレイヤーの顔文字</param>
    public PlayData(string SelectStageText = null, string[] SelectPlayersName = null, Sprite[] SelectPlayersFace = null)
    {
        Instance = this;

        //プレイヤーの顔のデータを元にプレイヤーの数を確定する
        if (SelectPlayersFace != null)
        {
            playerNum = SelectPlayersFace.Length;
        }

        //ステージデータを取得する
        if (SelectStageText != null)
        {
            StageText = System.IO.File.ReadAllText("Assets/Resources/Texts/" + SelectStageText + ".txt", Encoding.GetEncoding("Shift_JIS"));
        }

        //プレイヤーの名前と顔文字
        PlayersName = SelectPlayersName;
        PlayersFace = SelectPlayersFace;


        //プレイヤーが最大値を超えて生成しようとしたときに抑える
        if (playerNum > PLAYERMAX)
        {
            playerNum = PLAYERMAX;
        }

       //Playerのデータが設定されているかチェックする
        for (int i = 0; i < playerNum; i++)
        {
            //名前が設定されていなかったとき
            if(PlayersName[i] == null)
            {
                PlayersName[i] = "P" + i.ToString();
            }
            //顔が設定されていないとき
            if (PlayersFace[i] == null)
            {
                PlayersFace[i] = Sprite.Create((Texture2D)Resources.Load("textures/use/Player1"), new Rect(0, 0, 584, 211), new Vector2(0.5f, 0.5f));
            }

            
        }
    }

    public static PlayData Instance
    {
        get;
        private set;
    }

   
    
}
