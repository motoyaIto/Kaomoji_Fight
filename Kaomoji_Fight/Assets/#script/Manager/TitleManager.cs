using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour{

    enum SELECTMODE
    {
        TITLE,
        PLAYNAM,
        STAGE,
        CHARACTER,

        MAX
    }
    private SELECTMODE mode = SELECTMODE.TITLE;//選択されている画面のモード

    private bool ControllerLock = false;//コントローラーをロックする(true)しない(false)
    private bool Flickpage = true;     //次のページ(true)前のページ(false)

    [SerializeField]
    private GameObject Cursor;      //カーソル
    [SerializeField]
    private GameObject Note_Gizmo;  //ノートを回す中心
    [SerializeField]
    private float Flickspd = 0.1f;  //ノートがめくれるスピード
    private Quaternion Note_move;

    [SerializeField]
    private GameObject[] PlayersNumSelect_texts;    //プレイヤー人数セレクト画面のオブジェクト
    private Vector3[] PlayersNumSelect_texts_pos;   //プレイヤー人数セレクト画面のオブジェクトの座標

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
        if (ControllerLock == true)
        {
            Flick();
        }
        else
        {
            switch (mode)
            {
                case SELECTMODE.TITLE:
                    break;
                case SELECTMODE.PLAYNAM:
                    Debug.Log("PLAYERNAM");
                    this.PlayerNumController_Data();
                    break;
                case SELECTMODE.STAGE:


                    break;
                case SELECTMODE.CHARACTER:
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

        cursor_cs.Target_pos_data = PlayersNumSelect_texts_pos;
    }

    /// <summary>
    /// 1つ前または次のページに変える
    /// </summary>
    /// <param name="nextPage">次のページか前のページか</param>
    public void ChangePage(int nextPage)
    {
        //1ページより多くめくろうとしていたら1ページに変える
        if(nextPage < -1)
        {
            Flickpage = false;
            nextPage = -1;
        }
        if(nextPage > 1)
        {
            Flickpage = true;
            nextPage = 1;
        }

        //最小又は最大のページより先に行かないようにする
        if(mode == SELECTMODE.TITLE && nextPage <= -1)
        {
            return;
        }
        if(mode == SELECTMODE.MAX && nextPage >= 1)
        {
            return;
        }
        //コントローラーの操作をロック
        ControllerLock = true;

        //ページを更新
        mode += nextPage;
    }
    
    /// <summary>
    /// ページをめくる
    /// </summary>
    private void Flick()
    {
        //Debug.Log(90 * (int)mode * Mathf.Deg2Rad);
        //次のページ
        if (Flickpage == true)
        {
            if (Note_Gizmo.transform.rotation.y * Mathf.Rad2Deg <  90 * (int)mode)
            {
                Note_move = Note_Gizmo.transform.rotation;
                Note_move.y += Flickspd;
                Note_Gizmo.transform.rotation = Note_move;
            }
            else
            {
                ControllerLock = false;
            }

            return;
        }

        if (Flickpage == false)//前のページ
        {
            if (Note_Gizmo.transform.rotation.y > 90 * (int)mode)
            {
                Note_move = Note_Gizmo.transform.rotation;
                Note_move.y -= Flickspd;
                Note_Gizmo.transform.rotation = Note_move;
            }
            else
            {
                ControllerLock = false;
            }

            return;
        }
    }

    public bool ControllerLock_data
    {
        get
        {
            return ControllerLock;
        }
    }


}
