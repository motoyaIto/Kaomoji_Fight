using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour{

    public enum SELECTMODE
    {
        TITLE,
        PLAYERNAM,
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
    private GameObject Title_Gizmo;         //ノートを回すギズモ
    [SerializeField]
    private GameObject Playernam_Gizmo;     //プレイヤー人数セレクトを回すギズモ
    [SerializeField]
    private GameObject Stageselect_Gizmo;   //ステージセレクトを回すギズモ
    [SerializeField]
    private GameObject Characterselect_Gizmo;     //キャラクターセレクトを回すギズモ

    [SerializeField]
    private float Flickspd = 0.1f;  //ノートがめくれるスピード
    private float count_rotation;   //回転角を記憶する
    //private Quaternion Note_move;

    [SerializeField]
    private GameObject[] PlayersNumSelect_texts;    //プレイヤー人数セレクト画面のオブジェクト
    private Vector3[] PlayersNumSelect_texts_pos;   //プレイヤー人数セレクト画面のオブジェクトの座標

    [SerializeField]
    private GameObject Cursor;      //カーソル
    [SerializeField]
    private GameObject[] StageSelect_texts;     //ステージセレクト画面のオブジェクト

    [SerializeField]
    private GameObject[] CharaSelect_texts;     //キャラクターセレクト画面のオブジェクト


    private PlayData playedata;         //プレイデータ

    private int playerNum;              //プレイヤーの合計人数
    private string Stage_name = null;   //選択したステージ
    private Sprite[] playersface = null;//プレイヤーの顔文字
    private string[] players_name = { "P1", "P2", "P3", "P4" };//各プレイヤーの名前


    private bool GamePlay_flag = false;//プレイスタートボタンを表示(true)非表示(false)

    // Use this for initialization
    void Start() {
        this.PlayerNumController_Data();
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
            case SELECTMODE.PLAYERNAM:
                Debug.Log("PLAYERNAM");
                
                break;
            case SELECTMODE.STAGESELECT:


                break;
            case SELECTMODE.CHARACTERSELECT:
                break;
            case SELECTMODE.MAX:

                playersface = new Sprite[playerNum];

                for (int i = 0; i < playerNum; i++)
                {
                    playersface[i] = Sprite.Create((Texture2D)Resources.Load("textures/use/Player" + (i + 1)), new Rect(0, 0, 584, 211), new Vector2(0.5f, 0.5f));
                }
                playedata = new PlayData(Stage_name, players_name, playersface);

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





    /// <summary>
    /// プレイヤーの合計人数を選択するときのカーソルのデータ
    /// </summary>
    private void PlayerNumController_Data()
    {
        //プレイヤーの合計人数選択テキストのワールド座標を取得する
        PlayersNumSelect_texts_pos = new Vector3[PlayersNumSelect_texts.Length];

        for (int i = 0; i < PlayersNumSelect_texts.Length; i++)
        {
            //カーソルの初期座標
            PlayersNumSelect_texts_pos[i] = Cursor.transform.position;
            //y座標をtxtの座標に修正
            PlayersNumSelect_texts_pos[i].y = PlayersNumSelect_texts[i].transform.position.y;
        }

        //カーソルにtxtの座標を伝える
        CursorController cursor_cs = Cursor.GetComponent<CursorController>();

    }

   
    public SELECTMODE Mode_data
    {
        get
        {
            return mode;
        }
    }
}
