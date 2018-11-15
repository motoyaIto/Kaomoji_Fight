using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using XboxCtrlrInput;
using TMPro;
using Cinemachine;
using System.Linq;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour {

    // UIキャンバス
    private GameObject canvas;

    // プレイヤーデータ
    private List<ResultData> players = new List<ResultData>();

    // プレイタイム
    private float time;
    private int min;
    private int second;

    // 鳴らすかもしれない音
    private AudioSource As;
    private AudioClip se;
        

    private void Awake()
    {
        // 音を鳴らす準備
        As = this.GetComponent<AudioSource>();
    }

    void Start () {
        // UIオブジェクトを設定
        canvas = GameObject.Find("ResultUI").transform.gameObject;

        // 遊んだ時間の取得
        time = ResultData.Instance.Time;

        // リザルトの表示
        ResultRender();
    }
	
	void Update () {
        // タイトルシーンへ戻る
        if(XCI.GetButton(XboxButton.B, XboxController.First))
        {
            //SceneManager.LoadScene("Title");
        }
	}

    private void ResultRender()
    {
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            // 順位表示

        }
    }

    private void DataRender()
    {
        // プレイ時間の計測
        if (time <= 60.0f)
        {
            min = (int)(time / 60.0f);
            second = (int)(time - 60 * min);
        }
        else
        {
            min = 0;
            second = (int)time;
        }

        // 時間表示
        TextMeshProUGUI timer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        timer.text = min + ":" + second;

        // ダメージを一番与えたプレイヤーの表示
        TextMeshProUGUI player = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        //player.text=
    }
}
