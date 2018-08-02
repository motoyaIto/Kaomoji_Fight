using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(Contoroller2d))]
public class Player : MonoBehaviour {

    #region 変数群
    // 公開
    [Header("プレイヤー指定")]
    public GameObject player;
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

    // 非公開
    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;

    Contoroller2d controller;
    #endregion

    void Start()
    {
        controller = GetComponent<Contoroller2d>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        Debug.Log("Gravity: " + gravity + "  Max Jump Velocity: " + maxJumpVelocity);
        Debug.Log("Gravity: " + gravity + "  Min Jump Velocity: " + minJumpVelocity);
    }

    void Update()
    {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, XboxController.First), XCI.GetAxis(XboxAxis.LeftStickY, XboxController.First));

        if (XCI.GetButton(XboxButton.A, XboxController.First) && controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            Debug.Log("(≧▽≦)");
        }
        if (XCI.GetButtonUp(XboxButton.A, XboxController.First) && controller.collisions.below)
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
                Debug.Log(";つД｀)");
            }
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


        // 落ちた時の対処
        if (player.transform.position.y <= -30)
        {
            player.transform.position = new Vector2(-5.8f, -0.4f);
        }
    }
}
