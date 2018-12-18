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
using UnityEngine.Video;
using Constant;


public class PlaySceneManager : MonoBehaviour
{
    private GameObject UICanvases;      //UI用キャンバス
    [SerializeField]
    private GameObject GameSet;        //ゲーム終了時に表示する

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
    private IEnumerator changescene = null;
    private RankingData[] ranking = null;

    //背景
    private GameObject Quad;
   

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

        //プレイヤーとHPを生成
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            //プレイヤーとHPバーを生成
            switch (i)
            {
                case 0:
                    P1.Mate_Data = (Material)Resources.Load("Material/P1Color");
                    P1.Mate_Data.SetColor("_EmissionColor", new Color(P1.Color_Data.r, P1.Color_Data.g, P1.Color_Data.b, P1.Color_Data.a));

                    P1.Player_obj = this.CreatePlayer(P1, i);
                    P1.HPgage_obj = this.CreateHPgage(P1, new Vector3(HPgagesize_in_UICanvas.x / 2 + remainder, HPgagesize_in_UICanvas.y / 2, 0));
                    
                    //カメラのターゲットに設定
                    CameraSet(P1, i);
                    break;

                case 1:
                    P2.Mate_Data = (Material)Resources.Load("Material/P2Color");
                    P2.Mate_Data.SetColor("_EmissionColor", new Color(P2.Color_Data.r, P2.Color_Data.g, P2.Color_Data.b, P2.Color_Data.a));

                    P2.Player_obj = this.CreatePlayer(P2, i);
                    P2.HPgage_obj = this.CreateHPgage(P2, new Vector3(HPgagesize_in_UICanvas.x / 2 + remainder * 2 + HPgagesize_in_UICanvas.x, HPgagesize_in_UICanvas.y / 2, 0));

                    //カメラのターゲットに設定
                    CameraSet(P2, i);
                    break;

                case 2:
                    P3.Mate_Data = (Material)Resources.Load("Material/P3Color");
                    P3.Mate_Data.SetColor("_EmissionColor", new Color(P3.Color_Data.r, P3.Color_Data.g, P3.Color_Data.b, P3.Color_Data.a));

                    P3.Player_obj = this.CreatePlayer(P3, i);
                    P3.HPgage_obj = this.CreateHPgage(P3, new Vector3(HPgagesize_in_UICanvas.x / 2 + remainder * 3 + HPgagesize_in_UICanvas.x * 2, HPgagesize_in_UICanvas.y / 2, 0));

                    //カメラのターゲットに設定
                    CameraSet(P3, i);
                    break;

                case 3:
                    P4.Mate_Data = (Material)Resources.Load("Material/P4Color");
                    P4.Mate_Data.SetColor("_EmissionColor", new Color(P4.Color_Data.r, P4.Color_Data.g, P4.Color_Data.b, P4.Color_Data.a));

                    P4.Player_obj = this.CreatePlayer(P4, i);
                    P4.HPgage_obj = this.CreateHPgage(P4, new Vector3(HPgagesize_in_UICanvas.x / 2 + remainder * 4 + HPgagesize_in_UICanvas.x * 3, HPgagesize_in_UICanvas.y / 2, 0));

                    //カメラのターゲットに設定
                    CameraSet(P4, i);
                    break;
            }
        }

        //背景を取得
        Quad = GameObject.Find("Quad");
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
        if (ranking == null)
        {
            for (int i = 0; i < PlayData.Instance.playerNum; i++)
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
            if ((death_count == 1 || DownTimer_cs.DownTimer_time <= 0) && PlayData.Instance.playerNum != 1)
            {
                this.EndFight(DownTimer_cs.DownTimer_time);
            }

            //1人プレイの時の終了処理
            if (XCI.GetButton(XboxButton.Start, XboxController.First) && PlayData.Instance.playerNum == 1)
            {
                SceneManager.LoadScene("Title");
            }
        }

        //背景のビデオプレイヤーを取得
        VideoPlayer videoplayer = Quad.GetComponent<VideoPlayer>();

        //ビデオの再生が終わったら消す
        if (videoplayer.enabled == true && ((ulong)videoplayer.frame == videoplayer.frameCount))
        {
            videoplayer.enabled = false;
        }
    }


    /// <summary>
    /// プレイヤーを生成
    /// </summary>
    /// <param name="player_data">プレイヤーデータ</param>
    /// <returns>プレイヤー</returns>
    private GameObject CreatePlayer(PlayerData player_data, int num)
    {
        //プレイヤーを生成
        GameObject player = Instantiate(player_data.Player_obj, player_data.InitialPos_Data, Quaternion.identity);
        //プレイヤーの設定
        this.SetPlayerStatus(player, player_data, num);

        return player;
    }

   
    /// <summary>
    /// プレイヤーの設定
    /// </summary>
    /// <param name="player">プレイヤーオブジェクト</param>
    /// <param name="player_data">プレイヤーデータ</param>
    private void SetPlayerStatus(GameObject player, PlayerData player_data, int num)
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

       
        player.transform.GetComponent<Renderer>().material = player_data.Mate_Data;
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
    /// 自分にのみ効果のある処理
    /// </summary>
    /// <param name="owner">オーナー</param>
    /// <param name="weapon">武器</param>
    /// <param name="num">オーナーの番号</param>
    public void Effect_myself(GameObject owner, GameObject weapon, int num)
    {
        // ダメージを受けたプレイヤーデータを取得する
        PlayerData player_data = CheckDamagePlayer(owner.name);

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
            var dedobj = Instantiate(dedEffect, owner.transform.position + transform.forward, Quaternion.identity) as GameObject;
            TrueDeath[num] = true;
            DeathNumber[num] = owner.name;
            audio.volume = 0.3f;
            audio.PlayOneShot(audioClip_ded);
            Destroy(owner);
            Destroy(HP_Slider[num].gameObject);
            death_count--;
        }
        return;
    }

   /// <summary>
   /// 敵プレイヤーのみにダメージを与える
   /// </summary>
   /// <param name="damagePlayer">ダメージを受けたプレイヤー</param>
   /// <param name="weapon">武器</param>
   /// <param name="num">ダメージを受けたプレイヤーの番号</param>
    public void Player_ReceiveDamage(GameObject damagePlayer, GameObject weapon, int num, bool InstantDeath = false)
    {
        //無敵身代わり
        if(damagePlayer.transform.GetComponent<Player>().Invincible_Data || damagePlayer.transform.GetComponent<Player>().Substitution_Data)
        {
            this.invincible_Substitution(damagePlayer);
            return;
        }

        // ダメージを与えたプレイヤーの名前
        string giveDamagePlayer = weapon.GetComponent<WeaponBlocController>().Owner_Data;

        // 武器の所有者の名前とダメージを受けたプレイヤーの名前が同じならばダメージを受けない
        if (damagePlayer.name == giveDamagePlayer)
        {
            return;
        }
        else
        {
            float DamageValue = weapon.GetComponent<WeaponBlocController>().DamageValue_Data;

            //即死の時
            if(InstantDeath == true)
            {
                DamageValue = HP_Slider[num].value;
            }

            // ダメージ音
            audio.volume = .3f;
            audio.PlayOneShot(audioClip_hit);

            //ダメージを与えたプレイヤーに値を加える
            for(int i = 0; i < PlayData.Instance.playerNum; i++)
            {
                if (PlayData.Instance.PlayersData[i].Name_Data == giveDamagePlayer)
                {
                    PlayData.Instance.PlayersData[i].DamageCount = (int)DamageValue;
                }
            }

            // ダメージを受けたプレイヤーデータを取得する
            PlayerData player_data = CheckDamagePlayer(damagePlayer.name);

            //ダメージを受けたプレイヤーがいなかったとき
            if (player_data == null)
            {
                return;
            }

            //ダメージを与える
            HP_Slider[num].value -= DamageValue;

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
    /// プレイヤーにバットステータスを与える
    /// </summary>
    /// <param name="damagePlayer">ダメージを受けたプレイヤー</param>
    /// <param name="owner_name">オーナーの名前</param>
    /// <param name="damage">ダメージ量</param>
    /// <param name="num">プレイヤー番号</param>
    /// <param name="state">バットステータス</param>
    /// <param name="time">治るまでの時間</param>
    public void Player_BatStatus(GameObject damagePlayer, string owner_name, float damage, int num, State state, float time)
    {
        //無敵身代わり
        if (damagePlayer.transform.GetComponent<Player>().Invincible_Data || damagePlayer.transform.GetComponent<Player>().Substitution_Data)
        {
            this.invincible_Substitution(damagePlayer);
            return;
        }

        // ダメージを与えたプレイヤーの名前
        string giveDamagePlayer = owner_name;

        Player DamagePlayer_cs = damagePlayer.GetComponent<Player>();

        switch(state)
        {
            case State.Stan:
                DamagePlayer_cs.Stan_Data = true;
                break;
        }
        // ダメージ音
        audio.volume = .3f;
        audio.PlayOneShot(audioClip_hit);

        //ダメージを与えたプレイヤーに値を加える
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            if (PlayData.Instance.PlayersData[i].Name_Data == giveDamagePlayer)
            {
                PlayData.Instance.PlayersData[i].DamageCount = (int)damage;
            }
        }


        //ダメージを与える
        HP_Slider[num].value -= damage;

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
        else
        {
            StartCoroutine(Release_BatStatus(time, DamagePlayer_cs, state));
        }
    }

    /// <summary>
    /// 状態異常を解除する
    /// </summary>
    /// <param name="DamagePlayer_cs">状態異常のプレイヤー</param>
    /// <param name="state">解除する状態異常</param>
    public IEnumerator Release_BatStatus(float coroutineTime, Player DamagePlayer_cs, State state)
    {
        yield return new WaitForSeconds(coroutineTime);

        switch (state)
        {
            case State.Stan:
                DamagePlayer_cs.Stan_Data = false;
                break;

            case State.Sleep:
                DamagePlayer_cs.Sleep_Data = false;
                break;
        }
      
    }

    /// <summary>
    /// どのプレイヤーもダメージを受ける
    /// </summary>
    /// <param name="damagePlayer">ダメージを受けたプレイヤー</param>
    /// <param name="weapon">武器</param>
    /// <param name="num">ダメージを受けたプレイヤーの番号</param>
    public void AllPlayer_Damage(GameObject damagePlayer, GameObject weapon, int num)
    {
        //無敵身代わり
        if (damagePlayer.transform.GetComponent<Player>().Invincible_Data || damagePlayer.transform.GetComponent<Player>().Substitution_Data)
        {
            this.invincible_Substitution(damagePlayer);
            return;
        }

        // ダメージを与えたプレイヤーの名前
        string giveDamagePlayer = weapon.GetComponent<WeaponBlocController>().Owner_Data;

        {
            //effectControll.HitEffect();
            // ダメージ音
            audio.volume = .3f;
            audio.PlayOneShot(audioClip_hit);

            //ダメージを与えたプレイヤーに値を加える
            for (int i = 0; i < PlayData.Instance.playerNum; i++)
            {
                if (PlayData.Instance.PlayersData[i].Name_Data == giveDamagePlayer)
                {
                    PlayData.Instance.PlayersData[i].DamageCount = (int)weapon.GetComponent<WeaponBlocController>().DamageValue_Data;
                }
            }

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
    /// どのプレイヤーもダメージを受ける
    /// </summary>
    /// <param name="damagePlayer">ダメージを受けたプレイヤー</param>
    /// <param name="owner_name">武器のオーナーの名前</param>
    /// <param name="weaponDamage">武器のダメージ量</param>
    /// <param name="num">ダメージを受けたプレイヤーの番号</param>
    public void AllPlayer_Damage(GameObject damagePlayer, string owner_name, float weaponDamage, int num)
    {
        //無敵身代わり
        if (damagePlayer.transform.GetComponent<Player>().Invincible_Data || damagePlayer.transform.GetComponent<Player>().Substitution_Data)
        {
            this.invincible_Substitution(damagePlayer);
            return;
        }
        
            //effectControll.HitEffect();
            // ダメージ音
            audio.volume = .3f;
            audio.PlayOneShot(audioClip_hit);

            //ダメージを与えたプレイヤーに値を加える
            for (int i = 0; i < PlayData.Instance.playerNum; i++)
            {
                if (PlayData.Instance.PlayersData[i].Name_Data == owner_name)
                {
                    PlayData.Instance.PlayersData[i].DamageCount = (int)weaponDamage;
                }
            }

            // ダメージを受けたプレイヤーデータを取得する
            PlayerData player_data = CheckDamagePlayer(damagePlayer.name);

            //ダメージを受けたプレイヤーがいなかったとき
            if (player_data == null)
            {
                return;
            }

            //ダメージを与える
            HP_Slider[num].value -= weaponDamage;

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

    
    private void invincible_Substitution(GameObject damagePlayer)
    {
        //無敵
        if (damagePlayer.transform.GetComponent<Player>().Invincible_Data)
        {
            return;
        }

        //身代わり
        if (damagePlayer.transform.GetComponent<Player>().Substitution_Data)
        {
            foreach (Transform Child in damagePlayer.transform)
            {
                if (Child.name == "WeaponBlock(に)" || Child.name == "WeaponBlock(ニ)")
                {
                    //武器を表示
                    Child.GetChild(0).gameObject.SetActive(true);

                    //親子関係を解除
                    Child.parent = null;

                    //身代わり状態解除
                    damagePlayer.transform.GetComponent<Player>().Substitution_Data = false;

                    StartCoroutine(DelayMethod(1.5f, () => { Destroy(Child.gameObject); }));
                    return;
                }
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
                    P1.Player_obj = this.CreatePlayer(P1, num);
                    CameraSet(P1, num);
                    break;

                case 1:
                    P2.Player_obj = this.CreatePlayer(P2, num);
                    CameraSet(P2, num);

                    break;

                case 2:
                    P3.Player_obj = this.CreatePlayer(P3, num);
                    CameraSet(P3, num);
                    break;

                case 3:
                    P4.Player_obj = this.CreatePlayer(P4, num);
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
        changescene = this.ChangeScene();
        StartCoroutine(changescene);

        ranking = new RankingData[PlayData.Instance.playerNum];   //ランキング順
        RankingData[] dummy = new RankingData[PlayData.Instance.playerNum];     //データ置き場

        int MAXDamage = 0;              //最大合計ダメージ値
        string MAXDamagePlayer = null;  //最大合計ダメージを出したプレイヤー名

        //プレイヤーデータを取得
        dummy = GetInitializeRankingData();

        //与えたダメージ順にソート
        Array.Sort(dummy, (a, b) => b.AttackDamage_data - a.AttackDamage_data);

        //最大ダメージ値とプレイヤー名を獲得
        MAXDamage = dummy[0].AttackDamage_data;
        MAXDamagePlayer = dummy[0].PlayerName_data;

        //ランキングデータの初期化
        ranking = new RankingData[PlayData.Instance.playerNum]; ;

        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            ranking[i] = new RankingData();
        }

        //死んだ順で並べ直してランキングに格納
        //this.DeathPlayerSort(dummy);
        

       resultdata = new ResultData(endtime, MAXDamage, MAXDamagePlayer,dummy);

        GameSet.SetActive(true);

    }

    /// <summary>
    /// 各プレイヤーのデータを回収
    /// </summary>
    /// <returns>各プレイヤーのデータ</returns>
    private RankingData[] GetInitializeRankingData()
    {
        RankingData[] playerdata = new RankingData[PlayData.Instance.playerNum]; ;

        for(int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            playerdata[i] = new RankingData();
        }

        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            switch (i)
            {
                case 0:
                    playerdata[0].PlayerName_data = P1.Name_Data;
                    playerdata[0].PlayerFace_data = P1.PlayerFace_Data;
                    playerdata[0].AttackDamage_data = PlayData.Instance.PlayersData[i].DamageCount;
                    break;

                case 1:
                    playerdata[1].PlayerName_data = P2.Name_Data;
                    playerdata[1].PlayerFace_data = P2.PlayerFace_Data;
                    playerdata[1].AttackDamage_data = PlayData.Instance.PlayersData[i].DamageCount;
                    break;

                case 2:
                    playerdata[2].PlayerName_data = P3.Name_Data;
                    playerdata[2].PlayerFace_data = P3.PlayerFace_Data;
                    playerdata[2].AttackDamage_data = PlayData.Instance.PlayersData[i].DamageCount;
                    break;

                case 3:
                    playerdata[3].PlayerName_data = P4.Name_Data;
                    playerdata[3].PlayerFace_data = P4.PlayerFace_Data;
                    playerdata[3].AttackDamage_data = PlayData.Instance.PlayersData[i].DamageCount;
                    break;
            }
        }

        return playerdata;
    }

    /// <summary>
    /// 死んだ奴から格納する
    /// </summary>
    /// <param name="ranking">ランキング</param>
    /// <param name="dummy">ダメージ量でソートされたデータ</param>
    private void DeathPlayerSort(RankingData[] dummy)
    {
        int count = 0;

        for (int i = PlayData.Instance.playerNum - 1; i >= 0; i--)
        {
            for (int j = 0; j < PlayData.Instance.playerNum; j++)
            {
                //死んでいてダメージ量が低い順
                if(dummy[i].PlayerName_data == DeathNumber[j])
                {
                    count++;
                    ranking[PlayData.Instance.playerNum - i - 1] = dummy[j];
                    i--;
                    j = 0;

                    if (i < 0)
                    {
                        break;
                    }
                }
            }
        }

        this.NotDeathPlayerSort(dummy, count);
    }

    /// <summary>
    /// 死んでないプレイヤーを設定
    /// </summary>
    /// <param name="ranking">ランキング</param>
    /// <param name="dummy">死んだ順</param>
    /// <param name="count">死んでいるプレイヤーの数</param>
    private void NotDeathPlayerSort(RankingData[] dummy, int count)
    {
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            for (int j = 0; j < PlayData.Instance.playerNum; j++)
            {
                //i番目に殺された奴と同じ名前だったら次へ
                if (dummy[i].PlayerName_data == DeathNumber[j] )
                {
                    i++;

                    j = 0;

                    //プレイヤーの数をオーバーしたら返す
                    if(i >= PlayData.Instance.playerNum)
                    {
                        return;
                    }
                }
            }

            //死んでいなかったら
            ranking[count] = dummy[i];

            count++;
        }
    }

    /// <summary>
    /// 渡された処理を指定時間後に実行する
    /// </summary>
    /// <param name="waitTime">遅延時間</param>
    /// <param name="action">実行する処理</param>
    /// <returns></returns>
    protected IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    private IEnumerator ChangeScene()
    {
       
        SceneManagerController.LoadScene();
        yield return new WaitForSeconds(3.0f);
        SceneManagerController.ChangeScene();
    }
}
