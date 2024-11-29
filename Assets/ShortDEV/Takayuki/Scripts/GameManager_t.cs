using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager_t : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerInstance;

    void Start()
    {
        if (PlayerController_t.instance == null)
        {
            playerInstance = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
