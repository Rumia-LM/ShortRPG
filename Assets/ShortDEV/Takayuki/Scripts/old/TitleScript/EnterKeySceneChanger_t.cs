using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterKeySceneChanger_t : MonoBehaviour
{
    [SerializeField] private string sceneName; // 切り替えたいシーンの名前

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Enterキーが押されたらシーンを変更する
            SceneManager.LoadScene(sceneName);
        }
    }
}
