using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class PlayData
{
    private static readonly int PLAYERMAX = 4;//プレイヤーの最大人数

    public int playerNum = 1;
    public string StageText = System.IO.File.ReadAllText("Assets/Resources/Texts/test.txt", Encoding.GetEncoding("Shift_JIS"));
    public Sprite[] PlayersFace ;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="players">プレイヤーの人数</param>
    public PlayData(int players = 1, Sprite[] SelectPlayersFace = null, string SelectStageText = null)
    {
        Instance = this;
        playerNum = players;
        PlayersFace = SelectPlayersFace;
        StageText = SelectStageText;

        //プレイヤーが最大値を超えて生成しようとしたときに抑える
        if (playerNum > PLAYERMAX)
        {
            playerNum = PLAYERMAX;
        }

        //プレイヤーの数より多く登録されている場合
        if (playerNum != PlayersFace.Length)
        {
            playerNum = PlayersFace.Length;
        }

       //顔がすべて設定されているかチェックする
        for (int i = 0; i < playerNum; i++)
        {
            //顔が設定されていないとき
            if (PlayersFace[i] == null)
            {
                PlayersFace[i] = Sprite.Create((Texture2D)Resources.Load("textures/use/Player1"), new Rect(0, 0, 584, 385), new Vector2(0.5f, 0.5f));
            }
        }
    }

    public static PlayData Instance
    {
        get;
        private set;
    }

   
    
}
