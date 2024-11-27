using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounter : MonoBehaviour
{
    public float encounterChance = 0.01f;  // エンカウントの確率（1%）
    public GameObject encounterPrefab;     // モンスターのプレハブ

    void Update()
    {
        // キャラクターが移動したかどうかをチェック
        if (IsCharacterMoving())
        {
            // ランダムなエンカウント判定
            if (Random.value < encounterChance)
            {
                TriggerEncounter();
            }
        }
    }

    bool IsCharacterMoving()
    {
        // 簡単な移動判定、実際の移動判定方法に合わせて調整
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    void TriggerEncounter()
    {
        // エンカウントイベントをトリガー
        Debug.Log("Encounter Triggered!");

        // 必要に応じて、エンカウントのためのUIやイベントを呼び出す
        // 例：Instantiate(encounterPrefab, transform.position, Quaternion.identity);

        // 一時的にキャラクターの移動を停止
        // Time.timeScale = 0;

        // 実際のエンカウント処理を行う
    }
}
