using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using Timers;

[RequireComponent(typeof(Contoroller2d))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : RaycastController {

    #region 変数群
    // 公開
    [Header("移動速度")]
    public float moveSpeed = 10;    

    [SerializeField, Header("無敵時間")]
    private float Invincible_time = .5f;

    [Header("幅")]
    public float scroll = 5f;


    // 非公開
    [SerializeField, Header("コントローラー番号")]
    private XboxController ControlerNamber = XboxController.First;//何番目のコントローラーを適用するか

    private Vector3 velocity;

    private float maxflap = 800f;  // ジャンプの高さ（最大）
    private float minflap = 400f;   // ジャンプの高さ（最小）

    private float direction = 0;    // 方向
    private float thrust = 1000f;       // 投擲物の推進力

    private GameObject weapon;

    private bool Start_Wait = false;
    private bool HaveWeapon = false;//武器を持っている(true)いない(false)
    private bool Avoidance = false; // 回避フラグ
    private bool jump = false;  // ジャンプ中か？
    private float FlameCount = .0f;

    private string p_name;  // プレイヤーネーム

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

        // プレイヤー同士の当たり判定をしない
        int P_layer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(P_layer, P_layer);

        // タイマーのセット
        TimersManager.SetTimer(this, 2f, delegate { ChangeState(); });
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

        if (HaveWeapon)
        {
            //武器の位置を持ち変える
            WeaponPositionControll();
        }

        //キャラのy軸のdirection方向にscrollの力をかける
        rig.velocity = new Vector2(scroll * direction, rig.velocity.y);

        
        if (XCI.GetButtonDown(XboxButton.Y, ControlerNamber) && !jump)
        {
            // 大ジャンプ
            rig.AddForce(Vector2.up * maxflap);
            jump = true;
            //this.gameObject.layer = LayerName.Through;

            //if (XCI.GetButtonUp(XboxButton.Y, ControlerNamber) && !jump)
            //{
            //    // 小ジャンプ・・・したかった・・・(´・ω・｀)
            //    rig.AddForce(Vector2.up * minflap);
            //    jump = true;
            //    //this.gameObject.layer = LayerName.Through;
            //}
        }

        // 回避をしたい
        if (XCI.GetAxis(XboxAxis.RightTrigger, ControlerNamber) < 0.0f)
        {
            // 回避時間
            float Avoidance_time = .0f;
            // アニメーションに差し替え予定？
            if (!Avoidance)
            {
                if (input.x < .0f)
                {
                    this.transform.position += new Vector3(-5f, 0f);
                }
                else if (input.x > .0f)
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
            else
            {
                // 攻撃を受け付けるようにする
                Avoidance = false;
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
            WBController.Owner_Data = p_name;
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
            if (XCI.GetButtonDown(XboxButton.B, ControlerNamber))
            {
                ChangeWeaponState();
                WeaponBlocController WB = weapon.GetComponent<WeaponBlocController>();

                WB.Attack(input, thrust);             
            }

            // 武器を捨てる
            if (XCI.GetButton(XboxButton.X, ControlerNamber))
            {
                ChangeWeaponState();
                Destroy(weapon);
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
            Debug.DrawRay(ray.origin, ray.direction * 0.5f, Color.green, 1.0f);

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

            //プレイヤーの移動する向きに合わせて位置を調整
            this.WeaponPositionControll();

            // 持ったプレイヤーの名前を取得
            p_name = this.name;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ダメージ判定
        if(collision.transform.tag == "Weapon" && Avoidance == false)
        {
            WeaponBlocController WBController = collision.gameObject.GetComponent<WeaponBlocController>();
            PSM.Player_ReceiveDamage(this.gameObject, collision.gameObject, CNConvert(ControlerNamber));
        }

        // ジャンプ制限
        if (collision.gameObject.CompareTag("Stage") || collision.gameObject.CompareTag("Player"))
        {
            jump = false;
        }
    }

    private void OnDisable()
    {
        PSM.death_player[CNConvert(ControlerNamber)] = false;
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
        return 4;
    }

    private void ChangeState()
    {
        Start_Wait = true;
    }

    private void ChangeWeaponState()
    {
        HaveWeapon = false;
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

    public void WeaponPositionControll()
    {
        if (Start_Wait == true)
        { 
            foreach (Transform child in this.transform)
            {
                if (direction >= 1 && child.name == "TopRight")//右
                {
                    weapon.transform.position = child.transform.position;
                }

                else if (direction <= -1 && child.name == "TopLeft")//左
                {
                    weapon.transform.position = child.transform.position;
                }
                else if (child.name == "Top")//移動していない
                {
                    weapon.transform.position = child.transform.position;
                }
            }
        }
    }

    public GameObject ThisPlayer
    {
        get
        {
            return this.transform.gameObject;
        }
    }
}
