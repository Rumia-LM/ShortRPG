using UnityEngine;

public class CharacterRaycast2D_t : MonoBehaviour
{
    public float raycastDistance = 10f; // レイの距離
    public LayerMask raycastLayerMask; // レイが当たるレイヤー

    void Update()
    {
        // レイをキャラクターの右方向に飛ばす（2Dではforwardではなくrightやupを使う）
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, raycastDistance, raycastLayerMask);
        if (hit.collider != null)
        {
            // レイがオブジェクトに当たった場合の処理
            Debug.Log("Hit object: " + hit.collider.name);
        }

        // レイを視覚的に確認するためのデバッグライン
        Debug.DrawRay(transform.position, transform.right * raycastDistance, Color.red);
    }
}
