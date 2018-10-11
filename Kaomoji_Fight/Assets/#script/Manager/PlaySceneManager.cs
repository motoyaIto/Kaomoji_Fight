using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using XboxCtrlrInput;
using TMPro;


public class PlaySceneManager : MonoBehaviour
{
  
    [SerializeField]
    private GameObject UICanvases;      //UI用キャンバス

    [SerializeField]
    private Color P1_nameColor = new Color(0.000f, 0.000f, 0.000f);
    [SerializeField]
    private Color P2_nameColor = new Color(0.000f, 0.000f, 0.000f);
    [SerializeField]
    private Color P3_nameColor = new Color(0.000f, 0.000f, 0.000f);
    [SerializeField]
    private Color P4_nameColor = new Color(0.000f, 0.000f, 0.000f);

    private GameObject[] player_textuer;    //各プレイヤーの画像

    private GameObject[] players;       //プレイヤー
    private GameObject[] HPgage;        //HPゲージ

    // Use this for initialization
    void Start()
    {
        //プレイヤー分の配列を確保
        players = new GameObject[PlayersData.Instance.playerNum];
        HPgage = new GameObject[PlayersData.Instance.playerNum];

        //プレイヤーとHPを生成
        for (int i = 0; i < PlayersData.Instance.playerNum; i++)
        {
            //画像が送られてきていなかったら
            if (player_textuer == null)
            {
                players[i] = (GameObject)Resources.Load("prefab/Player");
                HPgage[i] = (GameObject)Resources.Load("prefab/UI/HPgage");
            }
            else
            {
                players[i] = player_textuer[i];
                HPgage[i] = player_textuer[i];
            }

            //プレイヤーとHPバーを生成
            this.CreatePlayer(players[i],HPgage[i], i);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    /// <summary>
    /// プレイヤーを生成
    /// </summary>
    /// <param name="player">プレイヤーオブジェクトデータ</param>
    /// <param name="i">何番目のプレイヤーか</param>
    private void CreatePlayer(GameObject player, GameObject HPgage, int i)
    {
        //ステージごとにリスポンする位置を調整する必要性あり
        switch (i)
        {
            case 0:
                GameObject P1 = Instantiate(player, new Vector3(2.5f, 50.0f, 0.0f), Quaternion.identity);

                this.SetPlayerStatus(P1, XboxController.First, "P1");
                this.CreateHPgage(HPgage,P1.name, i);
                break;

            case 1:
                GameObject P2 = Instantiate(player, new Vector3(15.5f, 50.0f, 0.0f), Quaternion.identity);

                this.SetPlayerStatus(P2, XboxController.Second, "P2");
                this.CreateHPgage(HPgage, P2.name, i);
                break;

            case 2:
                GameObject P3 = Instantiate(player, new Vector3(15.5f, 50.0f, 0.0f), Quaternion.identity);

                this.SetPlayerStatus(P3, XboxController.Third, "P3");
                this.CreateHPgage(HPgage, P3.name, i);
                break;

            case 3:
                GameObject P4 = Instantiate(player, new Vector3(2.5f, 50.0f, 0.0f), Quaternion.identity);

                this.SetPlayerStatus(P4, XboxController.Fourth, "P4");
                this.CreateHPgage(HPgage, P4.name, i);
                break;
        }
    } 
    /// <summary>
    /// プレイヤーのステータスを設定する
    /// </summary>
    /// <param name="player">プレイヤー</param>
    /// <param name="controllerNamber">コントローラー番号</param>
    private void SetPlayerStatus(GameObject player, XboxController controllerNamber, string name)
    {
        //名前
        player.name = name;

        //コントローラーをセット
        Player playerScript = player.GetComponent<Player>();
        playerScript.GetControllerNamber = controllerNamber;

        //カメラのターゲット用ダミーを取得する
        name += "_dummy";

        foreach(Transform child in this.transform)
        {
            if(child.name == name)
            {
                child.transform.parent = null;

                child.transform.parent = player.gameObject.transform;

                child.transform.position = player.transform.position;
                break;
            }
        }
    }

    /// <summary>
    /// HPゲージの生成
    /// </summary>
    /// <param name="HPgage">HPゲージ</param>
    /// <param name="i">何番目か</param>
    private void CreateHPgage(GameObject HPgage, string name, int i)
    {
        RectTransform size = HPgage.GetComponent<RectTransform>();

        switch (i)
        {
            case 0://元のトランス(281.5, 124)
                GameObject P1_HPgage = Instantiate(HPgage, new Vector3(size.sizeDelta.x / 2, Screen.height - 10, 0f), Quaternion.identity, UICanvases.transform);

                P1_HPgage.name = "P1_HPgage";

                //名前の設定
                TextMeshProUGUI P1name = P1_HPgage.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                P1name.text = name;
                P1name.color = P1_nameColor;

                break;

            case 1:
                GameObject P2_HPgage = Instantiate(HPgage, new Vector3(Screen.width - size.sizeDelta.x / 2, Screen.height - 10, 0), Quaternion.identity, UICanvases.transform);

                P2_HPgage.name = "P2_HPgage";

                //名前の設定
                TextMeshProUGUI P2name = P2_HPgage.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                P2name.text = name;
                P2name.color = P2_nameColor;
                break;

            case 2:
                GameObject P3_HPgage = Instantiate(HPgage, new Vector3(size.sizeDelta.x / 2, 10, 0), Quaternion.identity, UICanvases.transform);

                P3_HPgage.name = "P3_HPgage";

                //名前の設定
                TextMeshProUGUI P3name = P3_HPgage.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                P3name.text = name;
                P3name.color = P3_nameColor;
                break;

            case 3:
                GameObject P4_HPgage = Instantiate(HPgage, new Vector3(Screen.width - size.sizeDelta.x / 2, 10, 0), Quaternion.identity, UICanvases.transform);

                P4_HPgage.name = "P4_HPgage";

                //名前の設定
                TextMeshProUGUI P4name = P4_HPgage.transform.Find("Text").GetComponent<TextMeshProUGUI>();
                P4name.text = name;
                P4name.color = P4_nameColor;
                break;
        }
    }
    

    /// <summary>
    /// 各プレイヤーの画像
    /// </summary>
    public GameObject[] Player_textuer
    {
        set
        {
            player_textuer = new GameObject[value.Length];
            player_textuer = value;
        }
    }
}
