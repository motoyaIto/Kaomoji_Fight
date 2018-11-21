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
    private void Awake()
    {
        playedata = new PlayData(Stage_name, Players);
    }

    // Use this for initialization
    void Start () {
        //SceneManagerController.LoadScene();
		
	}
	
}
