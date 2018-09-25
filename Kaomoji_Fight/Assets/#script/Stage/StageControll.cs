using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

public class StageControll : MonoBehaviour {

    void Start()
    {    
        //テキスト一覧の取得
        string text = System.IO.File.ReadAllText(@"Assets\Resources\Texts\test.txt", Encoding.GetEncoding("Shift_JIS"));
        //文字数分の配列
        GameObject[] StageBloc = new GameObject[text.Length];
        //文字を表示するボックスをResourcesより読み込む
        StageBloc[0] = (GameObject)Resources.Load("prefab/Stage/StageBloc");

        //一文字ずつ設定する
        for (int i = 0; i < text.Length; i++)
        {
            if(i > 0)
            {
                StageBloc[i] = StageBloc[0];
            }

            //オブジェクトを生成する
            Instantiate(StageBloc[i], new Vector3(StageBloc[i].transform.localScale.x * i, StageBloc[i].transform.localScale.y, 0f), Quaternion.identity, this.transform);
            //ボックスの下のテキストを取得する
            GameObject textdata = StageBloc[i].transform.Find("Text").gameObject;
            //テキストに文字を書き込む
            textdata.GetComponent<TextMesh>().text = text.Substring(i, 1);
        }
    }

    void Update()
    {
        
    }
}
