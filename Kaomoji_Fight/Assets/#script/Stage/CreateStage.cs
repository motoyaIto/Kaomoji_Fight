using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class CreateStage : MonoBehaviour
{
    //武器になる文字群
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

        "が", "ぎ", "ぐ", "げ", "ご",
        "ざ", "じ", "ず", "ぜ", "ぞ",
        "だ", "ぢ", "づ", "で", "ど",
        "ば", "び", "ぶ", "べ", "ぼ",

        "ぱ", "ぴ", "ぷ", "ぺ", "ぽ",

        "ぁ", "ぃ", "ぅ", "ぇ", "ぉ",
        "ゃ",  　　"ゅ",  　　　"ょ",
        "っ"

    };

    private GameObject StageBlock;//ステージ


    void Start()
    {
        //ブロックの配列
        GameObject[] StageBlocks = null;
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
        StageBlocks = new GameObject[textnam];
        //文字を表示するボックスをResourcesから読み込む
        StageBlock = (GameObject)Resources.Load("prefab/Stage/StageBlock");
        



        //一文字ずつ設定する
        for (int i = 0; i < textnam; i++)
        {
            string mozi = text.Substring(i, 1);
            StageBlocks[i] = StageBlock;
           

            CreateStageBlock(mozi, StageBlocks[i], ref xCount, ref yCount);
        }
    }

    private void CreateStageBlock(string mozi, GameObject StageBlock, ref int x , ref int y)
    {
        //改行が入っていないとき
        if (mozi != "\r" && mozi != "\n")
        {
            //スペースが入っていないとき
            if (mozi != " " && mozi != "　")
            {
                //新しく作るオブジェクトの座標
                Vector3 pos = new Vector3(
                       this.transform.position.x + StageBlock.transform.localScale.x / 2 + StageBlock.transform.localScale.x * x,
                       this.transform.position.y + StageBlock.transform.localScale.y / 2 + StageBlock.transform.localScale.y * y,
                        0.0f);

                //オブジェクトを生成する
                StageBlock = Instantiate(StageBlock, pos, Quaternion.identity, this.transform);
                //ボックスの下のテキストを取得する
                GameObject textdata = StageBlock.transform.Find("Text").gameObject;
                //テキストに文字を書き込む
                textdata.GetComponent<TextMesh>().text = mozi;
                StageBlock.name = "StageBlock" + "(" + mozi + ")";
                // RectTransformを追加
                StageBlock.AddComponent<RectTransform>();
                 //weaponだったら
                if (Array.IndexOf(WEAPO_MOZI, mozi) >= 0)
                {
                    //武器の文字用マテリアルに変更
                    Material StageBlock_WeaponMateral = (Material)Resources.Load("Material/StageBlock_Weapon");
                   StageBlock.GetComponent<Renderer>().material = StageBlock_WeaponMateral;
                    

                    //武器フラグを立てる
                    BlockController Block_cs = StageBlock.transform.GetComponent<BlockController>();
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
