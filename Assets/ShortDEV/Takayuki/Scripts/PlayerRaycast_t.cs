using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public float rayDistance = 10f; // レイの飛距離
    public LayerMask hitLayers; // レイが衝突を検出するレイヤー

    void Update()
    {
        // プレイヤーの進行方向にレイキャストを飛ばす
        if (Input.GetKey(KeyCode.X)) // 前進している時にレイキャストを飛ばす
        {
            RaycastHit hit;
            Vector3 rayOrigin = transform.position + Vector3.up * 1.5f; // キャラクターの中心位置からレイキャストを飛ばす

            Debug.DrawRay(rayOrigin, transform.forward * rayDistance, Color.red); // デバッグ用にレイキャストを視覚化

            if (Physics.Raycast(rayOrigin, transform.forward, out hit, rayDistance, hitLayers))
            {
                Debug.Log($"Hit {hit.collider.gameObject.name} at distance {hit.distance}");
                // ヒットしたオブジェクトに対して何かアクションを起こしたい場合は、ここにコードを追加します
            }
            else
            {
                Debug.Log("No hit detected.");
            }
        }
    }

    void OnDrawGizmos()
    {
        // シーンビューでレイキャストを視覚化します（デバッグ用）
        Gizmos.color = Color.red;
        Vector3 rayOrigin = transform.position + Vector3.up * 1.5f;
        Gizmos.DrawRay(rayOrigin, transform.forward * rayDistance);
    }
}
