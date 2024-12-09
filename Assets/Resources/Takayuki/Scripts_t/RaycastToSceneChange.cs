using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Coroutineを使用するために必要

public class RaycastToSceneChange : MonoBehaviour
{
    [SerializeField] private float rayDistance = 1f; // レイの飛ばす距離
    [SerializeField] private LayerMask hitLayer; // レイが当たるレイヤー
    [SerializeField] private string targetTag = "NPC"; // ターゲットタグ
    [SerializeField] private string targetSceneName = "BossBattle_Master2"; // 遷移先のシーン名
    [SerializeField] private float waitTime = 2f; // 待機時間

    private Vector2 movementDirection;

    void Update()
    {
        // キャラクターの移動方向を取得（例として右方向を使用）
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (movementDirection != Vector2.zero) // 移動しているときのみレイキャスト
        {
            // レイキャストを実行
            RaycastHit2D hit = Physics2D.Raycast(transform.position, movementDirection, rayDistance, hitLayer);
            
            // レイキャストが何かに当たったか確認
            if (hit.collider != null && hit.collider.CompareTag(targetTag))
            {
                Debug.Log("Hit: " + hit.collider.name + ". Changing scene to: " + targetSceneName);
                StartCoroutine(ChangeSceneAfterWait()); // コルーチンを開始
            }

            // デバッグ用にレイを可視化
            Debug.DrawRay(transform.position, movementDirection * rayDistance, Color.red);
        }
    }

    private IEnumerator ChangeSceneAfterWait()
    {
        yield return new WaitForSeconds(waitTime); // 指定した時間待機

        // シーンを変更する前にビルド設定を確認
        if (Application.CanStreamedLevelBeLoaded(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName); // シーンを変更
        }
        else
        {
            Debug.LogError($"Scene '{targetSceneName}' couldn't be loaded because it has not been added to the build settings.");
        }
    }
}
