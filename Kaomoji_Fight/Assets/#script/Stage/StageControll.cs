using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

public class StageControll : MonoBehaviour
{
    //文字ブロック管理
    private struct Bloc
    {
        public GameObject StageBloc;
        public int countTime;
    }

    //ブロックの配列
    private Bloc[] StageBloc;
    //テキストの文字数
    private int textnam;

    //x座標の移動
    private int xCount = 0;
    //y座標の移動
    private int yCount = 0;

    void Start()
    {
        //テキスト一覧の取得
        string text = System.IO.File.ReadAllText(@"Assets\Resources\Texts\test.txt", Encoding.GetEncoding("Shift_JIS"));
        textnam = text.Length;

        //文字数分の配列
        StageBloc = new Bloc[textnam];
        //文字を表示するボックスをResourcesから読み込む
        StageBloc[0].StageBloc = (GameObject)Resources.Load("prefab/Stage/StageBloc");

        //一文字ずつ設定する
        for (int i = 0; i < textnam; i++)
        {
            if (i > 0)
            {
                StageBloc[i] = StageBloc[0];
            }

            if (text.Substring(i, 1) != "\r" && text.Substring(i, 1) != "\n")
            {
                //新しく作るオブジェクトの座標
                Vector3 pos = new Vector3(
                   this.transform.position.x + StageBloc[i].StageBloc.transform.localScale.x / 2 + StageBloc[i].StageBloc.transform.localScale.x * xCount,
                   this.transform.position.y + StageBloc[i].StageBloc.transform.localScale.y / 2 + StageBloc[i].StageBloc.transform.localScale.y * yCount,
                    0.0f);

                //オブジェクトを生成する
                StageBloc[i].StageBloc = Instantiate(StageBloc[i].StageBloc, pos, Quaternion.identity, this.transform);
                //ボックスの下のテキストを取得する
                GameObject textdata = StageBloc[i].StageBloc.transform.Find("Text").gameObject;
                //テキストに文字を書き込む
                textdata.GetComponent<TextMesh>().text = text.Substring(i, 1);
                StageBloc[i].StageBloc.name = "StageBloc" +  "(" + text.Substring(i, 1) + ")";

                //右に一文字ずらす
                xCount++;
            }
            else
            {
                //一行下にずらす
                yCount--;
                //文字位置をスタートに戻す
                xCount = 0;
            }

        }
    }

    void Update()
    {
       
    }
}
