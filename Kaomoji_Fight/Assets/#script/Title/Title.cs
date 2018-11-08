using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;
using TMPro;
using UnityEngine.Video;

public class Title : MonoBehaviour
{

    [SerializeField]
    private GameObject TManager;        //Titleマネージャー
    private TitleManager TManager_cs;   //Titleマネージャーcs

    [SerializeField]
    private VideoPlayer videoPlayer;//ビデオプレイヤー
   [SerializeField]
    private float speed = 1.0f; //点滅スピード

    private float time;         //タイマー

    TextMeshPro TMPro_cs;//テキストメッシュプロcs
    AudioSource audio;
    
    void Start()
    {
        //初回の鳴らすのを止める
        audio = this.GetComponent<AudioSource>();
        audio.Stop();

        //各csの取得
        TManager_cs = TManager.GetComponent<TitleManager>();
        TMPro_cs = this.GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (TManager_cs.Mode_Data != TitleManager.SELECTMODE.TITLE || TManager_cs.ControllerLock_Data == true)
        {
            return;
        }

        //点滅処理
        TMPro_cs.color = GetAlphaColor(TMPro_cs.color);
                   
        //スペースキー(デバッグ用)・1PコントローラーのBボタンが押されたらページをめくる
        if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, XboxController.First))
        {
            videoPlayer.Stop();
            audio.PlayOneShot(audio.clip);
            TManager_cs.ChangePage(TitleManager.SELECTMODE.PLAYERNUM);
        }
    }

    /// <summary>
    /// Alpha値を更新
    /// </summary>
    /// <param name="color">色</param>
    /// <returns>更新のかかった色</returns>
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}