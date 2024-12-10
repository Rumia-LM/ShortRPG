using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSceneTest_Master2 : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text buttonText;

    void Start(){
        //タイトルテキストを設定
        if(titleText!=null){
            titleText.text="Compliance Communications. Development All the brave men and women. Collaborate Four Heavens";
        }

        //ボタンテキストを設定
        if(buttonText!=null){
            buttonText.text="Game Start !";
        }
    }
    public void GoToFieldScene()
    {
        SceneManager.LoadScene("FirstTown_Master2"); //フィールド画面に移動
        PlayerDataManagerTest_r.Instance.ShowPlayer(); //プレイヤーを表示
    }

}
