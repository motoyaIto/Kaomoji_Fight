using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBlocController : MonoBehaviour {

    [SerializeField, Header("破棄されるエリア（左上）")]
    private Vector3 Death_LUpos = new Vector3(-40f, 15f, 0f);    // 飛んでったオブジェクトが破棄されるエリアの左上
    [SerializeField, Header("破棄されるエリア（右下）")]
    private Vector3 Death_RDpos = new Vector3(100f, -20f, 0f);   // 飛んでったオブジェクトが破棄されるエリアの右下

    private Player player;
    private Vector3 Shot = Vector3.zero;    //打つ方向
    private float Thrust = 0.0f;            //推進力
    private bool AttackFlag = false;            //攻撃する(true)しない(false)

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(AttackFlag)
        {
            Rigidbody2D rb2 = this.GetComponent<Rigidbody2D>();

            if(Shot == Vector3.left)
            {
                rb2.AddForce(Vector2.left * Thrust, ForceMode2D.Impulse);
            }
            else
            {
                rb2.AddForce(Vector2.right * Thrust, ForceMode2D.Impulse);
            }
        }

        // 飛んでったブロックの削除
        if (this.transform.position.x < Death_LUpos.x || this.transform.position.x > Death_RDpos.x)
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
        this.transform.gameObject.AddComponent<Rigidbody2D>();
        this.transform.gameObject.AddComponent<BoxCollider2D>();
        this.transform.parent = null;
        Shot = shot;
        Thrust = thrust;
        AttackFlag = true;
        this.tag = "Weapon";
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
