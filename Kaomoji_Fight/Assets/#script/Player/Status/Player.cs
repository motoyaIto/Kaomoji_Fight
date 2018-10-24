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

    // 非公開
    [SerializeField, Header("コントローラー番号")]
    private XboxController ControlerNamber = XboxController.First;//何番目のコントローラーを適用するか

    private float gravity;  // 重力
    private float maxJumpVelocity;  // 最大ジャンプ時の勢い
    private float minJumpVelocity;  // 最小ジャンプ時の勢い
    private Vector3 velocity;
    private float velocityXSmoothing;

    private float nowHp = 100;    // プレイヤーのHP

    private GameObject weapon;
    private bool HaveWeapon = false;//武器を持っている(true)いない(false)
    private bool Avoidance = false; // 回避フラグ

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

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

    }

    private void Reset()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }


        // 大ジャンプ
        if (XCI.GetButtonDown(XboxButton.Y, ControlerNamber))
        {
            if (controller.collisions.below)
            {
                velocity.y = maxJumpVelocity;
                this.gameObject.layer = LayerName.Through;
            }
        }
        // 小ジャンプ
        if (XCI.GetButtonUp(XboxButton.Y, ControlerNamber))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }
        
        // Controllerの左スティックのAxisを取得            
        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, ControlerNamber), XCI.GetAxis(XboxAxis.LeftStickY, ControlerNamber));

        {
            float targetVelocityX = input.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
                                        (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime, input);
        }


        // 回避をしたい
        if (XCI.GetAxis(XboxAxis.RightTrigger) < 0.0f)
        {
            //Instantiate()
            float rand = Random.Range(-1.0f, 1.0f);
            // 回避時間
            float Avoidance_time = .0f;
            // アニメーションに差し替え予定
            if (!Avoidance)
            {
                if (velocity.x < 0.0f)
                {
                    this.transform.position += new Vector3(-5f, 0f);
                }
                else if (velocity.x > 0.0f)
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
        float directionX = Mathf.Sign(velocity.x);          //float型の値が正か負かを返す
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;//rayの長さを計算する

        //rayの数分回す
        for (int i = 0; i < horizontalRayCount; i++)
        {
            //右を向いているか左を向いているか
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            
            //rayを描画し始める場所
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            //rayを飛ばした先で獲得できたものを入れる
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            // 武器をゲットするかも
            if (XCI.GetButtonDown(XboxButton.B, ControlerNamber))
            {
                //Rayを伸ばす
                float rayLine = 2.0f;
                //当たっている物があれば
                RaycastHit2D hitFoot = Physics2D.Raycast(this.transform.position, -Vector2.up, rayLine);
                //当たっている物があれば
                if (hitFoot)
                {
                    this.GetWeapon(hitFoot, directionX);

                    break;
                }
            }
        }
    }

    /// <summary>
    ///  武器を獲得する
    /// </summary>
    /// <param name="hitFoot">足元にあった武器</param>
    /// <param name="directionX">右か左か</param>
    private void GetWeapon(RaycastHit2D hitFoot, float directionX)
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

            //プレイヤーの移動する向きに合わせて位置を調整
            if (directionX == -1)
            {
                weapon.transform.position = new Vector3(0.5f, this.transform.position.y + this.transform.localScale.y * 2, 0.0f);
            }
            else
            {
                weapon.transform.position = new Vector3(this.transform.position.x + this.transform.localScale.x + 0.5f, this.transform.position.y + this.transform.localScale.y * 2, 0.0f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ダメージ判定
        if(collider.tag == "Weapon")
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
