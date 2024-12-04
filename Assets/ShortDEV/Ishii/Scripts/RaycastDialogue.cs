using UnityEngine;

public class RaycastHandler : MonoBehaviour
{
    public Rigidbody2D rb; // プレイヤーのRigidbody2Dコンポーネント
    public Vector2 lookDirection = Vector2.right; // プレイヤーの見る方向
    public float maxDistance = 1.5f; // レイの最大距離
    public LayerMask npcLayerMask; // NPCレイヤーマスク
    public KeyCode interactionKey = KeyCode.X; // インタラクトキー

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            // Ray2Dの作成(原点、方向)
            Ray2D ray = new Ray2D(rb.position + Vector2.up * 0.2f, lookDirection);
            // RaycastHit2D構造体の検出
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, maxDistance, npcLayerMask);

            // Rayをデバッグ（可視化)
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 5f);

            if (hit.collider != null)
            {
                Debug.Log("Raycast has hit the object: " + hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("Raycast did not hit any object.");
            }
        }
    }
}
