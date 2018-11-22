using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponBlocController : MonoBehaviour
{
    [SerializeField]
    private float DamageValue = 5.0f;

    private PlaySceneManager PSManager_cs;

    private Vector3 Death_LUpos = new Vector3(-150f, 100f, 0f);    // オブジェクトが破棄されるエリアの左上
    private Vector3 Death_RDpos = new Vector3(200f, -80f, 0f);   // オブジェクトが破棄されるエリアの右下

    private GameObject Weapon;

    private Player parent;                  //親となるプレイヤー
    private string parentName;              //↑の名前

    private Rigidbody2D rig2d;

    private bool AttackFlag = false;        //攻撃する(true)しない(false)
    private string weapon_name;             //持った武器の名前
    private string onwer;                   //所有者の名前
    private bool weapon_throw = false;      //武器を投げた(true)投げてない(false)

    private GameObject hitEffect;           // ヒットエフェクト
   

    private void Awake()
    {
        PSManager_cs = GameObject.Find("PlaySceneManager").GetComponent<PlaySceneManager>();
        hitEffect = Resources.Load<GameObject>("prefab/Effect/Wave_01");
    }

    // Use this for initialization
    void Start()
    {        
        Weapon = this.transform.gameObject;
        Weapon.AddComponent<Rigidbody2D>();

        // 持たれているプレイヤーを取得
        parent = this.transform.parent.GetComponent<Player>();
        parentName = parent.name;

        // タグの設定
        this.tag = "Weapon";

        // レイヤーの変更
        Weapon.layer = LayerName.Weapon;

        rig2d = Weapon.GetComponent<Rigidbody2D>();
        rig2d.gravityScale = .01f;

        //Debug.Log(this.name.Substring(this.name.IndexOf("("), this.name.IndexOf(")")));
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
        GameObject parent = this.transform.parent.gameObject;

        // 親から離れる
        this.transform.parent = null;

        Weapon.AddComponent<BoxCollider2D>();

        AttackFlag = true;
        weapon_throw = true;
        weapon_name = this.name;

        switch (this.transform.GetChild(0).GetComponent<TextMeshPro>().text)
        {
            case "じ":
                DamageValue = 50;
                PSManager_cs.Player_ReceiveDamage(parent, this.gameObject, 0);

                Destroy(this.gameObject);
                break;

            default:
                // 動かずに投げたら
                if (shot == Vector3.zero)
                {
                    // 上に投げる
                    shot = Vector3.up;
                }
                // ⊂二二二（ ＾ω＾）二⊃ ﾌﾞｰﾝ
                rig2d.AddForce(shot * thrust);
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (parentName != collision.gameObject.name && weapon_throw && collision.transform.tag != "Stage")
        {
            var hitobj = Instantiate(hitEffect, this.transform.position + transform.forward, Quaternion.identity) as GameObject;
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
