using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(Contoroller2d))]
public class PlayerTest : MonoBehaviour {

    //#region
    //// 速度
    //public Vector2 SPEED = new Vector2(0.05f, 0.05f);
    //// Use this for initialization
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // 移動処理
    //    Move();
    //}

    //// 移動関数
    //void Move()
    //{
    //    // 現在位置をPositionに代入
    //    Vector2 Position = transform.position;
    //    // 左キーを押し続けていたら
    //    if (Input.GetKey("left"))
    //    {
    //        // 代入したPositionに対して加算減算を行う
    //        Position.x -= SPEED.x;
    //    }
    //    else if (Input.GetKey("right"))
    //    { // 右キーを押し続けていたら
    //      // 代入したPositionに対して加算減算を行う
    //        Position.x += SPEED.x;
    //    }
    //    else if (Input.GetKey("up"))
    //    { // 上キーを押し続けていたら
    //      // 代入したPositionに対して加算減算を行う
    //        Position.y += SPEED.y;
    //    }
    //    else if (Input.GetKey("down"))
    //    { // 下キーを押し続けていたら
    //      // 代入したPositionに対して加算減算を行う
    //        Position.y -= SPEED.y;
    //    }
    //    // 現在の位置に加算減算を行ったPositionを代入する
    //    transform.position = Position;
    //}
    //#endregion

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
    public float moveSpeed = 6;
    [SerializeField, Header("復帰時の場所指定")]
    private float RevivalPosX = 0f;
    [SerializeField]
    private float RevivalPosY = 3.0f;
    [SerializeField, Header("エフェクト関係")]
    private ParticleSystem _deth;   // プレイヤーが死んだときのエフェクト

    // 非公開
    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;

    private float nowHp;    // プレイヤーのHP

    Contoroller2d controller;   // コントローラー
    #endregion

    void Start()
    {
        controller = GetComponent<Contoroller2d>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, XboxController.First), XCI.GetAxis(XboxAxis.LeftStickY, XboxController.First));

        // ジャンプ
        if (XCI.GetButton(XboxButton.A, XboxController.First) && controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
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

        // 地面を引っこ抜く
        if (XCI.GetButton(XboxButton.X, XboxController.First) && controller.collisions.below)
        {

        }


        // 落ちた時の対処
        if (this.transform.position.y <= -30)
        {
            this.transform.position = new Vector2(RevivalPosX, RevivalPosY);
        }
    }

}
