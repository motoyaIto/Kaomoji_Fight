using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using XboxCtrlrInput;
using TMPro;
using Cinemachine;
using System.Linq;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour {

    private GameObject canvas;  // UIキャンバス


    private AudioSource As;
    private AudioClip se;

    private List<PlayerData> players = new List<PlayerData>();  // プレイヤーのリスト

    private void Awake()
    {
        // 音を鳴らす準備
        As = this.GetComponent<AudioSource>();
    }

    void Start () {
        // UIオブジェクトを設定
        canvas = GameObject.Find("ResultUI").transform.gameObject;
    }
	
	void Update () {
		
	}
}
