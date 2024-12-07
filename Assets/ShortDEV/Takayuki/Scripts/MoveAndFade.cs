using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveAndFade : MonoBehaviour
{
    public RectTransform imageRect; // イメージのRectTransform
    public CanvasGroup imageCanvasGroup; // 透明度を制御するためのCanvasGroup
    public Vector2 startPos; // 開始位置
    public Vector2 endPos;   // 終了位置
    public float duration = 3f; // アニメーションの時間

    private float timer = 0f;

    void Start()
    {
        // 初期位置と透明度を設定
        imageRect.anchoredPosition = startPos;
        imageCanvasGroup.alpha = 0f; // 完全に透明
    }

    void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // 位置を補間
            imageRect.anchoredPosition = Vector2.Lerp(startPos, endPos, progress);

            // 透明度を補間
            imageCanvasGroup.alpha = Mathf.Lerp(0f, 1f, progress);
        }
    }
}
