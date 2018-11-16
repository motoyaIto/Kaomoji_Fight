using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingData
{
    private string PlayerName = null;   //名前
    private Sprite PlayerFace = null;   //顔

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
