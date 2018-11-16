using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankingData
{
    [SerializeField]
    private string PlayerName = null;   //名前
    [SerializeField]
    private Sprite PlayerFace = null;   //顔
    [SerializeField]
    private int AttackDamage = 0;     //攻撃したダメージ数

    public string PlayerName_data
    {
        set
        {
            if(PlayerName == null)
            {
                PlayerName = value;
            }
        }

        get
        {
            return PlayerName;
        }
    }

    public Sprite PlayerFace_data
    {
        set
        {
            if(PlayerFace == null)
            {
                PlayerFace = value;
            }
        }

        get
        {
            return PlayerFace;
        }
    }

    public int AttackDamage_data
    {
        set
        {
            AttackDamage = value;
        }

        get
        {
            return AttackDamage;
        }
    }
}
