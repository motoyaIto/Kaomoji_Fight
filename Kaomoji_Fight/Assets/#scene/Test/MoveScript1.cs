using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript1 : MonoBehaviour {

    public float moveForce = 0f;
    public float jumpForce = 0f;
    private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal") * moveForce;
        if (h != 0)
        {
            rbody.AddForce(Vector2.right * h);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rbody.AddForce(Vector2.up * jumpForce);
        }

    }
}
