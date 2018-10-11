using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class CreateStage : MonoBehaviour
{     
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
        string text = PlayeData.Instance.StageText;

        textnam = text.Length;

        //文字数分の配列
        StageBloc = new GameObject[textnam];
        //文字を表示するボックスをResourcesから読み込む
        StageBloc[0] = (GameObject)Resources.Load("prefab/Stage/StageBloc");

        

        //一文字ずつ設定する
        for (int i = 0; i < textnam; i++)
        {
            string mozi = text.Substring(i, 1);

            //0番目でなければ、0番目をコピーして作る
            if (i > 0)
            {
                StageBloc[i] = StageBloc[0];
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
