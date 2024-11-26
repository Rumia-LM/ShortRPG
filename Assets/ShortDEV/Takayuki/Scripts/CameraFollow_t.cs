using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // キャラクターのTransformを設定します
    public Vector3 offset;    // オフセットを設定します

    void LateUpdate()
    {
        // キャラクターの位置にオフセットを加えた位置にカメラを移動します
        transform.position = target.position + offset;
    }
}

