using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

abstract public class WeaponBlocController : MonoBehaviour
{
    protected PlaySceneManager PSManager_cs;//プレイシーンマネージャー
    protected string mozi;    //自分の文字


    [SerializeField]
    protected float DamageValue = 5.0f;     //ダメージ量
    private float thrust = 1000f;           // 投擲物の推進力



    private Vector3 Death_LUpos = new Vector3(-150f, 100f, 0f);    // オブジェクトが破棄されるエリアの左上
    private Vector3 Death_RDpos = new Vector3(200f, -80f, 0f);   // オブジェクトが破棄されるエリアの右下

    private GameObject Weapon;

    private string parentName;              //親の名前

    private string owner;                   //所有者の名前
    protected bool weapon_use = false;      //武器を投げた(true)投げてない(false)

    private GameObject hitEffect;           // ヒットエフェクト

    // 音
    private AudioSource As;
    private AudioClip ac;

    // Use this for initialization
    protected virtual void Awake()
    {
        this.enabled = false;
        //自分の文字
        mozi = this.transform.GetChild(0).GetComponent<TextMeshPro>().text;

        PSManager_cs = GameObject.Find("PlaySceneManager").GetComponent<PlaySceneManager>();
        hitEffect = Resources.Load<GameObject>("prefab/Effect/Wave_01");
        
    }

    private void OnEnable()
    {
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

        this.transform.parent.GetComponent<Player>().ChangeWeapon_Data = false;

        // 親から離れる
        this.transform.parent = null;

        //ウェポンにボックスコライダーをつける
        Weapon.AddComponent<BoxCollider2D>();

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckHit_Rival(collision) == true)
        {
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

    protected bool CheckHit_Rival(Collision2D collision)
    {
        if (parentName != collision.gameObject.name && weapon_use && collision.transform.tag != "Stage")
        {
            return true;
        }

        return false;
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
