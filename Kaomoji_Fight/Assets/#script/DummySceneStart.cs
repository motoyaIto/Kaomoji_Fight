using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySceneStart : MonoBehaviour {

    [SerializeField, Header("プレイ人数（デバッグ用）")]
    int Player_Num = 2;
    PlayeData playerdata;
    private void Awake()
    {
        playerdata = new PlayeData(Player_Num);
    }

    // Use this for initialization
    void Start () {
        SceneManagerController.LoadScene();
		
	}
	
}
