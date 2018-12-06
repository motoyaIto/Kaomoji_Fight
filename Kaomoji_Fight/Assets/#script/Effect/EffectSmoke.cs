using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSmoke : MonoBehaviour {

    private PlaySceneManager PSManager;     //プレイシーンマネージャー
    GameObject FireEffect;                  //炎が舞い上がるエフェクト
    private string owner;                   //オーナーの名前
    private float DamageValue = 5.0f;       //ダメージ量

    private void Start()
    {
        FireEffect = Resources.Load<GameObject>("prefab/Effect/FireMoment");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //爆発
        FireEffect = Instantiate(FireEffect, this.transform) as GameObject;
        FireEffect.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 2f);
    }

    public PlaySceneManager PSManager_Data
    {
        set
        {
            PSManager = value;
        }
    }

    public string Owner_Data
    {
        set
        {
            owner = value;
        }
    }

    public float DamageValue_Data
    {
        set
        {
            DamageValue = value;
        }
    }
}
