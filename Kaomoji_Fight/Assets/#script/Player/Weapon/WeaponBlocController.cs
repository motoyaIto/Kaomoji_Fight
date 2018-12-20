using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using System;

abstract public class WeaponBlocController : MonoBehaviour
{
    protected PlaySceneManager PSManager_cs;//プレイシーンマネージャー
    protected string mozi;    //自分の文字

    protected GameObject Weapon;//自分のゲームオブジェクト

    protected Sprite sprite;                        //テクスチャー
    protected Transform Weapon_Sprites;             //スプライト群
    protected SpriteRenderer Weapon_SRenderer;      //武器画像を描画するレンダー
    protected bool Weapon_SRFlag = false;          //テクスチャーのα値プラス(turue)マイナス(false)
    protected bool Weapon_spriteFlag = false;       //武器の画像がある(true)ない(false)

    [SerializeField]
    protected float DamageValue = 5.0f;     //ダメージ量
    private float thrust = 1000f;           // 投擲物の推進力



    private Vector3 Death_LUpos = new Vector3(-150f, 100f, 0f);    // オブジェクトが破棄されるエリアの左上
    private Vector3 Death_RDpos = new Vector3(200f, -80f, 0f);   // オブジェクトが破棄されるエリアの右下

    

    private string parentName;              //親の名前

    public string owner;                   //所有者の名前
    protected Player owner_cs;              //所有者のplayerスクリプト
    protected bool weapon_use = false;      //武器を投げた(true)投げてない(false)

    private GameObject hitEffect;           // ヒットエフェクト

    // 音
    private AudioSource As;
    private AudioClip ac;
    
    // Use this for initialization
    protected virtual void Awake()
    {
        //自分の文字
        mozi = this.transform.GetChild(0).GetComponent<TextMeshPro>().text;

        Weapon_Sprites = this.transform.GetChild(1);
        Weapon_SRenderer = Weapon_Sprites.GetChild(0).GetComponent<SpriteRenderer>();

        this.enabled = false;
    }

    protected virtual void OnEnable()
    {
        //所有者のスクリプト
        owner_cs = this.transform.parent.GetComponent<Player>();

        PSManager_cs = GameObject.Find("PlaySceneManager").GetComponent<PlaySceneManager>();
        hitEffect = Resources.Load<GameObject>("prefab/Effect/Wave_01");

        Weapon = this.transform.gameObject;
       
        // 持たれているプレイヤーを取得
        //parent = this.transform.parent.GetComponent<Player>();
        parentName = this.transform.parent.GetComponent<Player>().name;

        // タグの設定
        this.tag = "Weapon";

        // レイヤーの変更
        Weapon.layer = LayerName.Weapon;       
    }

    public virtual void Update()
    {
        // 飛んでったブロックの削除
        if (this.transform.position.x < Death_LUpos.x || this.transform.position.x > Death_RDpos.x || this.transform.position.y > Death_LUpos.y || this.transform.position.y < Death_RDpos.y)
        {
            Destroy(this.transform.gameObject);
        }

        //テクスチャーの画像点滅
        if(Weapon_SRenderer.enabled == true && Weapon_spriteFlag == true)
        {
            //α値の加算
            if(Weapon_SRFlag == true)
            {
                Weapon_SRenderer.color = new Vector4(Weapon_SRenderer.color.r, Weapon_SRenderer.color.g, Weapon_SRenderer.color.b, Weapon_SRenderer.color.a + 0.01f);

                if(Weapon_SRenderer.color.a >=  0.9f)
                {
                    Weapon_SRFlag = false;
                }
            }
            else//α値のマイナス
            {
                Weapon_SRenderer.color = new Vector4(Weapon_SRenderer.color.r, Weapon_SRenderer.color.g, Weapon_SRenderer.color.b, Weapon_SRenderer.color.a - 0.01f);

                if (Weapon_SRenderer.color.a <= 0.01f)
                {
                    Weapon_SRFlag = true;
                }
            }
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    public void Attack(Vector3 shot)
    {
        if(AttackMozi(shot) == true) { return; }

        this.SpecifiedOperation_NoneWeapon(shot);
    }

    /// <summary>
    /// 攻撃文字
    /// </summary>
    /// <param name="shot">使用した座標</param>
    /// <returns>文字攻撃をしたら(true)していなかったら(fasle)</returns>
    protected abstract bool AttackMozi(Vector3 shot);

    /// <summary>
    /// 動作指定のない武器
    /// </summary>
    /// <param name="shot">使用した座標</param>
    protected void SpecifiedOperation_NoneWeapon(Vector3 shot)
    {
        Rigidbody2D　rig2d = Weapon.AddComponent<Rigidbody2D>();
        rig2d.gravityScale = .01f;

        owner_cs.ChangeWeapon_Data = false;

        // 親から離れる
        this.transform.parent = null;

        //ウェポンにボックスコライダーをオンにする
        Weapon.GetComponent<BoxCollider2D>().enabled = true;
        Weapon.GetComponent<BoxCollider2D>().isTrigger = true;
        weapon_use = true;

        // 動かずに投げたら
        if (shot == Vector3.zero)
        {
            // 上に投げる
            shot = Vector3.up;
        }
        // ⊂二二二（ ＾ω＾）二⊃ ﾌﾞｰﾝ
        rig2d.AddForce(shot * thrust);

        
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (CheckHit_Rival(collision) == true)
        {
            //プレイヤーにダメージを与える
            PSManager_cs.Player_ReceiveDamage(collision.gameObject, this.gameObject, collision.GetComponent<Player>().PlayerNumber_data);

            //エフェクト
            var hitobj = Instantiate(hitEffect, this.transform.position + transform.forward, Quaternion.identity) as GameObject;
            Destroy(this.gameObject);
            weapon_use = false;
        }
    }

    protected bool CheckHit_Rival(Collider2D collider)
    {
        if (parentName != collider.gameObject.name && weapon_use && collider.transform.tag != "Stage")
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 渡された処理を指定時間後に実行する
    /// </summary>
    /// <param name="waitTime">遅延時間</param>
    /// <param name="action">実行する処理</param>
    /// <returns></returns>
    protected IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    //座標を入れる
    public Vector3 SetPosition
    {
        set
        {
            this.transform.position = value;
        }
    }

    public float DamageValue_Data
    {
        get
        {
            return DamageValue;
        }
    }


    public string Owner_Data
    {
        set
        {
            owner = value;
        }

        get
        {
            return owner;
        }
    }
}
