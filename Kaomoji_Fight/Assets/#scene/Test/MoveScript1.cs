using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript1 : MonoBehaviour {

    [Header("移動速度に対する追従度")]
    public float moveForceMultiplier = 0f;
    [Header("移動速度")]
    public float moveSpeed = 10f;

    private Rigidbody2D rbody;
    private AudioSource audio;
    private float XaxisInput;
    private float YaxisInput;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        XaxisInput = Input.GetAxis("Horizontal");
        YaxisInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 movevector = Vector2.zero;


        if (XaxisInput != .0f)
        {
            movevector.x = moveSpeed * XaxisInput;
        }
        else
        {
            movevector.x = .0f;
        }

        if (YaxisInput != .0f)
        {
            movevector.y = moveSpeed * YaxisInput;
        }
        else
        {
            movevector.y = .0f;
        }

        if (Input.GetButtonDown("Jump"))
        {
            rbody.AddForce(transform.up * 10f);
        }

        rbody.AddForce(moveForceMultiplier * (movevector - rbody.velocity));

    }
}
