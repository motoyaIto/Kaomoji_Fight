using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneManager : MonoBehaviour{

    enum SELECTMODE
    {
        PLAYNAM,
        STAGE,
        CHARACTER,

        MAX
    }

    private SELECTMODE mode = SELECTMODE.PLAYNAM;

    private int playerNam;

    private PlayData playedata;     //プレイデータ

    private string Stage_name = null; //選択したステージ

    private string[] players_name = { "P1", "P2", "P3", "P4" };//各プレイヤーの名前

    private Sprite[] playersface = null;//プレイヤーの顔文字

    private bool GamePlay_flag = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(mode == SELECTMODE.CHARACTER)
        {
            playersface = new Sprite[playerNam];

            for(int i = 0; i < playerNam; i++)
            {
                playersface[i] = Sprite.Create((Texture2D)Resources.Load("textures/use/Player" + (i + 1)), new Rect(0, 0, 584, 211), new Vector2(0.5f, 0.5f));
            }
            playedata = new PlayData(Stage_name, players_name, playersface);

            SceneManagerController.ChangeScene();
        }
	}

    public int PlayerNam_Data
    {
        set
        {
            playerNam = value;

            mode = SELECTMODE.STAGE;
        }
    }

    public string Stage_name_Data
    {
        set
        {
            Stage_name = value;

            mode = SELECTMODE.CHARACTER;
        }
    }

    public string[] Player_name_Data
    {
        set
        {
            players_name = value;
        }
    }

    public Sprite[] Playersface_Data
    {
        set
        {
            playersface = value;

            mode = SELECTMODE.MAX;
        }
    }

}
