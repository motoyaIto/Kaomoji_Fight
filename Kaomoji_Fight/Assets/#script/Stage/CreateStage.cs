using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class CreateStage : MonoBehaviour
{
    private string[] WEAPO_MOZI =
    {
        "あ", "い", "う", "え", "お",
        "か", "き", "く", "け", "こ",
        "さ", "し", "す", "せ", "そ",
        "た", "ち", "つ", "て", "と",
        "な", "に", "ぬ", "ね", "の",
        "は", "ひ", "ふ", "へ", "ほ",
        "ま", "み", "む", "め", "も",
        "や",       "ゆ",       "よ",
        "ら", "り", "る", "れ", "ろ",
        "わ", "を",             "ん",
    };

    GameObject StageBlock_weapon;
    GameObject StageBlock_not;
    void Start()
    {
        //ブロックの配列
        GameObject[] StageBloc = null;
        //テキストの文字数
        int textnam = 0;

        //x座標の移動
        int xCount = 0;
        //y座標の移動
        int yCount = 0;


        //テキスト一覧の取得
        string text = PlayData.Instance.StageText;

        textnam = text.Length;

        //文字数分の配列
        StageBloc = new GameObject[textnam];
        //文字を表示するボックスをResourcesから読み込む
        StageBlock_weapon = (GameObject)Resources.Load("prefab/Stage/StageBlock_Weapon");
        StageBlock_not = (GameObject)Resources.Load("prefab/Stage/StageBlock_not");



        //一文字ずつ設定する
        for (int i = 0; i < textnam; i++)
        {
            string mozi = text.Substring(i, 1);
            //weaponだったらフラグを立てる
            if (Array.IndexOf(WEAPO_MOZI, mozi) >= 0)
            {
                StageBloc[i] = StageBlock_weapon;
            }
            else
            {
                StageBloc[i] = StageBlock_not;
            }

            CreateStageBloc(mozi, StageBloc[i], ref xCount, ref yCount);
        }
    }

    private void CreateStageBloc(string mozi, GameObject StageBloc, ref int x , ref int y)
    {
        //改行が入っていないとき
        if (mozi != "\r" && mozi != "\n")
        {
            //スペースが入っていないとき
            if (mozi != " " && mozi != "　")
            {
                //新しく作るオブジェクトの座標
                Vector3 pos = new Vector3(
                       this.transform.position.x + StageBloc.transform.localScale.x / 2 + StageBloc.transform.localScale.x * x,
                       this.transform.position.y + StageBloc.transform.localScale.y / 2 + StageBloc.transform.localScale.y * y,
                        0.0f);

                    //オブジェクトを生成する
                    StageBloc = Instantiate(StageBloc, pos, Quaternion.identity, this.transform);
                    //ボックスの下のテキストを取得する
                    GameObject textdata = StageBloc.transform.Find("Text").gameObject;
                    //テキストに文字を書き込む
                    textdata.GetComponent<TextMesh>().text = mozi;
                    StageBloc.name = "StageBloc" + "(" + mozi + ")";
                    // RectTransformを追加
                    StageBloc.AddComponent<RectTransform>();
                 //weaponだったらフラグを立てる
                if (Array.IndexOf(WEAPO_MOZI, mozi) >= 0)
                {
                    BlockController Block_cs = StageBloc.transform.GetComponent<BlockController>();
                    Block_cs.Weapon = true;
                }
                
            }
            //右に一文字ずらす
            x++;
        }
        else
        {
            //一行下にずらす
            y--;
            //文字位置をスタートに戻す
            x = 0;
        }
    }
}
