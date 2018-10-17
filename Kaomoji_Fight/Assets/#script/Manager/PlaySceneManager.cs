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

    [SerializeField, Header("プレイヤーの復帰時の場所指定")]
    private Vector3 RevivalPos = new Vector3(0f, 30f, 0f);


    private GameObject[] player_textuer;    //各プレイヤーの画像

    private GameObject[] players;       //プレイヤー
    private GameObject[] HPgage;        //HPゲージ

    private Player playerCS;

    private Slider[] hpgage_slider;

    private int death_player;

    // Use this for initialization
    void Start()
    {
        playerCS = GetComponent<Player>();
        death_player = -1;


        //プレイヤー分の配列を確保
        players = new GameObject[PlayData.Instance.playerNum];
        HPgage = new GameObject[PlayData.Instance.playerNum];

        //プレイヤーとHPを生成
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
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
            this.CreatePlayer(players[i], HPgage[i], i);

            // HPを管理する
            HPgage[i].GetComponent<Slider>().maxValue = players[i].transform.gameObject.GetComponent<Player>().HP;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーが死んだ
        if (death_player >= 0)
        {
            // 死んだプレイヤーの生成
            players[death_player] = (GameObject)Resources.Load("prefab/Player");
            GameObject P = Instantiate(players[death_player], new Vector3(2.5f, 50.0f, 0.0f), Quaternion.identity);

            switch (death_player)
            {
                case 0:
                    this.SetPlayerStatus(P, XboxController.First, "P1", PlayData.Instance.PlayersFace[death_player]);
                    break;
                case 1:
                    this.SetPlayerStatus(P, XboxController.Second, "P2", PlayData.Instance.PlayersFace[death_player]);
                    break;
                case 2:
                    this.SetPlayerStatus(P, XboxController.Third, "P3", PlayData.Instance.PlayersFace[death_player]);
                    break;
                case 3:
                    this.SetPlayerStatus(P, XboxController.Fourth, "P4", PlayData.Instance.PlayersFace[death_player]);
                    break;
                default:
                    break;
            }
            //hpgage_slider[death_player].value = players[death_player].transform.gameObject.GetComponent<Player>().Damage(players[death_player].transform.gameObject.GetComponent<Player>().HP / 10f);
            death_player = -1;
        }
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

                this.SetPlayerStatus(P1, XboxController.First, "P1", PlayData.Instance.PlayersFace[0]);
                this.CreateHPgage(HPgage,P1.name, i);
                break;

            case 1:
                GameObject P2 = Instantiate(player, new Vector3(15.5f, 50.0f, 0.0f), Quaternion.identity);

                this.SetPlayerStatus(P2, XboxController.Second, "P2", PlayData.Instance.PlayersFace[1]);
                this.CreateHPgage(HPgage, P2.name, i);
                break;

            case 2:
                GameObject P3 = Instantiate(player, new Vector3(15.5f, 50.0f, 0.0f), Quaternion.identity);

                this.SetPlayerStatus(P3, XboxController.Third, "P3", PlayData.Instance.PlayersFace[2]);
                this.CreateHPgage(HPgage, P3.name, i);
                break;

            case 3:
                GameObject P4 = Instantiate(player, new Vector3(2.5f, 50.0f, 0.0f), Quaternion.identity);

                this.SetPlayerStatus(P4, XboxController.Fourth, "P4", PlayData.Instance.PlayersFace[3]);
                this.CreateHPgage(HPgage, P4.name, i);
                break;
        }
    } 
    /// <summary>
    /// プレイヤーのステータスを設定する
    /// </summary>
    /// <param name="player">プレイヤー</param>
    /// <param name="controllerNamber">コントローラー番号</param>
    private void SetPlayerStatus(GameObject player, XboxController controllerNamber, string name, Sprite FaceTextures)
    {
        //キャラの顔をセット
        SpriteRenderer playerFace = player.GetComponent<SpriteRenderer>();
        playerFace.sprite = FaceTextures;

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

    public int destroy_p
    {
        set
        {
            death_player = value;
        }
    }

}
