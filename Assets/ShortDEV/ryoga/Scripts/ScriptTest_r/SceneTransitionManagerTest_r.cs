using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManagerTest_r : MonoBehaviour
{
    public static SceneTransitionManagerTest_r Instance{get; private set;} //シングルトンインスタンス
    
    private void Awake() {
        //シングルトンパターンの実装
        if(Instance==null){
            Instance=this;
            DontDestroyOnLoad(gameObject); //シーンを跨いで保持
        }else{
            Destroy(gameObject); //複数インスタンスを削除
        }
    }

    //シーンを非同期でロードし、遷移後にコールバックを実行する
    public void TranssitionToScene(string sceneName,System.Action onTransitionComplete=null){
        StartCoroutine(LoadSceneAsync(sceneName,onTransitionComplete));
    }

    //非同期でシーンをロードするコルーチン
    private IEnumerator LoadSceneAsync(string sceneName,System.Action onTransitionComplete){
        Debug.Log($"Starting transition to scene:{sceneName}");

        //シーンを非同期でロード
        AsyncOperation asyncOperation=SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation=false; //シーンの自動アクティブ化を制御

        //ロード進捗を監視
        while(!asyncOperation.isDone){
            Debug.Log($"Loading progress:{asyncOperation.progress*100}%");
            if(asyncOperation.progress>=0.9f){
                //ロード完了後にシーンをアクティブ化
                asyncOperation.allowSceneActivation=true;
            }
            yield return null;
        }
        Debug.Log($"Scene {sceneName} loaded successfully.");

        //コールバックを実行
        onTransitionComplete?.Invoke();
    }
}
