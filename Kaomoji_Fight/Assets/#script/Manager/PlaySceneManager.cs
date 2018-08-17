using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;


public class PlaySceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject UICanvases;//UI用キャンバス

    private GameObject[] players;   //プレイヤー
    private GameObject[] HPgage;    //HPゲージ


    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");//プレイヤーの数を取得

        //HPゲージを生成
        HPgage = new GameObject[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            HPgage[i] = (GameObject)Resources.Load("prefab/UI/HPgage");

            CreateHPgage(HPgage[i], i);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
     
    }

  
    public void Damage(GameObject Attack_player, GameObject Damage_player)
    {
        //Wepon Aplayer_wepon = Attack_player.GetComponent<PlayerController>().gameObject.GetComponent<Wepon>();
        PlayerController Dplayer = Damage_player.GetComponent<PlayerController>();

        //ダメージを数値に反映
        //Damage_player.HP -= Aplayer_wepon.attack;

        //HPゲージを減少させる



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
}
