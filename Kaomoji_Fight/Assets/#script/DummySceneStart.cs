using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class DummySceneStart : MonoBehaviour {

    private PlayData playedata;//プレイデータ

    [SerializeField, Header("選択したステージ名(.txt記述しなくて良い)（デバッグ用）")]
    private string Stage_name = "stage1";

    [SerializeField, Header("プレイヤーの名前(デバッグ用)")]
    private string[] players_name = { "P1", "P2", "P3", "P4"};
    [SerializeField, Header("プレイヤーの顔（デバッグ用）")]
    Sprite[] playersface;
   
    private void Awake()
    {
        string DammySelectStage = Stage_name;

        playedata = new PlayData(Stage_name, players_name, playersface);
    }

    // Use this for initialization
    void Start () {
        SceneManagerController.LoadScene();
		
	}
	
}
