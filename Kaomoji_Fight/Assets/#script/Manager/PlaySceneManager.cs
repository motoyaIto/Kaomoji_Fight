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
using System;


public class PlaySceneManager : MonoBehaviour
{
    private GameObject UICanvases;      //UI用キャンバス

    private GameObject DownTimer_obj;   //ダウンタイマー
    private DownTimer DownTimer_cs;     //ダウンタイマーのcs

    private int death_count;            //残り人数が一人になったかどうかを調べるやつ
    private bool pause = false;         //ポーズ画面のフラグ

    private new AudioSource audio;      //オーディオ

    private AudioClip audioClip_gong;   //スタートで鳴らす音
    private AudioClip audioClip_ded;    //プレイヤーが死んだときに鳴らす音
    private AudioClip audioClip_hit;    //ぶつかった時の音   

    PlayerData P1;
    PlayerData P2;
    PlayerData P3;
    PlayerData P4 ;
   
    [HideInInspector]
    public List<bool> death_player = new List<bool>();      // 落ちて死んだプレイヤーを判別するためのリスト
    private List<bool> TrueDeath = new List<bool>();        // HPが無くなって死んだプレイヤーのリスト
    private string[] DeathNumber = null;
    private List<Slider> HP_Slider = new List<Slider>();    // HPゲージのリスト
    private List<XboxController> controller = new List<XboxController>();   // コントローラー番号のリスト

    private Vector3 RevivalPos = new Vector3(2.5f, 30f, 0f);    // プレイヤーの復帰場所

    private CinemachineTargetGroup TargetGroup;

    [SerializeField]
    public GameObject dedEffect;        // 死亡エフェクト

    private ResultData resultdata;//resultデータ
    //private EffectControll effectControll;  //エフェクト

