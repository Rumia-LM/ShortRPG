using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTest : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        player.transform.position = new Vector3(0,0,0);
    }



        void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.gameObject.CompareTag("Player"))
        {
            
            SceneManager.LoadScene("FieldMapScene＿I");
        }
    }
}
