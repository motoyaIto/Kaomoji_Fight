using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneManager : MonoBehaviour {

    [SerializeField]
    private SliderJoint2D slider1;
    [SerializeField]
    private SliderJoint2D slider2;

    [SerializeField]
    private GameObject[] players;
   // private PlayerController[] pController;
	// Use this for initialization
	void Start () {
        foreach(var Player in players)
        {
            //pController += Player.GetComponent<PlayerController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Damage(GameObject Attack_player, GameObject Damage_player)
    {
        PlayerController Aplayer = Attack_player.GetComponent<PlayerController>();
        Playercontroller Dplayer = Damage_player.GetComponent<PlayerController>();

        //ダメージを数値に反映

        //HPゲージを減少させる



    }
}
