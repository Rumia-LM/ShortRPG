using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverTest_Master : MonoBehaviour
{
    public TMP_Text gameOverText;
    public TMP_Text buttonText;

    void Start(){
        //ゲームオーバーテキストを設定
        if(gameOverText!=null){
            gameOverText.text="Game Over";
        }

        //ボタンテキストを設定
        if(buttonText!=null){
            buttonText.text="To Start Scene";
        }
    }

    public void GoToStartScene()
    {
        SceneManager.LoadScene("StartSceneTest_r"); //スタート画面へ移動
    }

}
