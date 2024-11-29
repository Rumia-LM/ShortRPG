using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2_t : MonoBehaviour
{
    public FadeEffect_t fadeEffect; // 正しいクラス名を使用

    void Start()
    {
        // フェードインを開始
        fadeEffect.StartFadeIn(2.0f);
    }

    void Update()
    {
        // フェードアウトを開始する場合の例（キー入力などのイベントをトリガーに）
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fadeEffect.StartFadeOut(2.0f);
        }
    }
}
