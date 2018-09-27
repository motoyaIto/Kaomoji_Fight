using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(Contoroller2d))]
public class Player : MonoBehaviour {

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
        _deth = this.GetComponent<ParticleSystem>();
        //_deth.Stop();
  
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
        if(XCI.GetButton(XboxButton.X,XboxController.First) && controller.collisions.below)
        {
            //_deth.Play();
        }


        if (XCI.GetButton(XboxButton.RightBumper, XboxController.First) && controller.collisions.below)
        {
            //Instantiate()
            float rand = Random.Range(1.0f, 10.0f);
        }


            // 落ちた時の対処
            if (this.transform.position.y <= -30)
        {
            this.transform.position = new Vector2(RevivalPosX, RevivalPosY);
        }
    }
}
