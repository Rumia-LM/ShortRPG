using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSceneTest_Master : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text buttonText;

    void Start(){
        //タイトルテキストを設定
        if(titleText!=null){
            titleText.text="ShortRPG";
        }

        //ボタンテキストを設定
        if(buttonText!=null){
            buttonText.text="Game Start !";
        }
    }
    public void GoToFieldScene()
    {
        SceneManager.LoadScene("FirstTown_Master"); //フィールド画面に移動
    }

}
