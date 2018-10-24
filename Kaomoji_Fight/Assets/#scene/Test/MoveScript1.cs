using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript1 : MonoBehaviour {

    //[Header("移動速度に対する追従度")]
    //public float moveForceMultiplier = 0f;
    //[Header("移動速度")]
    //public float moveSpeed = 10f;

    //private Rigidbody2D rbody;
    //private AudioSource audio;
    //private float XaxisInput;
    //private float YaxisInput;

    //// Use this for initialization
    //void Start()
    //{
    //    rbody = GetComponent<Rigidbody2D>();
    //    audio = GetComponent<AudioSource>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    XaxisInput = Input.GetAxis("Horizontal");
    //    YaxisInput = Input.GetAxis("Vertical");
    //}

    //private void FixedUpdate()
    //{
    //    Vector2 movevector = Vector2.zero;


    //    if (XaxisInput != .0f)
    //    {
    //        movevector.x = moveSpeed * XaxisInput;
    //    }
    //    else
    //    {
    //        movevector.x = .0f;
    //    }

    //    if (YaxisInput != .0f)
    //    {
    //        movevector.y = moveSpeed * YaxisInput;
    //    }
    //    else
    //    {
    //        movevector.y = .0f;
    //    }

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        rbody.AddForce(transform.up * 10f);
    //    }

    //    rbody.AddForce(moveForceMultiplier * (movevector - rbody.velocity));

    //}


    //変数定義
    public float flap = 1000f;
    public float scroll = 5f;
    float direction = 0f;
    Rigidbody2D rb2d;
    bool jump = false;

    // Use this for initialization
    void Start()
    {
        //コンポーネント読み込み
        rb2d = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {

        //キーボード操作
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -1f;
        }
        else
        {
            direction = 0f;
        }


        //キャラのy軸のdirection方向にscrollの力をかける
        rb2d.velocity = new Vector2(scroll * direction, rb2d.velocity.y);

        //ジャンプ判定
        if (Input.GetKeyDown("space") && !jump)
        {
            rb2d.AddForce(Vector2.up * flap);
            jump = true;
        }


    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            jump = false;
        }
    }
}
