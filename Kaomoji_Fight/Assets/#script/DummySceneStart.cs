using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class DummySceneStart : MonoBehaviour {

    private PlayData playedata;//プレイデータ

    [SerializeField, Header("選択したステージ名(.txt記述しなくて良い)（デバッグ用）")]
    private string Stage_name = "stage1";

    [SerializeField, Header("プレイヤーの名前(デバッグ用)")]
    PlayerData[] Players;
    //[SerializeField, Header("プレイヤーの名前(デバッグ用)")]
    //private string[] players_name = { "P1", "P2", "P3", "P4"};
    //[SerializeField, Header("プレイヤーの顔（デバッグ用）")]
    //Sprite[] playersface;
    //[SerializeField, Header("プレイヤーの名前の色（デバッグ用）")]
    //Color[] namesColor;
    //[SerializeField, Header("プレイヤーの初期リスポン位置（デバッグ用）")]
    //Vector3[] PlayersinitialPos;
    private void Awake()
    {
        playedata = new PlayData(Stage_name, Players);
        //playedata = new PlayData(Stage_name, players_name, playersface, namesColor, PlayersinitialPos);
    }

    // Use this for initialization
    void Start () {
        SceneManagerController.LoadScene();
		
	}
	
}
