using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using XboxCtrlrInput;


public class PlaySceneManager : MonoBehaviour
{
    static readonly int PLAYERMAX = 4;

    [SerializeField]
    private GameObject UICanvases;      //UI用キャンバス

    private GameObject[] player_textuer;    //各プレイヤーの画像

    private GameObject[] players;       //プレイヤー
    private GameObject[] HPgage;        //HPゲージ

   

    // Use this for initialization
    void Start()
    {
        //プレイヤーが最大値を超えて生成しようとしたときに抑える
        if (SelectPNControll.playerNum > PLAYERMAX)
        {
            SelectPNControll.playerNum = PLAYERMAX;
        }

        //プレイヤー分の配列を確保
        players = new GameObject[SelectPNControll.playerNum];
        HPgage = new GameObject[SelectPNControll.playerNum];

        //プレイヤーとHPを生成
        for (int i = 0; i < SelectPNControll.playerNum; i++)
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
            CreatePlayer(players[i], i);
            CreateHPgage(HPgage[i], i);
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
    private void CreatePlayer(GameObject player, int i)
    {
        
        switch(i)
        {
            case 0:
                GameObject P1 = Instantiate(player, new Vector3(-0.2f, 0.0f, 0.0f), Quaternion.identity);

                this.SetPlayerStatus(P1, XboxController.First, "P1");
               
                break;

            case 1:
                GameObject P2 = Instantiate(player, new Vector3(-0.2f, 0.0f, 0.0f), Quaternion.identity);

                this.SetPlayerStatus(P2, XboxController.Second, "P2");
                break;

            case 2:
                GameObject P3 = Instantiate(player, new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity);

                this.SetPlayerStatus(P3, XboxController.Third, "P3");
                break;

            case 3:
                GameObject P4 = Instantiate(player, new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity);

                this.SetPlayerStatus(P4, XboxController.Fourth, "P4");
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

                break;
            }
        }
    }

    /// <summary>
    /// HPゲージの生成
    /// </summary>
    /// <param name="HPgage">HPゲージ</param>
    /// <param name="i">何番目か</param>
    private void CreateHPgage(GameObject HPgage, int i)
    {
        RectTransform size = HPgage.GetComponent<RectTransform>();

        switch (i)
        {
            case 0://元のトランス(281.5, 124)
                GameObject P1_HPgage = Instantiate(HPgage, new Vector3(size.sizeDelta.x / 2, Screen.height - 10, 0f), Quaternion.identity, UICanvases.transform);

                P1_HPgage.name = "P1_HPgage";
                break;

            case 1:
                GameObject P2_HPgage = Instantiate(HPgage, new Vector3(Screen.width - size.sizeDelta.x / 2, Screen.height - 10, 0), Quaternion.identity, UICanvases.transform);

                P2_HPgage.name = "P2_HPgage";
                break;

            case 2:
                GameObject P3_HPgage = Instantiate(HPgage, new Vector3(size.sizeDelta.x / 2, 10, 0), Quaternion.identity, UICanvases.transform);

                P3_HPgage.name = "P3_HPgage";
                break;

            case 3:
                GameObject P4_HPgage = Instantiate(HPgage, new Vector3(Screen.width - size.sizeDelta.x / 2, 10, 0), Quaternion.identity, UICanvases.transform);

                P4_HPgage.name = "P4_HPgage";
                break;
        }
    }
    

    //各プレイヤーの画像
    public GameObject[] Player_textuer
    {
        set
        {
            player_textuer = new GameObject[value.Length];
            player_textuer = value;
        }
    }
}
