using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;


public class PlaySceneManager : MonoBehaviour
{
    static readonly int PLAYERMAX = 4;
    [SerializeField]
    private GameObject UICanvases;      //UI用キャンバス

    private int player_nam = 1;          //生成するプレイヤーの数
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

        //配列の値に調整
        player_nam -= 1;

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

            CreateHPgage(HPgage[i], i);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void CreatePlayer(GameObject Player, int i)
    {
<<<<<<< HEAD
        switch(i)
        {
            case 0:
                Instantiate(Player, new Vector3(-0.2f, 0.0f, 0.0f), Quaternion.identity, UICanvases.transform);
=======
        //Wepon Aplayer_wepon = Attack_player.GetComponent<PlayerController>().gameObject.GetComponent<Wepon>();
        Player Dplayer = Damage_player.GetComponent<Player>();
>>>>>>> master

                break;

            case 1:
                Instantiate(Player, new Vector3(-0.2f, 0.0f, 0.0f), Quaternion.identity, UICanvases.transform);

                break;

            case 2:
                Instantiate(Player, new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity, UICanvases.transform);
                break;

            case 3:
                Instantiate(Player, new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity, UICanvases.transform);

                break;
        }
    }
    /// <summary>
    /// HPゲージの生成
    /// </summary>
    /// <param name="HPgage">HPゲージ</param>
    /// <param name="i">何番目か</param>
    public void CreateHPgage(GameObject HPgage, int i)
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
