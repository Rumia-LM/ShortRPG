using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        rb.gravityScale = 0; // 重力スケールを0に設定
        transform.position = PlayerData_t.position;
        health = PlayerData_t.health;
        attack = PlayerData_t.attack;
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
}
