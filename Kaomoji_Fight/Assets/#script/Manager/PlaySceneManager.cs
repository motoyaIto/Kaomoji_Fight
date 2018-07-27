using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour
{
    private GameObject[] players;   //プレイヤー
    private GameObject[] HPgage;    //HPゲージ

    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");//プレイヤーの数を取得


        HPgage = new GameObject[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            HPgage[i] = (GameObject)Resources.Load("prefab//UI//HPgage");

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


    public void CreateHPgage(GameObject HPgage, int i)
    {
        switch (i)
        {
            case 0:
                Instantiate(HPgage, new Vector3(-5, -5, 0), Quaternion.identity);
                break;

            case 1:
                Instantiate(HPgage, new Vector3(5, -5, 0), Quaternion.identity);
                break;

            case 2:
                Instantiate(HPgage, new Vector3(-5, 5, 0), Quaternion.identity);
                break;

            case 3:
                Instantiate(HPgage, new Vector3(5, 5, 0), Quaternion.identity);
                break;


        }
    }
}
