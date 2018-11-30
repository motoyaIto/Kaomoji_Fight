using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Explosion : MonoBehaviour {

    private PlaySceneManager PSManager;     //プレイシーンマネージャー
    private string owner_name;              //オーナーの名前
    private float DamageValue = 5.0f;       //ダメージ量
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            PSManager.AllPlayer_Damage(collision.gameObject, owner_name, DamageValue, collision.GetComponent<Player>().PlayerNumber_data);
        }
    }

    public PlaySceneManager PSManager_Data
    {
        set
        {
            PSManager = value;
        }
    }

    public float DamageValue_Data
    {
        set
        {
            DamageValue = value;
        }
    }

    public string OwnerName_Data
    {
        set
        {
            owner_name = value;
        }
    }
}
