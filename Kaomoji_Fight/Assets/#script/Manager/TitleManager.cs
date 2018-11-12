using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class TitleManager : MonoBehaviour{

    public enum SELECTMODE
    {
        TITLE,
        PLAYERNUM,
        STAGESELECT,
        CHARACTERSELECT,

        MAX
    }
    private SELECTMODE mode = SELECTMODE.TITLE;//選択されている画面のモード
    private SELECTMODE Nextmode;               //次のページ

    private bool ControllerLock = false;//コントローラーをロックする(true)しない(false)
    private bool Flickpage = true;     //次のページ(true)前のページ(false)

    //各ギズモ
    [SerializeField]
    private GameObject Title_Gizmo;             //ノートを回すギズモ
    [SerializeField]
    private GameObject Playernam_Gizmo;         //プレイヤー人数セレクトを回すギズモ
    [SerializeField]
    private GameObject Stageselect_Gizmo;       //ステージセレクトを回すギズモ
    [SerializeField]
    private GameObject Characterselect_Gizmo;   //キャラクターセレクトを回すギズモ

    [SerializeField]
    private float Flickspd = 0.1f;      //ノートがめくれるスピード
    private float count_rotation;       //回転角を記憶する

    private PlayData playdata;         //プレイデータ

    private int playerNum;              //プレイヤーの合計人数
    private string Stage_name = null;   //選択したステージ
    [SerializeField]
    private Sprite[] playersface = null;//プレイヤーの顔文字
    private string[] players_name = { "P1", "P2", "P3", "P4" };//各プレイヤーの名前
    private Color[] Players_color = { Color.red, Color.green, Color.blue, Color.yellow };//プレイヤーの色

    private PlayerData[] playerdata = null;


    private bool GamePlay_flag = false;//プレイスタートボタンを表示(true)非表示(false)

    // Use this for initialization
    void Start() {

    }
	// Update is called once per frame
	void Update () {
 
        switch (mode)
        {
            case SELECTMODE.TITLE:
                if(ControllerLock == true)
                {
                    if(Title_Gizmo.GetComponent<Gizmo>().Flickpage(Flickpage) == false)
                    {
                        ControllerLock = false;

                        mode = Nextmode;
                    }
                }
               
                break;
            case SELECTMODE.PLAYERNUM:
                if (ControllerLock == true)
                {
                    if (Playernam_Gizmo.GetComponent<Gizmo>().Flickpage(Flickpage) == false)
                    {
                        ControllerLock = false;

                        mode = Nextmode;
                    }
                }

                break;
            case SELECTMODE.STAGESELECT:
                if (ControllerLock == true)
                {
                    //if (Stageselect_Gizmo.GetComponent<Gizmo>().Flickpage(Flickpage) == false)
                    //{
                    //    ControllerLock = false;

                    //    mode = Nextmode;
                    //}

                    mode = SELECTMODE.MAX;
                    ControllerLock = false;
                }

             
                break;
            case SELECTMODE.CHARACTERSELECT:

                break;
            case SELECTMODE.MAX:

                //playersface = new Sprite[playerNum];

                //for (int i = 0; i < playerNum; i++)
                //{
                //    playersface[i] = Sprite.Create((Texture2D)Resources.Load("textures/use/Player" + (i + 1)), new Rect(0, 0, 584, 211), new Vector2(0.5f, 0.5f));
                //}
                ////playedata = new PlayData(Stage_name, players_name, playersface);

                //SceneManagerController.ChangeScene();

                CreatePlayer_data();

                playdata = new PlayData(Stage_name, playerdata);

                SceneManagerController.ChangeScene();
                break;
        }
        
		
	}

    /// <summary>
    /// 1つ前または次のページに変える
    /// </summary>
    /// <param name="nextPage">次のページか前のページか</param>
    public void ChangePage(SELECTMODE nextPageName)
    {
        //今のページを再度開こうとしたら
        if (nextPageName == mode)
        {
            return;
        }
            //1ページより多くめくろうとしていたら1ページに変える
            if (nextPageName < mode - 1)
        {
            Flickpage = false;
            nextPageName = mode - 1;
        }
        if (nextPageName > mode + 1)
        {
            Flickpage = true;
            nextPageName = mode + 1;
        }

       
        //最小又は最大のページより先に行かないようにする
        if (nextPageName == SELECTMODE.TITLE - 1)
        {
            return;
        }
        if (nextPageName == SELECTMODE.MAX + 1)
        {
            return;
        }
        //コントローラーの操作をロック
        ControllerLock = true;

        //次のページを保存
        Nextmode = nextPageName;
    }


    public void CreatePlayer_data()
    {
        playerdata = new PlayerData[playerNum];

        for(int i = 0; i < playerNum; i++)
        {
            playerdata[i] = new PlayerData(players_name[i], Players_color[i], playersface[i], new Vector3(10 * (i + 1), 50, 0), XboxController.First + i, 100);
        }
    }
   
    public SELECTMODE Mode_Data
    {
        get
        {
            return mode;
        }
    }
    public bool ControllerLock_Data
    {
        get
        {
            return ControllerLock;
        }
    }

    public int PlayerNum_Data
    {
        set
        {
           playerNum = value;
        }
    }

    public string Stage_name_Data
    {
        set
        {
            Stage_name = value;
        }
    }
}
