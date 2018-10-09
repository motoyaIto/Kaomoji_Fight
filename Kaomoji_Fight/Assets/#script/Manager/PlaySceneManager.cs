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

    private int player_nam = 2;          //生成するプレイヤーの数
    private GameObject[] player_textuer; //各プレイヤーの画像

    private GameObject[] players;       //プレイヤー
    private GameObject[] HPgage;        //HPゲージ

   

    // Use this for initialization
    void Start()
    {
        //プレイヤーが最大値を超えて生成しようとしたときに抑える
        if (player_nam > PLAYERMAX)
        {
            player_nam = PLAYERMAX;
        }

        //プレイヤー分の配列を確保
        players = new GameObject[player_nam];
        HPgage = new GameObject[player_nam];

        //プレイヤーとHPを生成
        for (int i = 0; i < player_nam; i++)
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

            CreatePlayer(players[i], i);
            CreateHPgage(HPgage[i], i);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void CreatePlayer(GameObject player, int i)
    {
        
        switch(i)
        {
            case 0:
                GameObject FirstPlayer = Instantiate(player, new Vector3(-0.2f, 0.0f, 0.0f), Quaternion.identity);

                FirstPlayer.name = "First";
                this.SetPlayerStatus(FirstPlayer, XboxController.First);
               
                break;

            case 1:
                GameObject SecondPlayer = Instantiate(player, new Vector3(-0.2f, 0.0f, 0.0f), Quaternion.identity);

                SecondPlayer.name = "Second";
                this.SetPlayerStatus(SecondPlayer, XboxController.Second);
                break;

            case 2:
                GameObject ThirdPlayer = Instantiate(player, new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity);

                ThirdPlayer.name = "Third";
                this.SetPlayerStatus(ThirdPlayer, XboxController.Third);
                break;

            case 3:
                GameObject FourthPlayer = Instantiate(player, new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity);

                FourthPlayer.name = "Fourth";
                this.SetPlayerStatus(FourthPlayer, XboxController.Fourth);
                break;
        }
    }

    /// <summary>
    /// プレイヤーのステータスを設定する
    /// </summary>
    /// <param name="player">プレイヤー</param>
    /// <param name="controllerNamber">コントローラー番号</param>
    private void SetPlayerStatus(GameObject player, XboxController controllerNamber)
    {
        Player playerScript = player.GetComponent<Player>();
        playerScript.GetControllerNamber = controllerNamber;
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
                Instantiate(HPgage, new Vector3(size.sizeDelta.x / 2, Screen.height - 10, 0f), Quaternion.identity, UICanvases.transform);
                break;

            case 1:
                Instantiate(HPgage, new Vector3(Screen.width - size.sizeDelta.x / 2, Screen.height - 10, 0), Quaternion.identity, UICanvases.transform);
                break;

            case 2:
                Instantiate(HPgage, new Vector3(size.sizeDelta.x / 2, 10, 0), Quaternion.identity, UICanvases.transform);
                break;

            case 3:
                Instantiate(HPgage, new Vector3(Screen.width - size.sizeDelta.x / 2, 10, 0), Quaternion.identity, UICanvases.transform);
                break;
        }
    }
    
    //プレイヤーの合計
    public int Playernam
    {
        set
        {
            player_nam = value;
        }
        get
        {
            return player_nam;
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
