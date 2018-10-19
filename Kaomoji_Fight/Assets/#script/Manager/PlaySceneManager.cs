using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using XboxCtrlrInput;
using TMPro;



public class PlaySceneManager : MonoBehaviour
{
    //プレイヤーのデータを管理する
    [System.Serializable]
    private class Player_data
    {
        [SerializeField]
        private string m_name;          //プレイヤー名
        [SerializeField]
        private Color m_nameColor;      //HPバーに表示される名前の色

        [SerializeField]
        private Sprite m_playerFace;//プレイヤーの顔文字

        [SerializeField]
        private Vector3 m_initialPos;   //初期ポップ位置


        private Slider m_hpgage_slider; //HPスライダー

        //コンストラクタ
        public Player_data(string name, Color col, Sprite player_face, Vector3 initialPos)
        {
            m_name = name;
            m_nameColor = col;
            m_playerFace = player_face;
            m_initialPos = initialPos;
            m_hpgage_slider = null;
        }
        
        //getter:seter
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

        public Sprite PlayerFace
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

        public Slider Hpgage_slider
        {
            set
            {
                if(m_hpgage_slider == null)
                {
                    m_hpgage_slider = value;
                }
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

    private GameObject[] players;       //プレイヤー
    private GameObject[] HPgage;        //HPゲージ

    private Player playerCS;

    private int death_player;

    // Use this for initialization
    void Start()
    {
        playerCS = GetComponent<Player>();
        death_player = -1;


        //プレイヤー分の配列を確保
        players = new GameObject[PlayData.Instance.playerNum];
        HPgage = new GameObject[PlayData.Instance.playerNum];

        //P1 = new Player_data(PlayData.Instance.PlayersName, new Color(0.000f, 0.000f, 0.000f), PlayData.Instance.PlayersFace, );

        //プレイヤーとHPを生成
        for (int i = 0; i < PlayData.Instance.playerNum; i++)
        {
            //プレイヤーとHPバーを生成
            switch (i)
            {
                case 0:
                    players[i] = this.CreatePlayer( HPgage[i], P1);
                    break;
                case 1:
                    break;
                case 2:
                    break;

                case 3:
                    break;
            }
            ////画像が送られてきていなかったら
            //if (player_textuer == null)
            //{
            //    players[i] = (GameObject)Resources.Load("prefab/Player");
            //    HPgage[i] = (GameObject)Resources.Load("prefab/UI/HPgage");
            //}
            //else
            //{
            //    players[i] = player_textuer[i];
            //    HPgage[i] = player_textuer[i];
            //}




            //// HPを管理する
            //HPgage[i].GetComponent<Slider>().maxValue = players[i].transform.gameObject.GetComponent<Player>().HP;
            //HPgage[i].GetComponent<Slider>().value = HPgage[i].GetComponent<Slider>().maxValue;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーが死んだ
        if (death_player >= 0)
        {
            // 死んだプレイヤーの生成
            players[death_player] = (GameObject)Resources.Load("prefab/Player");
            GameObject P = Instantiate(players[death_player], RevivalPos, Quaternion.identity);

            switch (death_player)
            {
                case 0:
                    this.SetPlayerStatus(P, XboxController.First, P1.Name_Data, PlayData.Instance.PlayersFace[death_player]);
                    break;
                case 1:
                    this.SetPlayerStatus(P, XboxController.Second, P2.Name_Data, PlayData.Instance.PlayersFace[death_player]);
                    break;
                case 2:
                    this.SetPlayerStatus(P, XboxController.Third, P3.Name_Data, PlayData.Instance.PlayersFace[death_player]);
                    break;
                case 3:
                    this.SetPlayerStatus(P, XboxController.Fourth, P4.Name_Data, PlayData.Instance.PlayersFace[death_player]);
                    break;
                default:
                    break;
            }
            //hpgage_slider[death_player].value = players[death_player].transform.gameObject.GetComponent<Player>().Damage(players[death_player].transform.gameObject.GetComponent<Player>().HP / 10f);
            death_player = -1;
        }
    }

    /// <summary>
    /// プレイヤーを生成
    /// </summary>
    /// <param name="player">プレイヤーオブジェクトデータ</param>
    /// <param name="i">何番目のプレイヤーか</param>
    private GameObject CreatePlayer(GameObject HPgage, Player_data player_data)
    {
        //プレイヤーを生成
        GameObject player = Instantiate((GameObject)Resources.Load("prefab/Player"), player_data.InitialPos_Data, Quaternion.identity);
        //プレイヤーの設定
        this.SetPlayerStatus(player, XboxController.First, "P1", PlayData.Instance.PlayersFace[0]);
        //HPゲージの生成
        //this.CreateHPgage(HPgage, player.name, i);

        return player;

       
    } 
    /// <summary>
    /// プレイヤーのステータスを設定する
    /// </summary>
    /// <param name="player">プレイヤー</param>
    /// <param name="controllerNamber">コントローラー番号</param>
    private void SetPlayerStatus(GameObject player, XboxController controllerNamber, string name, Sprite FaceTextures)
    {
        //キャラの顔をセット
        SpriteRenderer playerFace = player.GetComponent<SpriteRenderer>();
        playerFace.sprite = FaceTextures;

        //名前
        player.name = name;

        //コントローラーをセット
        Player playerScript = player.GetComponent<Player>();
        playerScript.GetControllerNamber = controllerNamber;

        //カメラのターゲット用ダミーを取得する
        name += "_dummy";

        foreach(Transform child in this.transform)
        {
            if(child.name == name)
            {
                child.transform.parent = null;

                child.transform.parent = player.gameObject.transform;

                child.transform.position = player.transform.position;
                break;
            }
        }
    }

    /// <summary>
    /// HPゲージの生成
    /// </summary>
    /// <param name="HPgage">HPゲージ</param>
    /// <param name="i">何番目か</param>
    private void CreateHPgage(GameObject HPgage, string name, int i)
    {
        RectTransform size = HPgage.GetComponent<RectTransform>();

        //switch (i)
        //{
        //    case 0://元のトランス(281.5, 124)
        //        GameObject P1_HPgage = Instantiate(HPgage, new Vector3(size.sizeDelta.x / 2, Screen.height - 10, 0f), Quaternion.identity, UICanvases.transform);

        //        P1_HPgage.name = "P1_HPgage";

        //        //名前の設定
        //        TextMeshProUGUI P1name = P1_HPgage.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        //        P1name.text = name;
        //        P1name.color = P1_nameColor;

        //        break;

        //    case 1:
        //        GameObject P2_HPgage = Instantiate(HPgage, new Vector3(Screen.width - size.sizeDelta.x / 2, Screen.height - 10, 0), Quaternion.identity, UICanvases.transform);

        //        P2_HPgage.name = "P2_HPgage";

        //        //名前の設定
        //        TextMeshProUGUI P2name = P2_HPgage.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        //        P2name.text = name;
        //        P2name.color = P2_nameColor;
        //        break;

        //    case 2:
        //        GameObject P3_HPgage = Instantiate(HPgage, new Vector3(size.sizeDelta.x / 2, 10, 0), Quaternion.identity, UICanvases.transform);

        //        P3_HPgage.name = "P3_HPgage";

        //        //名前の設定
        //        TextMeshProUGUI P3name = P3_HPgage.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        //        P3name.text = name;
        //        P3name.color = P3_nameColor;
        //        break;

        //    case 3:
        //        GameObject P4_HPgage = Instantiate(HPgage, new Vector3(Screen.width - size.sizeDelta.x / 2, 10, 0), Quaternion.identity, UICanvases.transform);

        //        P4_HPgage.name = "P4_HPgage";

        //        //名前の設定
        //        TextMeshProUGUI P4name = P4_HPgage.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        //        P4name.text = name;
        //        P4name.color = P4_nameColor;
        //        break;
        //}
    }


    /// <summary>
    /// 各プレイヤーの画像
    /// </summary>
    //public GameObject[] Player_textuer
    //{
    //    set
    //    {
    //        player_textuer = new GameObject[value.Length];
    //        player_textuer = value;
    //    }
    //}

    public int destroy_p
    {
        set
        {
            death_player = value;
        }
    }

}
