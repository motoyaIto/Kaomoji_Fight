﻿using System.Collections;
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


        private Slider m_hpgage_slider; //HPスライダー
        private XboxController m_controller;

        private Transform m_myCamera = null;

        private float m_hp;

        //コンストラクタ
        public Player_data(string name, Color col, Sprite player_face, Vector3 initialPos, XboxController controller, float hp)
        {
            m_Player = (GameObject)Resources.Load("prefab/Player");
            m_HPgage = (GameObject)Resources.Load("prefab/UI/HPgage");
            m_name = name;
            m_nameColor = col;
            m_playerFace = player_face;
            m_initialPos = initialPos;
            m_hpgage_slider = null;
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
                if(m_HPgage == null)
                {
                    m_HPgage = value;
                }
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

        public Slider Hpgage_slider_Data
        {
            set
            {
                if(m_hpgage_slider == null)
                {
                    m_hpgage_slider = value;
                }
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


    [SerializeField]
    private GameObject UICanvases;      //UI用キャンバス


    [SerializeField]
    Player_data P1;
    [SerializeField]
    Player_data P2;
    [SerializeField]
    Player_data P3;
    [SerializeField]
    Player_data P4 ;

    [SerializeField, Header("プレイヤーの復帰時の場所指定")]
    private Vector3 RevivalPos = new Vector3(2.5f, 50f, 0f);
   
    [HideInInspector]
    public List<bool> death_player = new List<bool>();   // 死んだプレイヤーを判別するためのリスト

    private List<Slider> slider = new List<Slider>();
    private float[] player_hp;

    private CinemachineTargetGroup TargetGroup;

    private void Awake()
    {
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
                    P1.Player_obj = this.CreatePlayer(P1);
                    P1.HPgage_obj = this.CreateHPgage(P1, new Vector3(HPgage_size.sizeDelta.x / 2, Screen.height - 10, 0f));

                    //カメラのターゲットに設定
                    CameraSet(P1, i);
                    break;

                case 1:
                    P2.Player_obj = this.CreatePlayer(P2);
                    P2.HPgage_obj = this.CreateHPgage(P2, new Vector3(Screen.width - HPgage_size.sizeDelta.x / 2, Screen.height - 10, 0));

                    //カメラのターゲットに設定
                    CameraSet(P2, i);
                    break;

                case 2:
                    P3.Player_obj = this.CreatePlayer(P3);
                    P3.HPgage_obj = this.CreateHPgage(P3, new Vector3(HPgage_size.sizeDelta.x / 2, 10, 0));

                    //カメラのターゲットに設定
                    CameraSet(P3, i);
                    break;

                case 3:
                    P4.Player_obj = this.CreatePlayer(P4);
                    P4.HPgage_obj = this.CreateHPgage(P4, new Vector3(Screen.width - HPgage_size.sizeDelta.x / 2, 10, 0));

                    //カメラのターゲットに設定
                    CameraSet(P4, i);
                    break;
            }
        }

    }

    // Use this for initialization
    void Start()
    {
        // リストの初期化
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            // 死亡リスト
            death_player.Add(true);

            // HPの取得
            GetSlider(i, "P" + (i + 1).ToString());
        }

        // Hpバーに値をセット
        HPSet(PlayData.Instance.playerNum);
    }

    // Update is called once per frame
    void Update()
    {
        // 死んだプレイヤーの蘇生
        for(int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            if(death_player[i] == false)
            {
                RegenerationPlayer(i);
            }
        }

        // HPを反映
        for(int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            slider[i].value = player_hp[i];
            //Debug.Log("Player" + i + "のHP : " + slider[i].value);
        }       
    }

    /// <summary>
    /// プレイヤーを生成
    /// </summary>
    /// <param name="player">プレイヤーオブジェクトデータ</param>
    /// <param name="i">何番目のプレイヤーか</param>
    private GameObject CreatePlayer(Player_data player_data)
    {
        //プレイヤーを生成
        GameObject player = Instantiate(player_data.Player_obj, player_data.InitialPos_Data, Quaternion.identity);
        //プレイヤーの設定
        this.SetPlayerStatus(player, player_data);

        return player;

       
    } 
    /// <summary>
    /// プレイヤーの設定
    /// </summary>
    /// <param name="player">プレイヤーオブジェクト</param>
    /// <param name="controllerNamber">コントローラー番号</param>
    /// <param name="name"></param>
    /// <param name="FaceTextures"></param>
    private void SetPlayerStatus(GameObject player, Player_data player_data)
    {
        //キャラの顔をセット
        SpriteRenderer playerFace = player.GetComponent<SpriteRenderer>();
        playerFace.sprite = player_data.PlayerFace_Data;

        //ヒエラルキー名をセット
        player.name = player_data.Name_Data;

        //コントローラーをセット
        Player playerScript = player.GetComponent<Player>();
        playerScript.GetControllerNamber = player_data.Controller_Data;

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
        //プレイヤーを生成
        GameObject HPgage = Instantiate(player_data.HPgage_obj, pos, Quaternion.identity, UICanvases.transform);

        HPgage.name = player_data.Name_Data + "_HPgage";

        //名前の設定
        TextMeshProUGUI name = HPgage.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        name.text = player_data.Name_Data;
        name.color = player_data.Color_Data;

        return HPgage;
    }

    private void GetSlider(int num, string p_Name)
    {
        slider[num] = GameObject.Find(p_Name + "_HPgage").GetComponent<Slider>();
    }

    public void Player_ReceiveDamage()
    {

        Debug.Log("hit");
    }


    // 死んだプレイヤーの再生成
    private void RegenerationPlayer(int num)
    {
        death_player[num] = true;
        switch (num)
        {
            case 0:
                P1.Player_obj = this.CreatePlayer(P1);
                CameraSet(P1, num);
                break;

            case 1:
                P2.Player_obj = this.CreatePlayer(P2);
                CameraSet(P2, num);

                break;

            case 2:
                P3.Player_obj = this.CreatePlayer(P3);
                CameraSet(P3, num);
                break;

            case 3:
                P4.Player_obj = this.CreatePlayer(P4);
                CameraSet(P4, num);
                break;

            default:
                break;
        }
        
    }

    private void CameraSet(Player_data player, int num)
    {
        //カメラのターゲットに設定
        TargetGroup.m_Targets[num].target = player.My_Camera_data;
        TargetGroup.m_Targets[num].weight = 1;
        TargetGroup.m_Targets[num].radius = 1;

    }

    // プレイヤーのＨＰをSliderにセットする
    private void HPSet(int num)
    {
        switch (num)
        {
            case 1:
                player_hp[0] = slider[0].maxValue = P1.HP_Date;
                break;

            case 2:
                player_hp[0] = slider[0].maxValue = P1.HP_Date;
                player_hp[1] = slider[1].maxValue = P2.HP_Date;
                break;

            case 3:
                player_hp[0] = slider[0].maxValue = P1.HP_Date;
                player_hp[1] = slider[1].maxValue = P2.HP_Date;
                player_hp[2] = slider[2].maxValue = P3.HP_Date;
                break;

            case 4:
                player_hp[0] = slider[0].maxValue = P1.HP_Date;
                player_hp[1] = slider[1].maxValue = P2.HP_Date;
                player_hp[2] = slider[2].maxValue = P3.HP_Date;
                player_hp[3] = slider[3].maxValue = P4.HP_Date;
                break;

            default:
                Debug.LogError(num + "人ではプレイできません");
                break;
        }
    }
}
