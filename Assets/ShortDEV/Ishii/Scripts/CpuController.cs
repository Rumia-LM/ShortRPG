using UnityEngine;
using TMPro; // TextMeshProの使用を宣言

public class CpuController : MonoBehaviour
{
    public Camera playerCamera;       // プレイヤーのカメラ
    public float maxDistance = 10f;   // レイの最大距離
    public TextMeshProUGUI dialogueUI; // 表示するテキストUI
    public string dialogueText;       // 表示するテキスト
    public KeyCode interactionKey = KeyCode.E; // インタラクトキー

    private bool isDisplayingDialogue = false;

    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("NPC")) // 対象オブジェクトにNPCタグを付ける
                {
                    if (!isDisplayingDialogue)
                    {
                        ShowDialogue();
                    }
                    else
                    {
                        HideDialogue();
                    }
                }
            }
        }
    }

    void ShowDialogue()
    {
        dialogueUI.text = dialogueText;
        dialogueUI.gameObject.SetActive(true);
        isDisplayingDialogue = true;
    }

    void HideDialogue()
    {
        dialogueUI.gameObject.SetActive(false);
        isDisplayingDialogue = false;
    }
}
