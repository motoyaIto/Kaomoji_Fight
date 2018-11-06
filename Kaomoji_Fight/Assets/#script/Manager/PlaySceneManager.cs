using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using XboxCtrlrInput;
using TMPro;
using Cinemachine;
using System.Linq;


public class PlaySceneManager : MonoBehaviour
{
    //プレイヤーのデータを管理する
    [System.Serializable]
    private class Player_data
    {
        private GameObject m_Player;
        private GameObject m_HPgage;

        [SerializeField]
        private string m_name;          //プレイヤー名
        [SerializeField]
        private Color m_nameColor;      //HPバーに表示される名前の色

        [SerializeField]
        private Sprite m_playerFace;//プレイヤーの顔文字

        [SerializeField]
        private Vector3 m_initialPos;   //初期ポップ位置

        private XboxController m_controller;

        private Transform m_myCamera = null;

        private float m_hp;

        private Slider m_hpSlider;

        //コンストラクタ
        public Player_data(string name, Color col, Sprite player_face, Vector3 initialPos, XboxController controller, float hp)
        {
            m_Player = (GameObject)Resources.Load("prefab/Player");
            m_HPgage = (GameObject)Resources.Load("prefab/UI/HPgage");
            m_name = name;
            m_nameColor = col;
            m_playerFace = player_face;
            m_initialPos = initialPos;
            m_controller = controller;
            m_hp = hp;
        }
        
        //getter:seter
        public GameObject Player_obj
        {
            get
            {
                return m_Player;
            }
            set
            {
                if(m_Player == null)
                {
                    m_Player = value;
                }
            }
        }

        public GameObject HPgage_obj
        {
            get
            {
                return m_HPgage;
            }
            set
            {
                m_HPgage = value;
            }
        }
        public string Name_Data
        {
            get
            {
                return m_name;
            }
        }

        public Color Color_Data
        {
            get
            {
                return m_nameColor;
            }
        }

        public Sprite PlayerFace_Data
        {
            get
            {
                return m_playerFace;
            }
        }

        public Vector3 InitialPos_Data
        {
            get
            {
                return m_initialPos;
            }
        }

        public XboxController Controller_Data
        {
            get
            {
                return m_controller;
            }
        }

        public Transform My_Camera_data
        {
            set
            {
                if(m_myCamera == null)
                {
                    m_myCamera = value;
                }
            }
            get
            {
                return m_myCamera;
            }
        }

        public float HP_Date
        {
            set
            {
                m_hp = value;
            }
            get
            {
                return m_hp;
            }
        }
    }

    private GameObject UICanvases;      //UI用キャンバス

    private GameObject DownTimer_obj;   //ダウンタイマー

    private bool pause = false;

    private new AudioSource audio;          //オーディオ

    private AudioClip audioClip_gong;   //スタートで鳴らす音
    private AudioClip audioClip_ded;    //プレイヤーが死んだときに鳴らす音
    private AudioClip audioClip_hit;    //ぶつかった時の音   

    [SerializeField]
    Player_data P1;
    [SerializeField]
    Player_data P2;
    [SerializeField]
    Player_data P3;
    [SerializeField]
    Player_data P4 ;
   
    [HideInInspector]
    public List<bool> death_player = new List<bool>();   // 落ちて死んだプレイヤーを判別するためのリスト
    private List<bool> TrueDeath = new List<bool>(); // HPが無くなって死んだプレイヤーのリスト
    private List<Slider> HP_Slider = new List<Slider>();    // HPゲージのリスト
    private List<XboxController> controller = new List<XboxController>();   // コントローラー番号のリスト

    private Vector3 RevivalPos = new Vector3(2.5f, 30f, 0f);    // プレイヤーの復帰場所

    private CinemachineTargetGroup TargetGroup;

