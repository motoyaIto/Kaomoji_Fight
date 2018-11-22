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

public class ResultSceneManager : MonoBehaviour
{
    // UIキャンバス
    private GameObject canvas;

    // プレイタイム
    private float time = .0f;
    private int min = 0;
    private int second = 0;

    // プレイ人数
    private int playerNum = 0;

    // 鳴らすかもしれない音
    private AudioSource As;
    private AudioClip se;

    // シーン遷移フラグ
    private bool changescene_flag = false;

    private void Awake()
    {
        // 音を鳴らす準備
        As = this.GetComponent<AudioSource>();
        As.volume = .1f;
        se = (AudioClip)Resources.Load("Sound/SE/Select/crrect");
    }

    void Start()
    {
        // UIオブジェクトを設定
        canvas = GameObject.Find("ResultUI").transform.gameObject;

        // 遊んだ時間の取得
        time = 180.0f - ResultData.Instance.Time;

        // プレイ人数の取得
        playerNum = ResultData.Instance.Ranking.Length;

        // リザルトの表示
        DataRender();
        ResultRender();
    }

    void Update()
    {
        // タイトルシーンへ戻る
        if ((Input.GetKeyDown(KeyCode.Space) || XCI.GetButton(XboxButton.B, XboxController.First)) && !changescene_flag)
        {
            changescene_flag = true;
            As.PlayOneShot(se);
            SceneManagerController.LoadScene();
            SceneManagerController.ChangeScene();
        }
    }

    private void ResultRender()
    {
        for (int i = 0; i < playerNum; i++)
        {
            //int j = playerNum - (i + 1);

            // 順位表示
            canvas.transform.GetChild(1).transform.GetChild(i).transform.gameObject.SetActive(true);
            TextMeshProUGUI playerName = canvas.transform.GetChild(1).transform.GetChild(i).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            playerName.text = ResultData.Instance.Ranking[i].PlayerName_data;

            // プレイヤーの顔表示
            PlayerSpriteRender(i, i);
        }
    }

    private void DataRender()
    {
        // プレイ時間の計測
        if (time >= 60.0f)
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
        timer.text = min + ":";
        if (second < 10) { timer.text += "0" + second; } else { timer.text += second; }

        // ダメージを一番与えたプレイヤーの表示
        TextMeshProUGUI player = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        player.text = ResultData.Instance.MAXDamage_playerName;

        // ダメージ値の表示
        TextMeshProUGUI damage = GameObject.Find("MaxDamage").GetComponent<TextMeshProUGUI>();
        damage.text = ResultData.Instance.MAXDamage + " ダメージ";
    }

    private void PlayerSpriteRender(int rank, int data)
    {
        GameObject playerFace;
        switch (rank)
        {
            case 0:
                playerFace = GameObject.Find("FirstPlayerFace").transform.gameObject;
                playerFace.GetComponent<Image>().sprite = ResultData.Instance.Ranking[data].PlayerFace_data;
                break;
            case 1:
                playerFace = GameObject.Find("SecondPlayerFace").transform.gameObject;
                playerFace.GetComponent<Image>().sprite = ResultData.Instance.Ranking[data].PlayerFace_data;
                break;
            case 2:
                playerFace = GameObject.Find("ThirdPlayerFace").transform.gameObject;
                playerFace.GetComponent<Image>().sprite = ResultData.Instance.Ranking[data].PlayerFace_data;
                break;
            case 3:
                playerFace = GameObject.Find("ForthPlayerFace").transform.gameObject;
                playerFace.GetComponent<Image>().sprite = ResultData.Instance.Ranking[data].PlayerFace_data;
                break;
            default:
                break;
        }
    }
}
