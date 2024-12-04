using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartOverTest_r : MonoBehaviour
{
    public TMP_Text startOverText;
    public TMP_Text buttonText;

    void Start(){
        //スタートオーバーテキストを設定
        if(startOverText!=null){
            startOverText.text="Start Over";
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