    private void Awake()
    {
        //UIオブジェクトを設定
        UICanvases = GameObject.Find("UI").transform.gameObject;

        //ダウンタイマーのobjとcsを取得
        DownTimer_obj = UICanvases.transform.Find("DownTimer").gameObject;
        DownTimer DownTimer_cs = DownTimer_obj.GetComponent<DownTimer>();

        //ダウンタイマーを起動
        DownTimer_cs.DownTimer_On_data = true;

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
                P1 = new Player_data(PlayData.Instance.PlayersName[0], P1.Color_Data, PlayData.Instance.PlayersFace[0], P1.InitialPos_Data, XboxController.First, 100f);

                break;
            case 2:
                P1 = new Player_data(PlayData.Instance.PlayersName[0], P1.Color_Data, PlayData.Instance.PlayersFace[0], P1.InitialPos_Data, XboxController.First, 100f);
                P2 = new Player_data(PlayData.Instance.PlayersName[1], P2.Color_Data, PlayData.Instance.PlayersFace[1], P2.InitialPos_Data, XboxController.Second, 100f);

                break;
            case 3:
                P1 = new Player_data(PlayData.Instance.PlayersName[0], P1.Color_Data, PlayData.Instance.PlayersFace[0], P1.InitialPos_Data, XboxController.First, 100f);
                P2 = new Player_data(PlayData.Instance.PlayersName[1], P2.Color_Data, PlayData.Instance.PlayersFace[1], P2.InitialPos_Data, XboxController.Second, 100f);
                P3 = new Player_data(PlayData.Instance.PlayersName[2], P3.Color_Data, PlayData.Instance.PlayersFace[2], P3.InitialPos_Data, XboxController.Third, 100f);

                break;
            case 4:
                P1 = new Player_data(PlayData.Instance.PlayersName[0], P1.Color_Data, PlayData.Instance.PlayersFace[0], P1.InitialPos_Data, XboxController.First, 100f);
                P2 = new Player_data(PlayData.Instance.PlayersName[1], P2.Color_Data, PlayData.Instance.PlayersFace[1], P2.InitialPos_Data, XboxController.Second, 100f);
                P3 = new Player_data(PlayData.Instance.PlayersName[2], P3.Color_Data, PlayData.Instance.PlayersFace[2], P3.InitialPos_Data, XboxController.Third, 100f);
                P4 = new Player_data(PlayData.Instance.PlayersName[3], P4.Color_Data, PlayData.Instance.PlayersFace[3], P4.InitialPos_Data, XboxController.Fourth, 100f);
                break;
        }

        RectTransform HPgage_size = P1.HPgage_obj.GetComponent<RectTransform>();

        //プレイヤーとHPを生成
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            //プレイヤーとHPバーを生成
            switch (i)
            {
                case 0:
                    P1.Player_obj = this.CreatePlayer(P1, i);
                    P1.HPgage_obj = this.CreateHPgage(P1, new Vector3(HPgage_size.sizeDelta.x / 2, Screen.height - 10, 0f));

                    //カメラのターゲットに設定
                    CameraSet(P1, i);
                    break;

                case 1:
                    P2.Player_obj = this.CreatePlayer(P2, i);
                    P2.HPgage_obj = this.CreateHPgage(P2, new Vector3(Screen.width - HPgage_size.sizeDelta.x / 2, Screen.height - 10, 0));

                    //カメラのターゲットに設定
                    CameraSet(P2, i);
                    break;

                case 2:
                    P3.Player_obj = this.CreatePlayer(P3, i);
                    P3.HPgage_obj = this.CreateHPgage(P3, new Vector3(HPgage_size.sizeDelta.x / 2, 10, 0));

                    //カメラのターゲットに設定
                    CameraSet(P3, i);
                    break;

                case 3:
                    P4.Player_obj = this.CreatePlayer(P4, i);
                    P4.HPgage_obj = this.CreateHPgage(P4, new Vector3(Screen.width - HPgage_size.sizeDelta.x / 2, 10, 0));

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

        // リストの初期化
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            death_player.Add(true);
            TrueDeath.Add(false);
            HP_Slider.Add(CheckDamagePlayer(PlayData.Instance.PlayersName[i]).HPgage_obj.GetComponent<Slider>());
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
    }


    /// <summary>
    /// プレイヤーを生成
    /// </summary>
    /// <param name="player_data">プレイヤーデータ</param>
    /// <returns>プレイヤー</returns>
    private GameObject CreatePlayer(Player_data player_data, int num)
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
    private void SetPlayerStatus(GameObject player, Player_data player_data, int num)
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
    }

    /// <summary>
    /// HPゲージを生成
    /// </summary>
    /// <param name="player_data">プレイヤーデータ</param>
    /// <param name="pos">HPゲージの描画座標</param>
    /// <returns>HPゲージ</returns>
    private GameObject CreateHPgage(Player_data player_data, Vector3 pos)
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
            // ダメージ音
            audio.volume = .3f;
            audio.PlayOneShot(audioClip_hit);

            // ダメージを受けたプレイヤーデータを取得する
            Player_data player_data = CheckDamagePlayer(damagePlayer.name);

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
                TrueDeath[num] = true;
                audio.volume = 0.3f;
                audio.PlayOneShot(audioClip_ded);
                Destroy(damagePlayer);
                Destroy(HP_Slider[num].gameObject);
            }

        }

    }

    /// <summary>
    /// ダメージを受けたプレイヤーデータを探す
    /// </summary>
    /// <param name="Damage_Pname">ダメージを受けたプレイヤーの名前</param>
    /// <returns>プレイヤーデータ</returns>
    private Player_data CheckDamagePlayer(string Damage_Pname)
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

    private void CameraSet(Player_data player, int num)
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
}
