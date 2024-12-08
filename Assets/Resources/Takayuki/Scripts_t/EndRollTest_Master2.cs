using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndRollTest_Master2 : MonoBehaviour
{
    public RectTransform content; //スクロールするテキスト
    public float scrollSpeed=50f; //スクロール速度

    private float initialPositionY; //初期位置
    private bool isRolling=true; //スクロール中かどうか

    void Start()
    {
        //初期位置を記録
        initialPositionY=content.localPosition.y;    
    }

    void Update()
    {
        if(isRolling){
            //縦方向にスクロール
            content.localPosition+=new Vector3(0,scrollSpeed*Time.deltaTime,0);

            //エンドロールが画面外に出たらリセット（または処理終了）
            if(content.localPosition.y-initialPositionY>=content.rect.height){
                //スクロール完了時の処理（シーン遷移など）
                Debug.Log("End roll finished!");
                isRolling=false; //スクロール終了
                StartCoroutine(WaitAndChangeScene());
            }
        }
    }

    private IEnumerator WaitAndChangeScene(){
        yield return new WaitForSeconds(3f); //待機時間
        SceneManager.LoadScene("StartOverTest_Master2");

    }
}
