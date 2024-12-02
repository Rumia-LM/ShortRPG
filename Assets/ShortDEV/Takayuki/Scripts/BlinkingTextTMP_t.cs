using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

public class BlinkingTextTMP : MonoBehaviour
{
    public TextMeshProUGUI textToBlink;
    public float blinkSpeed = 1.0f; // 点滅速度

    private Color originalColor;

    void Start()
    {
        if (textToBlink == null)
        {
            textToBlink = GetComponent<TextMeshProUGUI>();
        }
        originalColor = textToBlink.color;

        // デバッグログの設定を無効化
        Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
    }

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1.0f); // 0から1の範囲で時間に応じて変化
        textToBlink.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }
}

