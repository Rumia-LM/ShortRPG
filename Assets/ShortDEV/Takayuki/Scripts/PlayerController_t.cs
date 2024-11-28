using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_t : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    public int health;
    public int attack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found.");
        }
        else
        {
            Debug.Log("Rigidbody2D component assigned.");
        }

        rb.gravityScale = 0; // 重力スケールを0に設定
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // 連続的な衝突検出モード
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // 補間を有効に設定

        transform.position = PlayerData_t.targetPosition;
        health = PlayerData_t.health;
        attack = PlayerData_t.attack;

        Debug.Log("Player position set to: " + transform.position);
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero; // 壁にぶつかったら速度を0に設定
            Debug.Log("Collided with wall.");
        }
    }
}
