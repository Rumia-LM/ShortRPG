using UnityEngine;
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
    }

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1.0f); // 0から1の範囲で時間に応じて変化
        textToBlink.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }
}
