using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndRollTest_Master2 : MonoBehaviour
{
    public RectTransform content; // スクロールするテキスト
    public float scrollSpeed = 50f; // スクロール速度
    public RectTransform viewport; // ビューポート（画面表示範囲）

    private bool isRolling = true; // スクロール中かどうか

    void Start()
    {
        // テキストの初期位置を画面下にセット（開始時に見えない状態）
        Vector3 startPosition = content.localPosition;
        startPosition.y = -viewport.rect.height;
        content.localPosition = startPosition;
    }

    void Update()
    {
        if (isRolling)
        {
            // 縦方向にスクロール
            content.localPosition += new Vector3(0, scrollSpeed * Time.deltaTime, 0);

            // エンドロールが完全に画面外に出た場合
            if (content.localPosition.y >= content.rect.height)
            {
                Debug.Log("End roll finished!");
                isRolling = false; // スクロール終了
                StartCoroutine(WaitAndChangeScene());
            }
        }
    }

    private IEnumerator WaitAndChangeScene()
    {
        yield return new WaitForSeconds(3f); // 待機時間
        SceneManager.LoadScene("StartSceneTest_Master2");
    }
}