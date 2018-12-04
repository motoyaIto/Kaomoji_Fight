using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constant;

public class TaserNeedle : MonoBehaviour {

    private PlaySceneManager PSManager;     //プレイシーンマネージャー
    private string owner;                   //オーナーの名前
    private float DestroyTime = 0.15f;      //消すまでの時間
    private State state = State.Stan;       //状態異常：スタン 
    private float StanTime = 2.0f;          //スタンしている時間
    private float DamageValue = 5.0f;       //ダメージ量

    private void OnEnable()
    {
        Invoke("Destroy_ThisObj", DestroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != owner && collision.transform.tag == "Player")
        {
            //移動を止める
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            this.transform.parent = collision.gameObject.transform;

            //消す処理を止める
            CancelInvoke();

            //スタン終了後に消す
            Invoke("Destroy_ThisObj", StanTime);

            //スタンを付与する
            PSManager.Player_BatStatus(collision.gameObject, owner, DamageValue, collision.GetComponent<Player>().PlayerNumber_data, state, StanTime);
        }
    }

    /// <summary>
    /// 自分を殺す
    /// </summary>
    private void Destroy_ThisObj()
    {
        Destroy(this.gameObject);
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
