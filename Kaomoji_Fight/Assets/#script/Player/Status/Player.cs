using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(Contoroller2d))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : RaycastController {

    #region 変数群
    // 公開
    [Header("ジャンプの高さ")]
    public float maxJumpHeight = 5f;
    public float minJumpHeight = .5f;

    [Header("ジャンプの頂点までの時間")]
    public float timeToJumpApex = .4f;

    [Header("加速時間")]
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;

    [Header("移動速度")]
    public float moveSpeed = 10;

    [SerializeField, Header("投げるものの推進力")]
    private float thrust = 2f;

    [SerializeField, Header("無敵時間")]
    private float Invincible_time = .5f;

    [Header("飛ぶ量")]
    public float maxflap = 1000f;
    public float minflap = 500f;

    [Header("幅")]
    public float scroll = 5f;


    // 非公開
    [SerializeField, Header("コントローラー番号")]
    private XboxController ControlerNamber = XboxController.First;//何番目のコントローラーを適用するか

    private float gravity;  // 重力
    private float maxJumpVelocity;  // 最大ジャンプ時の勢い
    private float minJumpVelocity;  // 最小ジャンプ時の勢い
    private Vector3 velocity;
    private float velocityXSmoothing;
    private float direction = 0;    // 方向

    private float nowHp = 100;    // プレイヤーのHP

    private GameObject weapon;
    private bool HaveWeapon = false;//武器を持っている(true)いない(false)
    private bool Avoidance = false; // 回避フラグ
    private bool jump = false;  // ジャンプ中か？

    Contoroller2d controller;   // コントローラー
    Rigidbody2D rig = null;
    [HideInInspector]
    public CollisionInfo collisions;
    private PlaySceneManager PSM;

    #endregion

    new void Start()
    {
        controller = GetComponent<Contoroller2d>();
        PSM = GameObject.Find("PlaySceneManager").transform.GetComponent<PlaySceneManager>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void Reset()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Controllerの左スティックのAxisを取得            
        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, ControlerNamber), XCI.GetAxis(XboxAxis.LeftStickY, ControlerNamber));
        if (input.x > .0f)
        {
            direction = 1f;
        }
        else if (input.x < .0f)
        {
            direction = -1f;
        }
        else
        {
            direction = 0f;
        }


        //キャラのy軸のdirection方向にscrollの力をかける
        rig.velocity = new Vector2(scroll * direction, rig.velocity.y);


        // 大ジャンプ
        if (XCI.GetButtonDown(XboxButton.Y, ControlerNamber))
        {
            //if (controller.collisions.below)
            //{
                rig.AddForce(Vector2.up * maxflap);
                this.gameObject.layer = LayerName.Through;
            //}
        }
        // 小ジャンプ
        //if (XCI.GetButtonUp(XboxButton.Y, ControlerNamber))
        //{
        //    if (velocity.y > minJumpVelocity)
        //    {
        //        rig.AddForce(Vector2.up * minflap);
        //        Debug.Log("小ジャンプ！");
        //    }
        //}        



        // 回避をしたい
        if (XCI.GetAxis(XboxAxis.RightTrigger) < 0.0f)
        {
            // 回避時間
            float Avoidance_time = .0f;
            // アニメーションに差し替え予定
            if (!Avoidance)
            {
                if (rig.velocity.x < 0.0f)
                {
                    this.transform.position += new Vector3(-5f, 0f);
                }
                else if (rig.velocity.x > 0.0f)
                {
                    this.transform.position += new Vector3(5f, 0f);
                }
                Avoidance = true;
            }

            // 回避中であれば
            if (Avoidance_time <= Invincible_time)
            {
                // 攻撃を受け付けない


                
                Avoidance_time += .1f;
            }

        }
        else if (XCI.GetAxis(XboxAxis.RightTrigger) == .0f)
        {
            Avoidance = false;
        }
        
        //武器を持っている
        if (HaveWeapon == true)
        {
            //武器の位置を調整
            WeaponBlocController WBController = weapon.gameObject.GetComponent<WeaponBlocController>();
            Vector3 direction = Vector3.zero;
            if (velocity.x < 0.0f)
            {
                direction = Vector3.left;
                WBController.SetPosition = new Vector3(this.transform.position.x - this.transform.localScale.x, this.transform.position.y + this.transform.localScale.y * 2.5f, 0.0f);
            }
            else if (velocity.x > 0.0f)
            {
                direction = Vector3.right;
                WBController.SetPosition = new Vector3(this.transform.position.x + this.transform.localScale.x + 0.5f, this.transform.position.y + this.transform.localScale.y * 2.5f, 0.0f);
            }

            //武器を使う
            if (XCI.GetButtonDown(XboxButton.B, ControlerNamber) && controller.collisions.below)
            {
                WeaponBlocController WB = weapon.GetComponent<WeaponBlocController>();

                WB.Attack(direction, thrust);

                HaveWeapon = false;

            }

            // 武器を捨てる
            if (XCI.GetButton(XboxButton.X, ControlerNamber))
            {
                Destroy(weapon);

                HaveWeapon = false;

            }
        }


            // Ｒａｙ
            this.RayController();


        // 落ちた時の対処
        if (this.transform.position.y <= -50)
        {
            Destroy(this.transform.gameObject);
        }
    }


    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public int faceDir;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }


    /// <summary>
    /// rayを飛ばしてアイテムを取得する
    /// </summary>
    private void RayController()
    {
        // 武器をゲットするかも
        if (XCI.GetButtonDown(XboxButton.B, ControlerNamber))
        {
            //rayの開始地点
            Vector3 ray_initial = new Vector3(this.transform.position.x, this.transform.position.y - this.transform.localScale.y, this.transform.position.x);

            //rayを生成
            Ray2D ray = new Ray2D(ray_initial, Vector2.down);
            //rayを可視化する
            Debug.DrawRay(ray.origin, ray.direction * 0.5f, Color.green);

            //rayに当たったものを取得する
            RaycastHit2D hitFoot = Physics2D.Raycast(ray.origin, Vector2.down, ray.direction.y * 0.5f);
           
            //ステージから武器に変換
            if(hitFoot.transform.tag == "Stage")
            {
                this.GetWeapon(hitFoot);
            }
        }
    }

    /// <summary>
    ///  武器を獲得する
    /// </summary>
    /// <param name="hitFoot">足元にあった武器</param>
    /// <param name="directionX">右か左か</param>
    private void GetWeapon(RaycastHit2D hitFoot)
    {
        GameObject block = hitFoot.collider.gameObject;
        BlockController block_cs = block.GetComponent<BlockController>();

        //武器を持っていなかったら
        if (HaveWeapon == false && block_cs.Weapon == true)
        {
            //床を武器として取得
            weapon = Object.Instantiate(block) as GameObject;
            weapon.transform.parent = this.transform;
            weapon.name = "WeaponBlock" + block.name.Substring(block.name.IndexOf("("));
            weapon.tag = tag.Trim();
            //武器のスクリプトに張り替える
            Destroy(weapon.GetComponent<BlockController>());
            weapon.AddComponent<WeaponBlocController>();

            //床から切り抜く
            block.GetComponent<BlockController>().ChangeWeapon();

            Destroy(weapon.GetComponent<BoxCollider2D>());

            HaveWeapon = true;

            Debug.Log(this.transform.GetComponent<SpriteRenderer>().sprite.texture.texelSize.y);//0.004739337
            Debug.Log(this.transform.GetComponent<SpriteRenderer>().sprite.texture.height);//211
            Debug.Log(this.transform.GetComponent<SpriteRenderer>().sprite.texture.width);//584
            Debug.Log(this.transform.GetComponent<SpriteRenderer>().sprite.vertices[0].y);//0.535
            Debug.Log(this.transform.GetComponent<SpriteRenderer>().sprite.vertices[1].y);//-0.653
            Debug.Log(this.transform.GetComponent<SpriteRenderer>().sprite.vertices[2].y);//0.535
            Debug.Log(this.transform.GetComponent<SpriteRenderer>().sprite.vertices[3].y);//0.535
            Debug.Log(this.transform.GetComponent<SpriteRenderer>().sprite.vertices[4].y);//0.535
            weapon.transform.position = new Vector3(this.transform.position.x, this.transform.position.y +this.transform.GetComponent<SpriteRenderer>().sprite.rect.height * this.transform.GetComponent<SpriteRenderer>().transform.localScale.y * this.transform.localScale.y / 2, this.transform.position.z);
            //プレイヤーの移動する向きに合わせて位置を調整
            //if (direction >=1)//右
            //{

            //    weapon.transform.position = new Vector3(0.5f, this.transform.position.y + this.transform.localScale.y * 2, 0.0f);
            //    Debug.Log("右");
            //}

            //else if(direction <= 1)//左
            //{
            //    weapon.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x + 0.5f, this.transform.position.y + this.transform.localScale.y * 2, 0.0f);
            //    Debug.Log("左");
            //}
            //else//移動していない
            //{
            //    weapon.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + this.transform.localScale.y, 0.0f);
            //    Debug.Log("中心");
            //}
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ダメージ判定
        if(collision.transform.tag == "Weapon")
        {
            PSM.Player_ReceiveDamage();
        }
        //velocity = rig.velocity;
        //if (velocity.y <= 0)
        //{
        //    velocity.y = 6;
        //    rig.velocity = velocity;
        //}
    }

    private void OnDisable()
    {
        PSM.destroy_p = CNConvert(ControlerNamber);
    }

    // Controllerの番号をint型で取得
    private int CNConvert(XboxController controlerNum)
    {
        switch (controlerNum)
        {
            case XboxController.First:
                return 0;
            case XboxController.Second:
                return 1;
            case XboxController.Third:
                return 2;
            case XboxController.Fourth:
                return 3;
            default:
                break;
        }

        return -1;
    }


    // ダメージを受ける
    public float Damage(float damage)
    {
        return nowHp - damage;
    }


    // Hpのゲッターセッター
    public float HP
    {
        get
        {
            return nowHp;
        }
    }

    /// <summary>
    /// XBXcontrollerの番号を取得
    /// </summary>
    public XboxController GetControllerNamber
    {
        set
        {
            ControlerNamber = value;
        }
    }


}
