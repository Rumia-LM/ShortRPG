using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_t : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found.");
        }
    }

    void Update()
    {
        // 入力の取得
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 移動ベクトルの計算
        moveInput = new Vector2(moveHorizontal, moveVertical);
    }

    void FixedUpdate()
    {
        // キャラクターの移動
        rb.velocity = moveInput * speed;
    }
}
