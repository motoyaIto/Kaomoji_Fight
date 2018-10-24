using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using XboxCtrlrInput;
using TMPro;
using Cinemachine;



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
        //コンストラクタ
        public Player_data(string name, Color col, Sprite player_face, Vector3 initialPos, XboxController controller)
        {
            m_Player = (GameObject)Resources.Load("prefab/Player");
            m_HPgage = (GameObject)Resources.Load("prefab/UI/HPgage");
            m_name = name;
            m_nameColor = col;
            m_playerFace = player_face;
            m_initialPos = initialPos;
            m_hpgage_slider = null;
            m_controller = controller;
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
    }


    


    [SerializeField]
    private GameObject UICanvases;      //UI用キャンバス


    //[SerializeField]
    //Player_data P1 = new Player_data("P1", new Color(0.000f, 0.000f, 0.000f), Sprite.Create((Texture2D)Resources.Load("textures/use/Player1"), new Rect(0, 0, 584, 385), new Vector2(0.5f, 0.5f)), new Vector3(5.0f, 50.0f, 0.0f));
    //[SerializeField]
    //Player_data P2 = new Player_data("P2", new Color(0.000f, 0.000f, 0.000f), Sprite.Create((Texture2D)Resources.Load("textures/use/Player1"), new Rect(0, 0, 584, 385), new Vector2(0.5f, 0.5f)), new Vector3(10.0f, 50.0f, 0.0f));
    //[SerializeField]
    //Player_data P3 = new Player_data("P3", new Color(0.000f, 0.000f, 0.000f), Sprite.Create((Texture2D)Resources.Load("textures/use/Player1"), new Rect(0, 0, 584, 385), new Vector2(0.5f, 0.5f)), new Vector3(15.0f, 50.0f, 0.0f));
    //[SerializeField]
    //Player_data P4 = new Player_data("P4", new Color(0.000f, 0.000f, 0.000f), Sprite.Create((Texture2D)Resources.Load("textures/use/Player1"), new Rect(0, 0, 584, 385), new Vector2(0.5f, 0.5f)), new Vector3(20.0f, 50.0f, 0.0f));

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

    private Player playerCS;

    private int death_player;

    private CinemachineTargetGroup TargetGroup;
    // Use this for initialization
    void Start()
    {
        playerCS = GetComponent<Player>();
        death_player = -1;

        //カメラにターゲットするプレイヤーの数を設定
        TargetGroup = this.GetComponent<CinemachineTargetGroup>();
        TargetGroup.m_Targets = new CinemachineTargetGroup.Target[PlayData.Instance.playerNum];

        //プレイヤーデータの生成
        switch (PlayData.Instance.playerNum)
        {
            case 1:
                P1 = new Player_data(PlayData.Instance.PlayersName[0], P1.Color_Data, PlayData.Instance.PlayersFace[0], P1.InitialPos_Data, XboxController.First);
                break;
            case 2:
                P1 = new Player_data(PlayData.Instance.PlayersName[0], P1.Color_Data, PlayData.Instance.PlayersFace[0], P1.InitialPos_Data, XboxController.First);
                P2 = new Player_data(PlayData.Instance.PlayersName[1], P2.Color_Data, PlayData.Instance.PlayersFace[1], P2.InitialPos_Data, XboxController.Second);
                break;
            case 3:
                P1 = new Player_data(PlayData.Instance.PlayersName[0], P1.Color_Data, PlayData.Instance.PlayersFace[0], P1.InitialPos_Data, XboxController.First);
                P2 = new Player_data(PlayData.Instance.PlayersName[1], P2.Color_Data, PlayData.Instance.PlayersFace[1], P2.InitialPos_Data, XboxController.Second);
                P3 = new Player_data(PlayData.Instance.PlayersName[2], P3.Color_Data, PlayData.Instance.PlayersFace[2], P3.InitialPos_Data, XboxController.Third);
                break;
            case 4:
                P1 = new Player_data(PlayData.Instance.PlayersName[0], P1.Color_Data, PlayData.Instance.PlayersFace[0], P1.InitialPos_Data, XboxController.First);
                P2 = new Player_data(PlayData.Instance.PlayersName[1], P2.Color_Data, PlayData.Instance.PlayersFace[1], P2.InitialPos_Data, XboxController.Second);
                P3 = new Player_data(PlayData.Instance.PlayersName[2], P3.Color_Data, PlayData.Instance.PlayersFace[2], P3.InitialPos_Data, XboxController.Third);
                P4 = new Player_data(PlayData.Instance.PlayersName[3], P4.Color_Data, PlayData.Instance.PlayersFace[3], P4.InitialPos_Data, XboxController.Fourth);
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
                    TargetGroup.m_Targets[i].target = P1.My_Camera_data;
                    TargetGroup.m_Targets[i].weight = 1;
                    TargetGroup.m_Targets[i].radius = 1;

                    break;

                case 1:
                    P2.Player_obj = this.CreatePlayer(P2);
                    P2.HPgage_obj = this.CreateHPgage(P2, new Vector3(Screen.width - HPgage_size.sizeDelta.x / 2, Screen.height - 10, 0));

                    //カメラのターゲットに設定
                    TargetGroup.m_Targets[i].target = P2.My_Camera_data;
                    TargetGroup.m_Targets[i].weight = 1;
                    TargetGroup.m_Targets[i].radius = 1;
                    break;

                case 2:
                    P3.Player_obj = this.CreatePlayer(P3);
                    P3.HPgage_obj = this.CreateHPgage(P3, new Vector3(HPgage_size.sizeDelta.x / 2, 10, 0));

                    //カメラのターゲットに設定
                    TargetGroup.m_Targets[i].target = P3.My_Camera_data;
                    TargetGroup.m_Targets[i].weight = 1;
                    TargetGroup.m_Targets[i].radius = 1;
                    break;

                case 3:
                    P4.Player_obj = this.CreatePlayer(P4);
                    P4.HPgage_obj = this.CreateHPgage(P4, new Vector3(Screen.width - HPgage_size.sizeDelta.x / 2, 10, 0));

                    //カメラのターゲットに設定
                    TargetGroup.m_Targets[i].target = P4.My_Camera_data;
                    TargetGroup.m_Targets[i].weight = 1;
                    TargetGroup.m_Targets[i].radius = 1;
                    break;
            }

           
            // HPを管理する
            // HPgage.GetComponent<Slider>().maxValue = player.transform.gameObject.GetComponent<Player>().HP;
            //SHPgage.GetComponent<Slider>().value = HPgage[i].GetComponent<Slider>().maxValue;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーが死んだ
        if (death_player >= 0)
        {
            // 死んだプレイヤーの生成
            //players[death_player] = (GameObject)Resources.Load("prefab/Player");
            //GameObject P = Instantiate(players[death_player], RevivalPos, Quaternion.identity);

            //switch (death_player)
            //{
            //    case 0:
            //        P1.Player_obj = this.CreatePlayer(P1);
            //        break;

            //    case 1:
            //        P2.Player_obj = this.CreatePlayer(P2);
            //        break;

            //    case 2:
            //        P3.Player_obj = this.CreatePlayer(P3);
            //        break;

            //    case 3:
            //        P4.Player_obj = this.CreatePlayer(P4);
            //        break;

            //    default:
            //        break;
            //}
            //hpgage_slider[death_player].value = players[death_player].transform.gameObject.GetComponent<Player>().Damage(players[death_player].transform.gameObject.GetComponent<Player>().HP / 10f);
            death_player = -1;
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


    public void Player_ReceiveDamage()
    {

        Debug.Log("hit");
    }

    public int destroy_p
    {
        set
        {
            death_player = value;
        }
    }

}
