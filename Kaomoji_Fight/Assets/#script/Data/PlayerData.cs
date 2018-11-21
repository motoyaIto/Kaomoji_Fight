using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData{

    private GameObject m_Player;
    private GameObject m_HPgage;

    [SerializeField]
    private string m_name;          //プレイヤー名
    [SerializeField]
    private Color m_nameColor;      //HPバーに表示される名前の色
    private Material m_mate;        //material

    [SerializeField]
    private Sprite m_playerFace;    //プレイヤーの顔文字

    [SerializeField]
    private Vector3 m_initialPos;   //初期ポップ位置

    [SerializeField]
    private XboxController m_controller;//コントローラー

    private Transform m_myCamera = null;

    [SerializeField]
    private float m_hp = 100;//HP量

    private int m_give_damage;        // 他のプレイヤーにどれだけダメージを与えたか

    private Slider m_hpSlider;

    //コンストラクタ
    public PlayerData(string name, Color col, Sprite player_face, Vector3 initialPos, XboxController controller, float hp)
    {
        LoadGameObject();
        m_name = name;
        m_nameColor = col;
        m_playerFace = player_face;
        m_initialPos = initialPos;
        m_controller = controller;
        m_hp = hp;
        m_give_damage = 0;
    }

    public void LoadGameObject()
    {
        m_Player = (GameObject)Resources.Load("prefab/Player");
        m_HPgage = (GameObject)Resources.Load("prefab/UI/HPgage");
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
            if (m_Player == null)
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

    public Material Mate_Data
    {
        get
        {
            return m_mate;
        }
        set
        {
            m_mate = value;
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
            if (m_myCamera == null)
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

    public int DamageCount
    {
        set
        {
            m_give_damage += value;
        }
        get
        {
            return m_give_damage;
        }
    }

}
