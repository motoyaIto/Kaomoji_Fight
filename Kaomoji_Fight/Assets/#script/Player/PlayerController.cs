using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(Controller2D))]
public class PlayerController : MonoBehaviour {

    #region 変数
    // 非公開
    //private Rigidbody2D rgb2d;
    private int Max_Hp = 100;

    // 公開
    [Header("移動速度")]
    public float speed = 3;

    [Header("HP")]
    public int Hp = 100;

    [Header("ジャンプの高さ")]
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    [Header("移動速度")]
    float moveSpeed = 6;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;
    #endregion

    // Use this for initialization
    void Start () {
        //rgb2d = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller2D>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);    // origin
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        print("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);
    }
	
	// Update is called once per frame
	void Update () {

        // 使用例
        //if (XCI.GetButton(XboxButton.A, XboxController.First))
        //{
        //}

        #region 左スティックで移動
        // 右・左
        float axisX = XCI.GetAxis(XboxAxis.LeftStickX, XboxController.First);
        // 上・下
        float axisY = XCI.GetAxis(XboxAxis.LeftStickY, XboxController.First);

        // 移動する向きを求める
        Vector2 direction = new Vector2(axisX, axisY).normalized;

        // 移動
        //rgb2d.velocity = direction * speed;
        #endregion

        if (XCI.GetButton(XboxButton.A, XboxController.First))
        {
            //rgb2d.position += new Vector2(0, maxJumpHeight);
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        int wallDirX = (controller.collisions.left) ? -1 : 1;

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        bool wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                //if (input.x != wallDirX && input.x != 0)
                //{
                //    timeToWallUnstick -= Time.deltaTime;
                //}
                //else
                //{
                //    timeToWallUnstick = wallStickTime;
                //}
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallSliding)
            {
                //if (wallDirX == input.x)
                //{
                //    velocity.x = -wallDirX * wallJumpClimb.x;
                //    velocity.y = wallJumpClimb.y;
                //}
                //else if (input.x == 0)
                //{
                //    velocity.x = -wallDirX * wallJumpOff.x;
                //    velocity.y = wallJumpOff.y;
                //}
                //else
                //{
                //    velocity.x = -wallDirX * wallLeap.x;
                //    velocity.y = wallLeap.y;
                //}
            }
            if (controller.collisions.below)
            {
                velocity.y = maxJumpVelocity;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, input);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
    }
}
