using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBlocController : MonoBehaviour {

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
        Shot = shot;
        Thrust = thrust;
        AttackFlag = true;
        
        //rb.AddForce(transform.forward * thrust);
    }
}