    private void Awake()
    {

        //エフェクトを設定
        dedEffect = Resources.Load<GameObject>("prefab/Effect/Star_Burst_02");
        //UIオブジェクトを設定
        UICanvases = GameObject.Find("UI").transform.gameObject;

        //ダウンタイマーのobjとcsを取得
        DownTimer_obj = UICanvases.transform.Find("DownTimer").gameObject;
        DownTimer_cs = DownTimer_obj.GetComponent<DownTimer>();

        //ダウンタイマーを起動
        DownTimer_cs.DownTimer_State_data = true;

        //オーディオを取得
        audio = this.GetComponent<AudioSource>();
        audioClip_gong = (AudioClip)Resources.Load("Sound/SE/Start/gong");  //スタートゴング
        audioClip_ded = (AudioClip)Resources.Load("Sound/SE/Deth/ded");     //死亡時の音
        audioClip_hit = (AudioClip)Resources.Load("Sound/SE/Blow/Hit08-1");    //ぶつかる音

        //カメラにターゲットするプレイヤーの数を設定
        TargetGroup = this.GetComponent<CinemachineTargetGroup>();
        TargetGroup.m_Targets = new CinemachineTargetGroup.Target[PlayData.Instance.playerNum];

        //プレイヤーデータの生成
        switch (PlayData.Instance.playerNum)
        {
            case 1:
                P1 = PlayData.Instance.PlayersData[0];
                P1.LoadGameObject();

                break;
            case 2:
                P1 = PlayData.Instance.PlayersData[0];
                P1.LoadGameObject();

                P2 = PlayData.Instance.PlayersData[1];
                P2.LoadGameObject();

                break;
            case 3:
                P1 = PlayData.Instance.PlayersData[0];
                P1.LoadGameObject();

                P2 = PlayData.Instance.PlayersData[1];
                P2.LoadGameObject();

                P3 = PlayData.Instance.PlayersData[2];
                P3.LoadGameObject();
                break;
            case 4:
                P1 = PlayData.Instance.PlayersData[0];
                P1.LoadGameObject();

                P2 = PlayData.Instance.PlayersData[1];
                P2.LoadGameObject();

                P3 = PlayData.Instance.PlayersData[2];
                P3.LoadGameObject();

                P4 = PlayData.Instance.PlayersData[3];
                P4.LoadGameObject();

                break;
        }

        death_count = PlayData.Instance.playerNum;

        GameObject HPgage = Resources.Load<GameObject>("prefab/UI/HPgage");
        RectTransform HPgage_rectTrans = HPgage.transform.GetComponent<RectTransform>();

        RectTransform UICanvases_recttrans = UICanvases.GetComponent<RectTransform>();

        //HPゲージのキャンバス内サイズ
        Vector3 HPgagesize_in_UICanvas = new Vector3(HPgage_rectTrans.rect.width * UICanvases_recttrans.localScale.x, HPgage_rectTrans.rect.height * UICanvases_recttrans.localScale.y, 0);

        //HPゲージとHPゲージの間の余白
        float remainder = (Screen.width - HPgagesize_in_UICanvas.x * PlayData.Instance.playerNum) / (PlayData.Instance.playerNum + 1);

        Debug.Log("HPgage" + HPgage_rectTrans.rect.width);
        Debug.Log("UIcanvas" + UICanvases_recttrans.localScale.x);

        //プレイヤーとHPを生成
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            //プレイヤーとHPバーを生成
            switch (i)
            {
                case 0:
                    P1.Player_obj = this.CreatePlayer(P1, i, (Material)Resources.Load("Material/P1Color"));
                    P1.HPgage_obj = this.CreateHPgage(P1, new Vector3(HPgagesize_in_UICanvas.x / 2 + remainder, HPgagesize_in_UICanvas.y / 2, 0));
                    
                    //カメラのターゲットに設定
                    CameraSet(P1, i);
                    break;

                case 1:
                    P2.Player_obj = this.CreatePlayer(P2, i, (Material)Resources.Load("Material/P2Color"));
                    P2.HPgage_obj = this.CreateHPgage(P2, new Vector3(HPgagesize_in_UICanvas.x / 2 + remainder * 2 + HPgagesize_in_UICanvas.x, HPgagesize_in_UICanvas.y / 2, 0));

                    //カメラのターゲットに設定
                    CameraSet(P2, i);
                    break;

                case 2:
                    P3.Player_obj = this.CreatePlayer(P3, i, (Material)Resources.Load("Material/P3Color"));
                    P3.HPgage_obj = this.CreateHPgage(P3, new Vector3(HPgagesize_in_UICanvas.x / 2 + remainder * 3 + HPgagesize_in_UICanvas.x * 2, HPgagesize_in_UICanvas.y / 2, 0));

                    //カメラのターゲットに設定
                    CameraSet(P3, i);
                    break;

                case 3:
                    P4.Player_obj = this.CreatePlayer(P4, i, (Material)Resources.Load("Material/P4Color"));
                    P4.HPgage_obj = this.CreateHPgage(P4, new Vector3(HPgagesize_in_UICanvas.x / 2 + remainder * 4 + HPgagesize_in_UICanvas.x * 3, HPgagesize_in_UICanvas.y / 2, 0));

                    //カメラのターゲットに設定
                    CameraSet(P4, i);
                    break;
            }
        }
    }

    void Start()
    {
        //ゴング
        audio.volume = .5f;
        audio.PlayOneShot(audioClip_gong);

        DeathNumber = new string[PlayData.Instance.playerNum];
        // リストの初期化
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            DeathNumber[i] = null;
            death_player.Add(true);
            TrueDeath.Add(false);
            HP_Slider.Add(CheckDamagePlayer(PlayData.Instance.PlayersData[i].Name_Data).HPgage_obj.GetComponent<Slider>());
        }
    }

    void Update()
    {
        for(int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            // 一時停止
            if (XCI.GetButtonDown(XboxButton.Start, controller[i]))
            {
                if (!pause)
                {
                    Pause(.0f);
                    pause = true;
                }
                else
                {
                    Pause(1.0f);
                    pause = false;
                }
                
            }

            // 死んだプレイヤーの蘇生
            if (death_player[i] == false && TrueDeath[i] == false)
            {
                RegenerationPlayer(i);
            }
        }

        // プレイヤーが独り、または時間が来たらリザルトに遷移
        if (death_count == 1 || DownTimer_cs.DownTimer_time < 0)
        {
            this.EndFight(DownTimer_cs.DownTimer_time);
        }
    }


    /// <summary>
    /// プレイヤーを生成
    /// </summary>
    /// <param name="player_data">プレイヤーデータ</param>
    /// <returns>プレイヤー</returns>
    private GameObject CreatePlayer(PlayerData player_data, int num, Material player_mate)
    {
        //プレイヤーを生成
        GameObject player = Instantiate(player_data.Player_obj, player_data.InitialPos_Data, Quaternion.identity);
        //プレイヤーの設定
        this.SetPlayerStatus(player, player_data, num, player_mate);

        return player;

       
    }

   
    /// <summary>
    /// プレイヤーの設定
    /// </summary>
    /// <param name="player">プレイヤーオブジェクト</param>
    /// <param name="player_data">プレイヤーデータ</param>
    private void SetPlayerStatus(GameObject player, PlayerData player_data, int num, Material player_mate)
    {
        //キャラの顔をセット
        SpriteRenderer playerFace = player.GetComponent<SpriteRenderer>();
        playerFace.sprite = player_data.PlayerFace_Data;

        //ヒエラルキー名をセット
        player.name = player_data.Name_Data;

        //コントローラーをセット
        Player playerScript = player.GetComponent<Player>();
        playerScript.GetControllerNamber = player_data.Controller_Data;
        controller.Add(player_data.Controller_Data);

        //カメラのターゲット用ダミーを取得する
        string dummy_name = player_data.Name_Data + "_dummy";

       
        foreach(Transform child in this.transform)
        {
            //dummyのコピーを作成
            Transform clone_Child = Instantiate(child);
            clone_Child.name = child.name;

            //プレイヤーのデータに保存
            player_data.My_Camera_data = clone_Child;

            //プレイヤーと親子関係を作成
            clone_Child.transform.parent = player.gameObject.transform;
            clone_Child.transform.position = player.transform.position;

            break;
        }

        player_mate.SetColor("_EmissionColor", new Color(player_data.Color_Data.r, player_data.Color_Data.g, player_data.Color_Data.b, player_data.Color_Data.a));
        player.transform.GetComponent<Renderer>().material = player_mate;
    }

    /// <summary>
    /// HPゲージを生成
    /// </summary>
    /// <param name="player_data">プレイヤーデータ</param>
    /// <param name="pos">HPゲージの描画座標</param>
    /// <returns>HPゲージ</returns>
    private GameObject CreateHPgage(PlayerData player_data, Vector3 pos)
    {
        //HPゲージを生成
        GameObject HPgage = Instantiate(player_data.HPgage_obj, pos, Quaternion.identity, UICanvases.transform);

        HPgage.name = player_data.Name_Data + "_HPgage";

        //名前の設定
        TextMeshProUGUI name = HPgage.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        name.text = player_data.Name_Data;
        name.color = player_data.Color_Data;

        //HPゲージの最大値を与える
        Slider hp_slider = HPgage.GetComponent<Slider>();
        hp_slider.maxValue = player_data.HP_Date;
        hp_slider.value = player_data.HP_Date;
        return HPgage;
    }

    /// <summary>
    /// プレイヤーがダメージを受ける
    /// </summary>
    public void Player_ReceiveDamage(GameObject damagePlayer, GameObject weapon, int num)
    {
        // 武器の所有者の名前とダメージを受けたプレイヤーの名前が同じならばダメージを受けない
        if (damagePlayer.name == weapon.GetComponent<WeaponBlocController>().Owner_Data)
        {
            return;
        }
        else
        {
            //effectControll.HitEffect();
            // ダメージ音
            audio.volume = .3f;
            audio.PlayOneShot(audioClip_hit);

            // ダメージを受けたプレイヤーデータを取得する
            PlayerData player_data = CheckDamagePlayer(damagePlayer.name);

            //ダメージを受けたプレイヤーがいなかったとき
            if (player_data == null)
            {
                return;
            }

            //ダメージを与える
            HP_Slider[num].value -= weapon.GetComponent<WeaponBlocController>().DamageValue_Data;

            //HPが0以下になったらplayerを殺す
            if (HP_Slider[num].value <= 0)
            {
                var dedobj = Instantiate(dedEffect, damagePlayer.transform.position + transform.forward, Quaternion.identity) as GameObject;
                TrueDeath[num] = true;
                DeathNumber[num] = damagePlayer.name;
                audio.volume = 0.3f;
                audio.PlayOneShot(audioClip_ded);
                Destroy(damagePlayer);
                Destroy(HP_Slider[num].gameObject);
                death_count--;
            }

        }

    }

    /// <summary>
    /// ダメージを受けたプレイヤーデータを探す
    /// </summary>
    /// <param name="Damage_Pname">ダメージを受けたプレイヤーの名前</param>
    /// <returns>プレイヤーデータ</returns>
    private PlayerData CheckDamagePlayer(string Damage_Pname)
    {
        if (Damage_Pname == P1.Name_Data)
        {
            return P1;
        }

        if (Damage_Pname == P2.Name_Data)
        {
            return P2;
        }

        if (Damage_Pname == P3.Name_Data)
        {
            return P3;
        }

        if (Damage_Pname == P4.Name_Data)
        {
            return P4;
        }
        return null;
    }

    /// <summary>
    /// 死んだプレイヤーの再生成
    /// </summary>
    /// <param name="num">プレイヤー番号</param>
    private void RegenerationPlayer(int num)
    {
        death_player[num] = true;
        HP_Slider[num].value -= HP_Slider[num].maxValue / 10f;

        if (HP_Slider[num].value <= .0f)
        {
            TrueDeath[num] = true;
            audio.volume = 0.3f;
            audio.PlayOneShot(audioClip_ded);
            Destroy(HP_Slider[num].gameObject);
            death_count--;
        }

        if (!TrueDeath[num])
        {
            switch (num)
            {
                case 0:
                    P1.Player_obj = this.CreatePlayer(P1, num, (Material)Resources.Load("Material/P1Color"));
                    CameraSet(P1, num);
                    break;

                case 1:
                    P2.Player_obj = this.CreatePlayer(P2, num, (Material)Resources.Load("Material/P2Color"));
                    CameraSet(P2, num);

                    break;

                case 2:
                    P3.Player_obj = this.CreatePlayer(P3, num, (Material)Resources.Load("Material/P3Color"));
                    CameraSet(P3, num);
                    break;

                case 3:
                    P4.Player_obj = this.CreatePlayer(P4, num, (Material)Resources.Load("Material/P4Color"));
                    CameraSet(P4, num);
                    break;

                default:
                    break;
            }
        }
        else
        {
            return;
        }

        audio.volume = 0.3f;
        audio.PlayOneShot(audioClip_ded);
    }

    private void CameraSet(PlayerData player, int num)
    {
        //カメラのターゲットに設定
        TargetGroup.m_Targets[num].target = player.My_Camera_data;
        TargetGroup.m_Targets[num].weight = 1;
        TargetGroup.m_Targets[num].radius = 1;

    }

    private void Pause(float num)
    {
        Time.timeScale = num;
    }

    /// <summary>
    /// ゲーム終了時処理
    /// </summary>
    /// <param name="endtime">ゲーム終了時の残りタイム</param>
    public void EndFight(float endtime)
    {
        RankingData[] ranking = new RankingData[PlayData.Instance.playerNum];   //ランキング順
        RankingData[] dummy = new RankingData[PlayData.Instance.playerNum];     //データ置き場

        int MAXDamage = 0;              //最大合計ダメージ値
        string MAXDamagePlayer = null;  //最大合計ダメージを出したプレイヤー名

        //プレイヤーデータを取得
        dummy = GetInitializeRankingData();

        //与えたダメージ順にソート
        Array.Sort(dummy, (a, b) => a.AttackDamage_data - b.AttackDamage_data);

        //最大ダメージ値とプレイヤー名を獲得
        MAXDamage = dummy[0].AttackDamage_data;
        MAXDamagePlayer = dummy[0].PlayerName_data;

        //死んだ順で並べ直してランキングに格納
        this.DeathPlayerSort(ref ranking, dummy);
        

       resultdata = new ResultData(endtime, MAXDamage, MAXDamagePlayer, ranking);
        //SceneManagerController.LoadScene();
        //SceneManagerController.ChangeScene();
    }

    /// <summary>
    /// 各プレイヤーのデータを回収
    /// </summary>
    /// <returns>各プレイヤーのデータ</returns>
    private RankingData[] GetInitializeRankingData()
    {
        RankingData[] ranking = new RankingData[PlayData.Instance.playerNum]; ;

        for(int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            ranking[i] = new RankingData();
        }

        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            switch (i)
            {
                case 0:
                    ranking[0].PlayerName_data = P1.Name_Data;
                    ranking[0].PlayerFace_data = P1.PlayerFace_Data;
                    ranking[0].AttackDamage_data = P1.Player_obj.GetComponent<Player>().DamageCount;

                    break;

                case 1:
                    ranking[1].PlayerName_data = P2.Name_Data;
                    ranking[1].PlayerFace_data = P2.PlayerFace_Data;
                    ranking[1].AttackDamage_data = P2.Player_obj.GetComponent<Player>().DamageCount;

                    break;

                case 2:
                    ranking[2].PlayerName_data = P3.Name_Data;
                    ranking[2].PlayerFace_data = P3.PlayerFace_Data;
                    ranking[2].AttackDamage_data = P3.Player_obj.GetComponent<Player>().DamageCount;

                    break;

                case 3:
                    ranking[3].PlayerName_data = P4.Name_Data;
                    ranking[3].PlayerFace_data = P4.PlayerFace_Data;
                    ranking[3].AttackDamage_data = P4.Player_obj.GetComponent<Player>().DamageCount;

                    break;
            }
        }

        return ranking;
    }

    /// <summary>
    /// 死んだ奴から格納する
    /// </summary>
    /// <param name="ranking">ランキング</param>
    /// <param name="dummy">ダメージ量でソートされたデータ</param>
    private void DeathPlayerSort(ref RankingData[] ranking, RankingData[] dummy)
    {
        int count = 0;

        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            for (int j = 0; j < PlayData.Instance.playerNum; j++)
            {
                //i番目に殺された奴と同じ名前のを格納
                if (DeathNumber[i] == dummy[j].PlayerName_data)
                {
                    ranking[PlayData.Instance.playerNum - i - 1] = dummy[j];

                    count++;
                }
            }
        }

        this.NotDeathPlayerSort(ref ranking, dummy, count);
    }

    private void NotDeathPlayerSort(ref RankingData[] ranking, RankingData[] dummy, int count)
    {
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            for (int j = 0; j < PlayData.Instance.playerNum; j++)
            {
                //i番目に殺された奴と同じ名前だったら次へ
                if (dummy[i].PlayerName_data == DeathNumber[j] )
                {
                    break;
                }

                //死んでなかったら
                if(j == PlayData.Instance.playerNum - 1)
                {
                    ranking[count] = dummy[i];

                    count++;
                }
            }
        }
    }
}
