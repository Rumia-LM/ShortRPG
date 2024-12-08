using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public Vector2 spawnPosition;

    public void ChangeScene(string sceneName)
    {
        // 遷移先シーンの座標を保存
        PlayerPrefs.SetFloat("SpawnX", spawnPosition.x);
        PlayerPrefs.SetFloat("SpawnY", spawnPosition.y);
        SceneManager.LoadScene(sceneName);
    }
}

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        // シーン読み込み時に座標を取得
        float x = PlayerPrefs.GetFloat("SpawnX", 0);
        float y = PlayerPrefs.GetFloat("SpawnY", 0);
        transform.position = new Vector2(x, y);
    }
}
