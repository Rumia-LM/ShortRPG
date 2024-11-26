using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_t : MonoBehaviour
{
    public float speed = 5.0f;
    
    void Update()
    {
        // 入力の取得
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 移動ベクトルの計算
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        // キャラクターの移動
        transform.position += movement * speed * Time.deltaTime;
    }
}
