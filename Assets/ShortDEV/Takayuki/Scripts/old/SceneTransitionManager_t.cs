using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject mainCamera;

    public void TransitionToBattleScene()
    {
        // キャラクターとメインカメラを非アクティブ化
        if (character != null)
        {
            character.SetActive(false);
        }

        if (mainCamera != null)
        {
            mainCamera.SetActive(false);
        }

        // バトルシーンに遷移
        SceneManager.LoadScene("BattleSceneTest_Master");
    }
}

