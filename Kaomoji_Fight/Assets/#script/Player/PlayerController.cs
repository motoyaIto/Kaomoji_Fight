using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region
    // 非公開
    private Rigidbody2D rgb2d;

    // 公開
    public float speed = 3;
    #endregion

    // Use this for initialization
    void Start () {
        rgb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        // 右・左
        float x = Input.GetAxisRaw("Horizontal");

        // 上・下
        float y = Input.GetAxisRaw("Vertical");

        // 移動する向きを求める
        Vector2 direction = new Vector2(x, y).normalized;

        // 移動
        rgb2d.velocity = direction * speed;
	}
}
