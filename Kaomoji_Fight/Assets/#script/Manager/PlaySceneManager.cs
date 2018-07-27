using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour {

    [SerializeField]
    private Slider slider1;

    [SerializeField]
    private Slider slider2;

    [SerializeField]
    private GameObject[] players;
  
    // Use this for initialization
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Slider HpVer = new Slider[players.GetLength()];
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
}
