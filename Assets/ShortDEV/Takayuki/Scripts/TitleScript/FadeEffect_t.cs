using UnityEngine;
using System.Collections;

public class FadeEffect_t : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float duration = 2.0f; // フェードの長さ

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // ループを開始
        StartCoroutine(FadeLoop());
    }

    IEnumerator FadeLoop()
    {
        while (true)
        {
            // フェードイン
            yield return StartCoroutine(FadeIn());
            // 短い待機時間（例えば1秒）
            yield return new WaitForSeconds(1);
            // フェードアウト
            yield return StartCoroutine(FadeOut());
            // 短い待機時間（例えば1秒）
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator FadeIn()
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime / duration;
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(1); // 確実に完全表示にする
    }

    IEnumerator FadeOut()
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / duration;
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(0); // 確実に完全透過にする
    }

    void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    public void StartFadeIn(float fadeDuration)
    {
        duration = fadeDuration;
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut(float fadeDuration)
    {
        duration = fadeDuration;
        StartCoroutine(FadeOut());
    }
}
