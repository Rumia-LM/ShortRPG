using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController_t : MonoBehaviour
{
    public static PlayerController_t instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // シーン遷移時にオブジェクトが破棄されないようにする
        }
        else
        {
            Destroy(gameObject); // 既にインスタンスが存在する場合は新しいインスタンスを破棄する
            return;
        }
    }

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator; // アニメーターコンポーネントの参照
    private Vector2 moveInput;
    private Vector2 lookDirection = new Vector2(1f, 0); // RubyControllerから追加
    public int health;
    public int attack;
    public GameObject prefab; // RubyControllerから追加

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // アニメーターコンポーネントを取得
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on Player_t. Please attach a Rigidbody2D component.");
            return;
        }
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        transform.position = PlayerData_t.targetPosition;
        health = PlayerData_t.health;
        attack = PlayerData_t.attack;

        Debug.Log("Player position set to: " + transform.position);
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        // アニメーターにパラメータを設定
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);

        // 移動方向が変わった場合、アニメーションの方向を更新
        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("LastMoveX", moveInput.x);
            animator.SetFloat("LastMoveY", moveInput.y);
            lookDirection.Set(moveInput.x, moveInput.y); // RubyControllerから追加
            lookDirection.Normalize();
        }

        // レイキャストをXキーで実行
        if (Input.GetKeyDown(KeyCode.X))
        {
            Ray2D ray = new Ray2D(
                rb.position,
                lookDirection
            );
            RaycastHit2D hit = Physics2D.Raycast(
                ray.origin,
                ray.direction,
                1.5f,
                LayerMask.GetMask("NPC")
            );

            if (hit.collider != null)
            {
                NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
                if (npc != null)
                {
                    npc.DisplayDialog();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.velocity = moveInput * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector2.zero;
            Debug.Log("Collided with wall.");
        }
    }
}
