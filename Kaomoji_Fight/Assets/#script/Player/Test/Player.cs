using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(Contoroller2d))]
public class Player : RaycastController {

    #region 変数群
    // 公開
    [Header("ジャンプの高さ")]
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    [Header("ジャンプの頂点までの時間")]
    public float timeToJumpApex = .4f;
    [Header("加速時間")]
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    [Header("移動速度")]
    public float moveSpeed = 10;
    [SerializeField, Header("復帰時の場所指定")]
    private float RevivalPosX = 0f;
    [SerializeField]
    private float RevivalPosY = 3.0f;
    [SerializeField, Header("エフェクト関係")]
    private ParticleSystem _deth;   // プレイヤーが死んだときのエフェクト

    // 非公開
    private float gravity;  // 重力
    private float maxJumpVelocity;  // 最大ジャンプ時の勢い
    private float minJumpVelocity;  // 最小ジャンプ時の勢い
    private Vector3 velocity;
    private float velocityXSmoothing;
    private bool JumpFlag;  // ジャンプ中かどうか？（true = ジャンプ中, false = ジャンプしていない）

    private float nowHp;    // プレイヤーのHP

    private GameObject Weapon;      //武器
    private bool HaveWeapon = false;//武器を持っている(true)いない(false)

    [SerializeField]
    private float thrust = 5f;//推進力

    Contoroller2d controller;   // コントローラー
    [HideInInspector]
    public CollisionInfo collisions;
    #endregion

    void Start()
    {
        controller = GetComponent<Contoroller2d>();
        _deth = this.GetComponent<ParticleSystem>();
        //_deth.Stop();
  
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        JumpFlag = false;

        //文字を表示するボックスをResourcesから読み込む
       Weapon = (GameObject)Resources.Load("prefab/Weapon/WeaponBloc");
    }

    void Update()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
            JumpFlag = false;
        }

        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, XboxController.First), XCI.GetAxis(XboxAxis.LeftStickY, XboxController.First));

        // ジャンプ
        if (XCI.GetButton(XboxButton.A, XboxController.First) && controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            JumpFlag = true;
        }
        if (XCI.GetButton(XboxButton.B, XboxController.First) && controller.collisions.below)
        {
            //if (velocity.y > minJumpVelocity)
            //{
            velocity.y = minJumpVelocity;
            //}
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, 
                                    (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }


        // 回避をしたい
        if (XCI.GetButton(XboxButton.RightBumper, XboxController.First) && controller.collisions.below)
        {
            //Instantiate()
            float rand = Random.Range(1.0f, 10.0f);
        }
        
        //武器
        if (HaveWeapon)
        {
            //武器の位置を調整
            WeaponBlocController WBController = Weapon.gameObject.GetComponent<WeaponBlocController>();
            Vector3 direction = Vector3.zero;
            if (velocity.x < 0.0f)
            {
                direction = Vector3.left;
                WBController.SetPosition = new Vector3(this.transform.position.x - this.transform.localScale.x, this.transform.position.y + this.transform.localScale.y * 2, 0.0f);
            }
            else if(velocity.x > 0.0f)
            {
                direction = Vector3.right;
                WBController.SetPosition = new Vector3(this.transform.position.x + this.transform.localScale.x + 0.5f, this.transform.position.y + this.transform.localScale.y * 2, 0.0f);
            }

            //武器を使う
            if (XCI.GetButton(XboxButton.X, XboxController.First) && controller.collisions.below)
            {
                GameObject re = Weapon.GetComponent<GameObject>();
                re.AddComponent<Rigidbody2D>();
                //子オブジェクトをすべて解除(修正必須)
                this.transform.DetachChildren();

                WBController.Attack(direction, thrust);

                
                //新しいウエポンブロックをロード
                Weapon = (GameObject)Resources.Load("prefab/Weapon/WeaponBloc"); 

                HaveWeapon = false;
            }
        }


            // Ｒａｙ
            this.RayController();


        // 落ちた時の対処
        if (this.transform.position.y <= -30)
        {
            this.transform.position = new Vector2(RevivalPosX, RevivalPosY);
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
            if (XCI.GetButtonDown(XboxButton.X, XboxController.First) && controller.collisions.below)
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
        //Debug.DrawRay(this.transform.position, -Vector2.up, Color.red);
        BlockController blockcontroller = hitFoot.collider.gameObject.GetComponent<BlockController>();

        if (HaveWeapon == false)
        {
            //オブジェクトを生成する
            Weapon = Instantiate(Weapon, this.transform.position, Quaternion.identity, this.transform);
            //ボックスの下のテキストを取得する
            GameObject textdata = Weapon.transform.Find("Text").gameObject;
            //テキストに文字を書き込む
            textdata.GetComponent<TextMesh>().text = blockcontroller.GetBlockMozi;

            HaveWeapon = true;

            //武器の位置を調整
            WeaponBlocController WBController = Weapon.gameObject.GetComponent<WeaponBlocController>();
            if (directionX == -1)
            {
                WBController.SetPosition = new Vector3(0.5f, this.transform.position.y + this.transform.localScale.y * 2, 0.0f);
            }
            else
            {
                WBController.SetPosition = new Vector3(this.transform.position.x + this.transform.localScale.x + 0.5f, this.transform.position.y + this.transform.localScale.y * 2, 0.0f);
            }
        }
    }

    // Hpのゲッターセッター
    public float SetHP
    {
        set
        {
            nowHp = value;
        }
        get
        {
            return nowHp;
        }
    }

}
