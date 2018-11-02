using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBlocController : MonoBehaviour
{
    [SerializeField]
    private float DamageValue = 5.0f;

    private Vector3 Death_LUpos = new Vector3(-150f, 50f, 0f);    // オブジェクトが破棄されるエリアの左上
    private Vector3 Death_RDpos = new Vector3(200f, -60f, 0f);   // オブジェクトが破棄されるエリアの右下

    private GameObject Weapon;

    private Player parent;                  //親となるプレイヤー
    private Rigidbody2D rig2d;

    private bool AttackFlag = false;        //攻撃する(true)しない(false)
    private string weapon_name;             //持った武器の名前
    private string onwer;                   //所有者の名前
    private bool weapon_throw = false;      //武器を投げた(true)投げてない(false)

    // Use this for initialization
    void Start()
    {
        Weapon = this.transform.gameObject;
        Weapon.AddComponent<Rigidbody2D>();

        // 持たれているプレイヤーを取得
        parent = this.transform.parent.GetComponent<Player>();

        // レイヤーの変更
        Weapon.layer = LayerName.Weapon;

        rig2d = Weapon.GetComponent<Rigidbody2D>();
        rig2d.gravityScale = .01f;
    }

    // Update is called once per frame
    void Update()
    {
        // 飛んでったブロックの削除
        if (this.transform.position.x < Death_LUpos.x || this.transform.position.x > Death_RDpos.x || this.transform.position.y > Death_LUpos.y || this.transform.position.y < Death_RDpos.y)
        {
            Destroy(this.transform.gameObject);
        }

    }

    //座標を入れる
    public Vector3 SetPosition
    {
        set
        {
            this.transform.position = value;
        }
    }

    public void Attack(Vector3 shot, float thrust)
    {
        // 親から離れる
        this.transform.parent = null;

        Weapon.AddComponent<BoxCollider2D>();

        AttackFlag = true;
        weapon_throw = true;
        this.tag = "Weapon";
        weapon_name = this.name;

        // 動かずに投げたら
        if(shot == Vector3.zero)
        {
            // 上に投げる
            shot = Vector3.up;
        }
        // ⊂二二二（ ＾ω＾）二⊃ ﾌﾞｰﾝ
        rig2d.AddForce(shot * thrust);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(parent.name != collision.gameObject.name && weapon_throw && collision.transform.tag != "Stage")
        {
            Destroy(this.gameObject);
            weapon_throw = false;
        }
    }

    public float DamageValue_Data
    {
        get
        {
            return DamageValue;
        }
    }

    public string HaveWeapon_Name
    {
        get
        {
            return weapon_name;
        }
    }

    public string Owner_Data
    {
        set
        {
            onwer = value;
        }

        get
        {
            return onwer;
        }
    }
}
